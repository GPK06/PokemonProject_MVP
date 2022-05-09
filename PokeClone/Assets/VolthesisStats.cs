using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolthesisStats : MonoBehaviour
{
    public string primaryType = "fire";
    public string secondaryType = "fairy";

    public int MaxHealth = 110;
    public double currentHealth;
    
    public int maxSpeed = 21;
    public int currentSpeed;

    public int maxAttack = 55;
    public int currentAttack;

    public int maxSpecialAttack = 143;
    public int currentSpecialAttack;

    public int maxDefense = 129;
    public int currentDefense;

    public int maxSpecialDefense = 72;
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

    public double damageDone(MossamrStats mossamr, string typeBeingUsed, double effective)
    {
        //Damage = ((((2 * Level / 5 + 2) * AttackStat * AttackPower / DefenseStat) / 50) + 2) * STAB * Weakness/Resistance * RandomNumber / 100
        //Level will just be 50 before a level up system is added.

        //double effective = effectiveness(typeBeingUsed);

        int attackPower;
        Move type;
        if (fire.getType().Equals(typeBeingUsed)) {
            type = fire;
            attackPower = fire.getBasePower();
        } else
        {
            type = fairy;
            attackPower = fairy.getBasePower();
        }

        int attackStat;
        int defenseStat;

        if (type.getType().Equals("SpecialAttack"))
        {
            attackStat = getSpecialAttack(); // will have to use the move.getAttackStatBeingUsed
            defenseStat = mossamr.getSpecialDefense();
        } else
        {
            attackStat = getAttack();
            defenseStat = mossamr.getDefense();
        }

        double randomNum = Random.Range(85, 101);
        randomNum /= 100;

        Debug.Log("Stats: volthesis" + effective);

        double damage = ((((2 * 50 / 5 + 2) * attackStat * attackPower / defenseStat) / 50) + 2) * 1.5 * effective * randomNum;
        return damage;
    }

    public double damageDoneWargo(WargoStats wargo, string typeBeingUsed, double effective)
    {
        //Damage = ((((2 * Level / 5 + 2) * AttackStat * AttackPower / DefenseStat) / 50) + 2) * STAB * Weakness/Resistance * RandomNumber / 100
        //Level will just be 50 before a level up system is added.

        //double effective = effectiveness(typeBeingUsed);

        int attackPower;
        Move type;
        if (fire.getType().Equals(typeBeingUsed))
        {
            type = fire;
            attackPower = fire.getBasePower();
        }
        else
        {
            type = fairy;
            attackPower = fairy.getBasePower();
        }

        int attackStat;
        int defenseStat;

        if (type.getType().Equals("SpecialAttack"))
        {
            attackStat = getSpecialAttack(); // will have to use the move.getAttackStatBeingUsed
            defenseStat = wargo.getSpecialDefense();
        }
        else
        {
            attackStat = getAttack();
            defenseStat = wargo.getDefense();
        }

        double randomNum = Random.Range(85, 101);
        randomNum /= 100;

        Debug.Log("Stats: volthesis" + effective);

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

    public void takeDamage(double damage, MossamrStats mossamr)
    {
        currentHealth -= damage;
        Debug.Log("Volthesis took " + damage + " hitpoints of damage.");
        Debug.Log("Volthesis current health " + currentHealth + ".");

        if (currentHealth <= 0 && mossamr.getHealth() != 0)
        {
            die();
        }
    }

    public void takeDamageWargo(double damage, WargoStats wargo)
    {
        currentHealth -= damage;
        Debug.Log("Volthesis took " + damage + " hitpoints of damage.");
        Debug.Log("Volthesis current health " + currentHealth + ".");

        if (currentHealth <= 0 && wargo.getHealth() != 0)
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
        resistance.Add("bug 2"); // 4x resistance
        resistance.Add("dark");
    }

    public void addImmunity()
    {
        immunity.Add("dragon");
    }
}
