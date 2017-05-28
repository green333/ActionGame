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

public class LoadItemMaster : TextMasterManager
{
    const string filename = "Assets/Resources/MasterData/アイテママスタ.txt";

    /// <summary>
    /// 指定した名前に一致するアイテム情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ItemMaster.Param GetItemInfo(string name)
    {

        return null;
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
