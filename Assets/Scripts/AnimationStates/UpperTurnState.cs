using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class UpperTurnState : StateMachineBehaviour
{
    public LeaderAnimatorAsset AnimatorAsset;
    public bool Enter;

    bool upperTurn;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        upperTurn = !Enter;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (upperTurn != Enter)
        {
            bool upperTurning = Mathf.Abs(animator.GetFloat(AnimatorAsset.UpperTurnDirParam)) > 0f;
            upperTurn = (UpperTurnFirstCondition(animator) | UpperTurnSecondCondition(animator)) & upperTurning;
            animator.SetBool(AnimatorAsset.UpperTurnParam, upperTurn);
        }
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

    bool UpperTurnFirstCondition(Animator anim)
    {
        return anim.GetBool(AnimatorAsset.MoveParam) & (anim.GetInteger(AnimatorAsset.TracksNumberParam) == 4)
            & anim.GetBool(AnimatorAsset.CoupleParam) & !anim.GetBool(AnimatorAsset.SameLegsParam);
    }

    bool UpperTurnSecondCondition(Animator anim)
    {
        return anim.GetBool(AnimatorAsset.MoveParam) & (anim.GetInteger(AnimatorAsset.TracksNumberParam) == 3)
            & anim.GetBool(AnimatorAsset.SameLegsParam);
    }
}
