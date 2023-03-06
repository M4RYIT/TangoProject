using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float CameraRotSpeed;

    private Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float xSign = Input.GetAxis("Mouse X");
            float ySign = Input.GetAxis("Mouse Y");

            transform.rotation *= Quaternion.Euler(new Vector3(-ySign, xSign, 0f) * CameraRotSpeed);
        }
    }
}
