using UnityEngine;
using System.Collections;

public class LoadPlayerBaseMaster
{

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
    /// プレイヤーのレベル上限値
    /// </summary>
    static public readonly int PLAYER_LEVEL_MAX = 100;

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

    /// <summary>
    /// 指定したレベルに一致するプレイヤー情報を取得する
    /// </summary>
    /// <param name="lv"></param>
    /// <returns></returns>
    public PlayerBaseMaster.Param GetPlayerInfo(int lv)
    {
        // 指定したレベルがレベル上限以上ならレベル上限の情報を返す
        return ((lv >= PLAYER_LEVEL_MAX) ? master.list[PLAYER_LEVEL_MAX - 1] : master.list[lv - 1]);
    }

    /// <summary>
    /// 引数に渡したプレイヤーパラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(PlayerBaseMaster.Param param)
    {
        Debug.Log("level = " + param.level);
        Debug.Log("hp    = " + param.hp);
        Debug.Log("atk   = " + param.atk);
        Debug.Log("def   = " + param.def);
        Debug.Log("mgc   = " + param.mgc);
        Debug.Log("spd   = " + param.spd);
        Debug.Log("exp   = " + param.exp);
    }
}
