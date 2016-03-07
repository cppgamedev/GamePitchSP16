using UnityEngine;
using System.Collections;
using System;

public class Item : IItems, IComparable
{
    public int id { get; set; }
    public string itemName { get; set; }
    public string description { get; set; }
    public int power { get; set; }
    public ItemType type { get; set; }
    public int maxStack { get; set; }
    public string slug { get; set; }
    public Sprite sprite { get; set; }

    public enum ItemType
    {
        Health, Mana, Arrow, Bomb, Sword, Axe,
        Hammer, Dagger, Bow        
    }

    public Item(int id, string itemName, string description, int power,
        string type, int maxStack, string slug)
    {
        this.id = id;
        this.itemName = itemName;
        this.description = description;
        this.power = power;
        this.type = (ItemType)System.Enum.Parse(typeof(ItemType), type);
        this.maxStack = maxStack;
        this.slug = slug;
        this.sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    public Item()
    {
        id = -1;
    }

    public bool isStackable()
    {
        return maxStack > 1;
    }

    //Organize Items by ItemType and then by power.
    public int CompareTo(object obj)
    {
        if(obj is Item)
        {
            Item compareObj = (Item)obj;
            int returnValue = type.CompareTo(compareObj.type);
            if (returnValue == 0)
            {
                return this.power.CompareTo(compareObj.power);
            }
            return returnValue;
        }
        else
        {
            throw new ArgumentException("Object not a Item");
        }
    }
}