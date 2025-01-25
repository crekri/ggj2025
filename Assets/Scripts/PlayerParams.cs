using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerStat
{   
    //Movement
    public float moveVelocity;
    public float jumpVelocity;
    public float runDamp;
    //Jump
    public int numberOfJump;
    public float jumpHoldTime;
    public float jumpHoldStrengthRate;
    public float airBorneGravityIncreaseRate;
    public float gravity;

    public PlayerStat(
        float moveVelocity = 15.0f,
        float jumpVelocity = 15.0f,
        float runDamp = 0f,
        int numberOfJump = 1,
        float jumpHoldTime = 1.0f,
        float jumpHoldStrengthRate = 0.05f,
        float airBorneGravityIncreaseRate = 0.2f,
        float gravity = 0.3f
    )
    {
        this.moveVelocity = moveVelocity;
        this.jumpVelocity = jumpVelocity;
        this.runDamp = runDamp;
        this.numberOfJump = numberOfJump;
        this.jumpHoldTime = jumpHoldTime;
        this.jumpHoldStrengthRate = jumpHoldStrengthRate;
        this.airBorneGravityIncreaseRate = airBorneGravityIncreaseRate;
        this.gravity = gravity;
    }
}
//Store datas
public class PlayerParams : MonoBehaviour
{
    public PlayerStat baseStat;

    public PlayerStat Stat { get; private set; }

    private HashSet<Modifier> _modifiers;

    private void Awake()
    {
        Stat = baseStat;
    }

    private void RefreshStat()
    {
        Stat = baseStat;

        foreach (var mod in _modifiers)
        {
            Stat = mod.ApplyTo(Stat);
        }
    }
    public void AddModifer(Modifier mod)
    {
        _modifiers.Add(mod);
        RefreshStat();
    }

    public void RemoveModifer(Modifier mod)
    {
        _modifiers.Remove(mod);
        RefreshStat();
    }

}

public abstract class Modifier
{
    public abstract PlayerStat ApplyTo(PlayerStat stat);
    
}

public class ModifierSlow : Modifier
{

    public override PlayerStat ApplyTo(PlayerStat stat)
    {
        stat.moveVelocity -= stat.moveVelocity/2;
           
        return stat;
    }
}


