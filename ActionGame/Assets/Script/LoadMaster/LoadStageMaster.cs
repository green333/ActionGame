using UnityEngine;
using System.Collections;

public class LoadStageMaster :  BaseSingleton<LoadStageMaster>
{
    /// <summary>
    /// ステージマスタを格納する変数
    /// </summary>
    StageMaster master;

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

    /// <summary>
    /// ステージパラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(StageMaster.Param param)
    {
        Debug.Log("id              = " + param.id);
        Debug.Log("name            = " + param.name);
        Debug.Log("chapter         = " + param.chapter);
        Debug.Log("normal_bgm_name = " + param.normal_bgm_name);
        Debug.Log("event_bgm_name  = " + param.event_bgm_name);
        Debug.Log("battle_bgm_name = " + param.battle_bgm_name);
        Debug.Log("boss_bgm_name   = " + param.boss_bgm_name);

    }

}
