using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockInfo
{
    public float X;
    public float Z;
    [SerializeField] private BlockRotation _blockRotation;
    [SerializeField] private GameObject _wayPoints;
    [SerializeField] private GameObject _baseQuestion;
    [SerializeField] private Rotation _rotation;
    public GameObject tile;
    public BlockInfo(GameObject Tile, float x, float z){
        tile= Tile;
        X= x;
        Z = z;
    }
    public BlockInfo(GameObject Tile, float x, float z, GameObject WayPoints, GameObject Question, Rotation rotation){
        tile= Tile;
        X= x;
        Z = z;
        _wayPoints = WayPoints;
        _baseQuestion = Question;
        _rotation = rotation;
    }
    
    public JsonBlockInfo getJsonBlockInfo(){
        JsonBlockInfo jsonBlockInfo = new JsonBlockInfo();
        jsonBlockInfo.rotation = _rotation;
        jsonBlockInfo.baseQuestionName = _baseQuestion.name;
        jsonBlockInfo.wayPointName = _baseQuestion.name;
        jsonBlockInfo.tileName = tile.name;
        jsonBlockInfo.x = X;
        jsonBlockInfo.z = Z;
        return jsonBlockInfo;
    }
}
