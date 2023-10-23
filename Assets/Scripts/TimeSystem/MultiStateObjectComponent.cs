using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiStateObjectComponent : MonoBehaviour
{
    public bool isInteractable = false;
    
    // called when current global time state is changed
    public abstract void OnTimeStateChange(TimeState newTimeState);
    
    // called when player attempts to interact with this object
    public abstract void OnInteract();
    
    // if this component is getting destroyed, remove it from time manager
    private void OnDestroy()
    {
        if (TimeManager.Instance)
        {
            TimeManager.Instance.RemoveMultiStateObject(this);
        }
        
        // in case that this object is enemy, remove it from enemy tracker as well
        if (GameManager.Instance)
        {
            MultiStateEnemy mov = gameObject.GetComponent<MultiStateEnemy>();
            if (mov)
            {
                GameManager.Instance.RemoveEnemyFromList(mov);
            }
        }
    }
}
