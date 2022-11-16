using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    [SerializeField] int numOfRemainingAngels;
    [SerializeField] Maze maze;

    // Start is called before the first frame update
    private void Start() {
        maze = FindObjectOfType<Maze>();
        numOfRemainingAngels = maze.GetNumberOfAngels();
    }

    // Update is called once per frame
    void Update() {
    }

    public void ReduceNumOfAngel() {
        numOfRemainingAngels -= 1;
    }
}