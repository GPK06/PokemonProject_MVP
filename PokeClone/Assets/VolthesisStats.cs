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

    public Move fire;
    public Move fairy;

    private HashSet<string> weakness = new HashSet<string>();
    private HashSet<string> resistance = new HashSet<string>();
    private HashSet<string> immunity = new HashSet<string>();

    public VolthesisStats()
    {
        currentHealth = MaxHealth;
        currentSpeed = maxSpeed;
        currentAttack = maxAttack;
        currentSpecialAttack = maxSpecialAttack;
        currentDefense = maxDefense;
        currentSpecialDefense = maxSpecialDefense;

        fire = new Move(80, "SpecialAttack", "fire");
        fairy = new Move(80, "SpecialAttack", "fairy");

        addWeakness();
        addResistance();
        addImmunity();
    }

    public int damageDone(MossamrStats mossamr, string typeBeingUsed)
    {
        //Damage = ((((2 * Level / 5 + 2) * AttackStat * AttackPower / DefenseStat) / 50) + 2) * STAB * Weakness/Resistance * RandomNumber / 100
        //Level will just be 50 before a level up system is added.

        int effective = effectiveness(typeBeingUsed);

        int attackPower;
        if (fire.getType().Equals(typeBeingUsed)) { 
            attackPower = fire.getBasePower();
        } else
        {
            attackPower = fairy.getBasePower();
        }

        int attackStat = getSpecialAttack(); // will have to use the move.getAttackStatBeingUsed
        int defenseStat = mossamr.getSpecialDefense();
        int randomNum = Random.Range(85, 101) / 100;

        int damage = ((((2 * 50 / 5 + 2) * attackStat * attackPower / defenseStat) / 50) + 2) * 1.5 * effective * randomNum / 100;
        return damage;
    }

    public int effectiveness(string type)
    {
        int numerator = 1;
        int denominator = 1;
        
        if (immunity.Contains(type))
        {
            return 0;
        }

        foreach (string weaknessType in weakness)
        {
            if (String.Contains(type, weaknessType, 1) == 0)
            {
                numerator = 2;
                if (String.Contains("2", weaknessType, 1) == String.Contains("2", weaknessType, 1))
                {
                    numerator = 4;
                }
            }
        }

        foreach (string resistanceType in resistance)
        {
            if (String.Contains(type, resistanceType, 1) == 0)
            {
                denominator = 2;
                if (String.Contains("2", resistanceType, 1) == String.Contains("2", resistanceType, 1))
                {
                    denominator = 4;
                }
            }
        }

        return numerator / denominator;
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

    public void addWeakness()
    {
        weakness.Add("water");
        weakness.Add("ground");
        weakness.Add("poison");
        weakness.Add("rock");
    }

    public void addResistance()
    {
        resistance.Add("fire");
        resistance.Add("grass");
        resistance.Add("ice");
        resistance.Add("fighting");
        resistance.Add("fairy");
        resistance.Add("bug"); // 4x resistance
        resistance.Add("dark");
    }

    public void addImmunity()
    {
        immunity.Add("dragon");
    }
}
