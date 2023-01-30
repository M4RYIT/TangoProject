using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IkController : MonoBehaviour
{
    public LeaderController Leader;
    public Transform RightHand;
    public Transform LeftHand;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        int isMoving = Convert.ToInt32(Leader.IsMoving);

        if (RightHand)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, isMoving);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, RightHand.position);
        }
        
        if (LeftHand) 
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, isMoving);
            anim.SetIKPosition(AvatarIKGoal.RightHand, LeftHand.position);
        }        
    }
}
