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

    private void Start()
    {
        if (PokemonParty.Instance != null)
        {
            party[0] = 
        }
    }
}
