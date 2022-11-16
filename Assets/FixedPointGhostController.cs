using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPointGhostController : MonoBehaviour {
    [SerializeField] GameObject ghost;
    [SerializeField] int ghostFrequency = 50;
    // Start is called before the first frame update
    void Start() {
        if (Random.Range(0, 100) <= ghostFrequency) {
            ghost.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
    }
}