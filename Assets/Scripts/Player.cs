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
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Angel")) {
            mentalHealth.AddHealth(other.GetComponent<Angel>().HealthPoints);
            Destroy(other.gameObject);
        }
    }
}