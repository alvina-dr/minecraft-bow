using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target;
    public float multiplier = 0.01f;
    float mouseX;
    float mouseY;
    [SerializeReference] float heightOffset;

    Quaternion xRot;
    Quaternion yRot;

    void Update()
    {
        if (GPCtrl.Instance.pause) return;
        transform.position = new Vector3(target.position.x,
        target.position.y + heightOffset, target.position.z);

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        yRot = Quaternion.Euler(0f, mouseX * GPCtrl.Instance.player.cameraSpeed, 0f);
        xRot = Quaternion.Euler(-mouseY * GPCtrl.Instance.player.cameraSpeed, 0f, 0f);
        transform.rotation = yRot * transform.rotation * xRot;
    }
}
