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
            "[name:"                + param.Name            + "] " +
            "[id:"                  + param.Id              + "] " +
            "[chapter:"             + param.Chapter         + "] " +
            "[normal_bgm_name:"     + param.Normal_bgm_name + "] " +
            "[event_bgm_name:"      + param.Event_bgm_name  + "] " +
            "[battle_bgm_name:"     + param.Battle_bgm_name + "] " +
            "[boss_bgm_name:"       + param.Boss_bgm_name   + "] "
        );
    }

}
