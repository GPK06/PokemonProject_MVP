using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// move class that holds all of the information that has to do with a move
public class Move
{
    // all the different information for a move
    private int basePower;
    private string attackStatBeingUsed;
    private string type;

    // constructor to make the move
    public Move(int basePower, string attackStatBeingUsed, string type)
    {
        this.basePower = basePower;
        this.attackStatBeingUsed = attackStatBeingUsed;
        this.type = type;
    }

    // return the base power
    public int getBasePower()
    {
        return this.basePower;
    }


    // return the attack stat
    public string getAttackStat()
    {
        return this.attackStatBeingUsed;
    }

    // return the type
    public string getType()
    {
        return this.type;
    }
}
