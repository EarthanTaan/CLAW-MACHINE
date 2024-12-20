using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class clawMovement : MonoBehaviour
{
    // Establishing convenient handles for the GameObject's components. (They'll need to be assigned in Start() )    -E
    Rigidbody rb;                       
    CharacterJoint tether;
    Vector3 hiPt = new Vector3(0f, -0.5f, 0f); // "High Point" the top of the claw's vertical range      -E
    Vector3 lowPt = new Vector3(0f,-1f,0f);    // "Low Point" the bottom of the claw's vertical range   -E
    public float speed = 0.5f;
    private float timer = 0.0f;         // (what was this for?) (Maybe I can use it for the lower/raise function.)     -E
    [SerializeField] float wait = 2.5f;
    bool clawDown = false;                      // the claw is not down     -E
    

    // Start is called before the first frame update
    void Start()
    {
        //  Assigning components to our handlesfor ease of use in Update()   -E
        rb = GetComponent<Rigidbody>();
        tether = GameObject.Find("Claw").GetComponent<CharacterJoint>();    // Our movement controls are on the armature ('this' object), but the joint component is on the Claw.   -E
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;        // Each frame, the <timer>'s value increases by the fraction of a second that has passed between frame renders. (This might be redundant with FixedUpdate(), but I'm not sure)

        // Movement controls for the claw-machine's armature.   -E
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

        /*  Pseudocode for claw-grab action:
         *  Step 1: Lower claw.
         *      Method A) Temporarily increase CharacterJoint.connectedAnchor.y to a value of -1.0f
         *      Method B) ???
         *          Step 1a: Wait for some seconds. Coroutine?
         *  Step 2: Close claw.
         *      Method A) Operate hinge joints on claw bones, I think. This is possibly an animation?
         *  Step 3: Raise claw.
         *      Method A) Return CharacterJoint.connectedAnchor.y to its starting value of -0.5f
         */     // -E

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Lower();
        }


    }

    //  Methods Zone, the zone for methods.     -E

    // Lower the claw:
    public void Lower()
    {
        for (float f = hiPt.y; f > lowPt.y; f -= Time.deltaTime)
        {
            float clawAlt = tether.connectedAnchor.y;                       // "Claw Altitude" is the current value of the Anchor's Y position.
            clawAlt = Mathf.SmoothDamp(clawAlt, lowPt.y, ref speed, 1f);    // Each frame of Update(), "Claw Altitude" becomes SmoothDamped
            tether.connectedAnchor = new Vector3(0, clawAlt, 0f);
        }
        Invoke("Raise", wait);
    }

    public void Raise()     //Raise the claw        -E
    {
        for (float f = lowPt.y; f < hiPt.y; f += Time.deltaTime)
        {
            float clawAlt = tether.connectedAnchor.y;                       // "Claw Altitude" is the current value of the Anchor's Y position.
            clawAlt = Mathf.SmoothDamp(clawAlt, hiPt.y, ref speed, 1f);    // Each frame of Update(), "Claw Altitude" becomes SmoothDamped
            tether.connectedAnchor = new Vector3(0, clawAlt, 0f);
        }
    }

}   //end of Monobehavior; no code goes below/outside this line.    -E

