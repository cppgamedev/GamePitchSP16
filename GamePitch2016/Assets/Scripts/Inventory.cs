using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    ItemDatabase itemdatabase;
    List<Weapon> inventory = new List<Weapon>();

    // Use this for initialization
    void Start()
    {
        itemdatabase = GetComponent<ItemDatabase>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addWeapon(int id)
    {
        inventory.Add(itemdatabase.searchWeapons(id));
    }
}