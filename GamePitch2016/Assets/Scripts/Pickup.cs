using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour, iItems {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.getGameObject.tag == "Player")
        {

        }
    }
}
