using UnityEngine;
using System.Collections;

public class LoadEnemySpawnMaster : MonoBehaviour{

    LoadEnemySpawnMaster instance;
    EnemySpawnMaster master;

    public LoadEnemySpawnMaster GetInstance { get { return instance; } }

    /// <summary>
    /// 初期化時に敵出現マスタを読み込む
    /// </summary>
    void Awake()
    {
        instance = new LoadEnemySpawnMaster();
        master = Resources.Load<EnemySpawnMaster>("MasterData/EnemySpawnMaster");
    }

    /// <summary>
    /// 指定したステージと章に出現する敵の情報を取得
    /// </summary>
    /// <param name="stage_id"></param>
    /// <param name="chapter_id"></param>
    /// <returns></returns>
    public EnemySpawnMaster.Param GetEnemySpawanInfo(int stage_id,int chapter_id)
    {
        int index = 0;

        foreach (EnemySpawnMaster.Param param in master.list)
        {
            if(param.stage_id == stage_id && param.chapter_id == chapter_id)
            {
                break;
            }
            ++index;
        }

        return master.list[index];
    }
	
}
