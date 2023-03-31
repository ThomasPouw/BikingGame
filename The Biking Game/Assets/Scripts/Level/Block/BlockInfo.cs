using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockInfo
{
    public float X;
    public float Z;
    //[SerializeField] private BlockRotation _blockRotation;
    [SerializeField] private GameObject _wayPoints;
    [SerializeField] private GameObject _baseQuestion;
    [SerializeField] private Rotation _blockRotation;
    [SerializeField] private Rotation _waypointRotation;
    [SerializeField] private Rotation _questionRotation;
    public GameObject tile;
    public BlockInfo(GameObject Tile, float x, float z){
        tile= Tile;
        X= x;
        Z = z;
    }
    public BlockInfo(GameObject Tile, float x, float z, GameObject WayPoints, GameObject Question, Rotation blockRotation, Rotation waypointRotation, Rotation questionRotation){
        tile= Tile;
        X= x;
        Z = z;
        _wayPoints = WayPoints;
        _baseQuestion = Question;
        _blockRotation = blockRotation;
        _waypointRotation = waypointRotation;
        _questionRotation = questionRotation;
    }
    
    public JsonBlockInfo getJsonBlockInfo(){
        JsonBlockInfo jsonBlockInfo = new JsonBlockInfo();
        jsonBlockInfo.blockRotation = _blockRotation;
        jsonBlockInfo.baseQuestionName = _baseQuestion.name;
        jsonBlockInfo.wayPointName = _wayPoints.name;
        jsonBlockInfo.tileName = tile.name;
        jsonBlockInfo.wayPointRotation = _waypointRotation;
        jsonBlockInfo.questionRotation = _questionRotation;
        jsonBlockInfo.x = X;
        jsonBlockInfo.z = Z;
        return jsonBlockInfo;
    }
}
