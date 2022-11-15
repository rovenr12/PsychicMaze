using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMoveUpEffect : MonoBehaviour {
    [SerializeField] float heightLimit = 10f;
    [SerializeField] float speed = 10f;
    Vector3 startPosition;

    void OnEnable() {
        startPosition = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        MoveUp();
    }

    void MoveUp() {
        if(transform.localPosition.y >= heightLimit) gameObject.SetActive(false);
        transform.localPosition += transform.up * speed * Time.deltaTime;
    }

    void OnDisable() {
        transform.localPosition = startPosition;
    }
}

