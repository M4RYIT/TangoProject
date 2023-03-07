using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float CameraRotSpeed;
    public Transform TargetTransform;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float xSign = Input.GetAxis("Mouse X");
            float ySign = Input.GetAxis("Mouse Y");
            bool horizontal = Mathf.Abs(xSign) > Mathf.Abs(ySign);

            transform.RotateAround(TargetTransform.position, horizontal ? Vector3.up : transform.right, (horizontal ? xSign : -ySign) * CameraRotSpeed);            
        }
    }
}
