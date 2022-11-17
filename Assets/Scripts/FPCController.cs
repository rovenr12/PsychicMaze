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
    [SerializeField] Rigidbody rigidBody;

    CharacterController controller;
    FMOD.Studio.EventInstance footsteps;
    float timer = 0.0f;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();

        if (camera == null) {
            camera = Camera.main;
        }
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (levelManager.IsGameOver()) {
            return;
        }
        RotatePlayer();
        Move();
        
        // AdjustHeight();
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
            Vector3 move = transform.right * x + transform.forward * z;
            // transform.Translate(move * speed * Time.deltaTime);
            rigidBody.velocity = move * speed;
        }
        else {
            rigidBody.velocity = new Vector3(0, 0, 0);
        }
        
        // controller.Move(move * speed * Time.deltaTime);
    }

    void RotatePlayer() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        
        xRotation = Mathf.Clamp(xRotation, -verticalRange, verticalRange);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        // transform.rotation = Quaternion.Euler(0, yRotation, 0);
        
    }

    void PlayFootstep() {
        footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
        footsteps.start();
        footsteps.release();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Equals("Wall")) {
            rigidBody.velocity = new Vector3(0, 0, 0);
        }
    }
}