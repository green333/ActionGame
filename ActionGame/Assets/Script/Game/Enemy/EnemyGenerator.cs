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
    /// <summary>
    /// Resourceから読み込んだデータ
    /// </summary>
    Dictionary<int, GameObject> m_resourcesList = null;

    List<GameObject> m_enemyList = null;

    /// <summary>
    /// ステージIDと章IDから出現する敵のデータを読み込む
    /// </summary>
    /// <param name="stageId">ステージID</param>
    /// <param name="chapterId">章ID</param>
    /// <returns>読み込み成功:true 失敗:false</returns>
    public bool LoadEnemyMasterData(int stageId,int chapterId)
    {
        bool ret = false;

        LogExtensions.OutputInfo("敵データの読み込みを開始します");

        do
        {
            // 章毎の敵出現マスタを読み込む
            if (!LoadEnemySpawnMaster.instance.LoadEnemySpawanInfo(stageId, chapterId))
            {
                LogExtensions.OutputError("敵出現マスタの読み込みに失敗しました。stageId=" + stageId + ",chapterId=" + chapterId);
                break;
            }
            LoadEnemySpawnMaster.instance.DebugLog();

            // 出現する敵の基本マスタを読み込む
            if (!LoadEnemyBaseMaster.instance.LoadEnemyBaseInfo(LoadEnemySpawnMaster.instance.spawnList))
            {
                LogExtensions.OutputError("敵基本マスタの読み込みに失敗しました。");
                break;
            }
            LoadEnemyBaseMaster.instance.DebugLog();

            // 出現する敵の成長データを読み込み
            if (!LoadEnemyGrowthMaster.instance.LoadEnemyGrowthInfo(LoadEnemySpawnMaster.instance.spawnList))
            {
                LogExtensions.OutputError("敵成長マスタの読み込みに失敗しました。");
                break;
            }
            LoadEnemyGrowthMaster.instance.DebugLog();

            ret = true;

        } while (false);

        LogExtensions.OutputInfo("敵データの読み込みを終了します。");

        return ret;
    }

    /// <summary>
    /// 敵リソースを読み込む
    /// </summary>
    /// <returns>リソースの読み込みに失敗した場合:false 読み込みに成功:true</returns>
    public bool LoadResources()
    {
        LogExtensions.OutputInfo("敵リソースの読み込みを開始します");

        m_resourcesList = new Dictionary<int, GameObject>();

        GameObject resource = null;
        foreach(KeyValuePair<int, EnemyBaseMaster.Param> param in LoadEnemyBaseMaster.instance.enemeyBaseMasterList)
        {
            if(null == (resource = Resources.Load("EnemyData\\" + param.Value.path) as GameObject))
            {
                LogExtensions.OutputError("敵リソースの読み込みに失敗しました。path = Resources\\EnemyData\\" + param.Value.path);
                break;
            }
            LogExtensions.OutputInfo("敵リソースの読み込みに成功しました。path = Resources\\EnemyData\\" + param.Value.path);
            m_resourcesList.Add(param.Value.id, resource);
        }

        LogExtensions.OutputInfo("敵リソースの読み込みを終了します");

        return (m_resourcesList.Count != 0);
    }

    /// <summary>
    /// ステージ詳細ID、章IDに紐づく敵を生成する。
    /// 指定した区間の敵データを生成する
    /// </summary>
    /// <param name="stageDetaileId">ステージ詳細ID</param>
    /// <returns>生成成功:true 生成失敗:false</returns>
    public bool CreateEnemyOfThisPlace(int stageDetaileId)
    {
        LogExtensions.OutputInfo("敵を生成します。ステージ詳細ID[" + stageDetaileId + "]");

        m_enemyList     = new List<GameObject>();
        
        // 指定したステージ詳細IDに一致すてうステージ出現マスタを取得する
        EnemySpawnMaster.Param enemySpawnMasterParam = null;
        for (int i = 0; i < LoadEnemySpawnMaster.instance.spawnList.Count; ++i)
        {
            if (LoadEnemySpawnMaster.instance.spawnList[i].stage_detail_id != stageDetaileId)
            {
                continue;
            }
            enemySpawnMasterParam = LoadEnemySpawnMaster.instance.spawnList[i];
            break;
        }

        // 読み込みに失敗した場合
        if (enemySpawnMasterParam == null)
        {
            LogExtensions.OutputError("指定したステージ詳細IDに一致する敵出現マスタが存在しません。 stategDetailId = " + stageDetaileId);
            return false;
        }

        int rand = 0;

        // UnityEngine.Random.Rangeで使用する最大値
        // (UnityEngine.Random.Range()はintだとmin <= x < maxの範囲で乱数を作成するため、maxには+1した値を渡す)
        int frequency_rand_max = LoadEnemySpawnMaster.instance.FREQUENCY_MAX + 1;

        // 出現する敵の最大数、敵を生成する
        while (true)
        {
            // 出現確率
            rand = UnityEngine.Random.Range(1, frequency_rand_max);

            // 一種類目の敵の生成を試みる
            if (RandomCreateEnemy(enemySpawnMasterParam.enemy1_id, enemySpawnMasterParam.enemy1_lv, enemySpawnMasterParam.enemy1_frequency, rand))
            {
                if ((m_enemyList.Count >= enemySpawnMasterParam.respawn_max)) { break; }
            }

            // 二種類目の敵の生成を試みる
            if (RandomCreateEnemy(enemySpawnMasterParam.enemy2_id, enemySpawnMasterParam.enemy2_lv, enemySpawnMasterParam.enemy2_frequency, rand))
            {
                if ((m_enemyList.Count >= enemySpawnMasterParam.respawn_max)) { break; }
            }

            // 三種類目の敵の生成を試みる
            if (RandomCreateEnemy(enemySpawnMasterParam.enemy3_id, enemySpawnMasterParam.enemy3_lv, enemySpawnMasterParam.enemy3_frequency, rand))
            {
                if ((m_enemyList.Count >= enemySpawnMasterParam.respawn_max)) { break; }
            }

        }

        return true;
    }

    /// <summary>
    /// 敵を確率で生成する
    /// </summary>
    /// <param name="enemyId">敵の管理ID</param>
    /// <param name="enemyLv">敵のレベル</param>
    /// <param name="frequency">出現確率</param>
    /// <param name="rand">乱数</param>
    /// <returns>true:敵を生成した false:敵を生成しなかった</returns>
    private bool RandomCreateEnemy(int enemyId,int enemyLv,int frequency,int rand)
    {
        bool ret = false;

        do
        {
            // 敵出現マスタに敵管理IDが設定されていない場合、break
            if(enemyId == 0) { break; }

            // 出現確率が設定されている場合、出現確率が rand <= frequencyの条件を満たしているかをチェックし
            // 満たしていない場合はcontinue
            if (frequency != 0 && rand > frequency) { break; }

            // インスタンスデータを作成する
            GameObject temp = Instantiate(m_resourcesList[enemyId]);
            temp.GetComponent<EnemyWolf>().Initialize(LoadEnemyGrowthMaster.instance.enemyGrowthMasterList[enemyId][enemyLv]);
            m_enemyList.Add(temp);
            ret = true;

        } while (false);

        return ret;
    }

    void Start()
    {
        if (LoadEnemyMasterData(1, 1))
        {
            if(LoadResources())
            {
                CreateEnemyOfThisPlace(4);
            }
        }
    }

}
