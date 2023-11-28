using CooldownAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public abstract class SkillActionBase : AtomAction, IDisposable
{
    public float cooldownTime = 5f;
    public float currentTimeForCooldown => cooldown.Timer;
    public Cooldown cooldown;

    protected MovementController movement;

    

    public void Init(MovementController movement)
    {
        this.movement = movement;
        cooldown = new Cooldown(cooldownTime);

    }

    public abstract void OnKeyDown();
    public abstract void OnKeyUp();
    public abstract void OnKeyHold();

    public virtual void Dispose()
    {
        movement = null;
        // Reset cache
        cooldown = null;
    }
}
