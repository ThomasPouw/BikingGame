using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class LevelSize : MonoBehaviour
{
    public string levelName;
    public int xMax;
    public int yMax;
    public float blockSize;
    public GameObject Plate;
    public List<BlockInfo> tiles = new List<BlockInfo>();
    public List<JsonBlockInfo> alreadyPlacedTilesJSON = new List<JsonBlockInfo>();
    public List<BlockInfo> alreadyPlacedTiles = new List<BlockInfo>();
    public NavMeshSurface surfaces;
    
    // Start is called before the first frame update
    void Start()
    {
        surfaces.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
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
    public BlockInfo MakeBlock(JsonBlockInfo jsonBlockInfo, int x, int z){
        GameObject tile =tile = Instantiate((GameObject)Resources.Load("Prefab/Roads/"+ jsonBlockInfo.tileName), new Vector3(this.transform.position.x +x*blockSize, this.transform.position.y, this.transform.position.z +z*blockSize), this.transform.rotation);
        tile.name = jsonBlockInfo.tileName;
        GameObject _wayPoints = null;
        GameObject _baseQuestion = null;
        if(jsonBlockInfo.baseQuestionName != null){
            _baseQuestion = (GameObject)Instantiate(Resources.Load("Prefab/Questions/"+ jsonBlockInfo.baseQuestionName), tile.transform.Find("Question"));
            _baseQuestion.name = jsonBlockInfo.baseQuestionName;
            _baseQuestion.transform.parent = tile.transform.Find("Question").transform;
        }
        if(jsonBlockInfo.wayPointName != null){
            _wayPoints = (GameObject)Instantiate(Resources.Load("Prefab/BikeWayPoints/"+ jsonBlockInfo.wayPointName), tile.transform.Find("Waypoint"));
            _wayPoints.name = jsonBlockInfo.wayPointName;
            _wayPoints.transform.parent = tile.transform.Find("Waypoint").transform;
        }
        
        tile.GetComponent<BlockRotation>().SetRotation(jsonBlockInfo.rotation);
        tile.transform.parent = transform;
        return new BlockInfo(tile, x, z, _wayPoints, _baseQuestion, jsonBlockInfo.rotation);
    }
    [System.Serializable]
    public class Tiles{
        public Tiles(GameObject tile, float x, float z){
            Tile = tile;
            X = x;
            Z = z;
        }
        public float X;
        public float Z;
        public GameObject Tile;
    }
}
