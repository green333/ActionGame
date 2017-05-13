using UnityEngine;
using System.Collections;

/// <summary>
/// アイテム情報
/// </summary>
public class ItemInfo
{
    public ItemMaster.Param param;
    public int num;
}

public class LoadItemMaster : BaseSingleton<LoadItemMaster>
{
    /// <summary>
    /// アイテムマスタを格納する変数
    /// </summary>
    ItemMaster master;

    /// <summary>
    /// 初期化時にアイテムマスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if (null == (master = Resources.Load<ItemMaster>("MasterData/ItemMaster")))
        {
            Debug.Log("failed to Resources.Load<ItemMaster>");
        }
    }

    /// <summary>
    /// 指定した名前に一致するアイテム情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ItemMaster.Param GetItemInfo(string name)
    {
        int index = 0;

        foreach(ItemMaster.Param param in master.list)
        {
            if(param.name == name)
            {
                break;
            }
            ++index;
        }

        ItemMaster.Param temp = new ItemMaster.Param();
        temp.name   = master.list[index].name;
        temp.id     = master.list[index].id;
        temp.kind   = master.list[index].kind;
        temp.effect = master.list[index].effect;
        temp.desc   = master.list[index].desc;

        return temp;
    }
    
    /// <summary>
    /// アイテムパラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(ItemMaster.Param param)
    {
        Debug.Log("id     = " + param.id);
        Debug.Log("name   = " + param.name);
        Debug.Log("kind   = " + param.kind);
        Debug.Log("effect = " + param.effect);
        Debug.Log("desc   = " + param.desc);
    }
}
