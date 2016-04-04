using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    ItemDatabase itemdatabase;
    List<ItemData> inventory = new List<ItemData>();
    public GameObject item;
    public GameObject itemList;

    // Use this for initialization
    void Start()
    {
        itemdatabase = GetComponent<ItemDatabase>();
    }

    public bool addItem(int id)
    {
        ItemData item = inInventory(id);// cheks to see if item in inventory has ID.
        if ( item != null) // checks if inInventory cameback with a item.
        {
            if (!item.item.isStackable())
            {
                Debug.Log(item.item.itemName + " already in Inventory.");
            }
            else
            {
                if (item.addItem())
                {
                    //Debug.Log("add to stack: " + item.amount);
                    return true;
                }
            }
        }
            else//adds new item into inventory.
        {
            Item itemToAdd = itemdatabase.searchItem(id);
            if(itemToAdd != null)// checks to makesure there exist a item with ID
            {
                inventory.Add(new ItemData(itemToAdd));
                inventory.Sort();
                //Debug.Log(itemToAdd.itemName + " add to inventory");
                return true;
            }
            else
            {
                Debug.Log(id + " Does not exist in Database");
            }            
        }

        return false;
    }

    ItemData inInventory(int id)
    {
        if(inventory.Count > 0)
        {
            foreach( ItemData item in inventory)
            {
                if(item.item.id == id) { return item; }
            }
        }
        return null;
    }
}