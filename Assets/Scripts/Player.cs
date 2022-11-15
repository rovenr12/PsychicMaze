using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    MentalHealth mentalHealth;
    // Start is called before the first frame update
    void Start() {
        mentalHealth = GetComponent<MentalHealth>();
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y != 0.5f) {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }
    

}