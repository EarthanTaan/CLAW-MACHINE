using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


public class clawMovement : MonoBehaviour       // Note that although it's called "clawMovement", this script is attached to the "Armature", not the "Claw".
{
    // Establishing convenient handles for the GameObjects' components. (They'll need to be assigned in Start() )    -E
    GameObject Elevator;                // the invisible anchor to which the Claw is actually attached. This will move down and up.
    public float speed = 0.5f;
    private float timer = 0.0f;         // (what was this for?) (Maybe I can use it for the lower/raise function.)     -E
    [SerializeField] float wait = 2.5f;
    bool clawDown = false;                      // the claw is not down     -E
    

    // Start is called before the first frame update
    void Start()
    {
        //  Assigning components to our handles for ease of use in Update()   -E
        Elevator = GameObject.Find("Elevator");
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
            float ElevatorY = Elevator.transform.position.y;            // grab the Y value of the Elevator's position, to pass into the loop below.
            float lerpStart = ElevatorY;                                // Copy the Elevator's present position to use as the static start of the Lerp.

            for (float i = 0f; i < 1f; i += Time.deltaTime)             // the operation will complete in 1 second.
            {
                float remainingDist = Mathf.InverseLerp(0f, 1f, i);     // calculate the remaining time each loop before the second is over.
                
                // Elevator Y pos -> 9.25f
                ElevatorY = Mathf.Lerp(lerpStart, 9.25f, remainingDist);  // Lerp it: from start, to target, by a factor proportional to the time remaining
                Elevator.transform.position = new Vector3(0,ElevatorY,0);   // update the Elevator's transform with the adjusted position each loop

                //tether.connectedAnchor.y -> 0.5f

                /*
                 *              <-- This is where you were working last!!!
                 *                      HEY LOOK
                 *                          -E
                 */
                
                //tether.anchor.y -> 7.85f
            }
        }
    }

    //  Methods Zone, the zone for methods.     -E

    // Lower the claw:
    //public void Lower()
    //{
        
    //}

    //public void Raise()     //Raise the claw        -E
    //{
    //    tether.connectedAnchor = Vector3.MoveTowards(tether.connectedAnchor, hiPt, Time.deltaTime);
    //    clawDown = false;
    //}

}   //end of Monobehavior; no code goes below/outside this line.    -E

