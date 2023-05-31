using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePath : MonoBehaviour
{
    [SerializeField] public VehicleType vehicleType;
    [SerializeField] private int AmountofWayPoints;
    [SerializeField] public List<GameObject> Waypoints;
    [SerializeField] private bool _getsQuestion;
    [SerializeField] private bool _isSpecialRoad;
    [SerializeField] private Material lineMaterial;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() {
        //Waypoints.RemoveAll(GameObject => GameObject == null);
        if(Waypoints.Count == 0 || Waypoints.Count != AmountofWayPoints+2){
            //Waypoints.Remove(null);
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
                if(_getsQuestion){
                        if(i== 0){
                            SP.AddComponent<QuestionController>();
                            SphereCollider SC =SP.AddComponent<SphereCollider>();
                            SC.isTrigger = true;
                            SC.radius = 0.75f;
                            Gizmos.color = Color.green;
                            Gizmos.DrawWireSphere(Waypoints[i].transform.position, 0.75f);
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
            if((_isSpecialRoad && _getsQuestion) || i == 1)
            {
                SphereCollider SC = Waypoints[i].GetComponent<SphereCollider>();
                if(SC != null){
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(Waypoints[i].transform.position, 0.75f);
                    if(Waypoints[i].GetComponent<QuestionController>() == null)
                    {
                        Waypoints[i].AddComponent<QuestionController>();
                        SC.isTrigger = true;
                        SC.radius = 0.75f;
                    }
                }
            }
        }
    }
    public void EditorMeshMaker()
    {
        var mesh = new Mesh();
        mesh.name = gameObject.name;
        //mesh.colors = new []{Color.blue};
        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        for (int i = 0; i < Waypoints.Count; i++)
        {
            vertices.Add(Waypoints[i].transform.localPosition);
            indices.Add(i);
            if(i != 0 || i == Waypoints.Count-1)
            indices.Add(i);
        }
        Debug.Log(Waypoints.Count);
        foreach (var item in indices)
        {
            Debug.Log(item);
        }
        mesh.SetVertices(vertices);
        mesh.SetIndices(indices, MeshTopology.Lines, 0);
        GetComponent<MeshRenderer>().material = lineMaterial;
        Debug.Log("Here!");
        Debug.Log(mesh.name);
        GetComponent<MeshFilter>().mesh = mesh;

    }
}


