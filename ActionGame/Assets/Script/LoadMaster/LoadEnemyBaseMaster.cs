using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyBaseMaster : TextMasterManager
{
    static LoadEnemyBaseMaster _instance = new LoadEnemyBaseMaster();

    static public LoadEnemyBaseMaster instance { get { return _instance; } }

    const string filename = "Assets/Resources/MasterData/敵基本マスタ.txt";

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

        string[] searchNameList = { esMasterParam.enemy1_name, esMasterParam.enemy2_name, esMasterParam.enemy3_name };

        base.Open(filename);

        string[] getJsonStr = base.SearchList(searchNameList);

        base.Close();

        foreach (string str in getJsonStr)
        {
            if(str != null)
            {
                eneymInfoList.Add(JsonUtility.FromJson<EnemyBaseMaster.Param>(str));
            }
        }

        // 取得しなければならない数と一致していない
        if(eneymInfoList.Count != searchNameList.Length)
        {
            LogExtensions.OutputWarn("敵基本マスタから取得した敵情報の数が少ないです。");
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
