using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    public string isInteractingBool;
    public bool isInteracctingStatus;

    public string isUsingRootMotionBool;
    public bool isUsingRootMotionStatus;
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        animator.SetBool(isInteractingBool, isInteracctingStatus);
        animator.SetBool(isUsingRootMotionBool, isUsingRootMotionStatus);
    }    
}
