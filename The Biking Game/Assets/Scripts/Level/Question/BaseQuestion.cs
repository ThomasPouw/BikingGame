using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseQuestion : MonoBehaviour
{
    [SerializeField] public int PointsReceived = 100;
    [SerializeField] [Range(0,6)]public int QuestionWrongPunishment = 1;
    [SerializeField] public Entry Question;
    [SerializeField] public Entry[] Answers;

    [Header("Other Vehicle Users")]
    [SerializeField] public Vehicle[] vehicles;

    private void Start() {
        transform.transform.Find("Waypoint").GetComponentInChildren<QuestionController>().BlockQuestion = this;
        foreach(Vehicle vehicle in vehicles){
            vehicle.navMeshAgent.isStopped = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void QuestionVehicleMovement(StartEnd startEnd){
        foreach (Vehicle vehicle in vehicles)
        {
            if(startEnd == StartEnd.Start){
                StartCoroutine(vehicleStartMovement(vehicle));
            }
            else{
                StartCoroutine(vehicleEndMovement(vehicle));
            }
            
        }
    }
    IEnumerator vehicleStartMovement(Vehicle vehicle){
        yield return new WaitForSeconds(vehicle.waitStartTime);
        vehicle.navMeshAgent.isStopped = true;
    }
    IEnumerator vehicleEndMovement(Vehicle vehicle){
        yield return new WaitForSeconds(vehicle.waitEndTime);
        vehicle.navMeshAgent.isStopped = false;
    }
}
    [System.Serializable]
    public enum StartEnd{
        Start,
        End
    }
    