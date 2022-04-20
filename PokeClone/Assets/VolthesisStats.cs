using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolthesisStats : MonoBehaviour
{
    public string primaryType = "Fire";
    public string secondaryType = "Fairy";

    public int MaxHealth = 87;
    public int currentHealth;
    
    public int maxSpeed = 87;
    public int currentSpeed;

    public int maxAttack = 87;
    public int currentAttack;

    public int maxSpecialAttack = 87;
    public int currentSpecialAttack;

    public int maxDefense = 87;
    public int currentDefense;

    public int maxSpecialDefense = 87;
    public int currentSpecialDefense;

    public Move fire = new Move(80, "Attack", "fire");
    public Move steel = new Move(80, "Attack", "fairy");

    public VolthesisStats()
    {
        currentHealth = MaxHealth;
        currentSpeed = maxSpeed;
        currentAttack = maxAttack;
        currentSpecialAttack = maxSpecialAttack;
        currentDefense = maxDefense;
        currentSpecialDefense = maxSpecialDefense;
    }

    public void damageDone(MossamrStats mossamr)
    {
        //Damage = ((((2 * Level / 5 + 2) * AttackStat * AttackPower / DefenseStat) / 50) + 2) * STAB * Weakness/Resistance * RandomNumber / 100

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
