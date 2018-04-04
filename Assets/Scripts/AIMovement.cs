using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {

    public Transform target; //Target to get close to
    float minDist = 5.0f; //Distance to maintain with target
    float moveSpeed = 3; //Speed at which to move

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        seek();
	}

    // Moves toward the given target location, maintains the minimum distance
    void seek()
    {
        transform.LookAt(target);

        if(Vector3.Distance(transform.position, target.position) >= minDist)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
