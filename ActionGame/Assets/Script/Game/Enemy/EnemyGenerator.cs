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
    /// 指定したステージIDと章IDに一致する敵データをメモリに展開する
    /// </summary>
    /// <param name="baseParamList"></param>
    /// <param name="growthParamList"></param>
    public void LoadEnemyMasterData(int stageId,int chapterId,int playerLv)
    {
        m_resourcesList = new Dictionary<int, GameObject>();
        m_enemyList = new List<GameObject>();

        LogExtensions.OutputInfo("敵データの読み込みを開始します");

        // 章毎の敵出現マスタを読み込む
        if(!LoadEnemySpawnMaster.instance.LoadEnemySpawanInfo(stageId, chapterId))
        {
            LogExtensions.OutputError("敵基本マスタの読み込みに失敗しました。stageId=" + stageId + ",chapterId=" + chapterId + ",playerLv=" + playerLv);
            return;
        }

        // 出現する敵の基本マスタを読み込む
        LoadEnemyBaseMaster.instance.LoadEnemyBaseInfo(LoadEnemySpawnMaster.instance.spawnList);

        // TODO:リアルタイムで出現する敵のデータを取得する場合、この処理だけ関数化する必要がある。
        // (リアルタイムで取得するというのは、プレイヤーのレベルが上がった場合、取得する敵のレベル範囲が変わるため、もう一度取得する必要がある)
        // 出現する敵のデータを取得
        LoadEnemyGrowthMaster.instance.LoadEnemyGrowthInfo(playerLv, LoadEnemySpawnMaster.instance.spawnList);
    }

    /// <summary>
    /// ステージID、章ID,ステージ詳細IDに紐づく敵を生成する。
    /// 指定したステージの区分に出現する敵を生成する
    /// 
    /// </summary>
    public bool CreateEnemyOfThisPlace(int stageDetaileId,int playerLv)
    {
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
        int min = 0;
        int max = 0;
        int createdCount = 0;

        // 出現する敵の最大数、敵を生成する
        while (createdCount != enemySpawnMasterParam.respawn_max)
        {
            // 出現する確率は1~100まで( UnityEngine.Random.Range()はintだとmin <= x < maxの範囲で乱数を作成するため、maxには+1した値を渡す)
            rand = UnityEngine.Random.Range(1, 101);

            // 以下の処理。
            // 敵IDが0以外のものだけ、出現確率をチェックし生成を行えるかを判断する。
            // 生成できた場合はcreatedCountをカウントする。
            // なお以下の敵のどれか一体は必ず生成させる。
            // (一体目の敵が出現確率によって生成できない場合、出現確率を高める計算を行っている。)

            if (enemySpawnMasterParam.enemy1_id != 0)
            {
                if (rand <= enemySpawnMasterParam.enemy1_frequency)
                {
                    min = playerLv - enemySpawnMasterParam.enemy1_lvpm;
                    max = playerLv + enemySpawnMasterParam.enemy1_lvpm;
                    if (min < 1) { min = 1; }
                    if (max > LoadEnemyGrowthMaster.ENEMY_LEVEL_MAX) { max = LoadEnemyGrowthMaster.ENEMY_LEVEL_MAX; }

                    if (!m_resourcesList.ContainsKey(enemySpawnMasterParam.enemy1_id))
                    {
                        m_resourcesList.Add(enemySpawnMasterParam.enemy1_id, Resources.Load("EnemyData\\Cube") as GameObject);
                    }
                    GameObject temp = Instantiate(m_resourcesList[enemySpawnMasterParam.enemy1_id]);
                    temp.GetComponent<EnemyWolf>().Initialize(LoadEnemyGrowthMaster.instance.enemyList[enemySpawnMasterParam.enemy1_id][UnityEngine.Random.Range(min, max + 1)]);
                    m_enemyList.Add(temp);

                    ++createdCount;
                }
                rand = 100 - rand;
            }

            if (enemySpawnMasterParam.enemy2_id != 0)
            { 
                if (rand <= enemySpawnMasterParam.enemy2_frequency)
                {
                    min = playerLv - enemySpawnMasterParam.enemy2_lvpm;
                    max = playerLv + enemySpawnMasterParam.enemy2_lvpm;
                    if (min < 1) { min = 1; }
                    if (max > LoadEnemyGrowthMaster.ENEMY_LEVEL_MAX) { max = LoadEnemyGrowthMaster.ENEMY_LEVEL_MAX; }


                    if (!m_resourcesList.ContainsKey(enemySpawnMasterParam.enemy2_id))
                    {
                        m_resourcesList.Add(enemySpawnMasterParam.enemy2_id, Resources.Load("EnemyData\\Cube") as GameObject);
                    }
                    GameObject temp = Instantiate(m_resourcesList[enemySpawnMasterParam.enemy2_id]);
                    temp.GetComponent<EnemyWolf>().Initialize(LoadEnemyGrowthMaster.instance.enemyList[enemySpawnMasterParam.enemy2_id][UnityEngine.Random.Range(min, max + 1)]);
                    m_enemyList.Add(temp);
                    ++createdCount;
                }
            }

            if (enemySpawnMasterParam.enemy3_id != 0)
            {
                min = playerLv - enemySpawnMasterParam.enemy3_lvpm;
                max = playerLv + enemySpawnMasterParam.enemy3_lvpm;
                if (min < 1) { min = 1; }
                if (max > LoadEnemyGrowthMaster.ENEMY_LEVEL_MAX) { max = LoadEnemyGrowthMaster.ENEMY_LEVEL_MAX; }

                if (!m_resourcesList.ContainsKey(enemySpawnMasterParam.enemy3_id))
                {
                    m_resourcesList.Add(enemySpawnMasterParam.enemy3_id, Resources.Load("EnemyData\\Cube") as GameObject);
                }
                GameObject temp = Instantiate(m_resourcesList[enemySpawnMasterParam.enemy3_id]);
                temp.GetComponent<EnemyWolf>().Initialize(LoadEnemyGrowthMaster.instance.enemyList[enemySpawnMasterParam.enemy3_id][UnityEngine.Random.Range(min, max + 1)]);
                m_enemyList.Add(temp);
                ++createdCount;
            }
        }
   
        return true;
    }

    void Start()
    {
        LoadEnemyMasterData(1,1, 1);
        CreateEnemyOfThisPlace(1,1);
    }

}
