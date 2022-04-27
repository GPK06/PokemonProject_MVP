using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleOptions : MonoBehaviour
{
    MossamrStats mossamr;
    VolthesisStats volthesis;

    public BattleOptions()
    {
        mossamr = new MossamrStats();
        volthesis = new VolthesisStats();
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
            mossDamage = mossamr.damageDone(volthesis, stat);
            volthDamage = volthesis.damageDone(mossamr, mossamr.getPrimaryType());
        }
        else
        {
            // if int n is a 2 then we are using the secondary stat
            mossDamage = mossamr.damageDone(volthesis, stat);
            volthDamage = volthesis.damageDone(mossamr, volthesis.getSecondaryType());
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

    public void moveSelection()
    {
        SceneManager.LoadScene("MoveSelection");
    }
}
