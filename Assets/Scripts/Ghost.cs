using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
    [SerializeField] float damage;
    [SerializeField] GameObject player;
    [SerializeField] GameObject end;
    [SerializeField] float speed = 10f;
    [SerializeField] LevelManager levelManager;

    void Start() {
        
    }

    void OnEnable() {
        transform.position = player.transform.position + player.transform.forward * 1;
        transform.LookAt(end.transform);
    }

    // Update is called once per frame
    void Update() {
        if (levelManager.IsGameOver()) {
            gameObject.SetActive(false);
            return;
        }
        transform.LookAt(player.transform);
        transform.position = Vector3.MoveTowards(transform.position, end.transform.position, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, player.transform.position) > 10f) {
            gameObject.SetActive(false);
        }
    }
    
    
}