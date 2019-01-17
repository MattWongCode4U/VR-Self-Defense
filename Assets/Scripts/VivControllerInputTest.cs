using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VivControllerInputTest : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj; //object being tracked (controller)

    private SteamVR_Controller.Device Controller
    {
        //returns the controller's input
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
	
    void Awake()
    {
        //get reference to SteamVR_TrackedObject component
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

	// Update is called once per frame
	void Update () {
		if(Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        if (Controller.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " Trigger Press");
        }

        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + "Trigger Release");
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + "Grip Press");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + "Grip Release");
        }
	}
}
