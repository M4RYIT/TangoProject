using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LeaderController : MonoBehaviour
{
    public InputAsset InputAsset;
    public LeaderAnimatorAsset AnimatorAsset;
    public Transform ModelRoot;
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
    private bool moving;
    private bool siding;
    private bool ochosInput;

    public float MoveInput { get => moveInput; }
    public float TurnInput { get => turnInput; }
    public float SideInput { get => sideInput; }
    public float UpperTurnInput { get => upperTurnInput; }
    public float MedialunaInput { get => medialunaInput; }
    public bool MedialunaAction { get => Mathf.Abs(medialunaInput) > 0; }
    public bool OchosInput { get => ochosInput; }
    public int Tracks { get => MinTracks + tracksSwitchCounter; }
    public bool TracksInput { get => tracksSwitchInput; }
    public int CanMove { get => canMove; }
    public bool IsMoving { get => moving; }
    public bool IsSiding { get => siding; }
    public float MoveDir { get => anim.GetFloat(AnimatorAsset.DirParam); }
    public float OchosSpeed { get => anim.GetFloat(AnimatorAsset.OchosSpeedParam); }
    public Vector3 Forward { get => new(ModelRoot.forward.x, 0f, ModelRoot.forward.z); }

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
        ochosInput = Input.GetKey(InputAsset.OchosKey);

        moving = Mathf.Abs(moveInput) > 0;
        siding = Mathf.Abs(sideInput) > 0;
        canMove = Convert.ToInt32(moving ^ siding);

        anim.SetBool(AnimatorAsset.MoveParam, moving);
        anim.SetBool(AnimatorAsset.SideParam, siding);
        anim.SetFloat(AnimatorAsset.DirParam, (moving ? moveInput : sideInput) * canMove);
        anim.SetFloat(AnimatorAsset.UpperTurnDirParam, upperTurnInput);        
        anim.SetInteger(AnimatorAsset.TracksNumberParam, MinTracks + tracksSwitchCounter);
        anim.SetBool(AnimatorAsset.MedialunaParam, MedialunaAction);
        anim.SetFloat(AnimatorAsset.MedialunaDirParam, medialunaInput);
        anim.SetBool(AnimatorAsset.OchosParam, ochosInput);
    }

    private void OnAnimatorMove()
    {
        anim.ApplyBuiltinRootMotion(); 
        
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Time.fixedDeltaTime * turnInput * new Vector3(0f, TurnSpeed, 0f)));
    }
}
