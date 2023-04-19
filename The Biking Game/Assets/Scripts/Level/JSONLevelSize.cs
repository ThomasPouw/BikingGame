using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JSONLevelSize
{
    public string levelName;
    public int xMax;
    public int zMax;
    public float blockSize;
    public List<JsonBlockInfo> alreadyPlacedTilesJSON = new List<JsonBlockInfo>();
    public JSONLevelSize(LevelSize levelSize){
        levelName = levelSize.levelName;
        xMax = levelSize.xMax;
        zMax = levelSize.zMax;
        blockSize = levelSize.blockSize;
        List<JsonBlockInfo> jsonBlockInfo = new List<JsonBlockInfo>();
        for (int i = 0; i < levelSize.tiles.Count; i++)
        {
            jsonBlockInfo.Add(levelSize.tiles[i].getJsonBlockInfo());
        }
        alreadyPlacedTilesJSON = jsonBlockInfo;
    }
}
