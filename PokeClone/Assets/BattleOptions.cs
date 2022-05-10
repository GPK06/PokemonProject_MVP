using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleOptions : MonoBehaviour
{
    PokemonStats mossamr;
    PokemonStats volthesis;
    PokemonStats wargo;


    public BattleOptions()
    {
        HashSet<string> weaknessMossamr = new HashSet<string>();
        HashSet<string> resistanceMossamr = new HashSet<string>();
        HashSet<string> immunityMossamr = new HashSet<string>();
        
        HashSet<string> weaknessVolthesis = new HashSet<string>();
        HashSet<string> resistanceVolthesis = new HashSet<string>();
        HashSet<string> immunityVolthesis = new HashSet<string>();
       
        HashSet<string> weaknessWargo = new HashSet<string>();
        HashSet<string> resistanceWargo = new HashSet<string>();

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

    public void battle(int n)
    {
        int num = Random.Range(0, 2);
        string stat;
        if (num == 0)
        {
            stat = mossamr.getPrimaryType();
        } else
        {
            stat = mossamr.getSecondaryType();
        }

        double mossDamage;
        double volthDamage;
        if (n == 1)
        {
            // if int n is a 1 then we are using the primary stat
            double volthFrac = volthesis.effectiveness(stat);
            double mossFrac = mossamr.effectiveness(volthesis.getPrimaryType());
            mossDamage = mossamr.damageDone(volthesis, stat, volthFrac);
            volthDamage = volthesis.damageDone(mossamr, volthesis.getPrimaryType(), mossFrac);
        }
        else
        {
            // if int n is a 2 then we are using the secondary stat
            double volthFrac = volthesis.effectiveness(stat);
            double mossFrac = mossamr.effectiveness(volthesis.getSecondaryType());
            mossDamage = mossamr.damageDone(volthesis, stat, volthFrac);
            volthDamage = volthesis.damageDone(mossamr, volthesis.getSecondaryType(), mossFrac);
        }

        if (volthesis.getSpeed() > mossamr.getSpeed())
        {
            mossamr.takeDamage(volthDamage, volthesis);
            volthesis.takeDamage(mossDamage, mossamr);
        }
        else
        {
            volthesis.takeDamage(mossDamage, mossamr);
            mossamr.takeDamage(volthDamage, volthesis);
        }
    }

    public void battleWargo(int n)
    {
        int num = Random.Range(0, 2);
        string stat;
        if (num == 0)
        {
            stat = wargo.getPrimaryType();
        }
        else
        {
            stat = wargo.getSecondaryType();
        }

        double wargoDamage;
        double volthDamage;
        double wargoFrac;
        double volthFrac;

        if (n == 1)
        {
            // if int n is a 1 then we are using the primary stat
            volthFrac = volthesis.effectiveness(stat);
            wargoFrac = wargo.effectiveness(volthesis.getPrimaryType());
            wargoDamage = wargo.damageDone(volthesis, stat, volthFrac);
            volthDamage = volthesis.damageDoneWargo(wargo, volthesis.getPrimaryType(), wargoFrac);
        }
        else
        {
            // if int n is a 2 then we are using the secondary stat
            volthFrac = volthesis.effectiveness(stat);
            wargoFrac = wargo.effectiveness(volthesis.getSecondaryType());
            wargoDamage = wargo.damageDone(volthesis, stat, volthFrac);
            volthDamage = volthesis.damageDoneWargo(wargo, volthesis.getSecondaryType(), wargoFrac);
        }

        if (volthesis.getSpeed() > wargo.getSpeed())
        {
            wargo.takeDamage(volthDamage, volthesis);
            volthesis.takeDamageWargo(wargoDamage, wargo);
        }
        else
        {
            volthesis.takeDamageWargo(wargoDamage, wargo);
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
        weakness.Add("fire 2"); //4x weakness
        weakness.Add("fighting");
    }

    public void addResistanceMossamr()
    {
        resistance.Add("normal");
        resistance.Add("grass 2"); // 4x weakness
        resistance.Add("water");
        resistance.Add("electric");
        resistance.Add("psychic");
        resistance.Add("rock");
        resistance.Add("dragon");
        resistance.Add("steel");
        resistance.Add("fairy");
    }

    public void addImmunityMossamr()
    {
        immunity.Add("poison");
    }

    public void addWeaknessVolthesis()
    {
        weakness.Add("water");
        weakness.Add("ground");
        weakness.Add("poison");
        weakness.Add("rock");
    }

    public void addResistanceVolthesis()
    {
        resistance.Add("fire");
        resistance.Add("grass");
        resistance.Add("ice");
        resistance.Add("fighting");
        resistance.Add("fairy");
        resistance.Add("bug 2"); // 4x resistance
        resistance.Add("dark");
    }

    public void addImmunityVolthesis()
    {
        immunity.Add("dragon");
    }

    public void addWeaknessWargo()
    {
        weakness.Add("fairy"); //4x weakness
        weakness.Add("dragon");
    }

    public void addResistanceWargo()
    {
        resistance.Add("fire 2");
        resistance.Add("water 2");
        resistance.Add("steel");
    }
}
