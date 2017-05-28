using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyGrowthMaster : TextMasterManager
{
    static LoadEnemyGrowthMaster _instance = new LoadEnemyGrowthMaster();

    static public LoadEnemyGrowthMaster instance { get { return _instance; } }

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
      
        const int ENEMY_LEVEL_MAX = 20;
        
        // 下限上限を設定
        if (enemy1_lv < 0) { enemy1_lv = 1; }
        if (enemy2_lv < 0) { enemy2_lv = 1; }
        if (enemy3_lv < 0) { enemy3_lv = 1; }
      
        // TODO:レベルの上限値がまだ正確に決まっていないので、仮値にしておく
        if (enemy1_lv > ENEMY_LEVEL_MAX) { enemy1_lv = ENEMY_LEVEL_MAX; }
        if (enemy2_lv > ENEMY_LEVEL_MAX) { enemy2_lv = ENEMY_LEVEL_MAX; }
        if (enemy3_lv > ENEMY_LEVEL_MAX) { enemy3_lv = ENEMY_LEVEL_MAX; }
        
        // インデックスとレベルから作成する
        int index = 0;
      
        // EnemyBaseMaster.Paramのindexはレベル1が始まりなので、
        // レベル１の敵情報を取得する場合、index + 0となる必要があるのでここで-１する必要がある。
        // (あとは計算通り。レベル１０の敵情報を取得する場合はindex + 9となる)
        int[] indexList = { enemy1_lv - 1, enemy2_lv - 1, enemy3_lv - 1 };

        string[] searchList = null;
        string getJsnoStr = null;
        base.Open(filename);
        searchList = new string[2] 
        {
            base.VariableToJson(COL_NAME, spawnMasterParam.enemy1_name),
            base.VariableToJson(COL_LEVEL, enemy1_lv)
        };
        getJsnoStr = base.MultipleSearch(searchList);
        if (getJsnoStr != null)
        {
            growthListParam.Add(JsonUtility.FromJson<EnemyGrowthMaster.Param>(getJsnoStr));
        }

        base.Close();

        base.Open(filename);
        searchList = new string[2]
        {
            base.VariableToJson(COL_NAME, spawnMasterParam.enemy2_name),
            base.VariableToJson(COL_LEVEL, enemy2_lv)
        };
        getJsnoStr = base.MultipleSearch(searchList);
        if (getJsnoStr != null)
        {
            growthListParam.Add(JsonUtility.FromJson<EnemyGrowthMaster.Param>(getJsnoStr));
        }
        base.Close();

        base.Open(filename);
        searchList = new string[2]
        {
            base.VariableToJson(COL_NAME, spawnMasterParam.enemy3_name),
            base.VariableToJson(COL_LEVEL, enemy3_lv)
        };
        getJsnoStr = base.MultipleSearch(searchList);
        if (getJsnoStr != null)
        {
            growthListParam.Add(JsonUtility.FromJson<EnemyGrowthMaster.Param>(getJsnoStr));
        }
        base.Close();
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

