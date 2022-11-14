using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacementShader : MonoBehaviour {
    [SerializeField] Camera targetCamera;
    // Start is called before the first frame update
    void Start() {
        targetCamera.SetReplacementShader(Shader.Find("Unlit/Color"), "RenderType");
    }

}