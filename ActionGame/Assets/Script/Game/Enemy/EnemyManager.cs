using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// 敵を管理するクラス
/// </summary>
public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// Resourceから読み込んだデータ
    /// </summary>
    Dictionary<int, GameObject> m_resourcesList = null;

    Dictionary<GameObject,Enemy> m_enemyList = null;

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
            if(null == (resource = Resources.Load("Prefab\\EnemyData\\" + param.Value.path) as GameObject))
            {
                LogExtensions.OutputError("敵リソースの読み込みに失敗しました。path = Resources\\Prefab\\EnemyData\\" + param.Value.path);
                break;
            }
            LogExtensions.OutputInfo("敵リソースの読み込みに成功しました。path = Resources\\Prefab\\EnemyData\\" + param.Value.path);
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

        bool isError;

        // 出現する敵の最大数、敵を生成する
        while (true)
        {
            // 出現確率
            rand = UnityEngine.Random.Range(1, frequency_rand_max);

            // 一種類目の敵の生成を試みる
            if (RandomCreateEnemy(enemySpawnMasterParam.enemy1_id, enemySpawnMasterParam.enemy1_lv, enemySpawnMasterParam.enemy1_frequency, rand, enemySpawnMasterParam.enemy1_respawn_time, out isError))
            {
                if ((m_enemyList.Count >= enemySpawnMasterParam.respawn_max)) { break; }
            }
            if(isError)
            {
                break;
            }

            // 二種類目の敵の生成を試みる
            if (RandomCreateEnemy(enemySpawnMasterParam.enemy2_id, enemySpawnMasterParam.enemy2_lv, enemySpawnMasterParam.enemy2_frequency, rand, enemySpawnMasterParam.enemy2_respawn_time, out isError))
            {
                if ((m_enemyList.Count >= enemySpawnMasterParam.respawn_max)) { break; }
            }
            if (isError)
            {
                break;
            }

            // 三種類目の敵の生成を試みる
            if (RandomCreateEnemy(enemySpawnMasterParam.enemy3_id, enemySpawnMasterParam.enemy3_lv, enemySpawnMasterParam.enemy3_frequency, rand, enemySpawnMasterParam.enemy3_respawn_time,  out isError))
            {
                if ((m_enemyList.Count >= enemySpawnMasterParam.respawn_max)) { break; }
            }
            if (isError)
            {
                break;
            }
        }

        return !isError;
    }

    /// <summary>
    /// 敵を確率で生成する
    /// </summary>
    /// <param name="enemyId">敵の管理ID</param>
    /// <param name="enemyLv">敵のレベル</param>
    /// <param name="frequency">出現確率</param>
    /// <param name="rand">乱数</param>
    /// <returns>true:敵を生成した false:敵を生成しなかった</returns>
    private bool RandomCreateEnemy(int enemyId,int enemyLv,int frequency,int rand,int respawnTime, out bool isError)
    {
        bool ret = false;
        isError = false;

        do
        {
            // 敵出現マスタに敵管理IDが設定されていない場合、break
            if(enemyId == 0) { break; }

            // 出現確率が設定されている場合、出現確率が rand <= frequencyの条件を満たしているかをチェックし
            // 満たしていない場合はcontinue
            if (frequency != 0 && rand > frequency) { break; }

            // インスタンスデータを作成する
            GameObject instance = Instantiate(m_resourcesList[enemyId]) as GameObject;
            if(instance == null)
            {
                LogExtensions.OutputError("Instantia()に失敗しました。敵ID[" + enemyId + "]");
                isError = true;
                break;
            }
            // コンポーネントを取得する
            Enemy component = GetComponent(instance,LoadEnemyBaseMaster.instance.enemeyBaseMasterList[enemyId].path);
            if(component == null)
            {
                LogExtensions.OutputError("指定したプレハブが存在しません。敵ID[" + enemyId + "],path["+ LoadEnemyBaseMaster.instance.enemeyBaseMasterList[enemyId].path+"]");
                isError = true;
                break;
            }
            component.Initialize(LoadEnemyGrowthMaster.instance.enemyGrowthMasterList[enemyId][enemyLv], respawnTime);
            m_enemyList.Add(instance, component);
            
            ret = true;

        } while (false);

        return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance">Instantiate()で作成したインスタンス</param>
    /// <param name="path">敵基本に設定されているpath</param>
    /// <returns>プレハブにアタッチしたEnemyを継承したコンポーネント</returns>
    private Enemy GetComponent(GameObject instance,string path)
    {
        Enemy ret = null;
        switch(path)
        {
            case "walker": ret = instance.GetComponent<EnemyWolf>(); break;
            default: LogExtensions.OutputError("path[" + path + "]に紐づいたプレハブがありません。");break;
        }
        return ret;
    }


    void Start()
    {
        if (LoadEnemyMasterData(1, 1))
        {
            if(LoadResources())
            {
                m_enemyList = new Dictionary<GameObject, Enemy>();
                CreateEnemyOfThisPlace(4);
            }
        }
    }

    void Update()
    {
        // 削除するオブジェクトキーリスト
        GameObject[] deleteKeyList = Enumerable.Repeat<GameObject>(null, m_enemyList.Count).ToArray();
        int index = 0;

        //  死んだ敵がいるかをチェックする
        foreach (KeyValuePair<GameObject, Enemy> param in m_enemyList)
        {
            // 敵が死んでいる場合、削除リストに追加してcontinueする
            if(param.Value.IsDead())
            {
                deleteKeyList[index++] = param.Key;


                Vector3 genPos = new Vector3(0,1,0) + param.Key.transform.position;
                GameObject obj = ItemController.Instance.Generate(ItemController.ITEM_TYPE.HERBS, genPos);
                obj.GetComponent<Herbs>().Init(Item.POSSESSOR.ENEMY);


                continue;
            }
        }

        // 削除を行う
        foreach (GameObject key in deleteKeyList)
        {
            if(key == null) { continue; }

            // 削除していい場合は削除する
            if (m_enemyList[key].IsDelete())
            {
                // 経験値を取得する
                m_enemyList[key].GetEXP();
                // アイテムをステージ上に落とす
                m_enemyList[key].ItemDrop();
                // リストからの削除とインスタンスの削除を行う。
                m_enemyList.Remove(key);
                Destroy(key);
            }
        }
            
        // リスポーン
        Respawn();
    }

    void Respawn()
    {
        bool enableSpawn = true;
        int stageDetailId = 4;
        for (int i = 0; i < LoadEnemySpawnMaster.instance.spawnList.Count; ++i)
        {
            if (LoadEnemySpawnMaster.instance.spawnList[i].stage_detail_id != stageDetailId)
            {
                continue;
            }

            if(m_enemyList.Count == LoadEnemySpawnMaster.instance.spawnList[i].respawn_max)
            {
                enableSpawn = false;
            }
            break;
        }


        // 敵の数が減り、リスポーン処理が行える場合行う
        if(enableSpawn)
        {
            CreateEnemyOfThisPlace(stageDetailId);
        }

    }
}
