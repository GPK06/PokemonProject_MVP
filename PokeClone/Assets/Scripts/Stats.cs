using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats : MonoBehaviour
{

    [SerializeField]
    private int baseStat;

    public int getBaseStat()
    {
        return baseStat;
    }
}
