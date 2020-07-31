using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public Transform spawnPosition1;
    public Transform spawnPosition2;
    public Transform spawnPosition3;
    public Transform spawnPosition4;
    public GameObject obstacleLeft;
    public GameObject obstacleRight;
    public float wait = 0.65f;

    // start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            // spawns the prefabs at 4 random locations infinitely
            int random = Random.Range(0, 4);
            if (random == 0)
            {
                Instantiate(obstacleLeft, spawnPosition1.position, spawnPosition1.rotation);
            } else if (random == 1)
            {
                Instantiate(obstacleRight, spawnPosition2.position, spawnPosition2.rotation);
            } else if (random == 2)
            {
                Instantiate(obstacleRight, spawnPosition3.position, spawnPosition3.rotation);
            } else
            {
                Instantiate(obstacleLeft, spawnPosition4.position, spawnPosition4.rotation);
            }
            yield return new WaitForSeconds(wait);
        }
    }
}
