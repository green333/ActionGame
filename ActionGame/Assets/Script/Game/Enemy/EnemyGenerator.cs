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
        EnemySpawnMaster.Param enemySpawnParam = LoadEnemySpawnMaster.instance.GetEnemySpawanInfo(SaveData.Instance.stageId, SaveData.Instance.chapter);
        LoadEnemyBaseMaster.instance.GetEnemyInfo(out baseParamList, enemySpawnParam);
        LoadEnemyGrowthMaster.instance.GetEnemyInfo(out growthParamList, baseParamList, enemySpawnParam, 1);
    }
}
