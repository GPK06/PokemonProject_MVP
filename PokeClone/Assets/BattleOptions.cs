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

    public void battle()
    {
        mossamr.takeDamage(25);
        volthesis.takeDamage(25, mossamr);
    }
}