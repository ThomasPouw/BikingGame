using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonBlockInfo
{
    public float x;
    public float z;
    public string tileName;
    public string wayPointName;
    public string baseQuestionName;
    public Rotation blockRotation;
    public Rotation wayPointRotation;
    public Rotation questionRotation;
}
