using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public float speed = 0; //the speed applied to the character
    public float walkSpeed = 5; // how fast walking is
    public float crouchSpeed = 2.5f; //how fast crouching is
    public float leftRight = 0; //moves left/right direction
    public float forwardBack = 0;//forward to back direction
    public float mouseSensitivity = 8;
    public float jumpHeight = 5;


    public Animator animator; //this is a reference to the players animator/controller
    public CharacterController characterController;
    //this is a reference to the player's character controller, this allows us to move
    public float gravity = 20; //character controller doesn't have gravity so we make our own
    public Vector3 moveDirection;//we will use this to move in 3D space
    //we will do this by applying the leftRight to our x axis of the world
    //and by applying the forwardBack to the z axis.

    void Start()
    {
        animator = GetComponent<Animator>();//attaching the Animator on our game object to our reference
        characterController = GetComponent<CharacterController>(); //attaching our CharacterController on our game object to our reference

        //we need to atttach our references so that we know which compenent we are manipulating.
    }

    //Update is called once per frame
    void Update()//every frame or every time the computer thinks a.k.a very fast
    {
        if (characterController.isGrounded)//is grounded is built into the character
            //it checks if we are standing on a surface that has a collider
        {

            leftRight = Input.GetAxis("Horizontal");//get input value for left and right
            forwardBack = Input.GetAxis("Vertical");//get input value for forward and back
            //apply the inputs to our move direction value
            moveDirection = transform.TransformDirection(new Vector3(leftRight, 0, forwardBack));
            //adjust speed
            if (Input.GetKey(KeyCode.LeftShift))//if we press left shift we want to sprint
            {
                speed = walkSpeed;//set speed to walk
            }
            else if (Input.GetKey(KeyCode.LeftControl))//if we press left shift we want to crouch
            {
                //set speed to crouch
                speed = crouchSpeed;
            }
            else//we are applying no speed
            {
                speed = 0;
            }
            moveDirection *= speed;//apply the speed that we set
        }
        moveDirection.y -= gravity * Time.deltaTime;//apply a downward force to simulate gravity
        characterController.Move(moveDirection * Time.deltaTime);
        //using the CharacterController, we are utilizing the inbuilt Move function to apply our movement
        //Apply speed value to animator
        animator.SetFloat("Speed", speed);
        //Apply leftRight value to animator
        animator.SetFloat("LeftRight", leftRight);
        //Apply forwardBack value to animator
        animator.SetFloat("ForwardBack", forwardBack);
        //transform.Rotate(Vector3.up * mouseSensitivity * Input.GetAxisRaw("Mouse X"));

    }
}
