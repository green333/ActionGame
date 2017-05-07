using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyBaseMaster
{ 
    /// <summary>
    /// インスタンス
    /// </summary>
    static readonly LoadEnemyBaseMaster instance = new LoadEnemyBaseMaster();

    /// <summary>
    /// 敵基本マスタを格納する変数
    /// </summary>
    EnemyBaseMaster master;

    /// <summary>
    /// インスタンスを取得
    /// </summary>
    static public LoadEnemyBaseMaster Instace { get { return instance; } }

    /// <summary>
    /// 初期化時に敵基本マスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if (null == (master = Resources.Load<EnemyBaseMaster>("MasterData/EnemyBaseMaster")))
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

        EnemyBaseMaster.Param temp = new EnemyBaseMaster.Param();
        temp.id    = master.list[index].id;
        temp.index = master.list[index].index;
        temp.name  = master.list[index].name;

        return temp;
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
