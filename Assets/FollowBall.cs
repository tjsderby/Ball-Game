using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Transform Ball;

    // update is called once per frame
    void LateUpdate()
    {
        // the target the camera follows is always set to the position of the ball 
        transform.position = Ball.position;
    }
}
