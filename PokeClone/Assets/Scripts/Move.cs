using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private int basePower;
    private string attackStatBeingUsed;
    private string type;

    public Move(int basePower, string attackStatBeingUsed, string type)
    {
        this.basePower = basePower;
        this.attackStatBeingUsed = attackStatBeingUsed;
        this.type = type;
    }

    public int getBasePower()
    {
        return this.basePower;
    }

    public string getAttackStat()
    {
        return this.attackStatBeingUsed;
    }

    public string getType()
    {
        return this.type;
    }
}
