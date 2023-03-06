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

        if (tracksSwitch)
        {
            transform.position += deltaTracks * DistancePerTracks * transform.right;
        }

        float deltaDistance = Vector3.Distance(transform.position, Leader.transform.position) - LeaderDistance;
        if (Mathf.Abs(deltaDistance) > 0f)
        {
            transform.position += transform.forward * deltaDistance;
        }

        float horizontalDistance = Leader.transform.InverseTransformPoint(transform.position).x;
        float maxHorizontalDistance = DistancePerTracks * (currentTracks - Leader.MinTracks);
        float horizontalDelta = Mathf.Abs(horizontalDistance) - maxHorizontalDistance;
        if (horizontalDelta > 0f)
        {
            transform.position += transform.right * (horizontalDelta * Mathf.Sign(horizontalDistance));
        }
    }
}
