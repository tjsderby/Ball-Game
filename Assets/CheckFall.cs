using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CheckFall : MonoBehaviour
{
    // update is called once per frame
    void Update()
    {
        // if the user falls out of the map the scene is reloaded at last checkpoint
        if (transform.position.y < -15)
        {
            SceneManager.LoadScene("Lvl1");
        }
    }
}
