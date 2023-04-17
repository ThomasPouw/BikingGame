using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginLevel : MonoBehaviour
{

    private void OnEnable() {
        GameObject bike = GameObject.Find("BikeOperator");
        bike.transform.position = transform.position;
    }
    
}
