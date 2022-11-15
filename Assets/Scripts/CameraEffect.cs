using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraEffect : MonoBehaviour {
    float originalY;
    void Awake() {
        originalY = transform.localPosition.y;
    }

    public IEnumerator Shake(float duration, float magnitude) {
        float elapsed = 0f;

        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, originalY + y, 0);
            elapsed += Time.deltaTime;
            yield return 0;
        }

        transform.localPosition = new Vector3(0, originalY, 0);
    }
}