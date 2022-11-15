using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarks : MonoBehaviour {
    [SerializeField] GameObject[] landmarks;
    [SerializeField] [Range(0, 100)] int landmarkPercentage;
    // Start is called before the first frame update
    void Start() {
        if (Random.Range(0, 100) <= landmarkPercentage) {
            landmarks[Random.Range(0, landmarks.Length)].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {
    }
}