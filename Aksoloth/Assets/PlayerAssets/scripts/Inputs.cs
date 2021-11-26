using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{

    PlayerControl playerControl;
    PoruszaniePostaci poruszaniePostaci;
    AnimationControl animationControl;

    public Vector2 movementInput;
    public Vector2 cameraInput;


    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float Ivertical;
    public float Ihorizontal;


    public bool runTrigger;
    public bool jumpTrigger;
    public bool dodgeTrigger;
    public bool fightMode = false;
    public bool weaponAction = true;
    public bool triger;



    public bool atack;


    private void Awake()
    {
        poruszaniePostaci = GetComponent<PoruszaniePostaci>();
        animationControl = GetComponent<AnimationControl>();
    }


    private void OnEnable()
    {

        

        if(playerControl == null)
        {
            playerControl = new PlayerControl();


            playerControl.PlayerMovement.Movment.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControl.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();


            playerControl.PlayerActions.B.performed += i => runTrigger = true;
            playerControl.PlayerActions.B.canceled += i => runTrigger = false;
            playerControl.PlayerActions.X.performed += i => dodgeTrigger = true;
            playerControl.PlayerActions.Jump.performed += i => jumpTrigger = true;

            //figtTime

            
           playerControl.PlayerActions.FightMode.performed += i => fightMode = weaponAction;
           playerControl.PlayerActions.FightMode.performed += i => triger = true;


            //atack

            playerControl.PlayerActions.Atack.performed += i => atack = true;


            //playerControl.PlayerActions.FightMode.performed += i => fightMode = false;



            //weaponAction = true;


            //weaponAction = true;



        }


        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    public void MoveInputsHandler()
    {
        Ivertical = movementInput.y;
        Ihorizontal = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(Ihorizontal) + Mathf.Abs(Ivertical));
        animationControl.UpdateAnimatorV(0, moveAmount, poruszaniePostaci.isSprinting);
    }

    public void AllInputsHandler()
    {
        MoveInputsHandler();
        Sprinting();
        Jumping();
        Dodging();
        Atack();
        FightPreparation();
       
    }


    public void Sprinting()
    {
        if (runTrigger && moveAmount >0.5f)
        {
            poruszaniePostaci.isSprinting = true;
        }
        else
        {
            poruszaniePostaci.isSprinting = false;
        }
    }

    private void Jumping()
    {
        if(jumpTrigger)
        {
            jumpTrigger = false;
            poruszaniePostaci.Jump();
        }
    }


    private void Dodging()
    {
        if (dodgeTrigger)
        {
            dodgeTrigger = false;
            poruszaniePostaci.Dodge();
        }
    }


    private void FightPreparation()
    {

        if (triger)
        {
            triger = false;
            if (fightMode)
            {
                poruszaniePostaci.FightPreparation();
                weaponAction = false;
            }
            else
            {
                poruszaniePostaci.FightEnded();
                weaponAction = true;
            }
        }
    }


    private void Atack()
    {
        if(fightMode && animationControl.animator.GetBool("isGrounded"))
        {
            if(atack && !animationControl.animator.GetBool("isAtacking") && !animationControl.animator.GetBool("atack2"))   
            {
                poruszaniePostaci.Atack("slash");
                atack = false;
                
            }
            else if(atack && animationControl.animator.GetBool("isAtacking") && animationControl.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7)
            {
                poruszaniePostaci.Atack("slash2");
                atack = false;
            }
            else if (atack && animationControl.animator.GetBool("atack2") && animationControl.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7)
            {
                poruszaniePostaci.Atack("slash3");
                atack = false;
            }
        }
    }

}
