using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    public float speed = 4;

    [Header("References")]
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private Vector2 movementInput;
    private bool isWalking;

    [Header("Constants")]
    const string horizontal = "Horizontal";
    const string vertical = "Vertical";
    const string walking = "IsWalking";

    private void Start(){
        
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update(){
        
        float xMovement = Input.GetAxisRaw(horizontal);
        float yMovement = Input.GetAxisRaw(vertical);
        movementInput = new Vector2(xMovement, yMovement).normalized;


        if (xMovement == 0 && yMovement == 0) {
            isWalking = false;
        } else {
            isWalking = true;
        }

        //Set animations
        playerAnimator.SetFloat(horizontal, xMovement);
        playerAnimator.SetFloat(vertical, yMovement);

        playerAnimator.SetBool(walking, isWalking);
    }

    private void FixedUpdate(){
        
        playerRigidbody.MovePosition(playerRigidbody.position + movementInput * speed * Time.fixedDeltaTime);
    }
}
