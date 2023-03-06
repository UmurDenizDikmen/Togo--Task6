using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SmoothDamp : MonoBehaviour
{
    public Transform CurrentLeadTransform;
    private float CurrentVelocity = 0f;
    private float SmoothTime = .1f;
   

    private void Start()
    {
       
    }
    void Update()
    {
        if(CurrentLeadTransform == null)
        {
            return;
        }
        else
        {
            transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, CurrentLeadTransform.position.x, ref CurrentVelocity, SmoothTime), transform.position.y, transform.position.z);
        }
    }
    public void SetLeadTransform(Transform leadtransform)
    {
        leadtransform = CurrentLeadTransform;
    }
    
}
