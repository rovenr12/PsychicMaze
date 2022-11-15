using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour {
    [SerializeField] GameObject winCanvas;

    [SerializeField] LevelManager levelManager;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            winCanvas.SetActive(true);
            levelManager.GameOver();
        }
    }
}