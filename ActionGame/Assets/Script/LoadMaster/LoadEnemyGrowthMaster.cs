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

    /// <summary>
    /// 敵出現マスタから敵成長マスタを読み込む
    /// </summary>
    /// <param name="param">敵出現マスタ</param>
    /// <returns>読み込み成功:true 読み込み失敗:false</returns>
    public bool LoadEnemyGrowthInfo(List<EnemySpawnMaster.Param> param)
    {
        m_enemyGrowthMasterList = new Dictionary<int, Dictionary<int, EnemyGrowthMaster.Param>>();
 
        int[] enemyIdList   = Enumerable.Repeat<int>(0, param.Count * 3).ToArray();
        int[] enemyLvList   = Enumerable.Repeat<int>(0, param.Count * 3).ToArray();
        int index = 0;

        for (int i = 0; i < param.Count; ++i)
        {
            if (Array.IndexOf(enemyIdList, param[i].enemy1_id) == -1)
            {
                enemyIdList[index] = param[i].enemy1_id;
                enemyLvList[index] = param[i].enemy1_lv;
                ++index;
            }
            if (Array.IndexOf(enemyIdList, param[i].enemy2_id) == -1)
            {
                enemyIdList[index] = param[i].enemy2_id;
                enemyLvList[index] = param[i].enemy2_lv;
                ++index;
            }
            if (Array.IndexOf(enemyIdList, param[i].enemy3_id) == -1)
            {
                enemyIdList[index] = param[i].enemy3_id;
                enemyLvList[index] = param[i].enemy3_lv;
                ++index;
            }
        }

        List<string> searchList = new List<string>();
        for(int i = 0; i < index; ++i)
        {
            searchList.Add(base.VariableToJson(COL_ID, enemyIdList[i]) + "," + base.VariableToJson(COL_LEVEL, enemyLvList[i]));
        }

        base.Open(filename);
        string[] getJsnoStrList = base.SearchList(searchList, searchList.Count);
        base.Close();

        EnemyGrowthMaster.Param temp = null;
        foreach (string getJsonStr in getJsnoStrList)
        {
            if (getJsonStr == string.Empty)
            {
                break;
            }

            temp = JsonUtility.FromJson<EnemyGrowthMaster.Param>(getJsonStr);

            if (m_enemyGrowthMasterList.ContainsKey(temp.id) && !m_enemyGrowthMasterList[temp.id].ContainsKey(temp.level))
            {
                m_enemyGrowthMasterList[temp.id].Add(temp.level, temp);
            }
            else
            {
                m_enemyGrowthMasterList.Add(temp.id, new Dictionary<int, EnemyGrowthMaster.Param> { { temp.level, temp } });
            }
        }

        return (m_enemyGrowthMasterList.Count != 0);
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

