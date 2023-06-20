using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBike : MonoBehaviour
{
    [SerializeField]GameObject endScreenUI;
    [SerializeField] private EndScreen _endScreen;
    private void OnTriggerEnter(Collider other) {
        endScreenUI.SetActive(true);
        GameObject.Find("BikeOperator").GetComponent<VehicleMovement>().enabled = false;
        _endScreen.ShowEndScreen();
    }
    private void OnEnable() {
        endScreenUI = GameObject.Find("EndScreen");
        _endScreen = endScreenUI.GetComponent<EndScreen>();
        endScreenUI.SetActive(false);
    }
}
