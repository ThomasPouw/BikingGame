using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSingleOption : MonoBehaviour
{
    public GameObject BaseBlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnBlock(){
        RaycastHit hit;
        if(Physics.Raycast(Input.mousePosition, Vector3.down, out hit)){
            Destroy(hit.collider);
        }
        Instantiate(BaseBlock, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        //BaseBlock.GetComponent<DragAndDropObject>().UpdateBlockHold();
        BaseBlock.GetComponent<DragAndDropObject>().enabled = true;
        BaseBlock.GetComponent<CanvasMenuAppear>().enabled = false;
    }
}
