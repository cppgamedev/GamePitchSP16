using UnityEngine;

public interface IItems{

    int id { get; set; }
    string itemName { get; set; }
    string description { get; set; }
    string slug { get; set; }
    Sprite sprite { get; set; }
    
}
