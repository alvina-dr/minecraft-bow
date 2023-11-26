using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target;
    public float sens;
    public float multiplier = 0.01f;
    float mouseX;
    float mouseY;
    [SerializeReference] float heightOffset;

    Quaternion xRot;
    Quaternion yRot;

    void Update()
    {
        if (GPCtrl.Instance.pause) return;
        // This forces the camera to follow the "target"
        transform.position = new Vector3(target.position.x,
        target.position.y + heightOffset, target.position.z);

        // Input Processing
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // Camera Rotation
        yRot = Quaternion.Euler(0f, mouseX * sens, 0f);
        xRot = Quaternion.Euler(-mouseY * sens, 0f, 0f);
        transform.rotation = yRot * transform.rotation * xRot;
    }
}
