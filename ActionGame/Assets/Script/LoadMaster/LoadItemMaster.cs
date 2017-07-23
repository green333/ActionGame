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
    /// <summary>
    /// 自身のインスタンス
    /// </summary>
    static LoadItemMaster _instance = new LoadItemMaster();

    /// <summary>
    /// インスタンス取得プロパティ
    /// </summary>
    static public LoadItemMaster instance { get { return _instance; } }

    /// <summary>
    /// マスターデータのファイルパス
    /// </summary>
    const string filename = "Assets/Resources/MasterData/アイテムマスタ.txt";

    /// <summary>
    /// 名前カラム
    /// </summary>
    const string COL_NAME = "name";

    /// <summary>
    /// 指定した名前に一致するアイテム情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ItemMaster.Param GetItemInfo(string name)
    {
        ItemMaster.Param param = null;
        base.Open(filename);

        string getJsonStr = base.Search(base.VariableToJson(COL_NAME, name));
        if (getJsonStr != string.Empty)
        {
            param = JsonUtility.FromJson<ItemMaster.Param>(getJsonStr);
        }
        base.Close();

        return param;
    }
    
    /// <summary>
    /// アイテムパラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(ItemInfo itemInfo)
    {
        LogExtensions.OutputInfo("[アイテムマスタ] => " +
            "[id:"      + itemInfo.param.id     + "] " +
            "[name:"    + itemInfo.param.name   + "] " +
            "[kind:"    + itemInfo.param.kind   + "] " +
            "[effect:"  + itemInfo.param.effect + "] " +
            "[desc:"    + itemInfo.param.desc   + "] " +
            "[num:"     + itemInfo.num          + "] "
        );
    }
}
