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

    /// <summary>マスタデータのファイルパス </summary>
    const string filename = "Assets/Resources/MasterData/敵出現マスタ.txt";

    /// <summary>検索に使用するマスターのカラム </summary>
    const string COL_STAGE_ID   = "stage_id";
    const string COL_CHAPTER_ID = "chapter_id";

    // 敵出現マスタの一レコードに設定できる敵の数
    public const int ONE_RECORD_ENEMY_MAX_COUNT= 3;

    /// <summary>
    /// 指定したステージIDと章IDに出現する敵の情報を読み込む
    /// </summary>
    /// <param name="stage_id"></param>
    /// <param name="chapter_id"></param>
    /// <returns>読み込み成功:true 読み込み失敗:false</returns>
    public bool LoadEnemySpawanInfo(int stage_id,int chapter_id)
    {
        m_spawnList = new List<EnemySpawnMaster.Param>();

        string searchJson =  base.VariableToJson(COL_STAGE_ID, stage_id) + "," + base.VariableToJson(COL_CHAPTER_ID, chapter_id);

        base.Open(filename);
        List<string> getJsonStrList = base.SearchMultiple(searchJson);
        base.Close();

        for(int i = 0; i < getJsonStrList.Count;++i)
        {
            m_spawnList.Add(JsonUtility.FromJson<EnemySpawnMaster.Param>(getJsonStrList[i]));
        }

        return (m_spawnList.Count != 0);
    }

    /// <summary>
    /// 敵出現パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(EnemySpawnMaster.Param param)
    {

        LogExtensions.OutputInfo("[敵出現マスタ] => " +
             "[stage_id:"               + param.stage_id            + "] " +
             "[chapter_id:"             + param.chapter_id          + "] " +
             "[stage_detail_id:"        + param.stage_detail_id     + "] " +
             "[enemy1_lv:"              + param.enemy1_lv           + "] " +
             "[enemy1_id:"              + param.enemy1_id           + "] " +
             "[enemy1_respawn_time:"    + param.enemy1_respawn_time + "] " +
             "[enemy1_frequency:"       + param.enemy1_frequency    + "] " +
             "[enemy2_lv:"              + param.enemy2_lv           + "] " +
             "[enemy2_id:"              + param.enemy2_id           + "] " +
             "[enemy2_respawn_time:"    + param.enemy2_respawn_time + "] " +
             "[enemy2_frequency:"       + param.enemy2_frequency    + "] " +
             "[enemy3_lv:"              + param.enemy3_lv           + "] " +
             "[enemy3_id:"              + param.enemy3_id           + "] " +
             "[enemy3_respawn_time:"    + param.enemy3_respawn_time + "] " +
             "[enemy3_frequency:"       + param.enemy3_frequency    + "] "

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
