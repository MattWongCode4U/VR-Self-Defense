using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalPoint : MonoBehaviour {

    GameObject ai; //Reference to the ai object

	// Use this for initialization
	void Start () {
        ai = transform.parent.parent.gameObject;
	}
	
    public void OnTriggerEnter(Collider other)
    {
        //Vital Point is hit
        if (other.gameObject.tag == "playerAttack")
        {
            int damage = other.GetComponent<PlayerHand>().getDamage();
            ai.GetComponent<AIHealth>().takeDamage(damage);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
