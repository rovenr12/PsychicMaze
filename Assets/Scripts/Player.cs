using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    MentalHealth mentalHealth;
    FMODUnity.StudioEventEmitter breakgroundAudio;
   
    // Start is called before the first frame update
    void Start() {
        mentalHealth = GetComponent<MentalHealth>();
        breakgroundAudio = GetComponent<FMODUnity.StudioEventEmitter>();

    }

    // Update is called once per frame
    void Update() {
        breakgroundAudio.SetParameter("MentalHealth", mentalHealth.GetNormalizedHealth());
    }
    

}