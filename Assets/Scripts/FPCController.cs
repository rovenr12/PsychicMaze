using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCController : MonoBehaviour {
  
    [SerializeField] Camera camera;
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float verticalRange = 45f;
    [SerializeField] float speed = 12f;

    CharacterController controller;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();

        if (camera == null) {
            camera = Camera.main;
        }
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        Move();
        RotatePlayer();
    }
    
    void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void RotatePlayer() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalRange, verticalRange);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}