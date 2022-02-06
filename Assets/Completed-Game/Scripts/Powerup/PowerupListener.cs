using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerupListener : MonoBehaviour
{

    [SerializeField] private Powerup powerup;
    [SerializeField] private UnityEvent onActivate;
    [SerializeField] private UnityEvent onDeactivate;
    
    private IEnumerator deactivateCoroutine = null;
    
    public bool IsActive { get; private set; }

    protected void Awake()
    {
        if (powerup != null) powerup.Register(this);
    }

    protected void OnDestroy()
    {
        if (powerup != null) powerup.Unregister(this);
    }

    public void NotifyActive()
    {
        onActivate.Invoke();
        IsActive = true;

        switch (powerup.Mode)
        {
            case PowerupFinalizeMode.Instant:
                NotifyInactive();
                break;
            case PowerupFinalizeMode.Persistent:
                break;
            case PowerupFinalizeMode.Timed:
                deactivateCoroutine = Utility.DelayedFunction(this, powerup.Duration, NotifyInactive);
                break;
        }
    }

    private void NotifyInactive()
    {
        onDeactivate.Invoke();
        IsActive = false;
    }
    
    public void Reset()
    {
        if (deactivateCoroutine != null)
        {
            StopCoroutine(deactivateCoroutine);
            deactivateCoroutine = null;
        }
    }
}
