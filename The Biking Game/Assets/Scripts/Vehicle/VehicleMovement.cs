using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] GameObject WayPoints;
    [SerializeField] VehiclePath vehiclePath;
    [SerializeField] Transform BlockChecker;
    [SerializeField] public LayerMask IgnoreLayer;
    [SerializeField] private WheelMovement _wheelMovement;
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
        try{
            if(ready){
                //WriteDebugLog.WriteString("Im ready!");
            if (!_navMeshAgent.isActiveAndEnabled){
                _navMeshAgent.enabled = true;
                //WriteDebugLog.WriteString("Enabled...");
            }
            if(!_navMeshAgent.isOnNavMesh){
                //NavMeshSurface navMeshSurface = GameObject.Find("LevelMaker").GetComponent<NavMeshSurface>();
                //WriteDebugLog.WriteString("Lets try this...");
                //navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
                _navMeshAgent.enabled = false;
                _navMeshAgent.enabled = true;
            }
            //WriteDebugLog.WriteString("Im at: "+_navMeshAgent.gameObject.transform.position.ToString());
            //WriteDebugLog.WriteString("Am I on Navmesh?" + _navMeshAgent.isOnNavMesh);
            if(vehiclePath == null && _navMeshAgent.isOnNavMesh){
                //WriteDebugLog.WriteString("Looking down...");
                    RaycastHit hit;
                    if(Physics.Raycast(BlockChecker.position, transform.TransformDirection(Vector3.down), out hit, 100f, ~IgnoreLayer)){
                        WayPoints = hit.collider.gameObject;
                        vehiclePath = hit.collider.gameObject.name == "SubWaypoint" ? hit.collider.transform.parent.GetComponent<VehiclePath>() : hit.collider.transform.Find("Waypoint").GetComponentInChildren<VehiclePath>();
                    //if(vehiclePath == null){
                     //   Debug.Log("Here!");
                    //   vehiclePath = hit.collider.gameObject.transform.parent.GetComponent<VehiclePath>(); 
                    //}
                        BaseQuestion BQ = hit.collider.GetComponentInChildren<BaseQuestion>();
                        if(BQ != null){
                            BQ.QuestionVehicleMovement(StartEnd.Start);
                        }
                        Debug.Log(vehiclePath);
                    }
            }
            if(_navMeshAgent.remainingDistance <= 1){
                if(vehiclePath.Waypoints.Count == WayPointCount-1){
                    RaycastHit hit;
                    if(Physics.Raycast(BlockChecker.position, transform.TransformDirection(Vector3.down), out hit, 100f, ~IgnoreLayer)){
                        WayPoints = hit.collider.gameObject;
                        vehiclePath = hit.collider.gameObject.transform.Find("Waypoint").GetComponentInChildren<VehiclePath>();
                        BaseQuestion BQ = hit.collider.transform.Find("Question").GetComponentInChildren<BaseQuestion>();
                        if(BQ != null){
                            hit.collider.gameObject.transform.Find("Waypoint").GetComponentInChildren<QuestionController>().BlockQuestion = BQ;
                            BQ.QuestionVehicleMovement(StartEnd.Start);
                        
                        }
                        WayPointCount = -1;
                    }
                    else{
                        _navMeshAgent.isStopped = true;
                    }
                }
                WayPointCount++;
                if(vehiclePath.Waypoints[WayPointCount] != null){
                    _wheelMovement.RotateWheel(_navMeshAgent.destination, vehiclePath.Waypoints[WayPointCount].transform.position);
                    _navMeshAgent.SetDestination(vehiclePath.Waypoints[WayPointCount].transform.position);
                }
            }
            }
        }
        catch(Exception e){
            //WriteDebugLog.WriteString(e.Message);
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
