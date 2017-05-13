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
            LogExtensions.Red("failed to Resources.Load<StageMaster>");
        }
    }

    /// <summary>
    /// ステージパラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(StageMaster.Param param)
    {
        LogExtensions.Black("id              = " + param.id);
        LogExtensions.Black("name            = " + param.name);
        LogExtensions.Black("chapter         = " + param.chapter);
        LogExtensions.Black("normal_bgm_name = " + param.normal_bgm_name);
        LogExtensions.Black("event_bgm_name  = " + param.event_bgm_name);
        LogExtensions.Black("battle_bgm_name = " + param.battle_bgm_name);
        LogExtensions.Black("boss_bgm_name   = " + param.boss_bgm_name);

    }

}
