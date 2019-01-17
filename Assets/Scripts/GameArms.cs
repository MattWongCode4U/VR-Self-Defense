using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArms : MonoBehaviour {

    public GameObject energyBar; //Reference to the energy bar canvas
    public AudioSource hitSound;
    public AudioSource blockSound;

    private SteamVR_TrackedObject trackedObj; //object being tracked (controller)

    //Children gameobject references
    GameObject shield;
    GameObject attack;
    GameObject openHand;
    GameObject shieldInactive;

    bool triggerPressed; //Flag for the trigger being held down

    bool gripPressed; //Flag for the grip being held down

    //Shield variables
    float shieldRegenRate = 0.05f;
    float shieldDegenRate = 0.1f;
    float blockSpeed = 1.0f;
    float blockPersist = 1.0f;
    float currBlockPersist = 0.0f;

    //Energy usage variables
    float maxEnergy = 100.0f;
    float energy = 100.0f;
    float minEnergyUse = 5.0f;

    private SteamVR_Controller.Device Controller
    {
        //returns the controller's input
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Start()
    {
        //Initialize varaibles
        triggerPressed = false;
        gripPressed = false;

        //set object references
        foreach (Transform child in transform)
        {
            if(child.gameObject.name == "ArmModel")
            {
                foreach(Transform child2 in child)
                {
                    if(child2.gameObject.name == "Shield")
                    {
                        shield = child2.gameObject;
                    }
                    if(child2.gameObject.name == "ShieldInactive")
                    {
                        shieldInactive = child2.gameObject;
                    }
                    if(child2.gameObject.name == "Hand")
                    {
                        attack = child2.gameObject;
                    }
                    if(child2.gameObject.name == "HandOpen")
                    {
                        openHand = child2.gameObject;
                    }
                }
            }
        }

        if (attack)
        {
            attack.GetComponent<PlayerHand>().armParent = gameObject;
            attack.SetActive(false);
        }

        if (shield)
        {
            shield.GetComponent<PlayerShield>().armParent = gameObject;
            shield.SetActive(false);
        }
    }

    void Awake()
    {
        //get reference to SteamVR_TrackedObject component
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //handle inputs from the controller
        ControllerInputHandle();

        //Nothing pressed
        if(!triggerPressed && !gripPressed)
        {
            energy += shieldRegenRate;
            if(energy >= 100.0f)
            {
                energy = 100.0f;
            }
        }

        //Attack toggle
        if (triggerPressed && energy > minEnergyUse)
        {
            openHand.SetActive(false);
            attack.SetActive(true);
            attack.GetComponent<PlayerHand>().speed = Controller.velocity.magnitude;
            energy -= shieldDegenRate;
            if (energy <= 0.0f)
            {
                energy = 0.0f;
            }
        } else
        {
            openHand.SetActive(true);
            attack.SetActive(false);
        }

        //Shield toggle
        if (gripPressed && energy > minEnergyUse)
        {
            //Debug.Log("controller veloctiy: " + Controller.velocity.magnitude);
            if (Controller.velocity.magnitude > blockSpeed)
            {
                shieldInactive.SetActive(false);
                shield.SetActive(true);
                currBlockPersist = 0;
            }
            else
            {
                currBlockPersist += Time.deltaTime;
                if (currBlockPersist >= blockPersist) //holding the block without moving for a time
                {
                    shieldInactive.SetActive(true);
                    shield.SetActive(false);
                }
            }

            energy -= shieldDegenRate;
            if(energy <= 0.0f)
            {
                energy = 0.0f;
            }
        } else
        {
            shieldInactive.SetActive(true);
            shield.SetActive(false);
            currBlockPersist = 0;
        }

        updateEnergyBar(energy / maxEnergy);
    }

    //Handle any input to the controller
    void ControllerInputHandle()
    {
        if (Controller.GetAxis() != Vector2.zero)
        {
            //Debug.Log(gameObject.name + Controller.GetAxis());
        }

        if (Controller.GetHairTriggerDown())
        {
            //Debug.Log(gameObject.name + " Trigger Press");
            triggerPressed = true;
        }

        if (Controller.GetHairTriggerUp())
        {
            //Debug.Log(gameObject.name + "Trigger Release");
            triggerPressed = false;
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            //Debug.Log(gameObject.name + "Grip Press");
            gripPressed = true;
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            //Debug.Log(gameObject.name + "Grip Release");
            gripPressed = false;
        }
    }

    //Update the energy bar canvas with the energy amount
    void updateEnergyBar(float energyPerecent)
    {
        energyBar.transform.localScale = new Vector3(energyPerecent, energyBar.transform.localScale.y, energyBar.transform.localScale.z);
    }

    //Send haptic feedback pulse to the controller
    public void hapticPulse()
    {
        Controller.TriggerHapticPulse(2000);
    }

    //Play sound when a vital point is hit
    public void playHitSound()
    {
        hitSound.Play();
    }

    //Play sound when blocked an attack
    public void playBlockSound()
    {
        blockSound.Play();
    }
}
