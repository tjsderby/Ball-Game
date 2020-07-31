using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMaster : MonoBehaviour
{
    private GameMaster gm;
    public Rigidbody player;
    public Rigidbody rsngPltfrm1;
    public Rigidbody rsngPltfrm2;
    public Rigidbody rsngPltfrm3;
    public float jumpHeight = 150f;
    public float ballSpeed = 350f;
    public float fwdSpeed = 320f;
    public Text bestTimeText;
    bool isGrounded = true;
    bool downhillControl = false;
    public AudioClip otherClip;

    // start is called before the first frame update
    void Start()
    {
        // setting a game master variable to interact with persisted data
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        // sets postion of player to the last checkpoint location
        transform.position = gm.lastCheckPointPos;
    }

    void Update()
    {
        // checks if the loop audio has stopped
        if (!GetComponent<AudioSource>().isPlaying)
        {
            // destroys the barrier in the downhill section
            Destroy(GameObject.Find("Barrier1"));
            // plays the drop music
            GetComponent<AudioSource>().clip = otherClip;
            GetComponent<AudioSource>().Play();
        }
    }

    // update is called once per frame
    void FixedUpdate()
    {
        // checks if the timer is running and if the user is in the downhill section or not
        if (downhillControl && gm.running)
        {
            player.AddForce(0, 0, fwdSpeed * Time.deltaTime);

            if (Input.GetKey("d"))
            {
                player.AddForce(ballSpeed * Time.deltaTime, 0, 0);
            } 

            if (Input.GetKey("a"))
            {
                player.AddForce(-ballSpeed * Time.deltaTime, 0, 0);
            } 

        } else if (!downhillControl && gm.running)
        {
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                player.AddForce(Vector3.up * jumpHeight * Time.deltaTime, ForceMode.Impulse);
            }

            if (Input.GetKey("w") && isGrounded)
            {
                player.AddForce(Camera.main.transform.forward * ballSpeed * Time.deltaTime);
            }

            if (Input.GetKey("s") && isGrounded)
            {
                player.AddForce(-Camera.main.transform.forward * ballSpeed * Time.deltaTime);
            } 

            if (Input.GetKey("a") && isGrounded)
            {
                player.AddForce(-Camera.main.transform.right * ballSpeed * Time.deltaTime);
            } 
            
            if (Input.GetKey("d") && isGrounded)
            {
                player.AddForce(Camera.main.transform.right * ballSpeed * Time.deltaTime);
            }
        }

        // reset to last checkpoint and reset variables if the finish is reached
        if (Input.GetKey("r"))
        {
            if (!gm.running)
            {
                gm.running = true;
                gm.startTime = Time.time;
                gm.lastCheckPointPos = new Vector3(0f, 1f, -2f);
            }
            SceneManager.LoadScene("Lvl1");
        }
    }

    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.tag == "floor")
        {
            // allows jumping
            isGrounded = true;
        }
        if (theCollision.gameObject.tag == "Finish")
        {
            // updates best time and displays it to the user
            gm.running = false;
            gm.setTime();
            bestTimeText = GameObject.Find("bestTimeText").GetComponent<Text>();
            float hours = (PlayerPrefs.GetInt("BestTime", 10000)) / 3600;
            float minutes = ((PlayerPrefs.GetInt("BestTime", 10000)) % 3600) / 60;
            float seconds = ((PlayerPrefs.GetInt("BestTime", 10000)) % 3600 ) % 60;
            bestTimeText.text = "Best Time: " + (int) hours + ":" + (int) minutes + ":" + (int) seconds;
        }
        if (theCollision.gameObject.tag == "checkpoint1")
        {
            isGrounded = true;
            // sets checkpoint
            if (Physics.Raycast(transform.position, Vector3.down, 1))
            {
                gm.lastCheckPointPos = new Vector3(14.8f, 11.605f, 55.594f);
            }
        }
        if (theCollision.gameObject.tag == "checkpoint2")
        {
            isGrounded = true;
            // sets checkpoint
            if (Physics.Raycast(transform.position, Vector3.down, 1))
            {
                gm.lastCheckPointPos = new Vector3(-11.16f, 13.057f, 102.16f);
            }
        }
        if (theCollision.gameObject.tag == "checkpoint3")
        {
            isGrounded = true;
            // sets checkpoint
            if (Physics.Raycast(transform.position, Vector3.down, 1))
            {
                gm.lastCheckPointPos = new Vector3(-11.1f, 40.39f, 179.88f);
            }
        }
        if (theCollision.gameObject.tag == "rising1")
        {
            isGrounded = true;
            // waits and adds force to the rising platforms
            StartCoroutine(Rise());
        }
        if (theCollision.gameObject.tag == "rising2")
        {
            isGrounded = true;
            // waits and adds force to the rising platforms
            StartCoroutine(Rise2());
        }
        if (theCollision.gameObject.tag == "rising3")
        {
            isGrounded = true;
            // waits and adds force to the rising platforms
            StartCoroutine(Rise3());
        }
        if (theCollision.gameObject.tag == "timing")
        {
            downhillControl = true;
            // stops loop ready for the drop track to play
            StartCoroutine(waitDownhill());
        }
        if (theCollision.gameObject.tag == "obstacle")
        {
            // resets the level if a player hits an obstacle
            SceneManager.LoadScene("Lvl1");
        }
    }

    void OnCollisionExit(Collision theCollision)
    {
        // stops a user from jumping when in the air
        if (theCollision.gameObject.tag == "floor")
        {
            isGrounded = false;
        }
        if (theCollision.gameObject.tag == "checkpoint1")
        {
            isGrounded = false;
        }
        if (theCollision.gameObject.tag == "checkpoint2")
        {
            isGrounded = false;
        }
        if (theCollision.gameObject.tag == "checkpoint3")
        {
            isGrounded = false;
        }
        if (theCollision.gameObject.tag == "rising1")
        {
            isGrounded = false;
        }
        if (theCollision.gameObject.tag == "rising2")
        {
            isGrounded = false;
        }
        if (theCollision.gameObject.tag == "rising3")
        {
            isGrounded = false;
        }
    }

    IEnumerator Rise()
    {
        // waits and adds force to the rising platforms
        yield return new WaitForSeconds(0.6f);
        rsngPltfrm1.useGravity = true;
        rsngPltfrm1.AddForce(Vector3.up * 2000);
    }

    IEnumerator Rise2()
    {
        // waits and adds force to the rising platforms
        yield return new WaitForSeconds(0.8f);
        rsngPltfrm2.useGravity = true;
        rsngPltfrm2.AddForce(Vector3.up * 2000);
    }

    IEnumerator Rise3()
    {
        // waits and adds force to the rising platforms
        yield return new WaitForSeconds(0.8f);
        rsngPltfrm3.useGravity = true;
        rsngPltfrm3.AddForce(Vector3.up * 2500);
    }

    IEnumerator waitDownhill()
    {
        // stops loop ready for the drop track to play
        yield return new WaitForSeconds(5f);
        GetComponent<AudioSource>().loop = false;
    }
}
