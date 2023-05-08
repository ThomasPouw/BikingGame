using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasPanelDestroy : MonoBehaviour
{
    private void OnMouseDown() {
        if(Input.GetButtonDown("Fire2")){
            Destroy(gameObject.transform.parent);
        }
    }
}
