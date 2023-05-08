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
        popUpPanel = (GameObject)Resources.Load(Application.dataPath + "Prefab/Menu/OptionMenu.prefab");
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
            MenuPanel = Instantiate(popUpPanel);
            MenuPanel.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            //Testing.GetComponent<PanelControler>().setClickableEvent(updateClickable);

            MenuPanel.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Menu").position);
            ElementListAppear[] spawnElements = MenuPanel.GetComponentsInChildren<ElementListAppear>();
            AllowedElements allowedElements = GetComponent<AllowedElements>();
            foreach (ElementListAppear spawnElement in spawnElements)
            {
                Debug.Log(spawnElement.gameObject.tag);
                spawnElement.Parent = gameObject;
                Button btn;
                switch (spawnElement.gameObject.tag)
                {
                    case("Road"):
                        btn = spawnElement.transform.GetComponent<Button>();
                        btn.onClick.AddListener(() => GetComponent<BlockRotation>().SetRotation());
                    break;
                    case("Waypoint"):
                        spawnElement.SpawnList = allowedElements.AllowedWayPoints;
                       
                    break;
                    case("Question"):
                        spawnElement.SpawnList = allowedElements.AllowedQuestions;
                    break;
                }
            }
            popupStorage.panel = MenuPanel;    
            //Needs fixing 
    }
    

}
