using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Gameplay Event", menuName = "Pinball/Gameplay Event", order = 2)]
public class GameplayEvent : ScriptableObject
{
    private HashSet<GameplayEventListener> _listeners = new HashSet<GameplayEventListener>();

    public void Register(GameplayEventListener listener)
    {
        _listeners.Add(listener);
    }
        
    public void Unregister(GameplayEventListener listener)
    {
        _listeners.Remove(listener);
    }

    public void Invoke()
    {
        foreach (GameplayEventListener listener in _listeners)
        {
            listener.RaiseEvent();
        }
    }
}