using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    ItemDatabase itemdatabase;
    List<Item> inventory = new List<Item>();
    public GameObject item;
    public GameObject itemList;

    // Use this for initialization
    void Start()
    {
        itemdatabase = GetComponent<ItemDatabase>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addItem(int id)
    {
        Item item = inInventory(id);// cheks to see if item in inventory has ID.
        bool addToInventory = true;
        if ( item != null) // checks if inInventory cameback with a item.
        {
            if (!item.isStackable())
            {
                Debug.Log(item.itemName + " already in Inventory.");
            }
            else
            {
                //TODO for use with GameObject item that contains itemData
            }
            addToInventory = false;
        }
        if(addToInventory)//adds new item into inventory.
        {
            Item itemToAdd = itemdatabase.searchItem(id);
            if(itemToAdd != null)// checks to makesure there exist a item with ID
            {
                inventory.Add(itemToAdd);
                inventory.Sort();
            }
            else
            {
                Debug.Log(id + " Does not exist in Database");
            }            
        }
    }

    Item inInventory(int id)
    {
        if(inventory.Count > 0)
        {
            foreach( Item item in inventory)
            {
                if(item.id == id) { return item; }
            }
        }
        return null;
    }
}