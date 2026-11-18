using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField]private Transform player;
    private float mouseSens = 4f;
    float cameraVerticalRotation = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X")*mouseSens;
        float inputY = Input.GetAxis("Mouse Y")*mouseSens;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles=Vector3.right*cameraVerticalRotation;
    }
}
