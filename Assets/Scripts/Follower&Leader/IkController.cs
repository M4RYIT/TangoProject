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
    public Vector3 TargetLocalOffset { get => (TargetOffset.x * TargetTransform.forward) + (TargetOffset.y * TargetTransform.right) + (TargetOffset.z * TargetTransform.up); }
}

public class IkController : MonoBehaviour
{
    public IkControllerData IkData;
    public GameObject Other;
    public IkTarget LeftHand;
    public IkTarget RightHand;
    public IkTarget LeftHip;
    public IkTarget RightHip;
    public bool LeftSide;
    public bool OnHip { get; set; }

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        OnHip = false;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!Other) return;

        float distance = Vector3.Distance(transform.position, Other.transform.position);
        int far = Convert.ToInt32(distance > IkData.SecondStepDistance);
        bool close = distance < IkData.FirstStepDistance;
        bool onHip = close | OnHip;

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1 - far);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, (onHip & LeftSide) ? (LeftHip.TargetPosition + LeftHip.TargetLocalOffset) : 
            (LeftHand.TargetPosition + LeftHand.TargetLocalOffset));
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, Convert.ToInt32(onHip & LeftSide));
        anim.SetIKRotation(AvatarIKGoal.LeftHand, LeftHip.TargetRotation);

        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1 - far);
        anim.SetIKPosition(AvatarIKGoal.RightHand, (onHip & !LeftSide) ? (RightHip.TargetPosition + RightHip.TargetLocalOffset) : 
            (RightHand.TargetPosition + RightHand.TargetLocalOffset));
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, Convert.ToInt32(onHip & !LeftSide));
        anim.SetIKRotation(AvatarIKGoal.RightHand, RightHip.TargetRotation);
    }
}
