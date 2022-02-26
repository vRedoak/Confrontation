using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickScr : MonoBehaviour, IPointerClickHandler {

    const string layerThatReactsOnMouseClick = "Card1";

    public void OnPointerClick(PointerEventData eventData)
    {

        int layer = 1 << LayerMask.NameToLayer(layerThatReactsOnMouseClick);
        var mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var origin = new Vector2(mousePosition3D.x, mousePosition3D.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f, layer);
        if (hit.collider != null)
            Debug.Log("Gameobject was hit: " + hit.collider.gameObject.name);
    

       // Kolods.SelectedCard = ;
        Debug.Log("fff"+eventData);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
