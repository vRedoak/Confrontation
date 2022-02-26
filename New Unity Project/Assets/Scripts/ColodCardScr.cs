using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColodCardScr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    Camera MainCamera;
    Vector3 offset;
    public Transform DefaultParent, DefaultTempCardParent;
    GameObject TempCardGO;
    int startID;

    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        TempCardGO = GameObject.Find("TempCardGO");
    
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
        DefaultParent  = transform.parent;
        DefaultTempCardParent = transform.parent;
        Debug.Log(DefaultTempCardParent.GetChild(0) + "");
        startID = transform.GetSiblingIndex();

        TempCardGO.transform.SetParent(DefaultParent);
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        transform.position = newPos + offset;
        if (TempCardGO.transform.parent != DefaultTempCardParent)
            TempCardGO.transform.SetParent(DefaultTempCardParent);

        //if (DefaultParent.GetComponent<DropPlaceScr>().Type != FieldType.SELF_FIELD)
        CheckPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());
        
        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGO.transform.localPosition = new Vector3(2303, 0);

        //transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());


    }

    void CheckPosition()
    {
        int newIndex = DefaultTempCardParent.childCount;

        for (int i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            if (transform.position.y > DefaultTempCardParent.GetChild(i).position.y)
            {
                newIndex = i;
                if (TempCardGO.transform.GetSiblingIndex() < newIndex)
                    newIndex--;
                break;
            }
        }
        if (TempCardGO.transform.parent == DefaultParent)
            newIndex = startID;
        TempCardGO.transform.SetSiblingIndex(newIndex);
    }

    //public void MoveToField(Transform field)
    //{
    //    transform.DOMove(field.position, .5f);
    //}
    //public void MoveToTarget(Transform target)
    //{
    //    StartCoroutine(MoveToTargetCor(target));
    //}
    //IEnumerator MoveToTargetCor(Transform target)
    //{
    //    Vector3 pos = transform.position;
    //    Transform parent = transform.parent;
    //    int index = transform.GetSiblingIndex();

    //    transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = false;

    //    transform.SetParent(GameObject.Find("Canvas").transform);

    //    transform.DOMove(target.position, .25f);
    //    yield return new WaitForSeconds(.25f);
    //    transform.DOMove(pos, .25f);
    //    yield return new WaitForSeconds(.25f);

    //    transform.SetParent(parent);
    //    transform.SetSiblingIndex(index);
    //    transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = true;
    //}

}
