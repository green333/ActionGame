using UnityEngine;
using System.Collections;

public class LoadItemMaster{

    /// <summary>
    /// インスタンス
    /// </summary>
    static readonly LoadItemMaster instance = new LoadItemMaster();

    /// <summary>
    /// アイテムマスタを格納する変数
    /// </summary>
    ItemMaster master;

    /// <summary>
    /// インスタンスを取得
    /// </summary>
    static public LoadItemMaster Instace { get { return instance; } }

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
}
