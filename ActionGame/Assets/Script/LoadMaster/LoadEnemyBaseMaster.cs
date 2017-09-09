using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

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

    private Dictionary<int, EnemyBaseMaster.Param> m_enemyBaseMasterList = null;

    /// <summary>
    /// マスターデータのファイルパス
    /// </summary>
    const string filename = "Assets/Resources/MasterData/敵基本マスタ.txt";

    /// <summary>
    /// 名前カラム
    /// </summary>
    const string COL_NAME = "name";


    public void LoadEnemyBaseInfo(List<EnemySpawnMaster.Param> param)
    {
        m_enemyBaseMasterList = new Dictionary<int, EnemyBaseMaster.Param>();

        IEnumerator parmaList = param.GetEnumerator();

        EnemySpawnMaster.Param temp = null;
        int[] selectIdList = Enumerable.Repeat<int>(0, param.Count * 3).ToArray();
        int index = 0;
        while (parmaList.MoveNext())
        {
            temp = (EnemySpawnMaster.Param)parmaList.Current;
            selectIdList[index++] = temp.enemy1_id;
            selectIdList[index++] = temp.enemy2_id;
            selectIdList[index++] = temp.enemy3_id;
        }
        string[] selectList = Array.ConvertAll(selectIdList, delegate (int value) { return value.ToString(); });

        base.Open(filename);
        string[] getJsonStr = base.SearchList(selectList);
        base.Close();

        EnemyBaseMaster.Param temp2 = null;
        foreach (string str in getJsonStr)
        {
            if (str != string.Empty)
            {
                temp2= JsonUtility.FromJson<EnemyBaseMaster.Param>(str);
                m_enemyBaseMasterList.Add(temp2.id, temp2);
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
