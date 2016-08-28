using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public float startingHealth = PlayerStats.maxHealth;                            // The amount of health the player starts the game with.
    public float health = PlayerStats.Instance.getHealth();                                 // The current health the player has.
                                                                                             //RectTransform bar = GetComponent("healthRed");
    public Slider healthSlider;
    
    void Start()
    {
        healthSlider.maxValue = health;
    }
    void Update()
    {
        health = PlayerStats.Instance.getHealth();
        healthSlider.value = health;
        //Debug.Log("health" + health);
    }


}