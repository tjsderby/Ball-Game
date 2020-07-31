using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverRight : MonoBehaviour
{
    public float speed = 15.0f;
    
    // start is called before the first frame update
    void Start()
    {
        // adds a force to the object on spawn
        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = -transform.right * speed;
    }
}
