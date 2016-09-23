using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    public float health;
    public const float maxHealth = 3;

    void Start()
    {
        health = maxHealth;
    }

    public float getHelath()
    {
        return health;
    }

    public void removeHealth(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void addHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
            removeHealth(other.gameObject.GetComponent<Damage>().getDamage());
        }
    }
}