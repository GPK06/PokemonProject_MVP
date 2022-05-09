using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PokemonStats : MonoBehaviour
{
    public string primaryType;
    public string secondaryType;

    public int MaxHealth;
    public double currentHealth;

    public int maxSpeed;
    public int currentSpeed;

    public int maxAttack;
    public int currentAttack;

    public int maxSpecialAttack;
    public int currentSpecialAttack;

    public int maxDefense;
    public int currentDefense;

    public int maxSpecialDefense;
    public int currentSpecialDefense;

    public Move primaryMove;
    public Move secondaryMove;

    public HashSet<string> weakness;
    public HashSet<string> resistance;
    public HashSet<string> immunity;

    public PokemonStats(string primary, string secondary, int health, int speed, int attack, int spAttack, int defense, int spDefense, HashSet<string> weaknessP, HashSet<string> resistanceP, HashSet<string> immunityP);
    {
        primaryType = primary;
        secondaryType = secondary;

        currentHealth = health;
        currentSpeed = speed;
        currentAttack = attack;
        currentSpecialAttack = spAttack;
        currentDefense = defense;
        currentSpecialDefense = spDefense;

        weakness = weaknessP;
        resistance = resistanceP;
        immunity = immunityP;

        primaryMove = new Move(80, "Attack", primary);
        secondaryType = new Move(80, "Attack", secondary);
    }

    public double damageDone(VolthesisStats volthesis, string typeBeingUsed, double effective)
    {
        //Damage = ((((2 * Level / 5 + 2) * AttackStat * AttackPower / DefenseStat) / 50) + 2) * STAB * Weakness/Resistance * RandomNumber / 100
        //Level will just be 50 before a level up system is added.

        //double effective = effectiveness(typeBeingUsed);

        int attackPower;
        Move type;
        if (grass.getType().Equals(typeBeingUsed))
        {
            type = grass;
            attackPower = grass.getBasePower();
        }
        else
        {
            type = steel;
            attackPower = steel.getBasePower();
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

        //Debug.Log("Stats: Mossamr" + effective);

        double damage = ((((2 * 50 / 5 + 2) * attackStat * attackPower / defenseStat) / 50) + 2) * 1.5 * effective * randomNum;
        return damage;
    }

    public double effectiveness(string type)
    {
        int numerator = 1;
        int denominator = 1;
        int stringReturnVal = 0;

        if (immunity.Contains(type))
        {
            return 0;
        }

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
        weakness.Add("fire 2"); //4x weakness
        weakness.Add("fighting");
    }

    public void addResistance()
    {
        resistance.Add("normal");
        resistance.Add("grass 2"); // 4x weakness
        resistance.Add("water");
        resistance.Add("electric");
        resistance.Add("psychic");
        resistance.Add("rock");
        resistance.Add("dragon");
        resistance.Add("steel");
        resistance.Add("fairy");
    }

    public void addImmunity()
    {
        immunity.Add("poison");
    }
}
