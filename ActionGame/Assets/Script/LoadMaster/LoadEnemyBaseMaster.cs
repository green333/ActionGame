using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyBaseMaster : BaseSingleton<LoadEnemyBaseMaster>
{ 
  
    /// <summary>
    /// 敵基本マスタを格納する変数
    /// </summary>
    EnemyBaseMaster master = null;

    /// <summary>
    /// 初期化時に敵基本マスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if (null == (master = Resources.Load<EnemyBaseMaster>("MasterData/EnemyBaseMaster")))
        {
            LogExtensions.OutputError("failed to resources load enemy base master");
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
        bool bChecked = false;

        foreach (EnemyBaseMaster.Param param in master.list)
        {
            if (param.name == name)
            {
                bChecked = true;
                break;
            }
            ++index;
        }

        if(bChecked == false)
        {
            // 指定した名前に一致するデータがない
            LogExtensions.OutputWarn("there is no enemy base master matching the There is no data matching the specified name = " + name);
            return null;
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

        int index = 0;

        string[] checkList = { esMasterParam.enemy1_name, esMasterParam.enemy2_name, esMasterParam.enemy3_name };

        foreach (EnemyBaseMaster.Param param in master.list)
        {
            if(param.name == checkList[index])
            {
                eneymInfoList.Add(param);
                ++index;
            }

            if (eneymInfoList.Count >= 3)
            {
                break;
            }
        }
    }


    /// <summary>
    /// 敵基本パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(EnemyBaseMaster.Param param)
    {
        LogExtensions.OutputInfo("id    = " + param.id);
        LogExtensions.OutputInfo("name  = " + param.name);
        LogExtensions.OutputInfo("index = " + param.index);
    }
}
