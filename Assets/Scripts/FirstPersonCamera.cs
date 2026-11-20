using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField]private Transform player;
    private float mouseSensitivity = 4f;
    float cameraVerticalRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible=false;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);
    }
}
