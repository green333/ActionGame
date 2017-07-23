using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadEnemyBaseMaster : TextMasterManager
{
    /// <summary>
    /// 自身のインスタンス
    /// </summary>
    static LoadEnemyBaseMaster _instance = new LoadEnemyBaseMaster();

    /// <summary>
    /// インスタンス取得プロパティ
    /// </summary>
    static public LoadEnemyBaseMaster instance { get { return _instance; } }

    /// <summary>
    /// マスターデータのファイルパス
    /// </summary>
    const string filename = "Assets/Resources/MasterData/敵基本マスタ.txt";

    /// <summary>
    /// 名前カラム
    /// </summary>
    const string COL_NAME = "name";

    /// <summary>
    /// 指定した名前に一致する敵の情報を取得する。
    /// 指定した名前に一致するデータがなければnullを返す。
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public EnemyBaseMaster.Param GetEnemyInfo(string name)
    {
        EnemyBaseMaster.Param param = null;
        base.Open(filename);

        string getJsonStr = base.Search(base.VariableToJson(COL_NAME, name));
        if(getJsonStr != string.Empty)
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

        // 検索する名前一覧
        string[] searchNameList = 
        {
            base.VariableToJson(COL_NAME,esMasterParam.enemy1_name),
            base.VariableToJson(COL_NAME,esMasterParam.enemy2_name),
            base.VariableToJson(COL_NAME,esMasterParam.enemy3_name),
        };

        base.Open(filename);
        string[] getJsonStr = base.SearchList(searchNameList);
        base.Close();
        foreach (string str in getJsonStr)
        {
            if (str != string.Empty)
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
        LogExtensions.OutputInfo("[敵基本マスタ] => " +
            "[name:"                    + param.name                      + "] " +
            "[id:"                      + param.id                        + "] " +
            "[index:"                   + param.index                     + "] " +
            "[drop_item_name1:"         + param.drop_item_name1           + "] " +
            "[drop_item_num1:"          + param.drop_item_num1            + "] " +
            "[drop_item_add_rnd1:"      + param.drop_item_add_rnd1        + "] " +
            "[drop_item_name2:"         + param.drop_item_name2           + "] " +
            "[drop_item_num2:"          + param.drop_item_num2            + "] " +
            "[drop_item_add_rnd2:"      + param.drop_item_add_rnd2        + "] " +
            "[rare_drop_item1:"         + param.rare_drop_item1           + "] " +
            "[rare_drop_item_num1:"     + param.rare_drop_item_num1       + "] " +
            "[rare_drop_item_add_rnd1:" + param.rare_drop_item_add_rnd1   + "] " 
            );
    }
}
