using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Health : MonoBehaviour
{
    private int currentHealth = 10;
    public int maxHealth = 10;

    public bool IsDead { get => currentHealth <= 0; }

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
