using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoruszaniePostaci : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 moveDirection;
    Transform cameraPos;

    PlayerManager playerManager;
    AnimationControl animationControl;
    Inputs inputs;
    public Rigidbody rb;

    public float inAirTime;
    public float leepingVelocity;
    public float fallingSpeed;
    public float rayCastHightofSet = 0.1f;
    public LayerMask groundLayer;

    public bool isSprinting;
    public bool isOnGround;
    public bool isJumping;

    public float walkingSpeed = 1.5f;
    public float moveSpeed = 5;
    public float runSpeed = 7;
    public float rotateSpeed = 15;

    public float jumpHeight =3;
    public float gravityIntensity = -15;

    private void Awake()
    {
        
        playerManager = GetComponent<PlayerManager>();
        animationControl = GetComponent<AnimationControl>();
        inputs = GetComponent<Inputs>();
        rb = GetComponent<Rigidbody>();
        cameraPos = Camera.main.transform;
    }



    public void AllMove()
    {

        FallingLanding();
        if (playerManager.isInteracting)
        {
            return;
        }  
        Movement();
        Rotation();
    }


    private void Movement()
    {
        if (isJumping)
            return;

        moveDirection = cameraPos.forward * inputs.Ivertical;
        moveDirection = moveDirection + cameraPos.right * inputs.Ihorizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;


        if(isSprinting)
        {
            moveDirection = moveDirection * runSpeed;
        }
        else
        {
            if (inputs.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * moveSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkingSpeed;
            }
        }

        
       
        Vector3 movementVelocity = moveDirection;
        rb.velocity = movementVelocity;

    }
    private void Rotation()
    {
        if (isJumping)
            return;
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraPos.forward * inputs.Ivertical;
        targetDirection = targetDirection + cameraPos.right * inputs.Ihorizontal;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed*Time.deltaTime);

        transform.rotation = playerRotation;
    }


    private void FallingLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHightofSet;
        targetPosition = transform.position;
        if(!isOnGround && !isJumping)
        {
            if(!playerManager.isInteracting)
            {
                animationControl.PlayAnimation("falling", true);
            }

            animationControl.animator.SetBool("isUsingRootMotion", false);
            inAirTime = inAirTime + Time.deltaTime;
            rb.AddForce(transform.forward * leepingVelocity);
            rb.AddForce(-Vector3.up * fallingSpeed * inAirTime);

        }
        if(Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if(!isOnGround && playerManager.isInteracting)
            {
                animationControl.PlayAnimation("landing", true);
            }
            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;

            inAirTime = 0;
            isOnGround = true;
        }
        else 
        {
            isOnGround = false;
        }

        if(isOnGround && !isJumping)
        {
            if(playerManager.isInteracting || inputs.moveAmount >0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else 
            {
                transform.position = targetPosition;
            }
        }
    }


    public void Jump()
    {
        if (isOnGround && !playerManager.isInteracting)
        {
            animationControl.animator.SetBool("isJumping", true);
            animationControl.PlayAnimation("jump", false);
        

        float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
        Vector3 playerVelocity = moveDirection;
        playerVelocity.y = jumpingVelocity;
        rb.velocity = playerVelocity;
        }
    }


    public void Dodge()
    {
        if (playerManager.isInteracting || !isOnGround)
            return;

        animationControl.PlayAnimation("dodge", true, true);
        //No damage
        playerManager.isInteracting = true;
    }


    public void Atack()
    {
        if (playerManager.isInteracting)
            return;

        animationControl.PlayAnimation("slash", true, true);
        //No damage
        playerManager.isInteracting = true;
    }

    public void FightPreparation()
    {
       
        animationControl.PlayAnimation("FightMode", true);
        animationControl.animator.SetBool("isArming/Disarming", true);
        
    }


    public void FightEnded()
    {

        animationControl.PlayAnimation("FightModeOff", true);
        animationControl.animator.SetBool("isArming/Disarming", true);

    }

}
