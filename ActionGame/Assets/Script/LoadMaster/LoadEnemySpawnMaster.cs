using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemySpawnMaster : TextMasterManager
{
    static LoadEnemySpawnMaster _instance = new LoadEnemySpawnMaster();

    static public LoadEnemySpawnMaster instance { get { return _instance; } }

    const string filename = "Assets/Resources/MasterData/敵出現マスタ.txt";

    const string COL_STAGE_ID   = "stage_id";
    const string COL_CHAPTER_ID = "chapter_id";

    /// <summary>
    /// 指定したステージと章に出現する敵の情報を取得
    /// </summary>
    /// <param name="stage_id"></param>
    /// <param name="chapter_id"></param>
    /// <returns></returns>
    public void GetEnemySpawanInfo(ref List<EnemySpawnMaster.Param> paramList,int stage_id,int chapter_id)
    {
        paramList = new List<EnemySpawnMaster.Param>();

        string searchJson =  base.VariableToJson(COL_STAGE_ID, stage_id) + "," + base.VariableToJson(COL_CHAPTER_ID, chapter_id);

        base.Open(filename);
        List<string> getJsonStrList = base.SearchMultiple(searchJson);
        base.Close();

        IEnumerator getJsonStrListE = getJsonStrList.GetEnumerator();
        while(getJsonStrListE.MoveNext())
        {
            paramList.Add(JsonUtility.FromJson<EnemySpawnMaster.Param>((string)getJsonStrListE.Current));
        }
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
             "[enemy1_lvpm:"            + param.enemy1_lvpm         + "] " +
             "[enemy1_name:"            + param.enemy1_name         + "] " +
             "[enemy1_respawn_time:"    + param.enemy1_respawn_time + "] " +
             "[enemy2_lvpm:"            + param.enemy2_lvpm         + "] " +
             "[enemy2_name:"            + param.enemy2_name         + "] " +
             "[enemy2_respawn_time:"    + param.enemy2_respawn_time + "] " +
             "[enemy3_lvpm:"            + param.enemy3_lvpm         + "] " +
             "[enemy3_name:"            + param.enemy3_name         + "] " +
             "[enemy3_respawn_time:"    + param.enemy3_respawn_time + "] "
        );
          
    }
}
