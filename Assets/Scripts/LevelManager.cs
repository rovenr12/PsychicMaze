using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [SerializeField] bool isGameOver;
    
    // Start is called before the first frame update
    void Start() {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(1);
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void GameOver() {
        isGameOver = true;
    }

    public bool IsGameOver() {
        return isGameOver;
    }
}