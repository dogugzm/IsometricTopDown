using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;

    private void OnTriggerEnter(Collider other) {
        
        Player player = other.GetComponent<Player>();
        if (player!=null)
        {
            Debug.Log("Player inside trigger");
            OnPlayerEnterTrigger?.Invoke(this,EventArgs.Empty);
            
        }
        
    }
}
