using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemySpawnMaster : TextMasterManager
{
    /// <summary> 自身のインスタンス </summary>
    static LoadEnemySpawnMaster m_instance = new LoadEnemySpawnMaster();

    /// <summary>自身のインスタンスを取得 </summary>
    static public LoadEnemySpawnMaster instance { get { return m_instance; } }

    /// <summary>敵基本マスタリスト</summary>
    private List<EnemySpawnMaster.Param> m_spawnList = null;

    /// <summary>敵基本マスタリストを取得 </summary>
    public List<EnemySpawnMaster.Param> spawnList { get { return m_spawnList; } }

    /// <summary> 出現確率の最大値 </summary>
    public int FREQUENCY_MAX { get { return 100; } }
    
    /// <summary>マスタデータのファイルパス </summary>
    const string filename = "Assets/Resources/MasterData/敵出現マスタ.txt";

    public bool Init()
    {
        LogExtensions.OutputInfo("敵出現マスタを読み込みます。");

        bool ret = false;
        base.Open(filename);

        string[] lineAll = base.GetLineAll();
        if (lineAll != null)
        {
            m_spawnList = new List<EnemySpawnMaster.Param>(lineAll.Length);
            EnemySpawnMaster.Param temp = null;
            foreach (string line in lineAll)
            {
                temp = JsonUtility.FromJson<EnemySpawnMaster.Param>(line);
                m_spawnList.Add(temp);
            }

            ret = true;
            LogExtensions.OutputInfo("敵出現マスタの読み込みに成功しました。");
        }
        else
        {
            LogExtensions.OutputError("敵出現マスタの読み込みに失敗しました。");
        }
        base.Close();

        return ret;
    }
 
    /// <summary>
    /// 敵出現パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(EnemySpawnMaster.Param param)
    {

        LogExtensions.OutputInfo("[敵出現マスタ] => " +
             "[stage_id:"               + param.Stage_id            + "] " +
             "[chapter_id:"             + param.Chapter_id          + "] " +
             "[stage_detail_id:"        + param.Stage_detail_id     + "] " +
             "[enemy1_lv:"              + param.Enemy1_lv           + "] " +
             "[enemy1_id:"              + param.Enemy1_id           + "] " +
             "[enemy1_respawn_time:"    + param.Enemy1_respawn_time + "] " +
             "[enemy1_frequency:"       + param.Enemy1_frequency    + "] " +
             "[enemy2_lv:"              + param.Enemy2_lv           + "] " +
             "[enemy2_id:"              + param.Enemy2_id           + "] " +
             "[enemy2_respawn_time:"    + param.Enemy2_respawn_time + "] " +
             "[enemy2_frequency:"       + param.Enemy2_frequency    + "] " +
             "[enemy3_lv:"              + param.Enemy3_lv           + "] " +
             "[enemy3_id:"              + param.Enemy3_id           + "] " +
             "[enemy3_respawn_time:"    + param.Enemy3_respawn_time + "] " +
             "[enemy3_frequency:"       + param.Enemy3_frequency    + "] "

        );
    }

    /// <summary>
    /// 敵出現パラメーターをログに出力する
    /// </summary>
    public void DebugLog()
    {
        for(int i = 0; i < m_spawnList.Count; ++i)
        {
            DebugLog(m_spawnList[i]);
        }
    }
}
