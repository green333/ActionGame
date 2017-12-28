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

    /// <summary>
    /// ステージ上に出現している敵のインスタンスリスト
    /// </summary>
    Dictionary<GameObject, Enemy> m_enemyList = null;

    /// <summary>
    /// ステージに出現する敵の最大数
    /// </summary>
    private int m_enemyCountMax = 0;

    /// <summary>
    /// 敵インスタンスリスト取得プロパティ
    /// </summary>
    public Dictionary<GameObject,Enemy> EnemyList { get { return m_enemyList; } }
    /// <summary>
    /// 敵のプレハブを読み込む
    /// </summary>
    /// <returns>リソースの読み込みに失敗した場合:false 読み込みに成功:true</returns>
    private bool LoadPrefabs()
    {
        LogExtensions.OutputInfo("敵のプレハブデータの読み込みを開始します");

        m_resourcesList = new Dictionary<int, GameObject>();

        // 現在のステージIDと章IDから出現する敵
        foreach (EnemySpawnMaster.Param param in LoadEnemySpawnMaster.Instance.SpawnList)
        {
            // ステージID、または章IDが違っていたらcontinue
            if (param.Stage_id != SaveData.Instance.stageId && param.Chapter_id != SaveData.Instance.chapter)
            {
                continue;
            }
            // 出現する敵が設定されていない場合、EnemyX_idには0が入っている

            // 一体目の敵のプレハブデータを読み込み、リストに追加する
            if (param.Enemy1_id != 0 && !m_resourcesList.ContainsKey(param.Enemy1_id))
            {
                AddPrefabList(param.Enemy1_id);
            }
            // 二体目の敵のプレハブデータを読み込み、リストに追加する
            if (param.Enemy2_id != 0 && !m_resourcesList.ContainsKey(param.Enemy2_id))
            {
                AddPrefabList(param.Enemy2_id);
            }
            // 三体目の敵のプレハブデータを読み込み、リストに追加する
            if (param.Enemy3_id != 0 && !m_resourcesList.ContainsKey(param.Enemy3_id))
            {
                AddPrefabList(param.Enemy3_id);
            }
        }

        LogExtensions.OutputInfo("敵のプレハブデータの読み込みを終了します");

        return (m_resourcesList.Count != 0);
    }

    /// <summary>
    /// プレハブを読み込み、リストに追加する
    /// </summary>
    /// <param name="enemyId">敵管理ID</param>
    private void AddPrefabList(int enemyId)
    {
        if(LoadEnemyBaseMaster.Instance.EnemeyBaseMasterList.ContainsKey(enemyId))
        {
            EnemyBaseMaster.Param enemyBaseParam = LoadEnemyBaseMaster.Instance.EnemeyBaseMasterList[enemyId];
            GameObject resource = null;
            if (null == (resource = Resources.Load("Prefab\\EnemyData\\" + enemyBaseParam.Path) as GameObject))
            {
                LogExtensions.OutputError("敵のプレハブデータの読み込みに失敗しました。path = Resources\\Prefab\\EnemyData\\" + enemyBaseParam.Path);
            }
            else
            {
                LogExtensions.OutputInfo("敵のプレハブデータの読み込みに成功しました。path = Resources\\Prefab\\EnemyData\\" + enemyBaseParam.Path);
                m_resourcesList.Add(enemyId, resource);
            }
        }else
        {
            // 敵基本マスタには存在しない敵の管理IDが敵出現マスタに設定されている。
            LogExtensions.OutputError("敵出現マスタに、敵基本マスタに存在しない敵管理IDが設定されています。敵管理ID[" + enemyId + "],ステージID[" + SaveData.Instance.stageId + "],章ID[" + SaveData.Instance.chapter + "]");
        }
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

        // 指定したステージ詳細IDに一致するステージ出現マスタを取得する
        EnemySpawnMaster.Param enemySpawnMasterParam = null;
        foreach(EnemySpawnMaster.Param param in LoadEnemySpawnMaster.Instance.SpawnList)
        {
            if(param.Stage_id == SaveData.Instance.stageId && param.Chapter_id == SaveData.Instance.chapter && param.Stage_detail_id == stageDetaileId)
            {
                enemySpawnMasterParam = param;
                break;
            }
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
        int frequency_rand_max = LoadEnemySpawnMaster.Instance.FREQUENCY_MAX + 1;

        bool isError;

        // 出現する敵の最大数、敵を生成する
        while (true)
        {
            // 出現確率
            rand = UnityEngine.Random.Range(1, frequency_rand_max);

            // 一種類目の敵の生成を試みる
            if (RandomCreateEnemy(enemySpawnMasterParam.Enemy1_id, enemySpawnMasterParam.Enemy1_lv, enemySpawnMasterParam.Enemy1_frequency, rand, enemySpawnMasterParam.Enemy1_respawn_time, out isError))
            {
                // 生成できる限界数に到達していたらbreak
                if ((m_enemyList.Count >= m_enemyCountMax)) { break; }
            }
            if(isError)
            {
                break;
            }

            // 二種類目の敵の生成を試みる
            if (RandomCreateEnemy(enemySpawnMasterParam.Enemy2_id, enemySpawnMasterParam.Enemy2_lv, enemySpawnMasterParam.Enemy2_frequency, rand, enemySpawnMasterParam.Enemy2_respawn_time, out isError))
            {
                // 生成できる限界数に到達していたらbreak
                if ((m_enemyList.Count >= m_enemyCountMax)) { break; }
            }
            if (isError)
            {
                break;
            }

            // 三種類目の敵の生成を試みる
            if (RandomCreateEnemy(enemySpawnMasterParam.Enemy3_id, enemySpawnMasterParam.Enemy3_lv, enemySpawnMasterParam.Enemy3_frequency, rand, enemySpawnMasterParam.Enemy3_respawn_time,  out isError))
            {
                // 生成できる限界数に到達していたらbreak
                if ((m_enemyList.Count >= m_enemyCountMax)) { break; }
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
            if(!m_resourcesList.ContainsKey(enemyId))
            {
                // プレハブの読み込みに失敗している場合、インスタンス化はしない。
                LogExtensions.OutputError("プレハブが見つかりません。敵ID[" + enemyId + "],プレハブ名[" + LoadEnemyBaseMaster.Instance.EnemeyBaseMasterList[enemyId].Path + "]");
                break;
            }
            GameObject instance = Instantiate(m_resourcesList[enemyId]) as GameObject;
            if(instance == null)
            {
                LogExtensions.OutputError("Instantia()に失敗しました。敵ID[" + enemyId + "]");
                isError = true;
                break;
            }
            // コンポーネントを取得する
            Enemy component = GetComponent(instance,LoadEnemyBaseMaster.Instance.EnemeyBaseMasterList[enemyId].Path);
            if(component == null)
            {
                LogExtensions.OutputError("指定したプレハブが存在しません。敵ID[" + enemyId + "],path["+ LoadEnemyBaseMaster.Instance.EnemeyBaseMasterList[enemyId].Path+"]");
                isError = true;
                break;
            }
            component.Initialize(LoadEnemyGrowthMaster.Instance.EnemyGrowthMasterList[enemyId][enemyLv], respawnTime);
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

    public void LoadEnemyData()
    {
        if (LoadPrefabs())
        {
            m_enemyList = new Dictionary<GameObject, Enemy>();

            foreach(EnemySpawnMaster.Param param in LoadEnemySpawnMaster.Instance.SpawnList)
            {
                if(param.Stage_id == SaveData.Instance.stageId && param.Chapter_id == SaveData.Instance.chapter)
                {
                    m_enemyCountMax += param.Respawn_max;
                    CreateEnemyOfThisPlace(param.Stage_detail_id);
                }
            }
        }
    }

    void Start()
    {
        LoadEnemyData();
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
                // 以下を一度だけ行うようにしたい
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
        for (int i = 0; i < LoadEnemySpawnMaster.Instance.SpawnList.Count; ++i)
        {
            if (LoadEnemySpawnMaster.Instance.SpawnList[i].Stage_detail_id != stageDetailId)
            {
                continue;
            }

            if(m_enemyList.Count == m_enemyCountMax)
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
