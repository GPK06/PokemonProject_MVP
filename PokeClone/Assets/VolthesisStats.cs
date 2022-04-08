using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolthesisStats
{
    public int MaxHealth = 100;
    public int currentHealth { get; private set; }

    public VolthesisStats()
    {
        currentHealth = MaxHealth;
    }

    public void takeDamage(int damage, MossamrStats mossamr)
    {
        currentHealth -= damage;
        Debug.Log("Volthesis took " + damage + " hitpoints of damage.");
        Debug.Log("Volthesis current health " + currentHealth + ".");

        if (currentHealth <= 0 && mossamr.getHealth() != 0)
        {
            die();
        }
    }

    public void die()
    {
        Debug.Log("Volthesis has fainted");
        Debug.Log("All mighty Arceus has given you another chance");
        SceneManager.LoadScene("Route 1");
    }
}
