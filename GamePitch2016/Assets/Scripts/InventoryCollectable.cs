using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System.Collections.Generic;

public class InventoryCollectable : MonoBehaviour {

    private List<Collectable> Collectables = new List<Collectable>();
    private List<Collectable> CollectableParts = new List<Collectable>();
    private string jsonString;
    private JsonData itemData;

    // Use this for initialization
    void Start()
    {
        jsonString = File.ReadAllText(Application.dataPath + "/Resources/Collectables.json");
        itemData = JsonMapper.ToObject(jsonString);

        //seperated into these catagories for organization purposes.
        loadDatabase("Collectables");

        foreach(Collectable item in Collectables)
        {
            Debug.Log(item.itemName);
        }

        foreach (Collectable item in CollectableParts)
        {
            Debug.Log(item.itemName);
        }

    }

    //creates new Items by getting the info out of json object.
    private void loadDatabase(string type)
    {
        for (int i = 0; i < itemData[type].Count; i++)
        {
            Collectables.Add(new Collectable((int)itemData[type][i]["groupId"], (int)itemData[type][i]["id"], itemData[type][i]["title"].ToString(),
                itemData[type][i]["description"].ToString(), itemData[type][i]["slug"].ToString()));
            for(int j = 0; j < itemData[type][i]["parts"].Count; j++)
            {
                CollectableParts.Add(new Collectable((int)itemData[type][i]["groupId"], (int)itemData[type][i]["parts"][j]["id"], itemData[type][i]["parts"][j]["title"].ToString(),
                itemData[type][i]["parts"][j]["description"].ToString(), itemData[type][i]["parts"][j]["slug"].ToString()));
            }
        }
    }
}
