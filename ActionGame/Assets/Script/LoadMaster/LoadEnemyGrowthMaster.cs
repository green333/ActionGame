using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class LoadEnemyGrowthMaster : TextMasterManager
{
    /// <summary> 自身のインスタンス </summary>
    static LoadEnemyGrowthMaster m_instance = new LoadEnemyGrowthMaster();

    /// <summary>　自身のインスタンスを取得 </summary>
    static public LoadEnemyGrowthMaster instance { get { return m_instance; } }

    /// <summary>敵管理ID,レベル毎の敵成長マスタリスト </summary>
    private Dictionary<int, Dictionary<int,EnemyGrowthMaster.Param>> m_enemyGrowthMasterList = null;

    /// <summary> 敵成長マスタを取得 </summary>
    public Dictionary<int, Dictionary<int, EnemyGrowthMaster.Param>> enemyGrowthMasterList { get { return m_enemyGrowthMasterList; } } 

    /// <summary> 敵の最大レベル </summary>
    public const int ENEMY_LEVEL_MAX = 120;

    /// <summary>　マスタデータのファイルパス </summary>
    const string filename = "Assets/Resources/MasterData/敵成長マスタ.txt";

    /// <summary> 検索に使用するマスターのカラム</summary>
    const string COL_ID         = "id";
    const string COL_LEVEL      = "level";


    public bool Init()
    {
        LogExtensions.OutputInfo("敵成長マスタを読み込みます。");

        bool ret = false;
        base.Open(filename);

        string[] lineAll = base.GetLineAll();
        if (lineAll != null)
        {
            m_enemyGrowthMasterList = new Dictionary<int, Dictionary<int, EnemyGrowthMaster.Param>>(lineAll.Length);

            EnemyGrowthMaster.Param temp = null;
            foreach (string line in lineAll)
            {
                temp = JsonUtility.FromJson<EnemyGrowthMaster.Param>(line);

                if (m_enemyGrowthMasterList.ContainsKey(temp.id))
                {
                    m_enemyGrowthMasterList[temp.id].Add(temp.level, temp);
                }
                else
                {
                    m_enemyGrowthMasterList.Add(temp.id, new Dictionary<int, EnemyGrowthMaster.Param> { { temp.level, temp } });
                }
            }
            LogExtensions.OutputInfo("敵成長マスタの読み込みに成功しました。");
            ret = true;
        }
        else
        {
            LogExtensions.OutputError("敵成長マスタの読み込みに失敗しました。");
        }
        base.Close();

        return ret;
    }

    /// <summary>
    /// 敵成長パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(EnemyGrowthMaster.Param param)
    {
        LogExtensions.OutputInfo("[敵成長マスタ] => "+
             "[id:"     + param.id      + "] " +
             "[level:"  + param.level   + "] " +
             "[hp:"     + param.hp      + "] " +
             "[atk:"    + param.atk     + "] " +
             "[def:"    + param.def     + "] " +
             "[spd:"    + param.spd     + "] " +
             "[exp:"    + param.exp     + "] " 
        );
    }



    /// <summary>
    /// 敵成長パラメーターをログに出力する
    /// </summary>
    public void DebugLog()
    {
        foreach (KeyValuePair<int, Dictionary<int, EnemyGrowthMaster.Param>> param in m_enemyGrowthMasterList)
        {
            foreach (KeyValuePair<int,EnemyGrowthMaster.Param> param2 in param.Value)
            {
                DebugLog(param2.Value);
            }
        }
    }
}

