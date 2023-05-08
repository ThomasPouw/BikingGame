using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockInfo
{
    public float X;
    public float Z;
    //[SerializeField] private BlockRotation _blockRotation;
    [SerializeField] public GameObject _wayPoints;
    [SerializeField] public GameObject _baseQuestion;
    [SerializeField] public Rotation _blockRotation;
    [SerializeField] public Rotation _waypointRotation;
    [SerializeField] public Rotation _questionRotation;
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
        jsonBlockInfo.blockRotation = tile.GetComponent<BlockRotation>()._rotation;
        if(_baseQuestion != null){
            jsonBlockInfo.baseQuestionName = _baseQuestion.name;
            jsonBlockInfo.questionRotation = _baseQuestion.GetComponent<BlockRotation>()._rotation;
        }
        if(_wayPoints != null){
            jsonBlockInfo.wayPointName = _wayPoints.name;
            jsonBlockInfo.wayPointRotation = _wayPoints.GetComponent<BlockRotation>()._rotation;
        }
        jsonBlockInfo.tileName = tile.name;
        
        jsonBlockInfo.x = X;
        jsonBlockInfo.z = Z;
        return jsonBlockInfo;
    }
}
