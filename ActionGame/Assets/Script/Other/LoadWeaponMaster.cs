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

}
