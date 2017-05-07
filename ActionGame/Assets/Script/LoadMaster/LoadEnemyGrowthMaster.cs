﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyGrowthMaster
{
    /// <summary>
    /// インスタンス
    /// </summary>
    static readonly LoadEnemyGrowthMaster instance = new LoadEnemyGrowthMaster();

    /// <summary>
    /// 敵成長マスタを格納する変数
    /// </summary>
    EnemyGrowthMaster master;

    /// <summary>
    /// インスタンスを取得
    /// </summary>
    static public LoadEnemyGrowthMaster Instace { get { return instance; } }

    /// <summary>
    /// 初期化時に敵成長マスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if(null == (master = Resources.Load<EnemyGrowthMaster>("MasterData/EnemyGrowthMaster")))
        {
            Debug.Log("failed to Resources.Load<EnemyGrowthMaster>");
        }
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

        const int ENEMY_LEVEL_MAX = 20;

        // 下限上限を設定
        if (enemy1_lv < 0) { enemy1_lv = 1; }
        if (enemy2_lv < 0) { enemy2_lv = 1; }
        if (enemy3_lv < 0) { enemy3_lv = 1; }

        // TODO:レベルの上限値がまだ正確に決まっていないので、仮値にしておく
        if (enemy1_lv > ENEMY_LEVEL_MAX) { enemy1_lv = ENEMY_LEVEL_MAX; }
        if (enemy2_lv > ENEMY_LEVEL_MAX) { enemy2_lv = ENEMY_LEVEL_MAX; }
        if (enemy3_lv > ENEMY_LEVEL_MAX) { enemy3_lv = ENEMY_LEVEL_MAX; }
        
        // インデックスとレベルから作成する
        int index = 0;

        // EnemyBaseMaster.Paramのindexはレベル1が始まりなので、
        // レベル１の敵情報を取得する場合、index + 0となる必要があるのでここで-１する必要がある。
        // (あとは計算通り。レベル１０の敵情報を取得する場合はindex + 9となる)
        int[] indexList = { enemy1_lv - 1, enemy2_lv - 1, enemy3_lv - 1 };

        foreach (EnemyBaseMaster.Param param in baseMasterParam)
        {
            // インデックスを算出する
            indexList[index] += param.index;

            // 下限上限をチェック
            // 下限は取得したindex 上限は取得したindex + レベル上限 - 1となる
            if(indexList[index] < param.index) { indexList[index] = param.index; }
            if(indexList[index] > (param.index + ENEMY_LEVEL_MAX - 1)) { indexList[index] = param.index + ENEMY_LEVEL_MAX - 1; }
            
            // 指定した敵のデータのはじまりからレベル分足した場所がその敵の成長情報となる。
            growthListParam.Add(master.list[indexList[index++]]);
        }
    }
}
