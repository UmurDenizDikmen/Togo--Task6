using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static float speed = 2f;
    public static Movement instance;
    
    private void Start()
    {
        instance = this;
    }
    void Update()
    {
      transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }
   

}
