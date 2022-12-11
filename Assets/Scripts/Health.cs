using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class Health : MonoBehaviour
{
    public int currentHealth = 10;
    public int MaxHealth = 10;

    public bool IsDead { get => currentHealth <= 0; }

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    public void Start()
    {
        currentHealth = MaxHealth;
    }

    public void Update()
    {
        var scene = SceneManager.GetActiveScene();
        if(scene.name != "FinishedScene")
        {
            currentHealth = MaxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
    }
}
