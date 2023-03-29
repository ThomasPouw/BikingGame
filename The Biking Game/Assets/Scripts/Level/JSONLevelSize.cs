using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JSONLevelSize
{
    public string levelName;
    public int xMax;
    public int yMax;
    public float blockSize;
    public List<JsonBlockInfo> alreadyPlacedTilesJSON = new List<JsonBlockInfo>();
    public JSONLevelSize(LevelSize levelSize){
        levelName = levelSize.levelName;
        xMax = levelSize.xMax;
        yMax = levelSize.yMax;
        blockSize = levelSize.blockSize;
        alreadyPlacedTilesJSON = levelSize.alreadyPlacedTilesJSON;
    }
}
