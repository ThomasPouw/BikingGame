using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Vehicle: MonoBehaviour
{
    public VehicleType vehicleType;
    public GameObject vehicle;
    public float waitEndTime;
    public float waitStartTime;
    public float Speed;
    public float Acceleration;
    public VehiclePath vehiclePath;
    public VehicleMovement vehicleMovement;
    public NavMeshAgent navMeshAgent;
}


