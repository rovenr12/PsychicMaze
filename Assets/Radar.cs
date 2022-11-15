using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {
    [SerializeField] GameObject minimap;

    // Start is called before the first frame update
    void Start() {
        minimap.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            minimap.SetActive(true);
        }
    }
}