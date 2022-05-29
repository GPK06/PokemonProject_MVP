using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleOptions : MonoBehaviour
{
    // Make new pokemons
    PokemonStats mossamr;
    PokemonStats volthesis;
    PokemonStats wargo;

    // make the sets for the constructor for all of the pokemon
    HashSet<string> weaknessMossamr = new HashSet<string>();
    HashSet<string> resistanceMossamr = new HashSet<string>();
    HashSet<string> immunityMossamr = new HashSet<string>();

    HashSet<string> weaknessVolthesis = new HashSet<string>();
    HashSet<string> resistanceVolthesis = new HashSet<string>();
    HashSet<string> immunityVolthesis = new HashSet<string>();

    HashSet<string> weaknessWargo = new HashSet<string>();
    HashSet<string> resistanceWargo = new HashSet<string>();

    // copnctructor to inuitalize all of the fields
    public BattleOptions()
    {
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
    }

    // changes to route 1 if the player ran away
    public void runAway()
    {
        SceneManager.LoadScene("Route 1");
    }

    // catches the pokemon by adding it to the pokemon party array
    public void catchPokemon()
    {
        // gets the name to see which pokemon is being caught
        Text name = transform.Find("EnemyName").GetComponent<Text>();
        if (name.text == "Mossamr")
        {
            PokemonParty.add(mossamr);
        } else
        {
            PokemonParty.add(wargo);
        }
    }

    // goes into battle and depending on the number sent then that is the move done by the pokemon
    public void battle(int n)
    {
        // initializes all of the variables that will ned to be used
        string stat;

        double mossamrDamage;
        double volthDamage;
        double mossamrFrac;
        double volthFrac;

        // makes the effectiveness fractions for volthesis and gets the damage done for both different type attacks
        volthFrac = volthesis.effectiveness(mossamr.getPrimaryType());
        double mossamrDamagePrimary = mossamr.damageDone(volthesis, mossamr.getPrimaryType(), volthFrac);
        volthFrac = volthesis.effectiveness(mossamr.getSecondaryType());
        double mossamrDamageSecondary = mossamr.damageDone(volthesis, mossamr.getSecondaryType(), volthFrac);

        // the better dame is done to the players pokemon
        if (mossamrDamagePrimary > mossamrDamageSecondary)
        {
            mossamrDamage = mossamrDamagePrimary;
        }
        else
        {
            mossamrDamage = mossamrDamageSecondary;
        }

        if (n == 1)
        {
            // if int n is a 1 then we are using the primary stat
            mossamrFrac = mossamr.effectiveness(volthesis.getPrimaryType());
            volthDamage = volthesis.damageDone(mossamr, volthesis.getPrimaryType(), mossamrFrac);
        }
        else
        {
            // if int n is a 2 then we are using the secondary stat
            mossamrFrac = mossamr.effectiveness(volthesis.getSecondaryType());
            volthDamage = volthesis.damageDone(mossamr, volthesis.getSecondaryType(), mossamrFrac);
        }

        // whichever pokemon is faster attacks first
        if (volthesis.getSpeed() > wargo.getSpeed())
        {
            mossamr.takeDamageEnemy(volthDamage, volthesis);
            volthesis.takeDamage(mossamrDamage, mossamr);
        }
        else
        {
            volthesis.takeDamage(mossamrDamage, mossamr);
            mossamr.takeDamageEnemy(volthDamage, volthesis);
        }
    }

    // the same battle method but if the enemy pokemon is wargo
    public void battleWargo(int n)
    {
        string stat;

        double wargoDamage;
        double volthDamage;
        double wargoFrac;
        double volthFrac;

        volthFrac = volthesis.effectiveness(wargo.getPrimaryType());
        double wargoDamagePrimary = wargo.damageDone(volthesis, wargo.getPrimaryType(), volthFrac);
        volthFrac = volthesis.effectiveness(wargo.getSecondaryType());
        double wargoDamageSecondary = wargo.damageDone(volthesis, wargo.getSecondaryType(), volthFrac);

        if (wargoDamagePrimary > wargoDamageSecondary)
        {
            wargoDamage = wargoDamagePrimary;
        } else
        {
            wargoDamage = wargoDamageSecondary;
        }

        if (n == 1)
        {
            // if int n is a 1 then we are using the primary stat
            wargoFrac = wargo.effectiveness(volthesis.getPrimaryType());
            volthDamage = volthesis.damageDone(wargo, volthesis.getPrimaryType(), wargoFrac);
        }
        else
        {
            // if int n is a 2 then we are using the secondary stat
            wargoFrac = wargo.effectiveness(volthesis.getSecondaryType());
            volthDamage = volthesis.damageDone(wargo, volthesis.getSecondaryType(), wargoFrac);
        }

        if (volthesis.getSpeed() > wargo.getSpeed())
        {
            wargo.takeDamageEnemy(volthDamage, volthesis);
            volthesis.takeDamage(wargoDamage, wargo);
        }
        else
        {
            volthesis.takeDamage(wargoDamage, wargo);
            wargo.takeDamageEnemy(volthDamage, volthesis);
        }
    }

    // if the player wants to battle then goes to the move selection scene
    public void moveSelection()
    {
        SceneManager.LoadScene("MoveSelection");
    }

    // if the player wants to battle wargo then goes to the wargo scene
    public void moveSelectionWargo()
    {
        SceneManager.LoadScene("MoveSelectionWargo");
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
        weaknessWargo.Add("fairy"); //4x weakness
        weaknessWargo.Add("dragon");
    }

    // method to add wargos resistance
    public void addResistanceWargo()
    {
        resistanceWargo.Add("fire 2");
        resistanceWargo.Add("water 2");
        resistanceWargo.Add("steel");
    }

    // method to catch pokemon with the name of the pokemon given as the parameter
    public void catchPokemon(Text name) 
    {
        string nameOfPokemon = name.text;
        if (nameOfPokemon.Equals("Wargo"))
        {
            PokemonParty.add(wargo);
        } else
        {
            PokemonParty.add(mossamr);
        }
        runAway();
    }
}
