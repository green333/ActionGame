using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 敵を生成させるクラス
/// </summary>
public class EnemyGenerator
{
    /// <summary>
    /// SaveDataに保存されたステージIDと章IDから敵情報を取得する
    /// </summary>
    /// <param name="baseParamList"></param>
    /// <param name="growthParamList"></param>
    public static void Generate(out List<EnemyBaseMaster.Param> baseParamList, out List<EnemyGrowthMaster.Param> growthParamList)
    {
        LogExtensions.OutputInfo("敵データの読み込みを開始します");
        EnemySpawnMaster.Param enemySpawnParam = LoadEnemySpawnMaster.instance.GetEnemySpawanInfo(SaveData.Instance.stageId, SaveData.Instance.chapter);
        LoadEnemyBaseMaster.instance.GetEnemyInfo(out baseParamList, enemySpawnParam);
        LoadEnemyGrowthMaster.instance.GetEnemyInfo(out growthParamList, baseParamList, enemySpawnParam, 1);
        foreach (EnemyBaseMaster.Param p in baseParamList)
        {
            LoadEnemyBaseMaster.instance.DebugLog(p);
        }
        foreach (EnemyGrowthMaster.Param p in growthParamList)
        {
            LoadEnemyGrowthMaster.instance.DebugLog(p);
        }
        LogExtensions.OutputInfo("敵データの読み込みを終了します");
    }
}
