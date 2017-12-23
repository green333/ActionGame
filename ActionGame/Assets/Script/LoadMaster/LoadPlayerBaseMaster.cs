using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// プレイヤー基本パラメータ
/// </summary>
public class CharacterStatus
{
    public PlayerBaseMaster.Param param;
    public int exp;
}

public class LoadPlayerBaseMaster : TextMasterManager
{
    static LoadPlayerBaseMaster _instance = new LoadPlayerBaseMaster();

    static public LoadPlayerBaseMaster instance { get { return _instance; } }

    const string filename = "Assets/Resources/MasterData/プレイヤー基本マスタ.txt";

    /// <summary>プレイヤーマスタリスト</summary>
    private Dictionary<int,PlayerBaseMaster.Param> m_playerList = null;

    /// <summary>プレイヤー本マスタリストを取得 </summary>
    public Dictionary<int,PlayerBaseMaster.Param> playerList { get { return m_playerList; } }

    /// <summary>
    /// プレイヤーのレベル上限値
    /// </summary>
    static public readonly int PLAYER_LEVEL_MAX = 100;


    public bool Init()
    {
        LogExtensions.OutputInfo("プレイヤーマスタを読み込みます。");

        bool ret = false;
        base.Open(filename);

        string[] lineAll = base.GetLineAll();
        if (lineAll != null)
        {
            m_playerList = new Dictionary<int, PlayerBaseMaster.Param>(lineAll.Length);
            PlayerBaseMaster.Param temp = null;
            foreach (string line in lineAll)
            {
                temp = JsonUtility.FromJson<PlayerBaseMaster.Param>(line);
                m_playerList.Add(temp.Level,temp);
            }

            ret = true;
            LogExtensions.OutputInfo("プレイヤーマスタの読み込みに成功しました。");
        }
        else
        {
            LogExtensions.OutputError("プレイヤーマスタの読み込みに失敗しました。");
        }
        base.Close();

        return ret;
    }
    /// <summary>
    /// 指定したレベルに一致するプレイヤー情報を取得する
    /// </summary>
    /// <param name="lv"></param>
    /// <returns></returns>
    public PlayerBaseMaster.Param GetPlayerInfo(int lv)
    {
        if(PLAYER_LEVEL_MAX < lv)
        {
            lv = PLAYER_LEVEL_MAX;
        }

        return m_playerList[lv];
    }

    /// <summary>
    /// プレイヤー基本パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(PlayerBaseMaster.Param param)
    {
        LogExtensions.OutputInfo("[プレイヤー基本マスタ] => " +
            "[level:"       + param.Level       + "] "  +
            "[hp:"          + param.Hp          + "] "  +
            "[atk:"         + param.Atk         + "] "  +
            "[def:"         + param.Def         + "] "  +
            "[spd:"         + param.Spd         + "] "  +
            "[next_exp:"    + param.Next_exp    + "]"
            );
    }
}
