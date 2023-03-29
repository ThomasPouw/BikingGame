using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] GameObject WayPoints;
    [SerializeField] VehiclePath vehiclePath;
    [SerializeField] Transform BlockChecker;
    private NavMeshAgent _navMeshAgent; 
    private int WayPointCount = -1;
    public bool ready;
    // Start is called before the first frame update
    void Start()
    {
        //vehiclePath = WayPoints.GetComponent<VehiclePath>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ready){
            if (!_navMeshAgent.isActiveAndEnabled)
        _navMeshAgent.enabled = true;
        if(vehiclePath == null){
                RaycastHit hit;
                if(Physics.Raycast(BlockChecker.position, transform.TransformDirection(Vector3.down), out hit)){
                    WayPoints = hit.collider.gameObject;
                    vehiclePath = hit.collider.transform.Find("Waypoint").GetComponentInChildren<VehiclePath>();
                    BaseQuestion BQ = hit.collider.GetComponentInChildren<BaseQuestion>();
                    if(BQ != null){
                        BQ.QuestionVehicleMovement(StartEnd.Start);
                    }
                    Debug.Log(vehiclePath);
                }
            _navMeshAgent.SetDestination(vehiclePath.Waypoints[WayPointCount].transform.position);
        }
        if(_navMeshAgent.remainingDistance <= 1){
            if(vehiclePath.Waypoints.Count == WayPointCount-1){
                RaycastHit hit;
                if(Physics.Raycast(BlockChecker.position, transform.TransformDirection(Vector3.down), out hit)){
                    WayPoints = hit.collider.gameObject;
                    vehiclePath = hit.collider.gameObject.transform.GetChild(0).GetComponentInChildren<VehiclePath>();
                    BaseQuestion BQ = hit.collider.GetComponentInChildren<BaseQuestion>();
                    if(BQ != null){
                        BQ.QuestionVehicleMovement(StartEnd.Start);
                    }
                    WayPointCount = -1;
                }
                else{
                    _navMeshAgent.isStopped = true;
                }
            }
            WayPointCount++;
            _navMeshAgent.SetDestination(vehiclePath.Waypoints[WayPointCount].transform.position);
        }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        if(BlockChecker != null){
             Gizmos.DrawLine(BlockChecker.position, (BlockChecker.position+ Vector3.down));
        }
       
    }
    private void Awake() { 
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
