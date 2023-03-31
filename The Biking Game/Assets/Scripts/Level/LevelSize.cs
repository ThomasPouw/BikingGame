using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class LevelSize : MonoBehaviour
{
    public LevelStorage levelStorage;
    public string levelName;
    public int xMax;
    public int yMax;
    public float blockSize;
    public GameObject Plate;
    public List<BlockInfo> tiles = new List<BlockInfo>();
    public List<JsonBlockInfo> alreadyPlacedTilesJSON = new List<JsonBlockInfo>();
    //public List<BlockInfo> alreadyPlacedTiles = new List<BlockInfo>();
    public NavMeshSurface surfaces;
    
    // Start is called before the first frame update
    void Start()
    {
        surfaces.BuildNavMesh();
    }

    private void OnEnable() {
        levelStorage = GameObject.Find("Storage").GetComponent<LevelStorage>();
        JSONLevelSize jSONLevelSize = levelStorage.ReadLevel(StaticMachine.menuInfo.levelName);
        SetValue(jSONLevelSize);
    }
    private void OnDrawGizmos() {
        if(tiles.Count != xMax*yMax)
        {
            foreach (BlockInfo t in tiles)
            {
                DestroyImmediate(t.tile);
            }
            tiles = new List<BlockInfo>();
            for(int x = 0; x < xMax; x++){
                for (int z = 0; z < yMax; z++)
                {
                    JsonBlockInfo APTJSON = alreadyPlacedTilesJSON.Find(t => t.x == x && t.z == z);
                    BlockInfo blockInfo = null;
                    GameObject tile;
                    if(APTJSON != null){
                       blockInfo = MakeBlock(APTJSON, x, z);
                       //blockInfo = new BlockInfo(tile, i, ii);
                       //blockInfo.setBlockInfo(APTJSON);
                    }
                    else
                    {
                        //BlockInfo APT = alreadyPlacedTiles.Find(t => t.X == i && t.Z == ii);
                        //if(APT != null){
                            //tile = Instantiate(APT.tile, new Vector3(this.transform.position.x +i*blockSize, this.transform.position.y, this.transform.position.z +ii*blockSize), this.transform.rotation);

                            
                       // }
                        //else{
                            tile = Instantiate(Plate, new Vector3(this.transform.position.x +x*blockSize, this.transform.position.y, this.transform.position.z +z*blockSize), this.transform.rotation);
                            
                        //}
                        blockInfo = new BlockInfo(tile, x, z);
                        tile.transform.parent = transform;
                        
                    }
                    tiles.Add(blockInfo);
                }
            }
        }
    }
    public void SetValue(JSONLevelSize jsonLevelSize)
    {
        levelName = jsonLevelSize.levelName;
        alreadyPlacedTilesJSON = jsonLevelSize.alreadyPlacedTilesJSON;
        xMax = jsonLevelSize.xMax;
        yMax = jsonLevelSize.yMax;
        MakeLevel();
    }
    public BlockInfo MakeBlock(JsonBlockInfo jsonBlockInfo, int x, int z){
        GameObject tile =tile = Instantiate((GameObject)Resources.Load("Prefab/Roads/"+ jsonBlockInfo.tileName), new Vector3(this.transform.position.x +x*blockSize, this.transform.position.y, this.transform.position.z +z*blockSize), this.transform.rotation);
        tile.name = jsonBlockInfo.tileName;
        GameObject _wayPoints = null;
        GameObject _baseQuestion = null;
        if(jsonBlockInfo.baseQuestionName != null){
            _baseQuestion = (GameObject)Instantiate(Resources.Load("Prefab/Questions/"+ jsonBlockInfo.baseQuestionName), tile.transform.Find("Question"));
            _baseQuestion.name = jsonBlockInfo.baseQuestionName;
            _baseQuestion.transform.parent = tile.transform.Find("Question").transform;
            _baseQuestion.GetComponent<BlockRotation>().SetRotation(jsonBlockInfo.questionRotation);
        }
        if(jsonBlockInfo.wayPointName != null){
            _wayPoints = (GameObject)Instantiate(Resources.Load("Prefab/BikeWayPoints/"+ jsonBlockInfo.wayPointName), tile.transform.Find("Waypoint"));
            _wayPoints.name = jsonBlockInfo.wayPointName;
            _wayPoints.transform.parent = tile.transform.Find("Waypoint").transform;
            _wayPoints.GetComponent<BlockRotation>().SetRotation(jsonBlockInfo.wayPointRotation);
        }
        
        tile.GetComponent<BlockRotation>().SetRotation(jsonBlockInfo.blockRotation);
        tile.transform.parent = transform;
        return new BlockInfo(tile, x, z, _wayPoints, _baseQuestion, jsonBlockInfo.blockRotation, jsonBlockInfo.wayPointRotation, jsonBlockInfo.questionRotation);
    }
    public void MakeLevel()
    {
        if(tiles.Count != xMax*yMax)
        {
            foreach (BlockInfo t in tiles)
            {
                Destroy(t.tile);
            }
            tiles = new List<BlockInfo>();
            for(int x = 0; x < xMax; x++){
                for (int z = 0; z < yMax; z++)
                {
                    JsonBlockInfo APTJSON = alreadyPlacedTilesJSON.Find(t => t.x == x && t.z == z);
                    BlockInfo blockInfo = null;
                    GameObject tile;
                    if(APTJSON != null){
                       MakeBlock(APTJSON, x, z);
                    }
                    else
                    {
                        tile = Instantiate(Plate, new Vector3(this.transform.position.x +x*blockSize, this.transform.position.y, this.transform.position.z +z*blockSize), this.transform.rotation);
                        blockInfo = new BlockInfo(tile, x, z);
                        tile.transform.parent = transform;  
                    }
                    tiles.Add(blockInfo);
                }
            }
        }
    }
}
