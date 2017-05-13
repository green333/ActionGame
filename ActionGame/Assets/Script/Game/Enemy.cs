using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    List<EnemyBaseMaster.Param> baseParamList;
    List<EnemyGrowthMaster.Param> growthParamList;

    /*
     * EnemyBaseMaster.Paramを変数として持たせること。
     * 
     * セーブデータクラスが保持している章とステージIDを使用してLoadEnemySpawnMasterのGetEnemySpawanInfo（）から出現する敵の情報を取得。
     * 
     * 取得した敵出現情報をLoadEnemyBaseMasterのGetEnemyInfo()に投げて、出現する敵の基本情報を取得する。
     * 
     * LoadEnemySpawnMaster()とGetEnemyInfo()から取得したデータと、
     * プレイヤーのレベルを使用してLoadEnemyGrowthMasterクラスのGetEnemyInfo()から敵のステータス情報を取得する。
     * 
     * なお出現する敵の数はGetEnemySpawanInfo()で取得した情報の中に入っているので、その数だけ敵を生成すること。
     * 
     * (現在プレイヤークラスにレベル変数を持たせていないので、敵のステータスを取得するときは引数に１を与えること。)
     */

    // Use this for initialization
	void Start () {
        SaveData.Instance.stageId = 1;
        SaveData.Instance.chapter = 1;
        EnemySpawnMaster.Param enemySpawnParam = LoadEnemySpawnMaster.Instance.GetEnemySpawanInfo(SaveData.Instance.stageId, SaveData.Instance.chapter);
        LoadEnemyBaseMaster.Instance.GetEnemyInfo(out baseParamList, enemySpawnParam);
        LoadEnemyGrowthMaster.Instance.GetEnemyInfo(out growthParamList, baseParamList, enemySpawnParam, 1);

        foreach (EnemyGrowthMaster.Param p in this.growthParamList)
        {
            Debug.Log("lv :" + p.level.ToString());
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
