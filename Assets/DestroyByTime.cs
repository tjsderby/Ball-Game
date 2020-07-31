using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    // start is called before the first frame update
    void Start()
    {
        // destorys the object after a certain 2 seconds
        Destroy(gameObject, 2);
    }
}
