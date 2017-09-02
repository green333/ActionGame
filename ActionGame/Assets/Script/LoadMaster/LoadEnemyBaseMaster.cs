using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    public void GetEnemyInfo(out List<EnemyBaseMaster.Param> eneymInfoList,int[] selectIdList)
    {
        eneymInfoList = new List<EnemyBaseMaster.Param>();

        base.Open(filename);
        string[] getJsonStr = base.SearchList(Array.ConvertAll(selectIdList, delegate (int value) { return value.ToString(); }));
        base.Close();
        foreach (string str in getJsonStr)
        {
            if (str != string.Empty)
            {
                eneymInfoList.Add(JsonUtility.FromJson<EnemyBaseMaster.Param>(str));
            }
        }
    }

    /// <summary>
    /// 敵出現マスタから取得したデータをもとに、敵の情報を取得する
    /// </summary>
    /// <param name="eneymInfoList"></param>
    /// <param name="esMasterParam"></param>
    public void GetEnemyInfo(ref List<EnemyBaseMaster.Param> eneymInfoList,EnemySpawnMaster.Param esMasterParam)
    {

        // 検索する名前一覧
        string[] searchNameList = new string[3] {"","",""};
        if (esMasterParam.enemy1_id != 0) { searchNameList[0] = base.VariableToJson(COL_NAME, esMasterParam.enemy1_id); }
        if (esMasterParam.enemy2_id != 0) { searchNameList[1] = base.VariableToJson(COL_NAME, esMasterParam.enemy2_id); }
        if (esMasterParam.enemy3_id != 0) { searchNameList[2] = base.VariableToJson(COL_NAME, esMasterParam.enemy3_id); }

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
            "[class_name:"              + param.class_name                + "] " +
            "[tribe_name:"              + param.tribe_name                + "] " +
            "[drop_item_id1:"           + param.drop_item_id1             + "] " +
            "[drop_item_num1:"          + param.drop_item_num1            + "] " +
            "[drop_item_add_rnd1:"      + param.drop_item_add_rnd1        + "] " +
            "[drop_item_name2:"         + param.drop_item_id2             + "] " +
            "[drop_item_num2:"          + param.drop_item_num2            + "] " +
            "[drop_item_add_rnd2:"      + param.drop_item_add_rnd2        + "] " +
            "[rare_drop_item_id1:"      + param.rare_drop_item_id1        + "] " +
            "[rare_drop_item_num1:"     + param.rare_drop_item_num1       + "] " +
            "[rare_drop_item_add_rnd1:" + param.rare_drop_item_add_rnd1   + "] " 
            );
    }
}
