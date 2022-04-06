using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleOptions : MonoBehaviour
{
    public void RunAway()
    {
        SceneManager.LoadScene("Route 1");
    }
}
