using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WargoStats : MonoBehaviour
{
    public string primaryType = "water";
    public string secondaryType = "dragon";

    public int MaxHealth = 54;
    public double currentHealth;

    public int maxSpeed = 88;
    public int currentSpeed;

    public int maxAttack = 104;
    public int currentAttack;

    public int maxSpecialAttack = 104;
    public int currentSpecialAttack;

    public int maxDefense = 90;
    public int currentDefense;

    public int maxSpecialDefense = 90;
    public int currentSpecialDefense;

    public Move water;
    public Move dragon;

    private HashSet<string> weakness = new HashSet<string>();
    private HashSet<string> resistance = new HashSet<string>();

    public WargoStats()
    {
        currentHealth = MaxHealth;
        currentSpeed = maxSpeed;
        currentAttack = maxAttack;
        currentSpecialAttack = maxSpecialAttack;
        currentDefense = maxDefense;
        currentSpecialDefense = maxSpecialDefense;

        addResistance();
        addWeakness();

        water = new Move(80, "SpecialAttack", "water");
        dragon = new Move(80, "Attack", "dragon");
    }

    public double damageDone(VolthesisStats volthesis, string typeBeingUsed, double effective)
    {
        //Damage = ((((2 * Level / 5 + 2) * AttackStat * AttackPower / DefenseStat) / 50) + 2) * STAB * Weakness/Resistance * RandomNumber / 100
        //Level will just be 50 before a level up system is added.

        //double effective = effectiveness(typeBeingUsed);

        int attackPower;
        Move type;
        if (water.getType().Equals(typeBeingUsed))
        {
            type = water;
            attackPower = water.getBasePower();
        }
        else
        {
            type = dragon;
            attackPower = dragon.getBasePower();
        }

        int attackStat;
        int defenseStat;

        if (type.getType().Equals("SpecialAttack"))
        {
            attackStat = getSpecialAttack(); // will have to use the move.getAttackStatBeingUsed
            defenseStat = volthesis.getSpecialDefense();
        }
        else
        {
            attackStat = getAttack();
            defenseStat = volthesis.getDefense();
        }

        double randomNum = Random.Range(85, 101);
        randomNum /= 100;

        double damage = ((((2 * 50 / 5 + 2) * attackStat * attackPower / defenseStat) / 50) + 2) * 1.5 * effective * randomNum;
        return damage;
    }

    public double effectiveness(string type)
    {
        int numerator = 1;
        int denominator = 1;
        int stringReturnVal = 0;

        foreach (string weaknessType in weakness)
        {
            if (weaknessType.Contains(type))
            {
                numerator = 2;

                if (weaknessType.Contains("2"))
                {
                    numerator = 4;
                }
            }
        }

        foreach (string resistanceType in resistance)
        {
            if (resistanceType.Contains(type))
            {
                denominator = 2;
                if (resistanceType.Contains(type))
                {
                    denominator = 4;
                }
            }
        }

        double returnVal = numerator;
        returnVal /= denominator;

        return returnVal;
    }

    public void takeDamage(double damage, VolthesisStats volthesis)
    {
        currentHealth -= damage;
        Debug.Log("Wargo took " + damage + " hitpoints of damage.");
        Debug.Log("Wargo current health " + currentHealth + ".");

        if (currentHealth <= 0 && volthesis.getHealth() != 0)
        {
            die();
        }
    }

    public void die()
    {
        SceneManager.LoadScene("Route 1");
    }

    public double getHealth()
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
        weakness.Add("fairy"); //4x weakness
        weakness.Add("dragon");
    }

    public void addResistance()
    {
        resistance.Add("fire 2");
        resistance.Add("water 2");
        resistance.Add("steel");
    }
}
