using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class LoadEnemyBaseMaster : TextMasterManager
{
    /// <summary>自身のインスタンス</summary>
    static LoadEnemyBaseMaster _instance = new LoadEnemyBaseMaster();

    /// <summary>自身のインスタンスを取得</summary>
    static public LoadEnemyBaseMaster instance { get { return _instance; } }

    /// <summary> 敵管理ID毎の敵基本マスタリスト </summary>
    private Dictionary<int, EnemyBaseMaster.Param> m_enemyBaseMasterList = null;

    /// <summary>敵基本マスタリストを取得 </summary>
    public Dictionary<int,EnemyBaseMaster.Param> enemeyBaseMasterList { get { return m_enemyBaseMasterList; } }
   
    /// <summary>マスターデータのファイルパス</summary>
    const string filename = "Assets/Resources/MasterData/敵基本マスタ.txt";

    /// <summary> 検索に使用するマスターのカラム </summary>
    const string COL_NAME = "name";


    /// <summary>
    /// 敵出現マスタから敵成長マスタを読み込む
    /// </summary>
    /// <param name="param">敵出現マスタリスト</param>
    /// <returns>読み込み成功:true 読み込み失敗:false</returns>
    public bool LoadEnemyBaseInfo(List<EnemySpawnMaster.Param> param)
    {
        m_enemyBaseMasterList = new Dictionary<int, EnemyBaseMaster.Param>();

        int[] selectIdList = Enumerable.Repeat<int>(0, param.Count * 3).ToArray();
        int index = 0;
        for (int i = 0; i < param.Count; ++i)
        {
            selectIdList[index++] = param[i].enemy1_id;
            selectIdList[index++] = param[i].enemy2_id;
            selectIdList[index++] = param[i].enemy3_id;
        }

        string[] selectList = Array.ConvertAll(selectIdList, delegate (int value) { return value.ToString(); });

        base.Open(filename);
        string[] getJsonStr = base.SearchList(selectList);
        base.Close();

        EnemyBaseMaster.Param temp = null;
        foreach (string str in getJsonStr)
        {
            if (str != string.Empty)
            {
                temp = JsonUtility.FromJson<EnemyBaseMaster.Param>(str);
                m_enemyBaseMasterList.Add(temp.id, temp);
            }
        }

        return (m_enemyBaseMasterList.Count != 0);
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

    /// <summary>
    /// 敵基本パラメーターをログに出力する
    /// </summary>
    public void DebugLog()
    {
        foreach (KeyValuePair<int, EnemyBaseMaster.Param> param in m_enemyBaseMasterList)
        {
            DebugLog(param.Value);
        }
    }
}
