using System;
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
    [SerializeField] private string name;
    [SerializeField] private UnityEvent effects;
    
    [SerializeField] private PowerupFinalizeMode finalizeMode = PowerupFinalizeMode.Instant;
    
    [SerializeField] private float duration = 0.0f;
    [SerializeField] private PowerupEndCondition[] endConditions;

    public void Grant()
    {
        effects.Invoke();
    }
}