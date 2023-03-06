using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct IkTarget
{
    public Transform TargetTransform;
    public Vector3 TargetOffset;
    public Vector3 TargetPosition { get => TargetTransform.position; }
    public Quaternion TargetRotation { get => TargetTransform.rotation; }
}

public class IkController : MonoBehaviour
{
    public IkControllerData IkData;
    public GameObject Other;
    public IkTarget LeftHand;
    public IkTarget RightHand;
    public IkTarget Hip;
    public bool LeftSide;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!Other) return;

        float distance = Vector3.Distance(transform.position, Other.transform.position);
        int isFar = Convert.ToInt32(distance > IkData.SecondStepDistance);
        bool onHip = distance < IkData.FirstStepDistance;

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1 - isFar);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, (onHip & LeftSide) ? (Hip.TargetPosition + Hip.TargetOffset) : 
            (LeftHand.TargetPosition + LeftHand.TargetOffset));
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, Convert.ToInt32(onHip));
        anim.SetIKRotation(AvatarIKGoal.LeftHand, Hip.TargetRotation);

        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1 - isFar);
        anim.SetIKPosition(AvatarIKGoal.RightHand, (onHip & !LeftSide) ? (Hip.TargetPosition + Hip.TargetOffset) : 
            (RightHand.TargetPosition + RightHand.TargetOffset));
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, Convert.ToInt32(onHip));
        anim.SetIKRotation(AvatarIKGoal.RightHand, Hip.TargetRotation);
    }
}
