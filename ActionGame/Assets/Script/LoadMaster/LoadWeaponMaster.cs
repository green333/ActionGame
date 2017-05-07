using UnityEngine;
using System.Collections;

public class LoadWeaponMaster{

    /// <summary>
    /// インスタンス
    /// </summary>
    static readonly LoadWeaponMaster instance = new LoadWeaponMaster();

    /// <summary>
    /// 武器マスタを格納する変数
    /// </summary>
    WeaponMaster master;

    /// <summary>
    /// インスタンスを取得
    /// </summary>
    static public LoadWeaponMaster Instace { get { return instance; } }

    /// <summary>
    /// 初期化時に武器マスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if (null == (master = Resources.Load<WeaponMaster>("MasterData/WeaponMaster")))
        {
            Debug.Log("failed to Resources.Load<WeaponMaster>");
        }
    }

    /// <summary>
    /// 指定した名前に一致する武器情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public WeaponMaster.Param getWeaponInfo(string name)
    {
        int index = 0;
        foreach (WeaponMaster.Param param in master.list)
        {
            if (param.name == name)
            {
                break;
            }
            ++index;
        }

        WeaponMaster.Param temp = new WeaponMaster.Param();
        temp.name   = master.list[index].name;
        temp.id     = master.list[index].id;
        temp.type   = master.list[index].type;
        temp.atk    = master.list[index].atk;

        return temp;
    }
}
