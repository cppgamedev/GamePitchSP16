using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {
    public static PlayerStats Instance;
    public float health;
    public const float maxHealth = 3;

	void Awake ()
    {
        Instance = this;
        health = maxHealth;
        
	}
	
    public float getHealth()
    {
        return health;
    }

	public void removeHealth (float amount)
    {
        health -= amount;
        Debug.Log("Damaged. Health is now: " + health);
        if (health <= 0)
        {
			die();
        }
	}
	
	public void die() {
        Debug.Log("Died, restarting");
		// add timer so level doesn't reset right away?
		 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
