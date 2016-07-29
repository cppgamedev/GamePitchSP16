using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

    public float damage = 1;
    public float knockBackX = 50;
    public float knockBackY = 15;

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
}
