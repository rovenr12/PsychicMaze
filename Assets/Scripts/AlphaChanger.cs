using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChanger : MonoBehaviour {
    [SerializeField] float interval = 0.1f;

    Image image;

    int direction = 1;

    void Start() {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (image.enabled) {
            if (Math.Abs(image.color.a - 1f) < 0.001 || Math.Abs(image.color.a) < 0.001) {
                direction *= -1;
            }

            float alpha = Mathf.Clamp(image.color.a + direction * interval, 0f, 1f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        } else {
            direction = 1;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
        
    }
}