using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotation : MonoBehaviour
{
    [SerializeField]public Rotation _rotation;
    public Rotation GetRotation(){
        return _rotation;
    }
    public void SetRotation(Rotation rotation){
        _rotation = rotation;
        rotateBlock();
    }
    private void rotateBlock(){
        switch (_rotation)
        {
            case(Rotation.North):
                transform.rotation = Quaternion.Euler(0,0,0);
                break;
            case(Rotation.East):
                transform.rotation = Quaternion.Euler(0,90,0);
                break;
            case(Rotation.South):
                transform.rotation = Quaternion.Euler(0,180,0);
                break;
            case(Rotation.West):
                transform.rotation = Quaternion.Euler(0,270,0);
                break;
        }
    }
    private void Start() {
        rotateBlock();
    }
    private void OnEnable() {
        //rotateBlock();
    }
    private void OnDrawGizmos() {
        rotateBlock();
    }
}
public enum Rotation{
    North,
    East,
    South,
    West
}
