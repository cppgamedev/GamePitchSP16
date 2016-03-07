using UnityEngine;
using System.Collections;

public class ItemData : MonoBehaviour {

    public GameObject TextItemName;
    public GameObject TextAmount;
    Item item { get; set; }
    int amount { get; set; }



    public bool addAmount(int i)
    {
        //checks if amount is greater than max Stack
        //if so set amount to maxStack
        if(amount + i > item.maxStack)
        {
            Debug.Log("Item has reached max Stacks");
            amount = item.maxStack;
            return false;
        }
        else
        {
            amount += i;
        }
        return true;
    }
}
