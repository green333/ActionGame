using UnityEngine;
using System.Collections;

public class LoadEnemySpawnMaster
{
  
    /// <summary>
    /// 指定したステージと章に出現する敵の情報を取得
    /// </summary>
    /// <param name="stage_id"></param>
    /// <param name="chapter_id"></param>
    /// <returns></returns>
    public EnemySpawnMaster.Param GetEnemySpawanInfo(int stage_id,int chapter_id)
    {
        return null;
    }

    /// <summary>
    /// 敵出現パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(EnemySpawnMaster.Param param)
    {
        LogExtensions.OutputInfo("chapter_id          = " + param.chapter_id);
        LogExtensions.OutputInfo("enemy1_lvpm         = " + param.enemy1_lvpm);
        LogExtensions.OutputInfo("enemy1_name         = " + param.enemy1_name);
        LogExtensions.OutputInfo("enemy1_respawn_time = " + param.enemy1_respawn_time);
        LogExtensions.OutputInfo("enemy2_lvpm         = " + param.enemy2_lvpm);
        LogExtensions.OutputInfo("enemy2_name         = " + param.enemy2_name);
        LogExtensions.OutputInfo("enemy2_respawn_time = " + param.enemy2_respawn_time);
        LogExtensions.OutputInfo("enemy3_lvpm         = " + param.enemy3_lvpm);
        LogExtensions.OutputInfo("enemy3_name         = " + param.enemy3_name);
    }
}
