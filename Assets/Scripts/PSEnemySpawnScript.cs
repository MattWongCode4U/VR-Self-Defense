using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSEnemySpawnScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(transform.childCount <= 0) //clean up object if the particle system is finished playing
        {
            Destroy(gameObject);
        }
	}
}
