using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    public GameObject laserPrefab; //Laser's prefab
    private GameObject laser; //reference to an instance of the laser
    private Transform laserTransform; //transform component
    private Vector3 hitPoint; //position where the laser hits

    public Transform cameraRigTransform; //camera rig transform
    public Transform headTransform; //Reference to a player's head
    public LayerMask buttonMask; //filter the areas on which laser shows up

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Start()
    {
        //spawn a new laser and save a reference
        laser = Instantiate(laserPrefab);
        //Store the laser's transform
        laserTransform = laser.transform;
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void ShowLaser(RaycastHit hit)
    {
        // Show the laser
        laser.SetActive(true);
        // Position the laser between the controller and the point where the raycast hits
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        // Point the laser at the position where the raycast hit
        laserTransform.LookAt(hitPoint);
        // Scale the laser so it fits perfectly between the two positions
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;

        // shoot a ray from the contoller, if it hits something, store the point and show the laser
        // can only hit GameObjects that you can teleport to
        if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, buttonMask))
        {
            hitPoint = hit.point;
            ShowLaser(hit);

            if (Controller.GetHairTriggerDown())
            {
                //Does the buttons intended click functionality
                Controller.TriggerHapticPulse(1000);
                hit.collider.gameObject.GetComponent<MyButton>().Clicked();
            }
            
        }
        else // hide laser when the player releases the touchpad
        {
            laser.SetActive(false);
        }
    }
}
