using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Y = transform.position.y + Input.mouseScrollDelta.y;
        Y= Mathf.Clamp(Y, _minY, _maxY);
        transform.position = new Vector3(transform.position.x, Y, transform.position.z);
    }
    
}
