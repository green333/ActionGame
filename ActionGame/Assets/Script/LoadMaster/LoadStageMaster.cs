using UnityEngine;
using System.Collections;

public class LoadStageMaster
{

    /// <summary>
    /// ステージパラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(StageMaster.Param param)
    {
        LogExtensions.OutputInfo("id              = " + param.id);
        LogExtensions.OutputInfo("name            = " + param.name);
        LogExtensions.OutputInfo("chapter         = " + param.chapter);
        LogExtensions.OutputInfo("normal_bgm_name = " + param.normal_bgm_name);
        LogExtensions.OutputInfo("event_bgm_name  = " + param.event_bgm_name);
        LogExtensions.OutputInfo("battle_bgm_name = " + param.battle_bgm_name);
        LogExtensions.OutputInfo("boss_bgm_name   = " + param.boss_bgm_name);

    }

}
