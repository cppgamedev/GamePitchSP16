using UnityEngine;
using System.Collections;

public class CloseInventory : MonoBehaviour {
    
    public GameObject parentObject;
    public void close()
    {
        parentObject = GameObject.Find("Panel");
        Debug.Log("Close button clicked");
    
        parentObject.SetActive(false);
        //InventoryUi.Panel.hide();
        // GUI.Window();
    }
}
