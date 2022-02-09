using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayEventListener : MonoBehaviour
{
    [SerializeField] private GameplayEvent gameplayEvent;
    [SerializeField] private UnityEvent unityEvent;

    private void Awake()
    {
        gameplayEvent.Register(this);
    }

    private void OnDestroy()
    {
        gameplayEvent.Unregister(this);
    }

    public void RaiseEvent()
    {
        unityEvent.Invoke();
    }
}
