using UnityEngine;
using System.Collections;

public class LoadStageMaster : TextMasterManager
{

    /// <summary>
    /// ステージパラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(StageMaster.Param param)
    {
        LogExtensions.OutputInfo("[ステージマスタ] => " +
            "[name:"                + param.name            + "] " +
            "[id:"                  + param.id              + "] " +
            "[chapter:"             + param.chapter         + "] " +
            "[normal_bgm_name:"     + param.normal_bgm_name + "] " +
            "[event_bgm_name:"      + param.event_bgm_name  + "] " +
            "[battle_bgm_name:"     + param.battle_bgm_name + "] " +
            "[boss_bgm_name:"       + param.boss_bgm_name   + "] "
        );
    }

}
