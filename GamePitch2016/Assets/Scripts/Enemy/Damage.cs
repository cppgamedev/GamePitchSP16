using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

    public float damage = 1;
    public float knockBackX = 50;
    public float knockBackY = 15;
    public float power = 10;
    public bool destructable = false;

    public float getDamage()
    {
        return damage;
    }

    public float getKnockBackX()
    {
        return knockBackX;
    }

    public float getKnockBackY()
    {
        return knockBackY;
    }

    public float getPower()
    {
        return power;
    }

    public bool getDestructable()
    {
        if (destructable)
            Destroy(this.gameObject);
        return destructable;
    }
}
