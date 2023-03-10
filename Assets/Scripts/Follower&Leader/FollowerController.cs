using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowerController : MonoBehaviour
{
    public LeaderController Leader;
    [Range(0f, 1f)]
    public float LeaderDistance;
    public float DistancePerTracks;
    public FollowerAnimatorAsset AnimatorAsset;
    //public Transform LeftHand;
    //public Transform RightHand;

    Animator anim;
    int currentTracks;
    int deltaTracks;
    bool tracksSwitch;

    //// Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentTracks = Leader.Tracks;
        deltaTracks = 0;
    }

    //// Update is called once per frame
    void LateUpdate()
    {
        anim.SetBool(AnimatorAsset.MoveParam, Leader.Moving);
        anim.SetBool(AnimatorAsset.SideParam, Leader.Siding);
        anim.SetFloat(AnimatorAsset.DirParam, -Leader.LinearDir);
        anim.SetFloat(AnimatorAsset.UpperTurnDirParam, -Leader.UpperTurnDir);
        anim.SetInteger(AnimatorAsset.TracksNumberParam, Leader.Tracks);
        anim.SetBool(AnimatorAsset.RotateParam, Leader.Rotating);
        anim.SetFloat(AnimatorAsset.RotateDirParam, -Leader.RotationDir);
        anim.SetBool(AnimatorAsset.PivotParam, Leader.Pivoting);
        anim.SetFloat(AnimatorAsset.PivotDirParam, -Leader.PivotDir);
        anim.SetBool(AnimatorAsset.CoupleParam, Leader.Couple);
        anim.SetBool(AnimatorAsset.SameLegsParam, Leader.SameLegs);

        if (tracksSwitch = Leader.TracksInput)
        {
            deltaTracks = currentTracks - Leader.Tracks;
            currentTracks = Leader.Tracks;
        }
    }

    private void OnAnimatorMove()
    {
        anim.ApplyBuiltinRootMotion();

        transform.RotateAround(Leader.ModelRoot.position, Vector3.up, Vector3.SignedAngle(-transform.forward, Leader.Forward, Vector3.up));

        transform.position = Leader.transform.position + (Leader.Forward * LeaderDistance) + ((currentTracks - Leader.MinTracks) * DistancePerTracks * -transform.right); 
    }
}
