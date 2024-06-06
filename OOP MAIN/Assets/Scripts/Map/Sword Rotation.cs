using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRotation : MonoBehaviour
{
    public Transform swordHandle;

    public float rotateSpeed;

    void Start()
    {
        
    }

    
    void Update()
    {
        swordHandle.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
