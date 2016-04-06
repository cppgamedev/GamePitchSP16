using UnityEngine;
using System.Collections;
using System;

public class ItemPickup : MonoBehaviour {

    public int itemID = -1;

    public int getItemID()
    {
        return itemID;
    }

    public void destroyItem()
    {
        Destroy(this.gameObject);
    }
}
