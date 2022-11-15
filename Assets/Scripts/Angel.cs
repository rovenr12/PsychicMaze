using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour {
    [SerializeField] int healthPoints;
    [SerializeField] float destroyTime = 5f;
    
    bool onDelete = false;
    
    void OnCollisionEnter(Collision collision) {
        if (onDelete) return;
        GameObject player = collision.gameObject;
        if (player.tag.Equals("Player")) {
            player.GetComponent<MentalHealth>().AddHealth(healthPoints);
            onDelete = true;
            Destroy(gameObject, destroyTime);
        }
    }

    void Update() {
        if (onDelete) {
            FadingObject();
        }
    }

    void FadingObject() {
        Color originalColor = GetComponent<Renderer>().material.color;
        float newAlpha = originalColor.a - 1 / destroyTime * Time.deltaTime;
        GetComponent<Renderer>().material.color =
            new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
    }

    void OnDestroy() {
        // throw new NotImplementedException();
    }
}