using UnityEngine;
using System.Collections;
using System;

public class ItemData : IComparable
{

    public GameObject TextItemName;
    public GameObject TextAmount;
    public Item item { get; set; }
    public int amount { get; set; }

    public ItemData(Item item)
    {
        this.item = item;
        if(item.isStackable())
            amount = 1;
        else
            amount = -1;
    }

    public bool addItem()
    {
        return addItem(1) == -1;
    }

    public int addItem(int i)
    {
        //checks if amount is greater than max Stack
        //if so set amount to maxStack return amount
        //over maxStack.
        if(amount + i > item.maxStack)
        {
            Debug.Log("Item has reached max Stacks");
            int overflow = amount + i - item.maxStack;
            amount = item.maxStack;
            return overflow;//return number of items over maxstack
        }
        else
        {
            amount += i;
        }
        return -1;
    }

    public bool removeItem()
    {
        return removeItem(1) == -1;
    }

    public int removeItem(int i)
    {
        //checks if amount is less than 0
        //if so set amount to 0 and return amount
        //that could not me removed.
        if (amount - i < 0)
        {
            Debug.Log("No more " + item.itemName + " in inventory!");
            int overflow = (amount - i)*-1;
            amount = 0;
            return overflow;//return number of items under 0
        }
        else
        {
            amount -= i;
        }
        return -1;
    }

    public int CompareTo(object obj)
    {
        if(obj is ItemData)
        {
            ItemData itemData = (ItemData)obj;
            return ((IComparable)item).CompareTo(itemData.item);
        }
        else
        {
            throw new ArgumentException("Not a ItemData");
        }
    }
}
