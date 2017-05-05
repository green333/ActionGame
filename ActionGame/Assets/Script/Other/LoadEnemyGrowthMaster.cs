using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyGrowthMaster : MonoBehaviour {


    static LoadEnemyGrowthMaster instance;
    EnemyGrowthMaster master;

    static public LoadEnemyGrowthMaster GetInstance { get { return instance; } }

    void Awake()
    {
        instance = new LoadEnemyGrowthMaster();
        master = Resources.Load<EnemyGrowthMaster>("MasterData/EnemyGrowthMaster");
    }

    
    /// <summary>
    /// 敵基本マスタパラメーターと敵出現パラメーターとプレイヤーのレベルから、出現する敵の情報を取得する
    /// </summary>
    /// <param name="growthListParam"></param>
    /// <param name="baseMasterParam"></param>
    /// <param name="spawnMasterParam"></param>
    /// <param name="playerLv"></param>
    public void GetEnemyInfo(out List<EnemyGrowthMaster.Param> growthListParam, List<EnemyBaseMaster.Param> baseMasterParam,EnemySpawnMaster.Param spawnMasterParam,int playerLv)
    {
        growthListParam = new List<EnemyGrowthMaster.Param>();

        // UnityEngine.Random.Rangeはint型だと min <= x < maxになるので、maxに指定した値を含めたいので+1をする必要がある。
        int enemy1_lv = playerLv + UnityEngine.Random.Range(-spawnMasterParam.enemy1_lvpm, spawnMasterParam.enemy1_lvpm + 1);
        int enemy2_lv = playerLv + UnityEngine.Random.Range(-spawnMasterParam.enemy2_lvpm, spawnMasterParam.enemy2_lvpm + 1);
        int enemy3_lv = playerLv + UnityEngine.Random.Range(-spawnMasterParam.enemy3_lvpm, spawnMasterParam.enemy3_lvpm + 1);

        // 下限上限を設定
        if (enemy1_lv < 0) { enemy1_lv = 1; }
        if (enemy2_lv < 0) { enemy2_lv = 1; }
        if (enemy3_lv < 0) { enemy3_lv = 1; }

        // TODO:レベルの上限値がまだ正確に決まっていないので、仮値にしておく
        if (enemy1_lv > 20) { enemy1_lv = 20; }
        if (enemy2_lv > 20) { enemy2_lv = 20; }
        if (enemy3_lv > 20) { enemy3_lv = 20; }

        int index = 0;

        // EnemyBaseMaster.Paramのindexはレベル1が始まりなので、
        // レベル１の敵情報を取得する場合、index + 0となる必要があるのでここで-１する必要がある。
        // (あとは計算通り。レベル１０の敵情報を取得する場合はindex + 9となる)
        int[] indexList = { enemy1_lv - 1, enemy2_lv - 1, enemy2_lv - 1 };

        foreach (EnemyBaseMaster.Param param in baseMasterParam)
        {
            // 指定した敵のデータのはじまりからレベル分足した場所がその敵の成長情報となる。
            growthListParam.Add(master.list[param.index + indexList[index++]]);
        }
    }
}

