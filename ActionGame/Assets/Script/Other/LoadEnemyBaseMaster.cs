using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyBaseMaster : MonoBehaviour {

    static LoadEnemyBaseMaster instance;
    EnemyBaseMaster master;

    static public LoadEnemyBaseMaster GetInstance { get { return instance; } }

    void Awake()
    {
        instance = new LoadEnemyBaseMaster();
        if(null == (master = Resources.Load<EnemyBaseMaster>("MasterData/EnemyBaseMaster")))
        {
            Debug.Log("failed to Resources.Load<EnemyBaseMaster>");
        }
    }

    /// <summary>
    /// 指定した名前に一致する敵の情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public EnemyBaseMaster.Param GetEnemyInfo(string name)
    {
        int index = 0;

        foreach (EnemyBaseMaster.Param param in master.list)
        {
            if (param.name == name)
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
    public void GetEnemyInfo(out List<EnemyBaseMaster.Param> eneymInfoList,EnemySpawnMaster.Param esMasterParam)
    {
        eneymInfoList = new List<EnemyBaseMaster.Param>();

        foreach (EnemyBaseMaster.Param param in master.list)
        {
            if(param.name == esMasterParam.enemy1_name)
            {
                eneymInfoList.Add(param);
            }
            if(param.name == esMasterParam.enemy2_name)
            {
                eneymInfoList.Add(param);
            }
            if (param.name == esMasterParam.enemy3_name)
            {
                eneymInfoList.Add(param);
            }

            if (eneymInfoList.Count >= 3)
            {
                break;
            }
        }
    }
}
