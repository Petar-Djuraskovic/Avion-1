using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atan2HomingComputer : MonoBehaviour
{

    public Transform TargetTransform;
    public float angle = 0;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 relative = transform.InverseTransformPoint(TargetTransform.position);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        // transform.Rotate(0, 0, 90 - angle);
        // Debug.Log(TargetTransform.position.ToString() + ' ' + relative + ' ' + angle + ' ' + transform.position.ToString());
        
    }
}
