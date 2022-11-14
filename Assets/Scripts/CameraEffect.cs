using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraEffect : MonoBehaviour {
    public IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y + originalPosition.y, 0);
            elapsed += Time.deltaTime;
            yield return 0;
        }

        transform.localPosition = originalPosition;
    }
}