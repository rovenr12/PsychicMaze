using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameObject minimapUI;
    [SerializeField] [Range(0f, 1f)] float minimapThreshold = 0.6f;
    [SerializeField] PostProcessVolume ghostEffect;
    [SerializeField] float ghostEffectPeriod = 5f;
    [SerializeField] Light light;
    [SerializeField] GameObject crackImg;
    
    MentalHealth mentalHealth;
    FMODUnity.StudioEventEmitter backgroundAudio;
   
    // Start is called before the first frame update
    void Start() {
        backgroundAudio = GetComponent<FMODUnity.StudioEventEmitter>();
        mentalHealth = GetComponent<MentalHealth>();
    }

    // Update is called once per frame
    void Update() {
        if (levelManager.IsGameOver()) {
            backgroundAudio.Stop();
            ghostEffect.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        backgroundAudio.SetParameter("MentalHealth", mentalHealth.GetNormalizedHealth());
        minimapUI.SetActive(mentalHealth.GetNormalizedHealth() < minimapThreshold);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "FixedPointGhost") {
            ghostEffect.enabled = true;
            crackImg.SetActive(true); 
            light.intensity /= 2;
            Invoke(nameof(StopGhostEffect), ghostEffectPeriod);
        }
    }

    void StopGhostEffect() {
        ghostEffect.enabled = false;
        crackImg.SetActive(false); 
        light.intensity *= 2;
    }
}