using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class LevelSize : MonoBehaviour
{
    public int xMax;
    public int yMax;
    public float blockSize;
    public GameObject Plate;
    public List<Tiles> tiles = new List<Tiles>();
    public List<Tiles> alreadyPlacedTiles = new List<Tiles>();
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
            foreach (Tiles t in tiles)
            {
                Destroy(t.Tile);
            }
            tiles = new List<Tiles>();
            for(int i = 0; i < xMax; i++){
                for (int ii = 0; ii < yMax; ii++)
                {
                    Tiles APT = alreadyPlacedTiles.Find(t => t.X == i && t.Z == ii);
                    GameObject tile = null;
                    if(APT != null){
                       tile = Instantiate(APT.Tile, new Vector3(this.transform.position.x +i*blockSize, this.transform.position.y, this.transform.position.z +ii*blockSize), this.transform.rotation); 
                    }
                    else
                    {
                        tile = Instantiate(Plate, new Vector3(this.transform.position.x +i*blockSize, this.transform.position.y, this.transform.position.z +ii*blockSize), this.transform.rotation);
                    }
                    tile.transform.parent = transform;
                    tiles.Add(new Tiles(tile, i, ii));
                }
            }
        }
    }
    private void OnDrawGizmos() {
        if(tiles.Count != xMax*yMax)
        {
            foreach (Tiles t in tiles)
            {
                DestroyImmediate(t.Tile);
            }
            tiles = new List<Tiles>();
            for(int i = 0; i < xMax; i++){
                for (int ii = 0; ii < yMax; ii++)
                {
                    Gizmos.color = Color.blue;
                    
                    Tiles APT = alreadyPlacedTiles.Find(t => t.X == i && t.Z == ii);
                    Debug.Log(APT);
                    GameObject tile;
                    if(APT != null){
                       tile = Instantiate(APT.Tile, new Vector3(this.transform.position.x +i*blockSize, this.transform.position.y, this.transform.position.z +ii*blockSize), APT.Tile.transform.rotation); 
                    }
                    else{
                        tile = Instantiate(Plate, new Vector3(this.transform.position.x +i*blockSize, this.transform.position.y, this.transform.position.z +ii*blockSize), this.transform.rotation);
                    }
                    tiles.Add(new Tiles(tile, i, ii));
                    Gizmos.DrawSphere(new Vector3(this.transform.position.x +i*blockSize, this.transform.position.y, this.transform.position.z +ii*blockSize),2f);
                }
            }
        }
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
