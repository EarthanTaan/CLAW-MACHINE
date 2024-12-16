using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class clawMovement : MonoBehaviour
{
    private float timer = 0.0f;
    Rigidbody rb;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    int inExampleFunction(int myInt)
    {

        return 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(0, 0, 1f, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(0, 0, -1f, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-1, 0, 0f, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(1, 0, 0f, ForceMode.Impulse);
        }


  //
        
    }
}

