using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private BagView bagView;
    public ItemName itemInfo;
    private Transform originalSlot;
    private Vector3 originalPosition;
    private int index;
    public void Init(BagView bagView,int index,ItemName itemInfo,Transform solt,GameObject item)
    {
        this.bagView = bagView;
        this.index = index;
        this.itemInfo = itemInfo;
        this.originalSlot = solt;

        Sprite iconsprite = ResourceMgr.Instance.ResLoadAsset<Sprite>(itemInfo.Icon);
        Image image=item.GetComponent<Image>();
        image.sprite = iconsprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(bagView.uiBase.transform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position=eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Transform newSlot = FindDropSlot(eventData);
        if(newSlot != null && newSlot !=originalSlot)
        {
            BagItem targetItem = GetItemInSlot(newSlot);
            if (targetItem != null)
            {
                SwapItems(targetItem);
            }
            else
            {
                MoveToNewSlot(newSlot);
            }
        }
        else
        {
            ReturnToOriginalSlot();
        }
    }

    private Transform FindDropSlot(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData,results);
        foreach(var result  in results)
        {
            if (result.gameObject.transform.IsChildOf(bagView.itemParent))
            {
                return result.gameObject.transform;
            }
        }
        return null;
    }
    private BagItem GetItemInSlot(Transform slot)
    {
        if (slot.childCount > 0)
        {
            return slot.GetChild(0).GetComponent<BagItem>();
        }
        return null;
    }
    private void SwapItems(BagItem targetItem)
    {
        Transform tempSlot=originalSlot;
        ReturnToSlot(targetItem.originalSlot);
        targetItem.ReturnToSlot(tempSlot);
        bagView.SwapItemData(this, targetItem);

        int tempIndex = index;
        index=targetItem.index;
        targetItem.index=tempIndex;
    }

    private void ReturnToOriginalSlot()
    {
        ReturnToSlot(originalSlot);
    }

    private void MoveToNewSlot(Transform newSlot)
    {
        originalSlot = newSlot;
        ReturnToSlot(newSlot);
    }

    public void ReturnToSlot(Transform slot)
    {
        originalSlot=slot;
        transform.SetParent(originalSlot);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
