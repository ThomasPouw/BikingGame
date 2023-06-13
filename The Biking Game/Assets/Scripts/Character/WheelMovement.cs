using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovement : MonoBehaviour
{
    public void RotateWheel(Vector3 beginPoint, Vector3 EndPoint)
    {
        Vector3 distance = EndPoint - beginPoint;
        distance = new Vector3(distance.x, 0, distance.z);
        Quaternion endRotation = Quaternion.LookRotation(distance);
        Debug.Log(endRotation);
        transform.rotation= endRotation;
    }
}