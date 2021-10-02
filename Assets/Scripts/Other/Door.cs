using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
   

    public void OpenDoor()
    {
        transform.DOMoveY(transform.position.y-2,0.8f); 
    }
    public void CloseDoor()
    {
        transform.DOMoveY(transform.position.y+2,Random.Range(1.5f,3.2f)); 
    }
}
