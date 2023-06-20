using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    public float sensX;
    public float sensY;


    public float maxAngle;
    float xMovement;
    float yMovement;
    // Start is called before the first frame update
    void Start()
    {
        xMovement = transform.position.x;
        yMovement = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float Y = transform.position.y + Input.mouseScrollDelta.y;
        Y= Mathf.Clamp(Y, _minY, _maxY);
        
        if(Input.GetButton("Fire2")){
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yMovement -= mouseY;

            xMovement += mouseX;
            transform.position = new Vector3(xMovement, Y, yMovement);
        }
        else{
            transform.position = new Vector3(transform.position.x, Y, transform.position.z);
        }
    }
}
