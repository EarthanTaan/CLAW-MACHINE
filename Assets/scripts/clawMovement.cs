using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class clawMovement : MonoBehaviour
{
    private float timer = 0.0f;         //what was this for?     -E
    Rigidbody rb;                       //a convenient handle for the armature's Rigidbody component. (Needs to be assigned in Start() )    -E
    public float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();     //  Assigning the Rigidbody to the handle for ease of use in Update()   -E
        
    }
  

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;        // Each frame, the <timer>'s value increases by the fraction of a second that has passed between frame renders. (This might be redundant with FixedUpdate(), but I'm not sure)

        // Movement controls for the claw-machine's armature.
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed);
        }

    }
}

