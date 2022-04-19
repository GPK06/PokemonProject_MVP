using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolthesisStats
{
    public string primaryType = "Fire";
    public string secondaryType = "Fairy";

    public int MaxHealth = 87;
    public int currentHealth { get; private set; }
    
    public int maxSpeed = 87;
    public int currentSpeed { get; private set; }

    public int maxAttack = 87;
    public int currentAttack { get; private set; }

    public int maxSpecialAttack = 87;
    public int currentSpecialAttack { get private set; }

    public int maxDefense = 87;
    public int currentDefense { get; private set; }

    public int maxSpecialDefense = 87;
    public int currentSpecialDefense { get; private set; }

    public VolthesisStats()
    {
        currentHealth = MaxHealth;
        currentSpeed = maxSpeed;
        currentAttack = maxAttack;
        currentSpecialAttack = maxSpecialAttack;
        currentDefense = maxDefense;
        currentSpecialDefense = maxSpecialDefense;
    }

    public int damageDone(MossamrStats mossamr)
    {

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

    public int getHealth()
    {
        return currentHealth;
    }
    
    public int getSpeed()
    {
        return currentSpeed;
    }

    public int getAttack()
    {
        return currentAttack;
    }

    public int getSpecialAttack()
    {
        return currentSpecialAttack;
    }

    public int getDefense()
    {
        return currentDefense;
    }

    public int getSpecialDefense()
    {
        return currentSpecialDefense;
    }

    public string getPrimaryType()
    {
        return primaryType;
    }

    public string getSecondaryType()
    {
        return secondaryType;
    }
}
