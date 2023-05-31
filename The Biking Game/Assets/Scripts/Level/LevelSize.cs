using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using TMPro;

public class LevelSize : MonoBehaviour
{
    
    //public LevelStorage levelStorage;
    public string levelName;
    public int xMax;
    public int zMax;
    public float blockSize;
    public GameObject Plate;
    public List<BlockInfo> tiles = new List<BlockInfo>();
    public List<JsonBlockInfo> alreadyPlacedTilesJSON = new List<JsonBlockInfo>();
    //public List<BlockInfo> alreadyPlacedTiles = new List<BlockInfo>();
    public float totalPossiblePoints;
    public NavMeshSurface surfaces;
    public LoadingScreen loadingScreen;

    [Header("LevelEditor Only info")]
    public TMP_InputField xMaxText;
    public TMP_InputField zMaxText;
    public bool skipMakeLevel = false;
    public bool panelActivate = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //MakeLevel();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetSize()
    {
        xMax = xMaxText.text != "" ? int.Parse(xMaxText.text) : 0;
        zMax = zMaxText.text != "" ? int.Parse(zMaxText.text) : 0;
        panelActivate = true;
        if(skipMakeLevel)
        return;
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
        tiles.RemoveAll(t => t.X > xMax-1 || t.Z > zMax-1);
        for(int x = 0; x < xMax; x++){
            for (int z = 0; z < zMax; z++)
            {
                BlockInfo blockInfo = null;
                GameObject tile;

                if(tiles.Find(t => t.X == x && t.Z == z) == null)
                {
                    JsonBlockInfo APTJSON = alreadyPlacedTilesJSON.Find(t => t.x == x && t.z == z);
                    if(APTJSON != null){
                        blockInfo = MakeBlock(APTJSON, x, z);
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
        StartCoroutine(BuildNavmesh());
        if(panelActivate || skipMakeLevel){
            foreach(BlockInfo blockInfo in tiles){
                if(blockInfo.tile.GetComponent<CanvasMenuAppear>() == null)
                {
                    blockInfo.tile.AddComponent<CanvasMenuAppear>();
                }
            }
        }
    }
    IEnumerator BuildNavmesh(){
        yield return new WaitForSeconds(0.1f);
        surfaces.BuildNavMesh();
        if(loadingScreen != null){
            loadingScreen.EverythingLoaded = true;
        }
    }
    public void SetValue(JSONLevelSize jsonLevelSize)
    {
        levelName = jsonLevelSize.levelName;
        alreadyPlacedTilesJSON = jsonLevelSize.alreadyPlacedTilesJSON;
        Debug.Log("jsonLevelSize.alreadyPlacedTilesJSON: "+jsonLevelSize.alreadyPlacedTilesJSON.Count);
        blockSize = jsonLevelSize.blockSize;
        xMax = jsonLevelSize.xMax;
        zMax = jsonLevelSize.zMax;
        MakeLevel();
    }
    public BlockInfo MakeBlock(JsonBlockInfo jsonBlockInfo, int x, int z){
        GameObject tile;
        Debug.Log(jsonBlockInfo.tileName);
        if(jsonBlockInfo.tileName.Contains("Deco")){
            tile = Instantiate((GameObject)Resources.Load("Prefab/Decoration/"+ jsonBlockInfo.tileName), new Vector3(this.transform.position.x +x*blockSize, this.transform.position.y, this.transform.position.z +z*blockSize), this.transform.rotation);
            tile.name = jsonBlockInfo.tileName;
            tile.GetComponent<BlockRotation>().SetRotation(jsonBlockInfo.blockRotation);
            tile.transform.parent = transform;
            return new BlockInfo(tile,jsonBlockInfo.blockRotation, x, z);
        }
        else{
            tile= Instantiate((GameObject)Resources.Load("Prefab/Roads/"+ jsonBlockInfo.tileName), new Vector3(this.transform.position.x +x*blockSize, this.transform.position.y, this.transform.position.z +z*blockSize), this.transform.rotation);
            tile.name = jsonBlockInfo.tileName;
            GameObject _wayPoints = null;
            GameObject _baseQuestion = null;
            if(jsonBlockInfo.baseQuestionName != ""){
                _baseQuestion = (GameObject)Instantiate(Resources.Load("Prefab/Question/"+ jsonBlockInfo.baseQuestionName), tile.transform.Find("Question"));
                _baseQuestion.name = jsonBlockInfo.baseQuestionName;
                _baseQuestion.transform.parent = tile.transform.Find("Question").transform;
                _baseQuestion.GetComponent<BlockRotation>().SetRotation(jsonBlockInfo.questionRotation);
            }
            if(jsonBlockInfo.wayPointName != ""){
                _wayPoints = (GameObject)Instantiate(Resources.Load("Prefab/Waypoint/"+ jsonBlockInfo.wayPointName), tile.transform.Find("Waypoint"));
                _wayPoints.name = jsonBlockInfo.wayPointName;
                _wayPoints.transform.parent = tile.transform.Find("Waypoint").transform;
                _wayPoints.GetComponent<BlockRotation>().SetRotation(jsonBlockInfo.wayPointRotation);
            }
        
            tile.GetComponent<BlockRotation>().SetRotation(jsonBlockInfo.blockRotation);
            tile.transform.parent = transform;
            return new BlockInfo(tile, x, z, _wayPoints, _baseQuestion, jsonBlockInfo.blockRotation, jsonBlockInfo.wayPointRotation, jsonBlockInfo.questionRotation);
        }
        
    }
    
}
