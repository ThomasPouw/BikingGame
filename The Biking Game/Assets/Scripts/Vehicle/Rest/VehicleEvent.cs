using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleEvent : MonoBehaviour
{
    [SerializeField] public VehicleType vehicleType;
    [SerializeField] public Vehicle vehicle;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private BaseQuestion _baseQuestion;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Vehicle>().vehicleType == vehicleType){
            _navMeshAgent = other.GetComponent<NavMeshAgent>();
            _navMeshAgent.isStopped = true;

        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.75f);
    }
    

}
public enum VehicleType{
    Bike,
    Car,
    Train,
    Padestrian
}
