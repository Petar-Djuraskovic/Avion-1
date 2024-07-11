using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorderWarp : MonoBehaviour
{
    public bool xWarping = true;
    public bool yWarping = true;

    public const float xMax = 8.5f;
    public const float yMax = 5f;

    [Header("Control Target")]
    public Rigidbody2D Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);

        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(-xMax, transform.position.y, transform.position.z);
            Debug.Log("Teleported to back");
        }

        if (transform.position.x < -xMax)
        {
            transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
            Debug.Log("Teleported to front");
        }

        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, -yMax, transform.position.z);
            Debug.Log("Teleported down");
        }

        if (transform.position.y < -yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
            Debug.Log("Teleported up");
        }
    }
}
