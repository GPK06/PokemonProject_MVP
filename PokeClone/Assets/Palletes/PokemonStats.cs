using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PokemonStats : MonoBehaviour
{
    public string name;

    public string primaryType;
    public string secondaryType;

    public int MaxHealth;
    public double currentHealth;

    public int MaxSpeed;
    public int currentSpeed;

    public int MaxAttack;
    public int currentAttack;

    public int MaxSpecialAttack;
    public int currentSpecialAttack;

    public int MaxDefense;
    public int currentDefense;

    public int MaxSpecialDefense;
    public int currentSpecialDefense;

    public Move primaryMove;
    public Move secondaryMove;

    public HashSet<string> weakness;
    public HashSet<string> resistance;
    public HashSet<string> immunity;

    public PokemonStats(string name, string primary, string secondary, int health, int speed, int attack, int spAttack, int defense, int spDefense, HashSet<string> weaknessP, HashSet<string> resistanceP, HashSet<string> immunityP)
    {
        this.name = name;
        
        primaryType = primary;
        secondaryType = secondary;

        MaxHealth = health;
        MaxSpeed= speed;
        MaxAttack = attack;
        MaxSpecialAttack = spAttack;
        MaxDefense = defense;
        MaxSpecialDefense = spDefense;

        currentHealth = MaxHealth;
        currentSpeed = MaxSpeed;
        currentSpecialAttack = MaxSpecialAttack;
        currentAttack = MaxAttack;
        currentSpecialDefense = MaxSpecialDefense;
        currentDefense = MaxDefense;

        weakness = weaknessP;
        resistance = resistanceP;
        immunity = immunityP;

        primaryMove = new Move(80, "Attack", primaryType);
        secondaryMove = new Move(80, "Attack", secondaryType);
    }

    public double damageDone(PokemonStats pokemon, string typeBeingUsed, double effective)
    {
        //Damage = ((((2 * Level / 5 + 2) * AttackStat * AttackPower / DefenseStat) / 50) + 2) * STAB * Weakness/Resistance * RandomNumber / 100
        //Level will just be 50 before a level up system is added.

        //double effective = effectiveness(typeBeingUsed);

        int attackPower;
        Move type;
        if (primaryMove.getType().Equals(typeBeingUsed))
        {
            type = primaryMove;
            attackPower = primaryMove.getBasePower();
        }
        else
        {
            type = secondaryMove;
            attackPower = secondaryMove.getBasePower();
        }

        int attackStat;
        int defenseStat;

        if (type.getType().Equals("SpecialAttack"))
        {
            attackStat = getSpecialAttack(); // will have to use the move.getAttackStatBeingUsed
            defenseStat = pokemon.getSpecialDefense();
        }
        else
        {
            attackStat = getAttack();
            defenseStat = pokemon.getDefense();
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

        if (immunity != null)
        {
            if (immunity.Contains(type))
            {
                return 0;
            }
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

    public void takeDamage(double damage, PokemonStats pokemon)
    {
        currentHealth -= damage;
        Debug.Log(name + " took " + damage + " hitpoints of damage.");
        Debug.Log(name + " current health " + currentHealth + ".");

        if (currentHealth <= 0 && pokemon.getHealth() != 0)
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
}