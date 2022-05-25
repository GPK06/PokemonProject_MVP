using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{
    public static PokemonParty Instance;

    public static PokemonStats[] party = new PokemonStats[6]; 

    private void Awake()
    {
        // to prevent multiple instances of the PokemonParty
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void start()
    {
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

        PokemonStats volthesis = new PokemonStats("Volthesis", "fire", "fairy", 110, 21, 55, 143, 129, 72, weaknessVolthesis, resistanceVolthesis, immunityVolthesis);

        add(volthesis);
    }

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

    public static PokemonStats[] getParty()
    {
        return party;
    }
}
