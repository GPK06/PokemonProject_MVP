using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleOptions : MonoBehaviour
{
    PokemonStats mossamr;
    PokemonStats volthesis;
    PokemonStats wargo;

    HashSet<string> weaknessMossamr = new HashSet<string>();
    HashSet<string> resistanceMossamr = new HashSet<string>();
    HashSet<string> immunityMossamr = new HashSet<string>();

    HashSet<string> weaknessVolthesis = new HashSet<string>();
    HashSet<string> resistanceVolthesis = new HashSet<string>();
    HashSet<string> immunityVolthesis = new HashSet<string>();

    HashSet<string> weaknessWargo = new HashSet<string>();
    HashSet<string> resistanceWargo = new HashSet<string>();

    public BattleOptions()
    {
        addWeaknessMossamr();
        addWeaknessVolthesis();
        addWeaknessWargo();

        addResistanceWargo();
        addResistanceVolthesis();
        addResistanceMossamr();

        addImmunityVolthesis();
        addImmunityMossamr();

        //public PokemonStats(string name, string primary, string secondary, int health, int speed, int attack, int spAttack, int defense, int spDefense, HashSet<string> weaknessP, HashSet<string> resistanceP, HashSet<string> immunityP)
        mossamr = new PokemonStats("Mossamr", "grass", "steel", 78, 75, 120, 41, 136, 80, weaknessMossamr, resistanceMossamr, immunityMossamr);
        volthesis = new PokemonStats("Volthesis", "fire", "fairy", 110, 21, 55, 143, 129, 72, weaknessVolthesis, resistanceVolthesis, immunityVolthesis);
        wargo = new PokemonStats("Wargo", "water", "dragon", 54, 88, 104, 104, 90, 90, weaknessWargo, resistanceWargo, null);
    }

    public void runAway()
    {
        SceneManager.LoadScene("Route 1");
    }

    public void catchPokemon()
    {
        Text name = transform.Find("EnemyName").GetComponent<Text>();
        if (name.text == "Mossamr")
        {
            PokemonParty.add(mossamr);
        } else
        {
            PokemonParty.add(wargo);
        }
    }

    public void battle(int n)
    {
        Text totalHealth;
        totalHealth = transform.Find("TotalHealth").GetComponent<Text>();
        totalHealth.text = volthesis.getHealth() + "";

        Text totalHealthMossamr;
        totalHealthMossamr = transform.Find("TotalHealthMossamr").GetComponent<Text>();
        totalHealthMossamr.text = mossamr.getHealth() + "";

        string stat;

        double mossamrDamage;
        double volthDamage;
        double mossamrFrac;
        double volthFrac;

        volthFrac = volthesis.effectiveness(mossamr.getPrimaryType());
        double mossamrDamagePrimary = wargo.damageDone(volthesis, mossamr.getPrimaryType(), volthFrac);
        volthFrac = volthesis.effectiveness(mossamr.getSecondaryType());
        double mossamrDamageSecondary = wargo.damageDone(volthesis, mossamr.getSecondaryType(), volthFrac);

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

        if (volthesis.getSpeed() > wargo.getSpeed())
        {
            mossamr.takeDamage(volthDamage, volthesis);
            volthesis.takeDamage(mossamrDamage, mossamr);
        }
        else
        {
            volthesis.takeDamage(mossamrDamage, mossamr);
            mossamr.takeDamage(volthDamage, volthesis);
        }
    }

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
            wargo.takeDamage(volthDamage, volthesis);
            volthesis.takeDamage(wargoDamage, wargo);
        }
        else
        {
            volthesis.takeDamage(wargoDamage, wargo);
            wargo.takeDamage(volthDamage, volthesis);
        }
    }

    public void moveSelection()
    {
        SceneManager.LoadScene("MoveSelection");
    }

    public void moveSelectionWargo()
    {
        SceneManager.LoadScene("MoveSelectionWargo");
    }

    public void addWeaknessMossamr()
    {
        weaknessMossamr.Add("fire 2"); //4x weakness
        weaknessMossamr.Add("fighting");
    }

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

    public void addImmunityMossamr()
    {
        immunityMossamr.Add("poison");
    }

    public void addWeaknessVolthesis()
    {
        weaknessVolthesis.Add("water");
        weaknessVolthesis.Add("ground");
        weaknessVolthesis.Add("poison");
        weaknessVolthesis.Add("rock");
    }

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

    public void addImmunityVolthesis()
    {
        immunityVolthesis.Add("dragon");
    }

    public void addWeaknessWargo()
    {
        weaknessWargo.Add("fairy"); //4x weakness
        weaknessWargo.Add("dragon");
    }

    public void addResistanceWargo()
    {
        resistanceWargo.Add("fire 2");
        resistanceWargo.Add("water 2");
        resistanceWargo.Add("steel");
    }

    public void catchPokemon(Text name) 
    {
        Debug.Log("I am running");
        string nameOfPokemon = name.text;
        if (nameOfPokemon.Equals("Wargo"))
        {
            PokemonParty.add(wargo);
        } else
        {
            Debug.Log("I am being added");
            PokemonParty.add(mossamr);
        }
        runAway();
    }
}
