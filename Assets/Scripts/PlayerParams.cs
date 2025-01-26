using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    public float trapTimerMult; 
    public float reduceTrapPerClick;

    public float baseStunTime;
    public float knockBackMult;
}



//Store datas
public class PlayerParams : MonoBehaviour
{
    public PlayerStat baseStat;

    public PlayerStat Stat { get; private set; }

    private HashSet<IParamModifier> _modifiers;
    private void Awake()
    {
        Stat = baseStat;
        _modifiers = new HashSet<IParamModifier>();                       
    }

    public void Update()
    {
        RefreshStat();
    }

    private void RefreshStat()
    {
        Stat = baseStat;

        foreach (var mod in _modifiers)
        {
            Stat = mod.ApplyTo(Stat);
        }
    }
    public void AddModifer(IParamModifier mod)
    {
        _modifiers.Add(mod);
    }
    
    
    
    public void RemoveModifer(IParamModifier mod)
    {
        _modifiers.Remove(mod);
    }
    

}

public interface  IParamModifier
{
    public PlayerStat ApplyTo(PlayerStat stat);
    
}

public class ModifierSlow : IParamModifier
{
    
    public PlayerStat ApplyTo(PlayerStat stat)
    {
        stat.moveVelocity -= stat.moveVelocity/2;
           
        return stat;
    }
    
}

public class ModifierStun : IParamModifier
{

    public PlayerStat ApplyTo(PlayerStat stat)
    {
        stat.moveVelocity = 0;
        stat.jumpVelocity = 0;
        return stat;
    }
    
}

public class ModifierStick : IParamModifier
{
    public PlayerStat ApplyTo(PlayerStat stat)
    {
        stat.moveVelocity -= stat.moveVelocity/2;
        stat.jumpVelocity -= stat.jumpVelocity/2;
        return stat;
    }
    
}


public class ModifierDoubleJump : IParamModifier
{
    public PlayerStat ApplyTo(PlayerStat stat)
    {
        stat.numberOfJump ++ ;
        return stat;
    }
}



