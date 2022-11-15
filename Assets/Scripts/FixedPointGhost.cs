using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FixedPointGhost : MonoBehaviour {
    [SerializeField] GameObject ghost;
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField][Range(0, 100)] int ghostAppearFrequency = 50;

    [SerializeField] bool isIn = false;
    
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (ghost.activeSelf) {
            sphereCollider.enabled = false;
        } else {
            sphereCollider.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            if (!isIn) {
                if (Random.Range(0, 100) <= ghostAppearFrequency) {
                    Vector3 playerLoc = other.gameObject.transform.position;
                    ghost.gameObject.transform.LookAt(new Vector3(playerLoc.x, ghost.transform.position.y, playerLoc.z));
                    ghost.SetActive(true);
                }
            }

            isIn = true;
        }
    }

    void OnTriggerExit(Collider other) {
        isIn = false;
    }
}