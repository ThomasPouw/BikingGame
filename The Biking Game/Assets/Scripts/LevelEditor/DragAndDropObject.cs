using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropObject : MonoBehaviour
{
    private Vector3 screenPoint;
	private Vector3 offset;
    [SerializeField]private bool holdingItem;
		
	void OnMouseDown(){
		UpdateBlockHold();
	}
    private void OnMouseUp() {
        UpdateBlockHold();
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit)){
            GameObject replaced = hit.collider.gameObject;
            LevelSize levelSize = GameObject.Find("LevelEditor").GetComponent<LevelSize>();
            int index = levelSize.tiles.FindIndex(x => x.tile == replaced);
            levelSize.tiles.Insert(index, new BlockInfo(gameObject, levelSize.tiles[index].X, levelSize.tiles[index].Z));

            transform.position = replaced.transform.position;
            transform.parent = GameObject.Find("LevelEditor").transform;
            Destroy(replaced);
            levelSize.tiles.RemoveAt(index+1);
            GetComponent<CanvasMenuAppear>().enabled = true;
            enabled = false;
        }
    }
    private void Update() {
        if(holdingItem){
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		    transform.position = cursorPosition;
        }
    }
    public void UpdateBlockHold(){
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        holdingItem = !holdingItem;
    }
}
