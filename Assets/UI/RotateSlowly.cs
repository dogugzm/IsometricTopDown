using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class RotateSlowly : MonoBehaviour
{   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateSlow();
    }

    public void RotateSlow(){
        
        transform.GetComponent<RectTransform>().DORotate(new Vector3(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z+50),10f);

    }
}
