using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // imported to change scenes back to the route
using UnityEngine.UI; // imported to chagne the ui component in unity


public class PokemonStats
{
    // a reference to the canvas so that I can change the battle screen
    public GameObject Canvas;

    // all of the fields that the pokemon needs
    public string name;

    public string primaryType;
    public string secondaryType;
    
    // max version of each stat if stat changing moves are added
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

    // sets for the weakness and resistance fraction in the damage done method
    public HashSet<string> weakness;
    public HashSet<string> resistance;
    public HashSet<string> immunity;

    // constructor to assign all of the stats to the pokemon
    public PokemonStats(string name, string primary, string secondary, int health, int speed, int attack, int spAttack, int defense, int spDefense, HashSet<string> weaknessP, HashSet<string> resistanceP, HashSet<string> immunityP)
    {
        // assign all information
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

        // makes new moves for the pokemon 
        primaryMove = new Move(60, "Attack", primaryType);
        secondaryMove = new Move(60, "Attack", secondaryType);
    }

    // method to calculate the damage that the pokemon does with the
    // parameters: enemy pokemon, type being used, and the fraction that is needed in the calculations 
    public double damageDone(PokemonStats pokemon, string typeBeingUsed, double effective)
    {
        //Damage = ((((2 * Level / 5 + 2) * AttackStat * AttackPower / DefenseStat) / 50) + 2) * STAB * Weakness/Resistance * RandomNumber / 100
        //Level will just be 50 before a level up system is added.

        // assigns the move and the attack power in the calculations depending on the type being used
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

        // gets the base stats of the attack being used and gets the corresponding defense stat of the opposing pokemon
        if (type.getType().Equals("SpecialAttack"))
        {
            attackStat = getSpecialAttack(); 
            defenseStat = pokemon.getSpecialDefense();
        }
        else
        {
            attackStat = getAttack();
            defenseStat = pokemon.getDefense();
        }

        // gets the random number that is in the damage calculation
        double randomNum = Random.Range(85, 101);
        randomNum /= 100;

        // does the damage calculatiom
        double damage = ((((2 * 50 / 5 + 2) * attackStat * attackPower / defenseStat) / 50) + 2) * 1.5 * effective * randomNum;
        return damage;
    }

    // method to see the effectiveness of the pokemon with the type being sent of the pokemon
    public double effectiveness(string type)
    {
        int numerator = 1;
        int denominator = 1;

        //checks the immunity set if it is not null and if it is equal to the type then it is returns 0
        if (immunity != null)
        {
            if (immunity.Contains(type))
            {
                return 0;
            }
        }

        // for each to see if the type is in the weakness set and sets the value for the numerator in the fraction
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

        // for each to see if the type is in the resitance set and sets the value for the denominator in the fraction.
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

        // returns the fraction after doing the math (did not want to work any other way I coded it)
        double returnVal = numerator;
        returnVal /= denominator;

        return returnVal;
    }

    // deals the damage to the pokemon by getting the damage dealt and the enemy pokemon
    public void takeDamage(double damage, PokemonStats pokemon)
    {
        currentHealth -= damage;

        Canvas = GameObject.FindWithTag("Canvas");
        
        // changes the value of the text diplayed on the screen
        Text healthText;
        healthText = Canvas.transform.Find("CurrentHealth").GetComponent<Text>();
        healthText.text = currentHealth + "";

        // if the pokemon dies then play the death method
        if (currentHealth < 0 && pokemon.getHealth() != 0)
        {
            healthText.text = 0 + "";
            die();
        }
    }

    // the enemy takes the damage. The only reason it is a different method is to update the enemies health
    public void takeDamageEnemy(double damage, PokemonStats pokemon)
    {
        currentHealth -= damage;

        Canvas = GameObject.FindWithTag("Canvas");

        // changes the value of the text diplayed on the screen
        Text healthText;
        healthText = Canvas.transform.Find("EnemyCurrentHealth").GetComponent<Text>(); // only way to get text that is why has to be own method
        healthText.text = currentHealth + "";

        // if the pokemon dies then play the death method
        if (currentHealth < 0 && pokemon.getHealth() != 0)
        {
            healthText.text = 0 + "";
            dieEnemy();
        }
    }

    // die method for the enmy so the battle ends when the enemies pokemon is dead
    public void dieEnemy()
    {
        PokemonStats[] party = PokemonParty.getEnemyPokemon();
        bool allPokemonDead = true;

        PokemonStats current = party[0];

        // checks if all of the pokemon in the party are dead
        for (int i = 1; i < 6; i++)
        {
            if (party[i] != null)
            {
                if (party[i].getHealth() > 1)
                {
                    allPokemonDead = false;
                    party[0] = party[i];
                    party[i] = current;
                    
                    BattleOptions.updateEnemyPokemon();
                }
            }
        }

        if (allPokemonDead)
        {
            PokemonParty.assignAllDead(true);

            // will have to have an array if trainer has multiple pokemon
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");

            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }

            PokemonParty.assignAllDead(true);

            GameObject nextButton = GameObject.Find("Canvas/BattleOptions/NextButton");
            nextButton.SetActive(true);
        }
    }

    // the pokemon dies so the player is sent back to route 1
    public void die()
    {
        PokemonStats[] party = PokemonParty.getParty();
        bool allPokemonDead = true;
        
        // checks if all of the pokemon in the party are dead
        foreach (PokemonStats pokemon in party)
        {
            if (pokemon != null)
            {
                if (pokemon.getHealth() > 1)
                {
                    allPokemonDead = false;
                }
            }
        }

        // if all pokemon are dead
        if (allPokemonDead)
        {

            GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");

            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }

            GameObject nextButton = GameObject.Find("Canvas/BattleOptions/NextButton");
            nextButton.SetActive(true);
        }
    }

    // returns the health
    public double getHealth()
    {
        return currentHealth;
    }

    //returns the max health
    public double maxHealth()
    {
        return MaxHealth;
    }

    // regens the pokemons health
    public void regen()
    {
        currentHealth = MaxHealth;
    }

    // returns the speed
    public int getSpeed()
    {
        return currentSpeed;
    }

    // returns the attack stat
    public int getAttack()
    {
        return currentAttack;
    }

    // returns the special attack stat
    public int getSpecialAttack()
    {
        return currentSpecialAttack;
    }

    // returns the defense stat
    public int getDefense()
    {
        return currentDefense;
    }

    // returns the special defenese stat
    public int getSpecialDefense()
    {
        return currentSpecialDefense;
    }

    // returns the primary type
    public string getPrimaryType()
    {
        return primaryType;
    }

    // returns the secondary type
    public string getSecondaryType()
    {
        return secondaryType;
    }

    // returns the name of the pokemon
    public string getName()
    {
        return name;
    }
}