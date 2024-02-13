using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float playerSpeed = 20f;
    private CharacterController myCC; //myCC = MyCharacter Controler
    public Animator camAnim;
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 movementVector;
    private float myGravity = -10f;
    void Start()
    {
        myCC = GetComponent<CharacterController>();
    }

    void Update()
    {
        getInput();
        MovePlayer();
        CheckForHeadBob();

        camAnim.SetBool("isWalking",isWalking  );
    }

    void getInput()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = transform.TransformDirection(inputVector);

        movementVector  = (inputVector * playerSpeed) + (Vector3.up * myGravity);
    }

    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }

    void CheckForHeadBob()
    {
       if (myCC.velocity.magnitude > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
}
