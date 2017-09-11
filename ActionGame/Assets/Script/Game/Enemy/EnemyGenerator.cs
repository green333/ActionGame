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
    /// ステージ詳細ID、章IDに紐づく敵を生成する。
    /// </summary>
    /// <param name="stageDetaileId">ステージ詳細ID</param>
    /// <returns>生成成功:true 生成失敗:false</returns>
    public bool CreateEnemyOfThisPlace(int stageDetaileId)
    {
        m_resourcesList = new Dictionary<int, GameObject>();
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
        if(enemySpawnMasterParam == null)
        {
            LogExtensions.OutputError("指定したステージ詳細IDに一致する敵出現マスタが存在しません。 stategDetailId = " + stageDetaileId);
            return false;
        }

        int rand = 0;

        // 出現する敵の最大数、敵を生成する
        while (true)
        {
            // 出現する確率は1~100まで( UnityEngine.Random.Range()はintだとmin <= x < maxの範囲で乱数を作成するため、maxには+1した値を渡す)
            rand = UnityEngine.Random.Range(1, 101);

            // 敵出現マスタに設定した三種類の敵のうち、一体は必ず生成させる(出現確率がすべて設定されている場合、生成する数は一体だけだが、
            // 出現確率が設定されていない敵がいる場合、生成する数は二体以上になる)
            // TODO:ただ、出現確率が設定されていないものがいたときの挙動ができていない。

            // 一種類目の敵の生成を試みる
            if(RandomCreateEnemy(enemySpawnMasterParam.enemy1_id, enemySpawnMasterParam.enemy1_lv, enemySpawnMasterParam.enemy1_frequency, rand))
            {
                if ((m_enemyList.Count >= enemySpawnMasterParam.respawn_max)){ break; }
                // 確率によって生成ができなかったので、生成される確率を変動させる
                rand = UnityEngine.Random.Range(1, 101 - enemySpawnMasterParam.enemy1_frequency);
            }
            
            // 二種類目の敵の生成を試みる
            if (RandomCreateEnemy(enemySpawnMasterParam.enemy2_id, enemySpawnMasterParam.enemy2_lv, enemySpawnMasterParam.enemy2_frequency, rand))
            {
                if ((m_enemyList.Count >= enemySpawnMasterParam.respawn_max)){ break; }
                // 確率によって生成ができなかったので、次の敵は必ず生成されるよう乱数を0に設定する
                rand = 0;
            }

            // 三種類目の敵の生成を試みる
            RandomCreateEnemy(enemySpawnMasterParam.enemy3_id, enemySpawnMasterParam.enemy3_lv, enemySpawnMasterParam.enemy3_frequency, rand);
            if ((m_enemyList.Count >= enemySpawnMasterParam.respawn_max)){ break; }

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

            // コピーする元データとなるリソースが追加されていなければ追加する
            if (!m_resourcesList.ContainsKey(enemyId))
            {
                m_resourcesList.Add(enemyId, Resources.Load("EnemyData\\" + LoadEnemyBaseMaster.instance.enemeyBaseMasterList[enemyId].path) as GameObject);
            }

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
            CreateEnemyOfThisPlace(4);
        }
    }

}
