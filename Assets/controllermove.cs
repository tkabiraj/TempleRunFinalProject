using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class controllermove : MonoBehaviour
{
    public CharacterController controller;
    public TextMesh scoreText;
    public SteamVR_Input_Sources handLeft;
    //public SteamVR_Input_Sources handRight;
    public SteamVR_Action_Vector2 trackrot;
    public float speed = 10.0f;
    public float horizontalMulitplier = 0.01f;
    bool alive = true;
    Vector2 camrot;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!alive) return;
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * camrot.x * speed * Time.fixedDeltaTime * horizontalMulitplier;
        Vector3 FG = new Vector3(0, -0.09f, 0);
        controller.Move(forwardMove + horizontalMove + FG);
    }

    // Update is called once per frame
    void Update()
    {
        camrot = trackrot.GetAxis(handLeft);
        RaycastHit hit;
        Vector3 tempPos = controller.transform.position;
        tempPos.y = 1.0f;
        if (Physics.Raycast(tempPos, controller.transform.forward, out hit, 1))
        {
            //Debug.Log(hit.transform.name);
            if (hit.transform.name == "Obstacle(Clone)")
            {
                Die();
                //Debug.Log("ded");
            //    moving = false;
            //    status.GetComponent<Renderer>().enabled = true;
            }
            if (hit.transform.name == "Coin(Clone)")
            {
                scoreText.text = "Score: " + ++score;
                Destroy(hit.transform.gameObject);
                speed += 0.5f;
                //Die();
                //Debug.Log("coin");
                //    moving = false;
                //    status.GetComponent<Renderer>().enabled = true;
            }
        }
        if (transform.position.y < -10)
            Die();
    }

    int seconds = 500;
    public void Die()
    {
        alive = false;
        seconds--;
        scoreText.text = "Score: " + score + "\nGame Over\nRestarting in " + seconds/100 + " secs";
        Invoke("Restart", 5);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
