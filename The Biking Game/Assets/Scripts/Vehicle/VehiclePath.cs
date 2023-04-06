using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePath : MonoBehaviour
{
    [SerializeField] public VehicleType vehicleType;
    [SerializeField] private int AmountofWayPoints;
    [SerializeField] public List<GameObject> Waypoints;
    [SerializeField] private bool _getsQuestion;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() {
        if(Waypoints.Count == 0 || Waypoints.Count != AmountofWayPoints+2){
            Waypoints = new List<GameObject>();
            if(transform.childCount != 2){
                Debug.LogError("This path does not have enough Waypoints!");
                return;
            }
            Vector3 Point1 = transform.GetChild(0).position;
            Vector3 Point2 = transform.GetChild(1).position;
            
            Vector3 DistanceBetweenPoints = (Point1 - Point2)/(AmountofWayPoints+1);
            Waypoints.Add(transform.GetChild(0).gameObject);
            for(int i = 0; i < AmountofWayPoints; i++){
                GameObject SP = new GameObject("SubWaypoint");
                SP.transform.position = Point1 - (DistanceBetweenPoints* (i+1));
                SP.transform.parent = transform;
                if(i == 0){
                    if(_getsQuestion){
                        SP.AddComponent<QuestionController>();
                        SphereCollider SC =SP.AddComponent<SphereCollider>();
                        SC.isTrigger = true;
                        SC.radius = 0.75f;
                    }
                }
                Waypoints.Add(SP);
            }
            Waypoints.Add(transform.GetChild(1).gameObject);
            Gizmos.DrawLine(Point1, Point2);
        }
        for(int i = 0; i < Waypoints.Count; i++){
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Waypoints[i].transform.position, 0.2f);
            if(i != 0){
                Gizmos.color = Color.red;
                Gizmos.DrawLine(Waypoints[i].transform.position, Waypoints[i-1].transform.position);
            }
            if(i == 1){
                if(_getsQuestion)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(Waypoints[i].transform.position, 0.75f);
                }
            }
        }
    }
}


