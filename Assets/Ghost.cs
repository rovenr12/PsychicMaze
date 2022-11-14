using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
    [SerializeField] float damage;
    [SerializeField] GameObject player;
    [SerializeField] float speed = 10f;
    
    void Start() {
        
    }

    void OnEnable() {
        transform.position = player.transform.position + player.transform.forward * 10;
        transform.LookAt(player.transform);
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(player.transform);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, player.transform.position) < 0.001f) {
            gameObject.SetActive(false);
        }
    }
}