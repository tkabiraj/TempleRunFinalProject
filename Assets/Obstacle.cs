using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    controllermove controllerMove;
    // Start is called before the first frame update
    void Start()
    {
        controllerMove = GameObject.FindObjectOfType<controllermove>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.name);
        if (collision.gameObject.name == "playercharacter")
            controllerMove.Die();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
