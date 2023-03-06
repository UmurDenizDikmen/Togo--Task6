using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour,IDragHandler
{
    public Transform CurrentPosition;
    private float MouseSense = 175f;


    public void OnDrag(PointerEventData eventData)
    {
        Vector3 CurrentVector = CurrentPosition.position;
        CurrentVector.x = Mathf.Clamp(CurrentVector.x+(eventData.delta.x / MouseSense), -3.5f, 3.5f);
        CurrentPosition.position = new Vector3(CurrentVector.x, CurrentPosition.position.y, CurrentPosition.position.z);
    }

    
}
