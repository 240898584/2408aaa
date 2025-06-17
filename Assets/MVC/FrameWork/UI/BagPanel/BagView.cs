using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;

public class BagView : ViewBase
{
    public Transform itemParent;
    public List<BagItem> itemobjs= new List<BagItem>();
    public override void Init(UIWindow uiBase)
    {
        base.Init(uiBase);
        itemParent = uiBase.transform.Find("ItemParent");
        InstantiateAllItems();
    }
    public BagItem GetItemInSlot(Transform slot)
    {
        if (slot.childCount > 0)
        {
            return slot.GetChild(0).GetComponent<BagItem>();
        }
        return null;
    }
    public void SwapItemData(BagItem item1,BagItem item2)
    {
        ItemName tempinfo = item1.itemInfo;
        item1.itemInfo=item2.itemInfo;
        item2.itemInfo=tempinfo;
    }
    private void InstantiateAllItems()
    {
        string path = Application.dataPath + "/data.json";
        string str = File.ReadAllText(path);
        Itemdata itemdata = JsonUtility.FromJson<Itemdata>(str);

        List<Transform> gridSlots= new List<Transform>();
        foreach(Transform child in itemParent)
        {
            gridSlots.Add(child);
        }
        for(int i = 0;i<itemdata.data.Length;i++)
        {
            Transform slot=gridSlots[i];

            GameObject itemObj = ResourceMgr.Instance.ResLoadAsset<GameObject>("Item");
            GameObject item=GameObject.Instantiate(itemObj,slot);
            item.transform.localPosition = Vector3.zero;
            BagItem bagItem=item.GetComponent<BagItem>();
            bagItem.Init(this, i, itemdata.data[i],slot,item);
            itemobjs.Add(bagItem);
        }
    }
}
[Serializable]
public class ItemName
{
    public string name;
    public int num;
    public string Icon;
}
[Serializable]
public class Itemdata
{
    public ItemName[] data;
}
