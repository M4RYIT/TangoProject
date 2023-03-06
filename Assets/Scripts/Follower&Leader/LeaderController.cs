using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using UnityEngine;

public class LeaderController : MonoBehaviour
{
    public InputAsset InputAsset;
    public LeaderAnimatorAsset AnimatorAsset;
    public Transform ModelRoot;
    public float TurnSpeed;
    public int MinTracks;
    public int MaxTracks;

    private Dictionary<InputEnum, Tuple<KeyCode, KeyCode>> inputs;
    private Animator anim;
    private IkController ikController;
    private float moveInput;
    private float turnInput;
    private float sideInput;
    private float linearDir;
    private float upperTurnInput;
    private float pivotInput;
    private bool tracksSwitchInput;
    private int tracksSwitchCounter;
    private bool moving;
    private bool siding;
    private bool rotating;
    private bool pivoting;
        
    public int Tracks { get => MinTracks + tracksSwitchCounter; }
    public bool TracksInput { get => tracksSwitchInput; }
    public bool Moving { get => moving; }
    public bool Siding { get => siding; }
    public bool Pivoting { get => pivoting; }
    public bool Rotating { get => rotating; }
    public float LinearDir { get => linearDir; }
    public float PivotDir { get => pivotInput; }
    public float RotationDir { get => turnInput; }
    public float UpperTurnDir { get => upperTurnInput; }
    public Vector3 Forward { get => new(ModelRoot.forward.x, 0f, ModelRoot.forward.z); }

    private void Awake()
    {
        inputs = InputAsset.inputs.ToDictionary(x => x.InputType, x => new Tuple<KeyCode, KeyCode>(x.InputKey1, x.InputKey2));
    }

    // Start is called before the first frame update
    void Start()
    {
        ikController = GetComponent<IkController>();
        anim = GetComponent<Animator>();
        tracksSwitchCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetKey(inputs[InputEnum.Move].Item1) ? -1f : Input.GetKey(inputs[InputEnum.Move].Item2) ? 1f : 0f;
        turnInput = Input.GetKey(inputs[InputEnum.Turn].Item1) ? -1f : Input.GetKey(inputs[InputEnum.Turn].Item2) ? 1f : 0f;
        sideInput = Input.GetKey(inputs[InputEnum.Side].Item1) ? -1f : Input.GetKey(inputs[InputEnum.Side].Item2) ? 1f : 0f;
        upperTurnInput = Input.GetKey(inputs[InputEnum.UpperTurn].Item1) ? -1f : Input.GetKey(inputs[InputEnum.UpperTurn].Item2) ? 1f : 0f;
        pivotInput = Input.GetKey(inputs[InputEnum.Pivot].Item1) ? -1f : Input.GetKey(inputs[InputEnum.Pivot].Item2) ? 1f : 0f;
        tracksSwitchInput = Input.GetKeyDown(inputs[InputEnum.Tracks].Item1);
        tracksSwitchCounter = (tracksSwitchCounter + Convert.ToInt32(tracksSwitchInput)) % (MaxTracks - MinTracks + 1);

        moving = Mathf.Abs(moveInput) > 0;
        siding = Mathf.Abs(sideInput) > 0;
        pivoting = Mathf.Abs(pivotInput) > 0;
        rotating = Mathf.Abs(turnInput) > 0;
        linearDir = moving ? moveInput : sideInput;

        anim.SetBool(AnimatorAsset.MoveParam, moving);
        anim.SetBool(AnimatorAsset.SideParam, siding);
        anim.SetFloat(AnimatorAsset.DirParam, linearDir);
        anim.SetFloat(AnimatorAsset.UpperTurnDirParam, upperTurnInput);
        anim.SetInteger(AnimatorAsset.TracksNumberParam, MinTracks + tracksSwitchCounter);
        anim.SetBool(AnimatorAsset.PivotParam, pivoting);
        anim.SetFloat(AnimatorAsset.PivotDirParam, pivotInput);        
    }

    private void OnAnimatorMove()
    {
        anim.ApplyBuiltinRootMotion();

        transform.rotation *= Quaternion.Euler(Time.fixedDeltaTime * turnInput * new Vector3(0f, TurnSpeed, 0f));
    }

    public void FollowerAddSub(bool add)
    {
        ikController.enabled = add;
        anim.SetBool(AnimatorAsset.CoupleParam, add);
    }
}
