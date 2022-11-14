using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour {
    [SerializeField] Transform targetToFollow;
    [SerializeField] bool rotateWithTheTarget = true;
    [SerializeField] float cameraHeight;

    void Awake() {
        cameraHeight = transform.position.y;
    }

    void Update() {
        Vector3 targetPosition = targetToFollow.transform.position;
        transform.position = new Vector3(targetPosition.x, targetPosition.y + cameraHeight, targetPosition.z);

        if (rotateWithTheTarget) {
            Quaternion targetRotation = targetToFollow.transform.rotation;
            transform.rotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, 0);
        }
    }
}