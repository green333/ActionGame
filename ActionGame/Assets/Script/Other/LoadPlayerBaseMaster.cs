using UnityEngine;
using System.Collections;

public class LoadPlayerBaseMaster{

    /// <summary>
    /// インスタンス
    /// </summary>
    static readonly LoadPlayerBaseMaster instance = new LoadPlayerBaseMaster();

    /// <summary>
    /// プレイヤー基本マスタを格納する変数
    /// </summary>
    PlayerBaseMaster master;

    /// <summary>
    /// インスタンスを取得
    /// </summary>
    static public LoadPlayerBaseMaster Instace { get { return instance; } }

    /// <summary>
    /// 初期化時にプレイヤー基本マスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if (null == (master = Resources.Load<PlayerBaseMaster>("MasterData/PlayerBaseMaster")))
        {
            Debug.Log("failed to Resources.Load<PlayerBaseMaster>");
        }
    }
}
