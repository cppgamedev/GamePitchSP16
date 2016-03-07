using UnityEngine;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class ItemDatabase : MonoBehaviour
{
    //private List<Collectables> collectable = new List<Collectables>();
    private List<Item> itemDatabase = new List<Item>();
    private string jsonString;
    private JsonData itemData;

    // Use this for initialization
    void Start()
    {
        jsonString = File.ReadAllText(Application.dataPath + "/Resources/Items.json");
        itemData = JsonMapper.ToObject(jsonString);

        //seperated into these catagories for organization purposes.
        loadItemDatabase("Weapons");
        loadItemDatabase("Consumables");
        loadItemDatabase("StackingWeapons");
    }

    //creates new Items by getting the info out of json object.
    private void loadItemDatabase(string type)
    {
        for (int i = 0; i < itemData[type].Count; i++)
        {
            itemDatabase.Add(new Item((int)itemData[type][i]["id"], itemData[type][i]["title"].ToString(),
                itemData[type][i]["description"].ToString(), (int)itemData[type][i]["power"],
                itemData[type][i]["type"].ToString(), (int)itemData[type][i]["maxStack"], 
                itemData[type][i]["slug"].ToString()));
        }        
    }

    //Search database for item with id and returns that item.
    public Item searchItem(int id)
    {
        foreach(var item in itemDatabase)
        {
            if (item.id == id)
            {
                return item;
            }
        }

        return null;
    }
}