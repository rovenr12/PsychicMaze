using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour {
    [SerializeField] int healthPoints;

    public int HealthPoints {
        get => healthPoints;
        set => healthPoints = value;
    }
}