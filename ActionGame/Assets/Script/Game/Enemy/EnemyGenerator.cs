using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// 敵を生成させるクラス
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    List<EnemySpawnMaster.Param>    m_enemySpawnParamList   = null;
    List<EnemyBaseMaster.Param>     m_enemyBaseParamList    = null;
    List<EnemyGrowthMaster.Param>   m_enemyGrowthParamList  = null;

    /// <summary>
    /// 現在のステージIDと章IDから敵情報をマスタから読み込む
    /// SaveDataに保存されたステージIDと章IDから敵情報を取得する
    /// </summary>
    /// <param name="baseParamList"></param>
    /// <param name="growthParamList"></param>
    public void LoadEnemyInfo()
    {
        SaveData.Instance.stageId = 1;
        SaveData.Instance.chapter = 1;

        LogExtensions.OutputInfo("敵データの読み込みを開始します");

        //  章毎のステージに出現する敵情報を取得する
        LoadEnemySpawnMaster.instance.GetEnemySpawanInfo(out m_enemySpawnParamList, SaveData.Instance.stageId, SaveData.Instance.chapter);

        //  敵出現マスタに設定されてレベル幅分の敵成長マスタを取得する
        int[] getEnemyIdList = null;
        LoadEnemyGrowthMaster.instance.GetEnemyInfo(out m_enemyGrowthParamList, m_enemySpawnParamList,1,out getEnemyIdList);

        // 敵基本マスタを取得する
        LoadEnemyBaseMaster.instance.GetEnemyInfo(out m_enemyBaseParamList, getEnemyIdList);
    }

    /// <summary>
    /// ステージID、章ID,ステージ詳細IDに紐づく敵を生成する。
    /// 敵出現マスタに指定されている敵最大数分生成する。
    /// (生成はするが描画・更新フラグをfalseにする)
    /// 
    /// </summary>
    public void CreateEnemyOfThisPlace(int stageDetaileId)
    {
        // 指定したステージ詳細IDに一致する敵のみ、生成する。
        IEnumerator ieEnemySpawnList = m_enemySpawnParamList.GetEnumerator();
        EnemySpawnMaster.Param tempEnemySpawn;
        while (ieEnemySpawnList.MoveNext())
        {
            tempEnemySpawn = (EnemySpawnMaster.Param)ieEnemySpawnList.Current;
            if(tempEnemySpawn.stage_detail_id != stageDetaileId) { continue; }

            // 名前が設定されていない敵データは無視する
            if (tempEnemySpawn.enemy1_id != 0) { }
            if (tempEnemySpawn.enemy2_id != 0) { }
            if (tempEnemySpawn.enemy3_id != 0) { }
        }
    }

    void Start()
    {
        LoadEnemyInfo();
      //  CreateEnemyOfThisPlace(1);
    }
}
