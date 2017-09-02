using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

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
    /// <param name="spawnMasterParam"></param>
    /// <param name="playerLv"></param>
    public void GetEnemyInfo(out List<EnemyGrowthMaster.Param> growthListParam, List<EnemySpawnMaster.Param> spawnMasterParamList,int playerLv,out int[] getEnemyNameList)
    {
        growthListParam     = new List<EnemyGrowthMaster.Param>();
        getEnemyNameList    = Enumerable.Repeat<int>(0, spawnMasterParamList.Count * 3).ToArray();

        IEnumerator ieEnemySpawnList =  spawnMasterParamList.GetEnumerator();
        EnemySpawnMaster.Param tempEnemySpawn = null;

       //  int SPAWN_COUNT = spawnMasterParamList.Count * LoadEnemySpawnMaster.ENEMY_SPAWN_KIND_MAX;
        // 敵成長マスタから取得する敵のレベル分のリストを作成する
        int[] checkedList    = Enumerable.Repeat<int>(0, spawnMasterParamList.Count * 3).ToArray();
        int[] chekedMinLvList   = new int[spawnMasterParamList.Count * 3];
        int[] chekedMaxLvList   = new int[spawnMasterParamList.Count * 3];
        int checkedIndex        = 0;

        while (ieEnemySpawnList.MoveNext())
        {
            tempEnemySpawn = (EnemySpawnMaster.Param)ieEnemySpawnList.Current;

            AddCheckList(ref checkedList, ref chekedMinLvList, ref chekedMaxLvList, ref checkedIndex, tempEnemySpawn.enemy1_id, tempEnemySpawn.enemy1_lvpm, playerLv, ref getEnemyNameList);
            AddCheckList(ref checkedList, ref chekedMinLvList, ref chekedMaxLvList, ref checkedIndex, tempEnemySpawn.enemy2_id, tempEnemySpawn.enemy2_lvpm, playerLv, ref getEnemyNameList);
            AddCheckList(ref checkedList, ref chekedMinLvList, ref chekedMaxLvList, ref checkedIndex, tempEnemySpawn.enemy3_id, tempEnemySpawn.enemy3_lvpm, playerLv, ref getEnemyNameList);
        }

        // 出現する敵の種類 * (レベル幅 * 2 + 1)数だけ取得するが、レベル幅は同じ敵が複数の場所に出現するとしても
        // 同じ値とは限らないため、あらかじめバッファを設定することができないためリストを使用する
        List<string> searchList = new List<string>();
        for(int i = 0; i < spawnMasterParamList.Count * 3; ++i)
        {
            if(checkedList[i] != 0)
            {
                for(int searchLv = chekedMinLvList[i]; searchLv <= chekedMaxLvList[i]; ++searchLv)
                {
                    searchList.Add(base.VariableToJson(COL_NAME, checkedList[i]) + "," + base.VariableToJson(COL_LEVEL, searchLv));
                }
            }
        }

        base.Open(filename);
        string[] getJsnoStrList = base.SearchList(searchList.ToArray(), 100);
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

    private void AddCheckList(ref int[] checkedList,ref int[] chekedMinLvList,ref int[] chekedMaxLvList, ref int checkedIndex,int enemyEnemy,int lvpm,int playerLv,ref int[] getEnemyList)
    {
        int getIndex = -1;
        int minLv = 0;
        int maxLv = 0;
        if (-1 == (getIndex = Array.IndexOf(checkedList, enemyEnemy)))
        {
            // 新しい敵データを見つけた場合、
            checkedList[checkedIndex] = enemyEnemy;
            minLv = playerLv - lvpm;
            maxLv = playerLv + lvpm;
            if (minLv < 1) { minLv = 1; }
            if (maxLv > ENEMY_LEVEL_MAX) { maxLv = ENEMY_LEVEL_MAX; }
            chekedMinLvList[checkedIndex] = minLv;
            chekedMaxLvList[checkedIndex] = maxLv;
            getEnemyList[checkedIndex] = enemyEnemy;
            ++checkedIndex;
        }
        else
        {
            // 一度見つけた敵データの場合、出現する最低レベルと最高レベルが更新されているかをチェックし,
            // 更新されている場合は上書きする
            minLv = playerLv - lvpm;
            maxLv = playerLv + lvpm;
            if (minLv < 1) { minLv = 1; }
            if (maxLv > ENEMY_LEVEL_MAX) { maxLv = ENEMY_LEVEL_MAX; }
            if (chekedMinLvList[getIndex] < minLv) { chekedMinLvList[getIndex] = minLv; }
            if (chekedMaxLvList[getIndex] > maxLv) { chekedMaxLvList[getIndex] = maxLv; }
        }
    }

    /// <summary>
    /// 敵成長パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(EnemyGrowthMaster.Param param)
    {
        LogExtensions.OutputInfo("[敵成長マスタ] => "+
             "[id:"     + param.id      + "] " +
             "[level:"  + param.level   + "] " +
             "[hp:"     + param.hp      + "] " +
             "[atk:"    + param.atk     + "] " +
             "[def:"    + param.def     + "] " +
             "[spd:"    + param.spd     + "] " +
             "[exp:"    + param.exp     + "] " 
        );
    }
}

