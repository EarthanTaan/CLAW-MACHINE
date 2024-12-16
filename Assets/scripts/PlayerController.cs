using System.Collections;
using System.Collections.Generic;
using UnityEngine;                      // above are default unity libraries DO NOT DELETE
using UnityEngine.InputSystem;          // you need this library for anything with the new Input System to work

public class PlayerController : MonoBehaviour
{
    [SerializeField]                        // this is so you can edit the value in the Inspector but keep it private
    private float normalSpeed = 2.0f;       // player speed
    PlayerInput playerControls;             // variable ref to inputs
    InputAction move;                       // var for specific action you want

    // Start is called before the first frame update
    void Start()
    {
        playerControls = GetComponent<PlayerInput>();       // setting you input reference
        move = playerControls.actions.FindAction("Move");   // setting your action reference by searching through all the actions by the name
    }

    // Update is called once per frame
    void Update()
    {
        Movement_WASD();       // calling you movement function
    }

    // this is a custome function for WASD movement
    void Movement_WASD()
    {
        // read input for which direction you are moving
        //Debug.Log(move.ReadValue<Vector2>());
        Vector2 direction = move.ReadValue<Vector2>();
        // sets gameObject's new position according to the input using Unity's realtime system
        transform.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime;        
    }
}
