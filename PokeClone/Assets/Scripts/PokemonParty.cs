using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PokemonParty : MonoBehaviour
{
    // make a field of myself so that I do not destroy the object in between scenes so that the party remains universal
    public static PokemonParty Instance;

    // static fields because the pokemon stay the same the entire game. If there were pokemon then it would be its own class with its own array
    public static PokemonStats volthesis;

    private static bool trainerBatttle;
    private static bool allDead;
    private static bool allDeadEnemy;

    // so the methods work to fill in the pokemon info

    HashSet<string> weaknessVolthesis = new HashSet<string>();
    HashSet<string> resistanceVolthesis = new HashSet<string>();
    HashSet<string> immunityVolthesis = new HashSet<string>();

    // An array of 6 pokemon that will be the party
    public static PokemonStats[] party = new PokemonStats[6];

    public static PokemonStats[] enemyPokemons = new PokemonStats[6];

    public static string previousRoute; // so i know which route I came from

    public static void partyRestore()
    {
        // save the pokemon in the first slot
        PokemonStats[] party = PokemonParty.getParty();
        PokemonStats pokemon = party[0];

        for (int i = 0; i < 6; i++)
        {
            if (party[i] != null)
            {
                // heal all the pokemon
                party[i].regen();

                // there will only ever be 1 volthesis in a party
                if (party[i].getName().Equals("Volthesis"))
                {
                    // switch with volthesis so that volthesis is in the front like it works in a real pokemon game.
                    party[0] = party[i];
                    party[i] = pokemon;
                }
            }
        }
    }

    // This method will run when the object is first made. Which is made when the game starts
    private void Awake()
    {
        // to prevent multiple instances of the PokemonParty
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Make the instance field this class.
        Instance = this;
        DontDestroyOnLoad(gameObject); // does not destroy the object in between scenes

        initialize();

        assignTrainerBattle(false);
        assignAllDead(false);
    }

    // initializes the first pokemon in the array
    public void initialize()
    {
        // methods to add all of the information to the sets without clogging the constrcutor
        addWeaknessVolthesis();
       
        addResistanceVolthesis();

        addImmunityVolthesis();

        // make the pokemon
        //public PokemonStats(string name, string primary, string secondary, int health, int speed, int attack, int spAttack, int defense, int spDefense, HashSet<string> weaknessP, HashSet<string> resistanceP, HashSet<string> immunityP)
        volthesis = new PokemonStats("Volthesis", "fire", "fairy", 87, 87, 87, 87, 87, 87, weaknessVolthesis, resistanceVolthesis, immunityVolthesis);

        add(volthesis);
    }

    public static PokemonStats getVolthesis()
    {
        return volthesis;
    }

    public static PokemonStats getMossamr()
    {
        PokemonStats mossamr;

        HashSet<string> weaknessMossamr = new HashSet<string>();
        HashSet<string> resistanceMossamr = new HashSet<string>();
        HashSet<string> immunityMossamr = new HashSet<string>();

        weaknessMossamr.Add("fire 2"); //4x weakness
        weaknessMossamr.Add("fighting");

        resistanceMossamr.Add("normal");
        resistanceMossamr.Add("grass 2"); // 4x weakness
        resistanceMossamr.Add("water");
        resistanceMossamr.Add("electric");
        resistanceMossamr.Add("psychic");
        resistanceMossamr.Add("rock");
        resistanceMossamr.Add("dragon");
        resistanceMossamr.Add("steel");
        resistanceMossamr.Add("fairy");

        immunityMossamr.Add("poison");

        mossamr = new PokemonStats("Mossamr", "grass", "steel", 87, 87, 87, 87, 87, 87, weaknessMossamr, resistanceMossamr, immunityMossamr);
        return mossamr;
    }

    // make a method to return a brand new wargo everytime so that they are not all linked
    public static PokemonStats getWargo()
    {
        PokemonStats wargo;
        HashSet<string> weaknessWargo = new HashSet<string>();
        HashSet<string> resistanceWargo = new HashSet<string>();
        
        weaknessWargo.Add("fairy");
        weaknessWargo.Add("dragon");
        
        resistanceWargo.Add("fire 2");
        resistanceWargo.Add("water 2");
        resistanceWargo.Add("steel");
        
        wargo = new PokemonStats("Wargo", "water", "dragon", 87, 87, 87, 87, 87, 87, weaknessWargo, resistanceWargo, null);
        return wargo;
    }

    // adds the pokemon to the array
    public static void add (PokemonStats Pokemon)
    {
        for (int i = 0; i < 6; i++)
        {
            if (party[i] == null)
            {
                party[i] = Pokemon;
                break;
            }
        }
    }

    // returns the party for use in other scripts
    public static PokemonStats[] getParty()
    {
        return party;
    }

    // assigns the pokemon info
    public static void assignPokemonInformation(PokemonStats[] enemyPokemonGiven)
    {
        for (int i = 0; i < 6; i++)
        {
            if (enemyPokemonGiven[i] != null)
            {
                enemyPokemons[i] = enemyPokemonGiven[i];
            }
        }
    }

    // returns the pokemon info
    public static PokemonStats[] getEnemyPokemon()
    {
        return enemyPokemons;
    }

    // asigns the route info
    public static void assignPreviousRoute()
    {
        previousRoute = SceneManager.GetActiveScene().name;
    }

    // gets the route info
    public static string getPrevRoute()
    {
        Debug.Log("Got previous route");
        return previousRoute;
    }

    // method to add volthesis wekaness'
    public void addWeaknessVolthesis()
    {
        weaknessVolthesis.Add("water");
        weaknessVolthesis.Add("ground");
        weaknessVolthesis.Add("poison");
        weaknessVolthesis.Add("rock");
    }

    // method to add volthesis resistance's
    public void addResistanceVolthesis()
    {
        resistanceVolthesis.Add("fire");
        resistanceVolthesis.Add("grass");
        resistanceVolthesis.Add("ice");
        resistanceVolthesis.Add("fighting");
        resistanceVolthesis.Add("fairy");
        resistanceVolthesis.Add("bug 2"); // 4x resistance
        resistanceVolthesis.Add("dark");
    }

    // method to add voltheis immunities
    public void addImmunityVolthesis()
    {
        immunityVolthesis.Add("dragon");
    }

    public static void assignTrainerBattle(bool boolean)
    {
        trainerBatttle = boolean;
    }

    public static bool getTrainerBattle()
    {
        return trainerBatttle;
    }

    public static void assignAllDead(bool boolean)
    {
        allDeadEnemy = boolean;
    }

    public static bool getAllDead()
    {
        return allDeadEnemy;
    }

    public static void assignAllDeadParty(bool boolean)
    {
        allDead = boolean;
    }

    public static bool getAllDeadParty()
    {
        return allDead;
    }
}
