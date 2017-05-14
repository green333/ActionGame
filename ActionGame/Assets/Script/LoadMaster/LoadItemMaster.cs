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
    ItemMaster master = null;

    /// <summary>
    /// 初期化時にアイテムマスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if (null == (master = Resources.Load<ItemMaster>("MasterData/ItemMaster")))
        {
            LogExtensions.OutputError("failed to resources load item master");
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
        bool bChecked = false;

        foreach(ItemMaster.Param param in master.list)
        {
            if(param.name == name)
            {
                bChecked = true;
                break;
            }
            ++index;
        }

        if(bChecked == false)
        {
            // 指定した名前に一致するデータがない
            LogExtensions.OutputWarn("there is no item master matching the There is no data matching the specified name, name = " + name);
            return null;
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
        LogExtensions.OutputInfo("id     = " + param.id);
        LogExtensions.OutputInfo("name   = " + param.name);
        LogExtensions.OutputInfo("kind   = " + param.kind);
        LogExtensions.OutputInfo("effect = " + param.effect);
        LogExtensions.OutputInfo("desc   = " + param.desc);
    }
}
