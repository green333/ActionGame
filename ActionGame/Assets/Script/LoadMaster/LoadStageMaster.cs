using UnityEngine;
using System.Collections;

public class LoadStageMaster{
   
    /// <summary>
    /// インスタンス
    /// </summary>
    static readonly LoadStageMaster instance = new LoadStageMaster();

    /// <summary>
    /// ステージマスタを格納する変数
    /// </summary>
    StageMaster master;

    /// <summary>
    /// インスタンスを取得
    /// </summary>
    static public LoadStageMaster Instace { get { return instance; } }

    /// <summary>
    /// 初期化時にステージマスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if (null == (master = Resources.Load<StageMaster>("MasterData/StageMaster")))
        {
            Debug.Log("failed to Resources.Load<StageMaster>");
        }
    }

}
