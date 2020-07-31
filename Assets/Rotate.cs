using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        // rotates the object side to side
        transform.rotation = Quaternion.Euler(0f, 0f, 30f * Mathf.Sin(Time.time * 2f));
    }
}
