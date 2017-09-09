using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class LoadEnemyGrowthMaster : TextMasterManager
{
    static LoadEnemyGrowthMaster _instance = new LoadEnemyGrowthMaster();

    static public LoadEnemyGrowthMaster instance { get { return _instance; } }

    private Dictionary<int, Dictionary<int,EnemyGrowthMaster.Param>> m_enemyList = new Dictionary<int, Dictionary<int, EnemyGrowthMaster.Param>>();
    public Dictionary<int, Dictionary<int, EnemyGrowthMaster.Param>> enemyList { get { return m_enemyList; } } 

    public const int ENEMY_LEVEL_MAX = 120;

    const string filename = "Assets/Resources/MasterData/敵成長マスタ.txt";

    const string COL_ID = "id";
    const string COL_LEVEL = "level";

    public void LoadEnemyGrowthInfo(int playerLv,List<EnemySpawnMaster.Param> param)
    {
        m_enemyList = new Dictionary<int, Dictionary<int, EnemyGrowthMaster.Param>>();

        IEnumerator paramList = param.GetEnumerator();
        EnemySpawnMaster.Param temp = null;

        int[] enemyIdList       = Enumerable.Repeat<int>(0, param.Count * 3).ToArray();
        int[] enemyMinLvList    = Enumerable.Repeat<int>(0, param.Count * 3).ToArray();
        int[] enemyMaxLvList    = Enumerable.Repeat<int>(0, param.Count * 3).ToArray();

        int index   = 0;
        int ofIndex = 0;
        int min     = 0;
        int max     = 0;
        while (paramList.MoveNext())
        {
            temp = (EnemySpawnMaster.Param)paramList.Current;

            ofIndex = Array.IndexOf(enemyIdList, temp.enemy1_id);
            if (ofIndex == -1)
            {
                enemyIdList[index] = temp.enemy1_id;
                enemyMinLvList[index] = playerLv - temp.enemy1_lvpm;
                enemyMaxLvList[index] = playerLv + temp.enemy1_lvpm;
                ++index;
            }else
            {
                min = playerLv - temp.enemy1_lvpm;
                max = playerLv + temp.enemy1_lvpm;
                if(min < enemyMinLvList[ofIndex]) { enemyMinLvList[ofIndex] = min; }
                if(max > enemyMaxLvList[ofIndex]) { enemyMaxLvList[ofIndex] = max; }
            }

            ofIndex = Array.IndexOf(enemyIdList, temp.enemy2_id);
            if (ofIndex == -1)
            {
                enemyIdList[index] = temp.enemy2_id;
                enemyMinLvList[index] = playerLv - temp.enemy2_lvpm;
                enemyMaxLvList[index] = playerLv + temp.enemy2_lvpm;
                ++index;
            }
            else
            {
                min = playerLv - temp.enemy2_lvpm;
                max = playerLv + temp.enemy2_lvpm;
                if (min < enemyMinLvList[ofIndex]) { enemyMinLvList[ofIndex] = min; }
                if (max > enemyMaxLvList[ofIndex]) { enemyMaxLvList[ofIndex] = max; }
            }




            ofIndex = Array.IndexOf(enemyIdList, temp.enemy3_id);
            if (ofIndex == -1)
            {
                enemyIdList[index] = temp.enemy3_id;
                enemyMinLvList[index] = playerLv - temp.enemy3_lvpm;
                enemyMaxLvList[index] = playerLv + temp.enemy3_lvpm;
                ++index;
            }
            else
            {
                min = playerLv - temp.enemy3_lvpm;
                max = playerLv + temp.enemy3_lvpm;
                if (min < enemyMinLvList[ofIndex]) { enemyMinLvList[ofIndex] = min; }
                if (max > enemyMaxLvList[ofIndex]) { enemyMaxLvList[ofIndex] = max; }
            }
        }

        List<string> searchList = new List<string>();
        for(int i = 0; i < index; ++i)
        {
            if(enemyMinLvList[i] < 1) { enemyMinLvList[i] = 1; }
            if(enemyMaxLvList[i] > ENEMY_LEVEL_MAX) { enemyMaxLvList[i] = ENEMY_LEVEL_MAX; }

            for (int searchLv = enemyMinLvList[i]; searchLv <= enemyMaxLvList[i]; ++searchLv )
            {
                searchList.Add(base.VariableToJson(COL_ID, enemyIdList[i]) + "," + base.VariableToJson(COL_LEVEL, searchLv));
            }
        }

        base.Open(filename);
        string[] getJsnoStrList = base.SearchList(searchList, searchList.Count);
        base.Close();

        EnemyGrowthMaster.Param temp2;
        foreach (string getJsonStr in getJsnoStrList)
        {
            if (getJsonStr == string.Empty)
            {
                break;
            }

            temp2 = JsonUtility.FromJson<EnemyGrowthMaster.Param>(getJsonStr);

            if (m_enemyList.ContainsKey(temp2.id) && !m_enemyList[temp2.id].ContainsKey(temp2.level))
            {
                m_enemyList[temp2.id].Add(temp2.level, temp2);
            }
            else
            {
                m_enemyList.Add(temp2.id, new Dictionary<int, EnemyGrowthMaster.Param> { { temp2.level, temp2 } });
            }
        }
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
}

