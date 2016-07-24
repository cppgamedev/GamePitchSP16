using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    public float health;
    public const float maxHealth = 3;

	void Awake ()
    {
        health = maxHealth;
	}
	
    public float getHelath()
    {
        return health;
    }

	public void removeHealth (float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

    public void addHealth (float amount)
    {
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
