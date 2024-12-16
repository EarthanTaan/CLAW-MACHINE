using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class clawMovement : MonoBehaviour
{
    private float timer = 0.0f;         //what was this for?     -E

    Rigidbody rb;                       //a convenient handle for the armature's Rigidbody component. (Needs to be assigned in Start() )    -E
    public InputActionMap ClawActions;
    

    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();     //  Assigning the Rigidbody to the handle for ease of use in Update()   -E
        ClawActions = GetComponent<InputActionMap>();
        ClawActions.Enable();
    }
  

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;        // Each frame, the <timer>'s value increases by the fraction of a second that has passed between frame renders. (This might be redundant with FixedUpdate(), but I'm not sure)

        /* What follows is physics-based movement code, but I don't know if we really want that.
         * We might prefer to translate the transform for absolute motion,
         * rather than using a force simulation to push it around.
         * I'm commenting it out instead of deleting it, just in case I've made a mistake and we end up wanting it back.
         */     // -E

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    rb.AddForce(0, 0, 1f, ForceMode.Impulse);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    rb.AddForce(0, 0, -1f, ForceMode.Impulse);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rb.AddForce(-1, 0, 0f, ForceMode.Impulse);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    rb.AddForce(1, 0, 0f, ForceMode.Impulse);
        //}

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right);
        }

    }
}

