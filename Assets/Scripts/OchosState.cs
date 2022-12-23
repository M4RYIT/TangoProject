using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OchosState : StateMachineBehaviour
{
    public LeaderAnimatorAsset AnimatorAsset;
    readonly float[] limits = { 0.99f, 0.01f };
    int index = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ChangeDir(Mathf.Abs(stateInfo.normalizedTime)))
        {            
            animator.SetFloat(AnimatorAsset.OchosSpeedParam, animator.GetFloat(AnimatorAsset.OchosSpeedParam)*-1f);
            index = (index + 1) % 2;
        }
    }

    bool ChangeDir(float absNormTime)
    {
        return (index == 0)? (absNormTime - (int)absNormTime) > limits[index] : (absNormTime - (int)absNormTime) < limits[index];
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
