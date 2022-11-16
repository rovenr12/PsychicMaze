using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCController : MonoBehaviour {

    [SerializeField] LevelManager levelManager;
    [SerializeField] Camera camera;
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float verticalRange = 45f;
    [SerializeField] float speed = 12f;
    [SerializeField] float footstepSpeed = 0.3f;

    CharacterController controller;
    FMOD.Studio.EventInstance footsteps;
    float timer = 0.0f;

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
        if (levelManager.IsGameOver()) {
            return;
        }
        Move();
        RotatePlayer();
        AdjustHeight();
    }

    void AdjustHeight() {
        float currentY = gameObject.transform.position.y;
        if (Math.Abs(currentY - 1.58f) > 0.001f) {
            controller.Move(new Vector3(0, currentY - 1.58f, 0));
        }
    }

    void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (x != 0 || z != 0) {
            if (timer > footstepSpeed) {
                PlayFootstep();
                timer = 0.0f;
            }

            timer += Time.deltaTime;
        }
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

    void PlayFootstep() {
        footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
        footsteps.start();
        footsteps.release();
    }
}