using UnityEngine;
using System.Collections;
using System;

public class ItemPickup : MonoBehaviour {

    public int itemID = -1;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Inventory>().addWeapon(itemID);
            Destroy(this);
        }
    }
}
