using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour {

    public GameObject armParent; //Reference to GameArm object

    //Used when blocking an enemy AI attack
    public void blocked()
    {
        armParent.GetComponent<GameArms>().hapticPulse(); //controller haptic feedback when hitting
        armParent.GetComponent<GameArms>().playBlockSound(); //audio feedback when blocking
    }
}
