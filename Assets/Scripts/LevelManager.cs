using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField] bool isGameOver;
    
    // Start is called before the first frame update
    void Start() {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update() {
    }

    public void GameOver() {
        isGameOver = true;
    }

    public bool IsGameOver() {
        return isGameOver;
    }
}