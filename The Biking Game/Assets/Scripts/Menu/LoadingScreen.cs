using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject SpinningObject;
    [SerializeField] public bool EverythingLoaded;
    [SerializeField] Animator animator;
    float t = 0.0f;
    [SerializeField] GameObject _bikeOperator;
    public void StartBike()
    {
        NavMeshAgent n = _bikeOperator.GetComponent<NavMeshAgent>();
        _bikeOperator.GetComponent<VehicleMovement>().enabled = true;
        GameObject.Find("PointSystem").transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().enabled = true;
        GameObject.Find("PointSystem").transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "NavMeshAgent: "+ n.enabled+ " bike on Navmesh"+n.isOnNavMesh + " levelmaker has navdata"+ (GameObject.Find("LevelMaker").GetComponent<NavMeshSurface>().navMeshData? true: false) + " NavMeshSurfuce: " +GameObject.Find("LevelMaker").GetComponent<NavMeshSurface>().enabled+ "ActiveSurfuces: "+GameObject.Find("LevelMaker").GetComponent<NavMeshSurface>();
        WriteDebugLog.WriteString("NavMeshAgent: "+ n.enabled+ " bike on Navmesh"+n.isOnNavMesh + " levelmaker has navdata"+ (GameObject.Find("LevelMaker").GetComponent<NavMeshSurface>().navMeshData? true: false) + " NavMeshSurfuce: " +GameObject.Find("LevelMaker").GetComponent<NavMeshSurface>().enabled+ "ActiveSurfuces: "+GameObject.Find("LevelMaker").GetComponent<NavMeshSurface>());
        //_bikeOperator.GetComponent<NavMeshAgent>().isStopped = false;
        _bikeOperator.GetComponent<VehicleMovement>().ready = true;
        _bikeOperator.transform.Find("CameraHolder").GetComponent<BikeAudio>().playSound = true;
        gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!EverythingLoaded){
            SpinningObject.transform.rotation= new Quaternion(0,0,Mathf.Lerp(360, 0, t), 0);
            t += 15f * Time.deltaTime;
            if (t > 360f)
            {
                t = 0;
            }
        }
        else
        {
            animator.SetTrigger("Fade");
        }
    }
}
