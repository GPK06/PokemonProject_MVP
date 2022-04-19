using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MossamrStats
{
    public string primaryType = "Grass";
    public string secondaryType = "Steel";

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

    public MossamrStats()
    {
        currentHealth = MaxHealth;
        currentSpeed = maxSpeed;
        currentAttack = maxAttack;
        currentSpecialAttack = maxSpecialAttack;
        currentDefense = maxDefense;
        currentSpecialDefense = maxSpecialDefense;
    }

    public int damageDone(VolthesisStats volthesis)
    {
        //Damage = ((((2 * Level / 5 + 2) * AttackStat * AttackPower / DefenseStat) / 50) + 2) * STAB * Weakness/Resistance * RandomNumber / 100
        
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

    public void die()
    {
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
