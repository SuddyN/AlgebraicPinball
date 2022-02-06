using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PowerupEndCondition
{
    [SerializeField] private GameplayEvent trigger;
    [SerializeField] private int count;
}

public enum PowerupFinalizeMode
{
    Instant,
    Timed,
    Event,
    Persistent
}

[CreateAssetMenu(fileName = "New Power Up", menuName = "Pinball/Power Up", order = 1)]
public class Powerup : ScriptableObject
{
    
    private HashSet<PowerupListener> listeners = new HashSet<PowerupListener>();
    
    [SerializeField] private PowerupFinalizeMode finalizeMode = PowerupFinalizeMode.Instant;
    
    [SerializeField] private float duration = 0.0f;
    [SerializeField] private PowerupEndCondition[] endConditions;

    public PowerupFinalizeMode Mode => finalizeMode;
    public float Duration => duration;
    public PowerupEndCondition[] EndConditions => endConditions;
    
    public void Register(PowerupListener listener)
    {
        listeners.Add(listener);
    }
        
    public void Unregister(PowerupListener listener)
    {
        listeners.Remove(listener);
    }

    public void Grant()
    {
        foreach (PowerupListener listener in listeners)
        {
            listener.NotifyActive();
        }
    }
}