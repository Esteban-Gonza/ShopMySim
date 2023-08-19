using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesMovement : MonoBehaviour{

    [Header("References")]
    private Animator playerAnimator;
    private bool isWalking;

    [Header("Constants")]
    const string horizontal = "Horizontal";
    const string vertical = "Vertical";
    const string walking = "IsWalking";

    private void Start(){
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {

        float xMovement = Input.GetAxisRaw(horizontal);
        float yMovement = Input.GetAxisRaw(vertical);


        if (xMovement == 0 && yMovement == 0){
            isWalking = false;
        }else{
            isWalking = true;
        }

        //Set animations
        playerAnimator.SetFloat(horizontal, xMovement);
        playerAnimator.SetFloat(vertical, yMovement);

        playerAnimator.SetBool(walking, isWalking);
    }
}
