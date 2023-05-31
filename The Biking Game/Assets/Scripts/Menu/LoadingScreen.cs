using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject SpinningObject;
    [SerializeField] public bool EverythingLoaded;
    [SerializeField] Animator animator;
    float t = 0.0f;
    // Start is called before the first frame update
    GameObject _bikeOperator;
    private void Awake() {
        _bikeOperator = GameObject.Find("BikeOperator");
        _bikeOperator.GetComponent<VehicleMovement>().ready = false;
        //_bikeOperator.GetComponent<NavMeshAgent>().isStopped = true;
    }
    public void StartBike()
    {
        _bikeOperator.GetComponent<VehicleMovement>().ready = true;
        //_bikeOperator.GetComponent<NavMeshAgent>().isStopped = false;
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
