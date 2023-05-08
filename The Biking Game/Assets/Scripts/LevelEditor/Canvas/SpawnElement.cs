using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnElement : MonoBehaviour
{
    [SerializeField] public GameObject SpawnPart;
    [SerializeField] public GameObject Parent;
    private GameObject _spawn;
    [SerializeField] public string GameObjectlocation {set{_gameObjectlocation = value;}}
    [SerializeField] private string _gameObjectlocation;


    public void Spawn(){
        if(Parent != null){
            GameObject _spawn = Instantiate(SpawnPart, Parent.transform.Find(_gameObjectlocation).transform);
            Button btn = GameObject.Find("OptionMenu/"+_gameObjectlocation).transform.GetChild(0).GetComponent<Button>();
            
            btn.onClick.AddListener(() => _spawn.GetComponent<BlockRotation>().SetRotation());
            _spawn.name = SpawnPart.name;
            if(_gameObjectlocation == "Waypoint"){
                _spawn.AddComponent<MeshFilter>();
                _spawn.AddComponent<MeshRenderer>();
                _spawn.GetComponent<VehiclePath>().EditorMeshMaker();
            }
            //_baseQuestion.name = jsonBlockInfo.baseQuestionName;
            _spawn.transform.parent = Parent.transform.Find(_gameObjectlocation).transform;
            LevelSize levelSize = GameObject.Find("LevelEditor").GetComponent<LevelSize>();
            int index = levelSize.tiles.FindIndex(x => x.tile == Parent);
            if(_gameObjectlocation == "Question"){
                levelSize.tiles[index]._baseQuestion = _spawn;
            }
            else if(_gameObjectlocation == "Waypoint"){
                levelSize.tiles[index]._wayPoints = _spawn;
            }
        }
        else
        {
            RaycastHit hit;
            if(Physics.Raycast(Input.mousePosition, Vector3.down, out hit)){
                Destroy(hit.collider);
            }
            GameObject baseBlock =Instantiate(SpawnPart, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            baseBlock.name = SpawnPart.name;
            //BaseBlock.GetComponent<DragAndDropObject>().UpdateBlockHold();
            baseBlock.transform.position = new Vector3(baseBlock.transform.position.x, 20, baseBlock.transform.position.z);
            baseBlock.AddComponent<DragAndDropObject>();
        }
    }
}
