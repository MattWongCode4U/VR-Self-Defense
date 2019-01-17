using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj; //object being tracked (controller)
    private GameObject collidingObject; //potential grab target
    private GameObject objectInHand; //Currently grabbing

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

    private void SetCollidingObject(Collider col)
    {
        //check if the other object has rigidbody and not holding something
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        //potential grab target
        collidingObject = col.gameObject;
    }

    //Enter collision, set other collider as potential grab target
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    //Keeps setting the other collider as a potential grab target
    public void OnStayTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    //Exits collision with collider, abandon grab target
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    //Grab the object
    private void GrabObject()
    {
        //grab the object and put in the player's hand
        objectInHand = collidingObject;
        collidingObject = null;

        //add joint that connects the controller to the object
        FixedJoint joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    //Make a fixed joint, adds it to the controller, and ensures separation is difficult
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        //Ensure there is a fixed joint attatched to the controller
        if (GetComponent<FixedJoint>())
        {
            //Remove connection to the object by the joint
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            //Add speed and rotation to the released object
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        //Remove reference to the formerly grabbed object
        objectInHand = null;
    }

    // Update is called once per frame
    void Update () {
        //Squeeze trigger, grab target
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }
        //Release trigger, release target
        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
	}
}
