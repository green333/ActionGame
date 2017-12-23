using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    static LoadItemMaster m_instance = new LoadItemMaster();

    /// <summary>
    /// インスタンス取得プロパティ
    /// </summary>
    static public LoadItemMaster instance { get { return m_instance; } }

    /// <summary> アイテムマスタリスト </summary>
    private Dictionary<string, ItemMaster.Param> m_itemList = null;

    /// <summary> アイテムマスタリストを取得 </summary>
    public Dictionary<string, ItemMaster.Param> itemList { get { return m_itemList; } }

    /// <summary>
    /// マスターデータのファイルパス
    /// </summary>
    const string filename = "Assets/Resources/MasterData/アイテムマスタ.txt";

    public bool Init()
    {
        LogExtensions.OutputInfo("アイテムマスタを読み込みます。");

        bool ret = false;
        base.Open(filename);

        string[] lineAll = base.GetLineAll();
        if (lineAll != null)
        {
            m_itemList = new Dictionary<string, ItemMaster.Param>();
            ItemMaster.Param temp = null;
            foreach (string line in lineAll)
            {
                temp = JsonUtility.FromJson<ItemMaster.Param>(line);
                m_itemList.Add(temp.Name, temp);
            }

            ret = true;
            LogExtensions.OutputInfo("アイテムマスタの読み込みに成功しました。");
        }
        else
        {
            LogExtensions.OutputError("アイテムマスタの読み込みに失敗しました。");
        }
        base.Close();

        return ret;
    }

    /// <summary>
    /// 指定した名前に一致するアイテム情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ItemMaster.Param GetItemInfo(string name)
    {
        ItemMaster.Param param = null;

        if(m_itemList.ContainsKey(name))
        {
            param = m_itemList[name];
        }

        return param;
    }

    /// <summary>
    /// アイテムパラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(ItemInfo itemInfo)
    {
        LogExtensions.OutputInfo("[アイテムマスタ] => " +
            "[id:"      + itemInfo.param.Id     + "] " +
            "[name:"    + itemInfo.param.Name   + "] " +
            "[kind:"    + itemInfo.param.Kind   + "] " +
            "[effect:"  + itemInfo.param.Effect + "] " +
            "[desc:"    + itemInfo.param.Desc   + "] " +
            "[num:"     + itemInfo.num          + "] "
        );
    }
}
