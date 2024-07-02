using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public Transform playerBody;

    private float yRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        yRotation -= mouseX;
        yRotation = Mathf.Clamp(-360.0f, yRotation, 360.0f);

        transform.localRotation = Quaternion.Euler(0.0f, yRotation, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
