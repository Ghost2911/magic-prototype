using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{
    private int health = 10;
    public int Health { get => health; set { health = value; if (value <= 0) Destroy(gameObject);} }

    public void Damage(int damage)
    {
        Health -= damage;
    }
}
