using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleOptions : MonoBehaviour
{
    MossamrStats mossamr;
    VolthesisStats volthesis;
    WargoStats wargo;


    public BattleOptions()
    {
        mossamr = new MossamrStats();
        volthesis = new VolthesisStats();
        wargo = new WargoStats();
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
}
