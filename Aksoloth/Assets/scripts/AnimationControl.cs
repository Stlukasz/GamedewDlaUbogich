using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{

    public Animator animator;
    PlayerManager playerManager;
    PoruszaniePostaci poruszaniePostaci;
    int horizontal;
    int vertical;
   


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        poruszaniePostaci = GetComponent<PoruszaniePostaci>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayAnimation(string targetAnim, bool isInteracting, bool useRootMotion = false)
    {
        
        animator.SetBool("isInteracting", isInteracting);
       
        animator.SetBool("isUsingRootMotion", useRootMotion);
        animator.CrossFade(targetAnim, 0.48f);
    }


    public void UpdateAnimatorV(float verticalMov, float horizontalMov, bool isSprinting)
    {

        float SnHorizontal;
        float SnVertical;

        if(horizontalMov > 0 && horizontalMov < 0.55f)
        {
            SnHorizontal = 0.5f;
        }
        else if (horizontalMov > 0.55f)
        {
            SnHorizontal = 1;
        }
        else if (horizontalMov < 0 && horizontalMov > -0.55f)
        {
            SnHorizontal = -0.5f;
        }
        else if (horizontalMov <= -0.55f)
        {
            SnHorizontal = -1;
        }
        else
        {
            SnHorizontal = 0;
        }


        if (verticalMov > 0 && verticalMov < 0.55f)
        {
            SnVertical = 0.5f;
        }
        else if (verticalMov > 0.55f)
        {
            SnVertical = 1;
        }
        else if (verticalMov < 0 && verticalMov > -0.55f)
        {
            SnVertical = -0.5f;
        }
        else if (verticalMov <= -0.55f)
        {
            SnVertical = -1;
        }
        else
        {
            SnVertical = 0;
        }

        if(isSprinting)
        {
            SnHorizontal = horizontalMov;
            SnVertical = 2;
        }

        animator.SetFloat(horizontal, SnHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, SnVertical, 0.1f, Time.deltaTime);
    }


    private void OnAnimatorMove()
    {
        if(playerManager.isUsingRootMotion)
        {
            poruszaniePostaci.rb.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            poruszaniePostaci.rb.velocity = velocity;
        }
    }



}
