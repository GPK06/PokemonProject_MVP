using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PokemonParty : MonoBehaviour
{
    // make a field of myself so that I do not destroy the object in between scenes so that the party remains universal
    public static PokemonParty Instance;

    // An array of 6 pokemon that will be the party
    public static PokemonStats[] party = new PokemonStats[6];

    public static PokemonStats enemyPokemon;

    public static string previousRoute;

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
        // makes a set for all the information needed to go in the pokemon
        HashSet<string> weaknessVolthesis = new HashSet<string>();
        HashSet<string> resistanceVolthesis = new HashSet<string>();
        HashSet<string> immunityVolthesis = new HashSet<string>();

        // add all of the information to the sets
        weaknessVolthesis.Add("water");
        weaknessVolthesis.Add("ground");
        weaknessVolthesis.Add("poison");
        weaknessVolthesis.Add("rock");

        resistanceVolthesis.Add("fire");
        resistanceVolthesis.Add("grass");
        resistanceVolthesis.Add("ice");
        resistanceVolthesis.Add("fighting");
        resistanceVolthesis.Add("fairy");
        resistanceVolthesis.Add("bug 2"); // 4x resistance
        resistanceVolthesis.Add("dark");

        immunityVolthesis.Add("dragon");

        PokemonStats volthesis; // make a new variable

        volthesis = new PokemonStats("Volthesis", "fire", "fairy", 110, 21, 55, 143, 129, 72, weaknessVolthesis, resistanceVolthesis, immunityVolthesis); // make the new pokemon

        add(volthesis); 
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
    public static void assignPokemonInformation(PokemonStats enemyPokemonGiven)
    {
        enemyPokemon = enemyPokemonGiven;
    }

    // returns the pokemon info
    public static PokemonStats getEnemyPokemon()
    {
        return enemyPokemon;
    }

    // asigns the route info
    public static void assignPreviousRoute()
    {
        previousRoute = SceneManager.GetActiveScene().name;
    }

    // gets the route info
    public static string getPrevRoute()
    {
        return previousRoute;
    }
}
