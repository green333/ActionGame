using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyBaseMaster : MonoBehaviour {

    LoadEnemyBaseMaster instance;
    EnemyBaseMaster master;

    public LoadEnemyBaseMaster GetInstance { get { return instance; } }

    void Awake()
    {
        instance = new LoadEnemyBaseMaster();
        master = Resources.Load<EnemyBaseMaster>("MasterData/EnemyBaseMaster");
    }

    /// <summary>
    /// 指定した名前とレベルに一致する敵の情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <param name="lv"></param>
    /// <returns></returns>
    public EnemyBaseMaster.Param GetEnemyInfo(string name,int lv)
    {
        int index = 0;

        foreach (EnemyBaseMaster.Param param in master.list)
        {
            if (param.name == name && param.level == lv)
            {
                break;
            }
            ++index;
        }

        return master.list[index];
    }

    /// <summary>
    /// 敵出現マスタから取得したデータをもとに、敵の情報を取得する
    /// </summary>
    /// <param name="eneymInfoList"></param>
    /// <param name="esMasterParam"></param>
    /// <param name="playerLv"></param>
    public void GetEnemyInfo(out List<EnemyBaseMaster.Param> eneymInfoList,EnemySpawnMaster.Param esMasterParam,int playerLv)
    {
        eneymInfoList = new List<EnemyBaseMaster.Param>();

        // 出現する敵のランダム幅
        int enemyLv1 = playerLv + UnityEngine.Random.Range(-esMasterParam.enemy1_lvpm, esMasterParam.enemy1_lvpm);
        int enemyLv2 = playerLv + UnityEngine.Random.Range(-esMasterParam.enemy2_lvpm, esMasterParam.enemy2_lvpm);
        int enemyLv3 = playerLv + UnityEngine.Random.Range(-esMasterParam.enemy3_lvpm, esMasterParam.enemy3_lvpm);

        // 下限上限制限
        if (enemyLv1 < 0) { enemyLv1 = 1; }
        if (enemyLv2 < 0) { enemyLv2 = 1; }
        if (enemyLv3 < 0) { enemyLv3 = 1; }
        if (enemyLv1 > 100) { enemyLv1 = 100; }
        if (enemyLv2 > 100) { enemyLv2 = 100; }
        if (enemyLv3 > 100) { enemyLv3 = 100; }

        foreach (EnemyBaseMaster.Param param in master.list)
        {
            if(param.name == esMasterParam.enemy1_name && param.level == enemyLv1)
            {
                eneymInfoList.Add(param);
            }
            if(param.name == esMasterParam.enemy2_name && param.level == enemyLv2)
            {
                eneymInfoList.Add(param);
            }
            if (param.name == esMasterParam.enemy3_name && param.level == enemyLv3)
            {
                eneymInfoList.Add(param);
            }
        }
    }
}
