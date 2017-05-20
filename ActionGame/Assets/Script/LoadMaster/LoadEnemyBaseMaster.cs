using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyBaseMaster : TextMasterManager
{
    const string filename = "Resources/MasterData/敵成長マスタ.txt";

    /// <summary>
    /// 指定した名前に一致する敵の情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public EnemyBaseMaster.Param GetEnemyInfo(string name)
    {
        EnemyBaseMaster.Param param = null;
        base.Open(filename);
        
        string getJsonStr = base.Search(name);
        if(getJsonStr != null)
        {
            param = JsonUtility.FromJson<EnemyBaseMaster.Param>(getJsonStr);
        }

        base.Close();

        return param;
    }

    /// <summary>
    /// 敵出現マスタから取得したデータをもとに、敵の情報を取得する
    /// </summary>
    /// <param name="eneymInfoList"></param>
    /// <param name="esMasterParam"></param>
    public void GetEnemyInfo(out List<EnemyBaseMaster.Param> eneymInfoList,EnemySpawnMaster.Param esMasterParam)
    {
        eneymInfoList = new List<EnemyBaseMaster.Param>();
        base.Open(filename);

        base.Close();
      
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
