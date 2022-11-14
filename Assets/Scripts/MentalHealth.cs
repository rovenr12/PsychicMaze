using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MentalHealth : MonoBehaviour {
    [SerializeField] float maxHealth = 100;
    [SerializeField] float health;

    [SerializeField] int timeLimitation = 60;
    [SerializeField] Image crackImage;
    [SerializeField] Image holeImage;

    [SerializeField] CameraEffect cameraEffect;
    [SerializeField] float shakingFrequency = 5f;
    [SerializeField] float shakingDuration = 1f;
    [SerializeField] float shakingMagnitude = 0.4f;

    float dps;
    [SerializeField] bool triggerShaking = false;
    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        dps = health / timeLimitation;
        InvokeRepeating(nameof(DecreaseHealthByTime), 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate() {
        ChangeVisualization();
    }
    
    public void AddHealth(float amount) {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
    }

    public void DecreaseHealth(float amount) {
        health = Mathf.Clamp(health - amount, 0, maxHealth);
    }

    public float GetHealth() {
        return health;
    }

    public float GetNormalizedHealth() {
        return health / maxHealth;
    }
    
    void DecreaseHealthByTime() {
        if (IsAlive()) {
            DecreaseHealth(dps);
        } else {
            CancelInvoke(nameof(DecreaseHealthByTime));
        }
    }
    bool IsAlive() {
        return health > 0;
    }
    
    void Shaking() {
        StartCoroutine(cameraEffect.Shake(shakingDuration, shakingMagnitude));
    }
    
    void ChangeVisualization() {
        float healthPercentage = GetNormalizedHealth();

        if (!triggerShaking && healthPercentage < 0.8) {
            triggerShaking = true;
            InvokeRepeating(nameof(Shaking), 0, shakingFrequency);
        } else if(triggerShaking && healthPercentage >= 0.8) {
            triggerShaking = false;
            CancelInvoke(nameof(Shaking));
        }

        if (healthPercentage < 0.6) {
            if (!crackImage.enabled) {
                crackImage.enabled = true;
            }
        } else {
            crackImage.enabled = false;
        }
        
        if (healthPercentage < 0.2) {
            if (!holeImage.enabled) {
                holeImage.enabled = true;
            }
        } else {
            holeImage.enabled = false;
        }

        // if (heathPercentage < 0.4) {
        //     wall.color = tensionWallColor;
        //     ground.color = tensionGroundColor;
        // } else {
        //     wall.color = normalWallColor;
        //     ground.color = normalGroundColor;           
        // }
    }
}