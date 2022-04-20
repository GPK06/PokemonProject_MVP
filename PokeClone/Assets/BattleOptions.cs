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
        // if int n is a 1 then we are using the primary stat
        // if int n is a 2 then we are using the secondary stat

        if (volthesis.getSpeed() > mossamr.getSpeed())
        {
            mossamr.takeDamage(25, volthesis);
            volthesis.takeDamage(25, mossamr);
        }
        else
        {
            volthesis.takeDamage(25, mossamr);
            mossamr.takeDamage(25, volthesis);
        }
    }

    public void moveSelection()
    {
        SceneManager.LoadScene("MoveSelection");
    }
}
