using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//public enum FieldType
//{
//    SELF_HAND,
//    SELF_FIELD,
//    ENEMY_HAND,
//    ENEMY_FIELD
//}

public class DropPlaceKolodScr : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    public void OnDrop(PointerEventData eventData)

    {
        ColodCardScr card = eventData.pointerDrag.GetComponent<ColodCardScr> ();
        if (card )
        {
            card.DefaultParent = transform;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (eventData.pointerDrag == null)
        {
            return;
        }
        ColodCardScr card = eventData.pointerDrag.GetComponent<ColodCardScr>();

        if (card)
        {
            card.DefaultTempCardParent = transform;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }
        ColodCardScr card = eventData.pointerDrag.GetComponent<ColodCardScr>();

        if (card && card.DefaultTempCardParent == transform)
        {
            card.DefaultTempCardParent = card.DefaultParent;
        }
    }
}
