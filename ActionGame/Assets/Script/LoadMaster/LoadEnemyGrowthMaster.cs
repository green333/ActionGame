using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyGrowthMaster : TextMasterManager
{
    static LoadEnemyGrowthMaster _instance = new LoadEnemyGrowthMaster();

    static public LoadEnemyGrowthMaster instance { get { return _instance; } }

    const int ENEMY_LEVEL_MAX = 120;

    const string filename = "Assets/Resources/MasterData/敵成長マスタ.txt";

    const string COL_NAME = "name";
    const string COL_LEVEL = "level";

    /// <summary>
    /// 敵基本マスタパラメーターと敵出現パラメーターとプレイヤーのレベルから、出現する敵の情報を取得する
    /// </summary>
    /// <param name="growthListParam"></param>
    /// <param name="baseMasterParam"></param>
    /// <param name="spawnMasterParam"></param>
    /// <param name="playerLv"></param>
    public void GetEnemyInfo(out List<EnemyGrowthMaster.Param> growthListParam, List<EnemyBaseMaster.Param> baseMasterParam,EnemySpawnMaster.Param spawnMasterParam,int playerLv)
    {
       growthListParam = new List<EnemyGrowthMaster.Param>();
      
        // UnityEngine.Random.Rangeはint型だと min <= x < maxになるので、maxに指定した値を含めたいので+1をする必要がある。
        int enemy1_lv = playerLv + UnityEngine.Random.Range(-spawnMasterParam.enemy1_lvpm, spawnMasterParam.enemy1_lvpm + 1);
        int enemy2_lv = playerLv + UnityEngine.Random.Range(-spawnMasterParam.enemy2_lvpm, spawnMasterParam.enemy2_lvpm + 1);
        int enemy3_lv = playerLv + UnityEngine.Random.Range(-spawnMasterParam.enemy3_lvpm, spawnMasterParam.enemy3_lvpm + 1);
      
        // 下限上限を設定
        if (enemy1_lv < 0) { enemy1_lv = 1; }
        if (enemy2_lv < 0) { enemy2_lv = 1; }
        if (enemy3_lv < 0) { enemy3_lv = 1; }
        if (enemy1_lv > ENEMY_LEVEL_MAX) { enemy1_lv = ENEMY_LEVEL_MAX; }
        if (enemy2_lv > ENEMY_LEVEL_MAX) { enemy2_lv = ENEMY_LEVEL_MAX; }
        if (enemy3_lv > ENEMY_LEVEL_MAX) { enemy3_lv = ENEMY_LEVEL_MAX; }

        string[] searchList = new string[] 
        {
            base.VariableToJson(COL_NAME, spawnMasterParam.enemy1_name) + "," +  base.VariableToJson(COL_LEVEL, enemy1_lv),
            base.VariableToJson(COL_NAME, spawnMasterParam.enemy2_name) + "," +  base.VariableToJson(COL_LEVEL, enemy2_lv),
            base.VariableToJson(COL_NAME, spawnMasterParam.enemy3_name) + "," +  base.VariableToJson(COL_LEVEL, enemy3_lv),
        };

        base.Open(filename);
        string[] getJsnoStrList = base.SearchList(searchList, 100);
        base.Close();

        foreach (string getJsonStr in getJsnoStrList)
        {
            if(getJsonStr == string.Empty)
            {
                break;
            }
            growthListParam.Add(JsonUtility.FromJson<EnemyGrowthMaster.Param>(getJsonStr));
        }
    }

    /// <summary>
    /// 敵成長パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(EnemyGrowthMaster.Param param)
    {
        LogExtensions.OutputInfo(
             "[id:"     + param.id      + "] " +
             "[name:"   + param.name    + "] " +
             "[level:"  + param.level   + "] " +
             "[hp:"     + param.hp      + "] " +
             "[atk:"    + param.atk     + "] " +
             "[def:"    + param.def     + "] " +
             "[mgc:"    + param.mgc     + "] " +
             "[spd:"    + param.spd     + "] " +
             "[exp:"    + param.exp     + "] " 
        );
    }
}

