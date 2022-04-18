using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MossamrStats
{
    public int MaxHealth = 100;
    public int currentHealth { get; private set; }
    public int currentSpeed { get; private set; }
    public int maxSpeed = 80;

    public MossamrStats()
    {
        currentHealth = MaxHealth;
        currentSpeed = maxSpeed;
    }

    public void takeDamage(int damage, VolthesisStats volthesis)
    {
        currentHealth -= damage;
        Debug.Log("Mossamr took " + damage + " hitpoints of damage.");
        Debug.Log("Mossamr current health " + currentHealth + ".");

        if (currentHealth <= 0 && volthesis.getHealth() != 0)
        {
            die();
        }
    }

    public int getSpeed()
    {
        return currentSpeed;
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
