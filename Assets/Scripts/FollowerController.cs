using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowerController : MonoBehaviour
{
    public LeaderController Leader;
    public float LeaderDistance;
    public float DistancePerTracks;
    public FollowerAnimatorAsset AnimatorAsset;

    Animator anim;
    int currentTracks;
    int deltaTracks;
    bool tracksSwitch;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentTracks = Leader.Tracks;
        deltaTracks = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        anim.SetBool(AnimatorAsset.MoveParam, Leader.IsMoving);
        anim.SetBool(AnimatorAsset.SideParam, Leader.IsSiding);
        anim.SetFloat(AnimatorAsset.DirParam, -Leader.MoveDir);

        anim.SetFloat(AnimatorAsset.UpperTurnDirParam, -Leader.UpperTurnInput);
        anim.SetInteger(AnimatorAsset.TracksNumberParam, Leader.Tracks);
        anim.SetBool(AnimatorAsset.MedialunaParam, Leader.MedialunaAction);
        anim.SetFloat(AnimatorAsset.MedialunaDirParam, Leader.MedialunaInput);
        anim.SetBool(AnimatorAsset.OchosParam, Leader.OchosInput);

        anim.SetBool(AnimatorAsset.RotateParam, Mathf.Abs(Leader.TurnInput) > 0f);
        anim.SetFloat(AnimatorAsset.RotateDirParam, Leader.TurnInput);       

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
