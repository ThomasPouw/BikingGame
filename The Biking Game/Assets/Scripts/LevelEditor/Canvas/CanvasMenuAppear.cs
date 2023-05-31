using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CanvasMenuAppear : MonoBehaviour
{
    [SerializeField] PopupStorage popupStorage;
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private GraphicRaycaster _graphicRaycaster;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private PointerEventData _PointerEventData;
    

    private GameObject MenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        popupStorage = GameObject.Find("LevelEditor").GetComponent<PopupStorage>();
        popUpPanel = (GameObject)Resources.Load("Prefab/Menu/OptionMenu");
        _graphicRaycaster= GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();  
        _eventSystem= GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
            if(popupStorage.panel != null){       
                   
                _PointerEventData = new PointerEventData(_eventSystem);
                _PointerEventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                _graphicRaycaster.Raycast(_PointerEventData, results);
                if(results.Count != 0)
                return;
            }
            Transform menuPosition = transform.Find("Menu");
            if(menuPosition == null){
                return;
            }
            MenuPanel = Instantiate(popUpPanel);
            MenuPanel.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            MenuPanel.transform.position = Camera.main.WorldToScreenPoint(menuPosition.position);
            ElementListAppear[] spawnElements = MenuPanel.GetComponentsInChildren<ElementListAppear>();
            AllowedElements allowedElements = GetComponent<AllowedElements>();
            Transform roadPanel = MenuPanel.transform.Find("OptionMenu").Find("Roads");
            if(roadPanel != null){
                Button btn = roadPanel.transform.GetComponent<Button>();
                btn.onClick.AddListener(() => GetComponent<BlockRotation>().SetRotation());
            }
            if(allowedElements != null){
                foreach (ElementListAppear spawnElement in spawnElements)
            {
                Debug.Log(spawnElement.gameObject.tag);
                spawnElement.Parent = gameObject;
                Button btn;
                switch (spawnElement.gameObject.tag)
                {
                    case("Waypoint"):
                        spawnElement.SpawnList = allowedElements.AllowedWayPoints;
                        if(gameObject.transform.Find("Waypoint").childCount != 0){
                            btn = spawnElement.transform.Find("Rotate").GetComponent<Button>();
                            BlockRotation blockRotation = gameObject.transform.Find("Waypoint").GetChild(0).gameObject.GetComponent<BlockRotation>();
                            if(blockRotation != null){
                                btn.onClick.AddListener(() => blockRotation.SetRotation());
                            }
                        }
                       
                    break;
                    case("Question"):
                        spawnElement.SpawnList = allowedElements.AllowedQuestions;
                        if(gameObject.transform.Find("Question").childCount != 0){
                            btn = spawnElement.transform.Find("Rotate").GetComponent<Button>();
                            BlockRotation blockRotation = gameObject.transform.Find("Question").GetChild(0).gameObject.GetComponent<BlockRotation>();
                            if(blockRotation != null){
                                btn.onClick.AddListener(() => blockRotation.SetRotation());
                            }
                        }
                    break;
                }
            }
            }
            popupStorage.panel = MenuPanel;    
            //Needs fixing 
    }
    

}
