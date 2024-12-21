using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UIElements;


public class clawMovement : MonoBehaviour       // Note that although it's called "clawMovement", this script is attached to the "Armature", not the "Claw".
{
    // Establishing convenient handles for the GameObjects' components. (They'll need to be assigned in Start() )    -E
    GameObject Elevator;                // the invisible anchor to which the Claw is actually attached. This will move down and up.
    GameObject Claw;                    // the claw object which holds the CharacterJoint component.
    GameObject Armature;                // the mounting which we move directly and which houses the Elevator (and to which this script is attached).
    /* Note: I tried to build myself a similar shortcut to the CharacterJoint component directly, but apparently the Component base class doesn't have access to those kinds of functions.
     * So I have to grab them individually with longer paths, as needed. It's fine.
     *       -E
     */

    public float speed = 0.5f;          // So far I'm just using this for the Armature's movement speed.

    // Claw high point target stats
    public float elevatorUp = 15;
    public float minLength = 2f;
    public float hingeUp = -12f;

    // Claw low point target stats
    public float elevatorDown = 9.25f;
    public float maxLength = 7f;      // Length of fully extended "cable". (yPos of 
    public float hingeDown = -0.5f;

    [SerializeField]
    private float timeDown = 1.0f;         // (what was this for?) (Maybe I can use it for the lower/raise function.)     -E
    [SerializeField] float wait = 2.5f; // How long the claw should stay down.
    bool clawActing = false;              // the claw is not acting     -E
    

    // Start is called before the first frame update
    void Start()
    {
        //  Assigning components to our handles for ease of use in Update()   -E
        Elevator = GameObject.Find("Elevator");     // What the Claw's Character Joint is attached to. Its only value to us is its Y position.
        Claw = GameObject.Find("Claw");             // Location of the Character Joint component itself. Holds most of the values we'll be adjusting.
        Armature = this.gameObject;                 // <-- Just a reminder. The "Armature" is what this script is actually attached to.     -E
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
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
         *      Method A) Lower elevator while raising hinge swing-axes.
         *          Step 1a: Wait for some seconds by housing the Raise action in a separate function, called with Invoke(String "methodName", Float seconds)
         *  Stuck on this step for way too long.
         *  
         *  Step 2: Close claw.
         *      Method A) Operate hinge joints on claw bones, I think. This is possibly an animation?
         *  Step 3: Raise claw.
         *      Method A) Return CharacterJoint.connectedAnchor.y to its starting value of -0.5f
         */     // -E

        // Okay, let's try it. Edit: Nope it acts completely insane. What's going wrong?
        if (Input.GetKeyDown(KeyCode.Space) && !clawActing && Elevator.transform.position.y == elevatorUp)
        {
            clawActing = true;
        }
        //then
        if (clawActing && Elevator.transform.position.y > elevatorDown)
        {
            Elevator.transform.Translate(Vector3.down * Time.deltaTime * 2);    //This is working DO NOT CHANGE.     -E

            Vector3 Hinge = Claw.GetComponent<CharacterJoint>().connectedAnchor;
            Hinge = Vector3.MoveTowards(Hinge, new Vector3(0, hingeDown, 0), Time.deltaTime * 2);
            Claw.GetComponent<CharacterJoint>().connectedAnchor = Hinge;

            Vector3 Length = Claw.GetComponent<CharacterJoint>().anchor;
            Length = Vector3.MoveTowards(Length, new Vector3(0, maxLength, 0), Time.deltaTime * 2);
            Claw.GetComponent<CharacterJoint>().anchor = Length;

        } else if (clawActing && Elevator.transform.position.y <= elevatorDown)
        {
            clawActing = false;
        }
        if (!clawActing && Elevator.transform.position.y < elevatorUp) {
            Elevator.transform.Translate(Vector3.up * Time.deltaTime * 2);

            Vector3 Hinge = Claw.GetComponent<CharacterJoint>().connectedAnchor;
            Hinge = Vector3.MoveTowards(Hinge, new Vector3(0, hingeUp, 0), Time.deltaTime * 2);
            Claw.GetComponent<CharacterJoint>().connectedAnchor = Hinge;

            Vector3 Length = Claw.GetComponent<CharacterJoint>().anchor;
            Length = Vector3.MoveTowards(Length, new Vector3(0, minLength, 0), Time.deltaTime * 2);
            Claw.GetComponent<CharacterJoint>().anchor = Length;
          }
    }

    //  Methods Zone, the zone for methods.     -E


}   //end of Monobehavior; no code goes below/outside this line.    -E

