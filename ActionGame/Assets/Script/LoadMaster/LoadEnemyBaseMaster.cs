using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class LoadEnemyBaseMaster : TextMasterManager
{
    /// <summary>自身のインスタンス</summary>
    static LoadEnemyBaseMaster m_instance = new LoadEnemyBaseMaster();

    /// <summary>自身のインスタンスを取得</summary>
    static public LoadEnemyBaseMaster Instance { get { return m_instance; } }

    /// <summary> 敵管理ID毎の敵基本マスタリスト </summary>
    private Dictionary<int, EnemyBaseMaster.Param> m_enemyBaseMasterList = null;

    /// <summary>敵基本マスタリストを取得 </summary>
    public Dictionary<int,EnemyBaseMaster.Param> EnemeyBaseMasterList { get { return m_enemyBaseMasterList; } }
   
    /// <summary>マスターデータのファイルパス</summary>
    const string filename = "Assets/Resources/MasterData/敵基本マスタ.txt";

    public bool Init()
    {
        LogExtensions.OutputInfo("敵基本マスタを読み込みます。");

        bool ret = false;
        base.Open(filename);

        string[] lineAll = base.GetLineAll();
        if(lineAll != null)
        {
            m_enemyBaseMasterList = new Dictionary<int, EnemyBaseMaster.Param>(lineAll.Length);
            EnemyBaseMaster.Param temp = null;
            foreach (string line in lineAll)
            {
                temp = JsonUtility.FromJson<EnemyBaseMaster.Param>(line);
                m_enemyBaseMasterList.Add(temp.Id, temp);
            }

            ret = true;
            LogExtensions.OutputInfo("敵基本マスタの読み込みに成功しました。");
        }
        else
        {
            LogExtensions.OutputError("敵基本マスタの読み込みに失敗しました。");
        }
        base.Close();

        return ret;
    }

    /// <summary>
    /// 敵基本パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(EnemyBaseMaster.Param param)
    {
        LogExtensions.OutputInfo("[敵基本マスタ] => " +
            "[name:"                    + param.Name                      + "] " +
            "[id:"                      + param.Id                        + "] " +
            "[path:"                    + param.Path                      + "]"  +
            "[class_name:"              + param.Class_name                + "] " +
            "[tribe_name:"              + param.Tribe_name                + "] " +
            "[drop_item_id1:"           + param.Drop_item_id1             + "] " +
            "[drop_item_num1:"          + param.Drop_item_num1            + "] " +
            "[drop_item_add_rnd1:"      + param.Drop_item_add_rnd1        + "] " +
            "[drop_item_name2:"         + param.Drop_item_id2             + "] " +
            "[drop_item_num2:"          + param.Drop_item_num2            + "] " +
            "[drop_item_add_rnd2:"      + param.Drop_item_add_rnd2        + "] " +
            "[rare_drop_item_id1:"      + param.Rare_drop_item_id1        + "] " +
            "[rare_drop_item_num1:"     + param.Rare_drop_item_num1       + "] " +
            "[rare_drop_item_add_rnd1:" + param.Rare_drop_item_add_rnd1   + "] " 
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
