using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelController : MonoBehaviour {
    [SerializeField] GameObject angel;

    public void ActivateAngel() {
        angel.SetActive(true);
    }
}