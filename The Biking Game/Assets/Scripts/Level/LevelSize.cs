using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using TMPro;

public class LevelSize : MonoBehaviour
{
    public string levelName;
    public int xMax;
    public int zMax;
    public float blockSize;
    public GameObject Plate;
    public List<BlockInfo> tiles = new List<BlockInfo>();
    public List<JsonBlockInfo> alreadyPlacedTilesJSON = new List<JsonBlockInfo>();
    public List<BlockInfo> alreadyPlacedTiles = new List<BlockInfo>();
    public NavMeshSurface surfaces;

    [Header("LevelEditor Only")]
    public TMP_InputField xMaxText;
    public TMP_InputField zMaxText;
    
    // Start is called before the first frame update
    void Start()
    {
        surfaces.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        //MakeLevel();
    }
    public void SetSize()
    {
        xMax = int.Parse(xMaxText.text);
        zMax = int.Parse(zMaxText.text);
        MakeLevel();
    }
    public void MakeLevel()
    {
        foreach (BlockInfo t in tiles)
        {
            if(t.X > xMax-1 || t.Z > zMax-1){
                Destroy(t.tile);
            }
        }
        Debug.Log(tiles.RemoveAll(s => s == null));
        for(int x = 0; x < xMax; x++){
            for (int z = 0; z < zMax; z++)
            {
                BlockInfo blockInfo = null;
                GameObject tile;
                if(tiles.Find(t => t.X == x && t.Z == z) == null){
                    Debug.Log(tiles.Find(t => t.X == x && t.Z == z));
                    
                    JsonBlockInfo APTJSON = alreadyPlacedTilesJSON.Find(t => t.x == x && t.z == z);
                    if(APTJSON != null){
                        MakeBlock(APTJSON, x, z);
                    }
                    else
                    {
                        tile = Instantiate(Plate, new Vector3(this.transform.position.x +x*blockSize, this.transform.position.y, this.transform.position.z +z*blockSize), this.transform.rotation);
                        blockInfo = new BlockInfo(tile, x, z);
                        tile.name = Plate.name;
                        tile.transform.parent = transform;
                    }
                    tiles.Add(blockInfo);
                }  
            }
        }
    }
    public BlockInfo MakeBlock(JsonBlockInfo jsonBlockInfo, int x, int z){
        GameObject tile =tile = Instantiate((GameObject)Resources.Load("Prefab/Roads/"+ jsonBlockInfo.tileName), new Vector3(this.transform.position.x +x*blockSize, this.transform.position.y, this.transform.position.z +z*blockSize), this.transform.rotation);
        tile.name = jsonBlockInfo.tileName;
        GameObject _wayPoints = null;
        GameObject _baseQuestion = null;
        if(jsonBlockInfo.baseQuestionName != null){
            _baseQuestion = (GameObject)Instantiate(Resources.Load("Prefab/Question/"+ jsonBlockInfo.baseQuestionName), tile.transform.Find("Question"));
            _baseQuestion.name = jsonBlockInfo.baseQuestionName;
            _baseQuestion.transform.parent = tile.transform.Find("Question").transform;
        }
        if(jsonBlockInfo.wayPointName != null){
            _wayPoints = (GameObject)Instantiate(Resources.Load("Prefab/Waypoint/"+ jsonBlockInfo.wayPointName), tile.transform.Find("Waypoint"));
            _wayPoints.name = jsonBlockInfo.wayPointName;
            _wayPoints.transform.parent = tile.transform.Find("Waypoint").transform;
        }
        
        tile.GetComponent<BlockRotation>().SetRotation(jsonBlockInfo.rotation);
        tile.transform.parent = transform;
        return new BlockInfo(tile, x, z, _wayPoints, _baseQuestion, jsonBlockInfo.rotation);
    }
}
