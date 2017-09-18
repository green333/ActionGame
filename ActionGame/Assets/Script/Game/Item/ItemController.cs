using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemController
{
    public enum ITEM_TYPE : int {
        HERBS = 0,
    }

    private static GameObject prefab;
    private static ItemController instance = null;
    public static ItemController Instance
    {
        get {
            if (instance == null)
            {
                instance = new ItemController();
                prefab = Resources.Load<GameObject>("Prefab/Item/Item");
                if (prefab == null) LogExtensions.OutputError("アイテムオブジェクトの読み込みに失敗しました。");
            }
            return instance;
        }
    }

    private void AddItemCommponent(GameObject obj, ITEM_TYPE type)
    {
        switch (type) {
            case ITEM_TYPE.HERBS:
                obj.AddComponent<Herbs>();
                break;
        }
    }

    public GameObject Generate(ITEM_TYPE type, Vector3 pos)
    {
        GameObject obj = Object.Instantiate(prefab) as GameObject;
        AddItemCommponent(obj, type);
        obj.transform.position = pos;
        return obj;
    }
}
