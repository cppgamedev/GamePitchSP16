using UnityEngine;
using System.Collections;

public class Weapon : IItems
{
    public int id { get; set; }
    public string itemName { get; set; }
    public string description { get; set; }
    public int power { get; set; }
    public string type { get; set; }
    public string slug { get; set; }
    public Sprite sprite { get; set; }

    public Weapon(int id, string itemName, string description, int power,
        string type, string slug)
    {
        this.id = id;
        this.itemName = itemName;
        this.description = description;
        this.power = power;
        this.type = type;
        this.slug = slug;
        this.sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    public Weapon()
    {
        id = -1;
    }
}
