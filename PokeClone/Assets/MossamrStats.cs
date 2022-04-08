using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MossamrStats
{
    public int MaxHealth = 100;
    public int currentHealth { get; private set; }

    public MossamrStats()
    {
        currentHealth = MaxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Mossamr took " + damage + " hitpoints of damage.");
        Debug.Log("Mossamr current health " + currentHealth + ".");

        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void die()
    {
        Debug.Log("Mossamr has fainted.");
        SceneManager.LoadScene("Route 1");
    }

    public int getHealth()
    {
        return currentHealth;
    }
}
