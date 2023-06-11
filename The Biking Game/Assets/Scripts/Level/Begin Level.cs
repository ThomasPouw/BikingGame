using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeginLevel : MonoBehaviour
{
    private void Start() {
        GameObject bike = GameObject.Find("BikeOperator");
        bike.transform.position = transform.position;
        bike.transform.rotation = transform.rotation;
        GameObject.Find("QuestionScreen").SetActive(false);
    }
    
}
