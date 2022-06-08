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
    public static PokemonStats wargo;
    public static PokemonStats mossamr;

    // so the methods work to fill in the pokemon info
    HashSet<string> weaknessMossamr = new HashSet<string>();
    HashSet<string> resistanceMossamr = new HashSet<string>();
    HashSet<string> immunityMossamr = new HashSet<string>();

    HashSet<string> weaknessVolthesis = new HashSet<string>();
    HashSet<string> resistanceVolthesis = new HashSet<string>();
    HashSet<string> immunityVolthesis = new HashSet<string>();

    HashSet<string> weaknessWargo = new HashSet<string>();
    HashSet<string> resistanceWargo = new HashSet<string>();

    // An array of 6 pokemon that will be the party
    public static PokemonStats[] party = new PokemonStats[6];

    public static PokemonStats[] enemyPokemons = new PokemonStats[6];

    public static string previousRoute;

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
    }

    // initializes the first pokemon in the array
    public void initialize()
    {
        // make the sets for the constructor for all of the pokemon
        HashSet<string> weaknessMossamr = new HashSet<string>();
        HashSet<string> resistanceMossamr = new HashSet<string>();
        HashSet<string> immunityMossamr = new HashSet<string>();

        HashSet<string> weaknessVolthesis = new HashSet<string>();
        HashSet<string> resistanceVolthesis = new HashSet<string>();
        HashSet<string> immunityVolthesis = new HashSet<string>();

        HashSet<string> weaknessWargo = new HashSet<string>();
        HashSet<string> resistanceWargo = new HashSet<string>();

        // methods to add all of the information to the sets without clogging the constrcutor
        addWeaknessMossamr();
        addWeaknessVolthesis();
        addWeaknessWargo();

        addResistanceWargo();
        addResistanceVolthesis();
        addResistanceMossamr();

        addImmunityVolthesis();
        addImmunityMossamr();

        // make the pokemon
        //public PokemonStats(string name, string primary, string secondary, int health, int speed, int attack, int spAttack, int defense, int spDefense, HashSet<string> weaknessP, HashSet<string> resistanceP, HashSet<string> immunityP)
        mossamr = new PokemonStats("Mossamr", "grass", "steel", 87, 87, 87, 87, 87, 87, weaknessMossamr, resistanceMossamr, immunityMossamr);
        volthesis = new PokemonStats("Volthesis", "fire", "fairy", 87, 87, 87, 87, 87, 87, weaknessVolthesis, resistanceVolthesis, immunityVolthesis);
        wargo = new PokemonStats("Wargo", "water", "dragon", 87, 87, 87, 87, 87, 87, weaknessWargo, resistanceWargo, null);

        add(volthesis);
    }

    public static PokemonStats getVolthesis()
    {
        return volthesis;
    }

    public static PokemonStats getMossamr()
    {
        return mossamr;
    }

    public static PokemonStats getWargo()
    {
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

    // method to add mossamrs weakness'
    public void addWeaknessMossamr()
    {
        weaknessMossamr.Add("fire 2"); //4x weakness
        weaknessMossamr.Add("fighting");
    }

    // method to add mossamrs resistances
    public void addResistanceMossamr()
    {
        resistanceMossamr.Add("normal");
        resistanceMossamr.Add("grass 2"); // 4x weakness
        resistanceMossamr.Add("water");
        resistanceMossamr.Add("electric");
        resistanceMossamr.Add("psychic");
        resistanceMossamr.Add("rock");
        resistanceMossamr.Add("dragon");
        resistanceMossamr.Add("steel");
        resistanceMossamr.Add("fairy");
    }

    // method to add mossamrs immunities
    public void addImmunityMossamr()
    {
        immunityMossamr.Add("poison");
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

    // method to add wargos weakness'
    public void addWeaknessWargo()
    {
        weaknessWargo.Add("fairy");
        weaknessWargo.Add("dragon");
    }

    // method to add wargos resistance
    public void addResistanceWargo()
    {
        resistanceWargo.Add("fire 2");
        resistanceWargo.Add("water 2");
        resistanceWargo.Add("steel");
    }
}
