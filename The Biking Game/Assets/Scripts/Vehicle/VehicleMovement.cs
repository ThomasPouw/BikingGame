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
    // Start is called before the first frame update
    void Start()
    {
        vehiclePath = WayPoints.GetComponent<VehiclePath>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_navMeshAgent.isActiveAndEnabled)
        _navMeshAgent.enabled = true;
        if(_navMeshAgent.hasPath || vehiclePath == null){
                RaycastHit hit;
                if(Physics.Raycast(BlockChecker.position, transform.TransformDirection(Vector3.down), out hit)){
                    WayPoints = hit.collider.gameObject;
                    vehiclePath = hit.collider.GetComponentInChildren<VehiclePath>();
                    Debug.Log(vehiclePath);
                }
            _navMeshAgent.SetDestination(vehiclePath.Waypoints[WayPointCount].transform.position);
        }
        if(_navMeshAgent.remainingDistance <= 0.4){
            if(vehiclePath.Waypoints.Count == WayPointCount-1){
                RaycastHit hit;
                if(Physics.Raycast(BlockChecker.position, transform.TransformDirection(Vector3.down), out hit)){
                    WayPoints = hit.collider.gameObject;
                    vehiclePath = hit.collider.GetComponentInChildren<VehiclePath>();
                    WayPointCount = -1;
                }
                
            }
            WayPointCount++;
            _navMeshAgent.SetDestination(vehiclePath.Waypoints[WayPointCount].transform.position);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(BlockChecker.position, (BlockChecker.position+ Vector3.down));
    }
    private void Awake() { 
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
