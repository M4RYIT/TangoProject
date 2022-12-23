using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LeaderController : MonoBehaviour
{
    public InputAsset InputAsset;
    public LeaderAnimatorAsset AnimatorAsset;
    public float TurnSpeed;
    public float MoveSpeed;
    public int MinTracks;
    public int MaxTracks;
    
    private Rigidbody rb;
    private Animator anim;
    private float moveInput;
    private float turnInput;
    private float sideInput;
    private float upperTurnInput;
    private float medialunaInput;
    private bool tracksSwitchInput;
    private int tracksSwitchCounter;
    private int canMove;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        tracksSwitchCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetKey(InputAsset.ForwardKey)?1f:Input.GetKey(InputAsset.BackwardKey)?-1f:0f;
        turnInput = Input.GetAxisRaw(InputAsset.TurnAxis);
        sideInput = Input.GetAxisRaw(InputAsset.SideAxis);
        upperTurnInput = Input.GetAxisRaw(InputAsset.UpperTurnAxis);        
        tracksSwitchInput = Input.GetKeyDown(InputAsset.TracksSwitchKey);
        tracksSwitchCounter = (tracksSwitchCounter + Convert.ToInt32(tracksSwitchInput)) % (MaxTracks - MinTracks + 1);
        medialunaInput = Input.GetAxis(InputAsset.MedialunaAxis);

        bool moving = (Mathf.Abs(moveInput) > 0);
        bool siding = (Mathf.Abs(sideInput) > 0);
        canMove = Convert.ToInt32(moving ^ siding);

        anim.SetBool(AnimatorAsset.MoveParam, moving);
        anim.SetBool(AnimatorAsset.SideParam, siding);
        anim.SetFloat(AnimatorAsset.DirParam, (moving ? moveInput : sideInput) * canMove);
        anim.SetFloat(AnimatorAsset.UpperTurnDirParam, upperTurnInput);        
        anim.SetInteger(AnimatorAsset.TracksNumberParam, MinTracks + tracksSwitchCounter);
        anim.SetBool(AnimatorAsset.MedialunaParam, Mathf.Abs(medialunaInput) > 0);
        anim.SetFloat(AnimatorAsset.MedialunaDirParam, medialunaInput);
        anim.SetBool(AnimatorAsset.OchosParam, Input.GetKey(InputAsset.OchosKey));
    }

    private void OnAnimatorMove()
    {
        anim.ApplyBuiltinRootMotion(); 
        
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Time.fixedDeltaTime * turnInput * new Vector3(0f, TurnSpeed, 0f)));
    }
}
