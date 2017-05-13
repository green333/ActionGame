using UnityEngine;
using System.Collections;

public class LoadEnemySpawnMaster : BaseSingleton<LoadEnemySpawnMaster>
{
    /// <summary>
    /// 敵出現マスタを格納する変数
    /// </summary>
    EnemySpawnMaster master;

    /// <summary>
    /// 初期化時に敵出現マスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if (null == (master = Resources.Load<EnemySpawnMaster>("MasterData/EnemySpawnMaster")))
        {
            Debug.Log("failed to Resources.Load<EnemySpawnMaster>");
        }
        
    }

    /// <summary>
    /// 指定したステージと章に出現する敵の情報を取得
    /// </summary>
    /// <param name="stage_id"></param>
    /// <param name="chapter_id"></param>
    /// <returns></returns>
    public EnemySpawnMaster.Param GetEnemySpawanInfo(int stage_id,int chapter_id)
    {
        int index = 0;

        foreach (EnemySpawnMaster.Param param in master.list)
        {
            if(param.stage_id == stage_id && param.chapter_id == chapter_id)
            {
                break;
            }
            ++index;
        }

        EnemySpawnMaster.Param temp = new EnemySpawnMaster.Param();
        temp.chapter_id = master.list[index].chapter_id;
        temp.enemy1_lvpm            = master.list[index].enemy1_lvpm;
        temp.enemy1_name            = master.list[index].enemy1_name;
        temp.enemy1_respawn_time    = master.list[index].enemy1_respawn_time;
        temp.enemy2_lvpm            = master.list[index].enemy2_lvpm;
        temp.enemy2_name            = master.list[index].enemy2_name;
        temp.enemy2_respawn_time    = master.list[index].enemy2_respawn_time;
        temp.enemy3_lvpm            = master.list[index].enemy3_lvpm;
        temp.enemy3_name            = master.list[index].enemy3_name;

        return temp;
    }

    /// <summary>
    /// 敵出現パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(EnemySpawnMaster.Param param)
    {
        Debug.Log("chapter_id          = " + param.chapter_id);
        Debug.Log("enemy1_lvpm         = " + param.enemy1_lvpm);
        Debug.Log("enemy1_name         = " + param.enemy1_name);
        Debug.Log("enemy1_respawn_time = " + param.enemy1_respawn_time);
        Debug.Log("enemy2_lvpm         = " + param.enemy2_lvpm);
        Debug.Log("enemy2_name         = " + param.enemy2_name);
        Debug.Log("enemy2_respawn_time = " + param.enemy2_respawn_time);
        Debug.Log("enemy3_lvpm         = " + param.enemy3_lvpm);
        Debug.Log("enemy3_name         = " + param.enemy3_name);
    }
}
