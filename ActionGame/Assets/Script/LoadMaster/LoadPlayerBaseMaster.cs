using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤー基本パラメータ
/// </summary>
public class CharacterStatus
{
    public PlayerBaseMaster.Param param;
    public int exp;
}

public class LoadPlayerBaseMaster
{
    /// <summary>
    /// プレイヤーのレベル上限値
    /// </summary>
    static public readonly int PLAYER_LEVEL_MAX = 100;
    
    /// <summary>
    /// 指定したレベルに一致するプレイヤー情報を取得する
    /// </summary>
    /// <param name="lv"></param>
    /// <returns></returns>
    public PlayerBaseMaster.Param GetPlayerInfo(int lv)
    {
        return null;
    }

    /// <summary>
    /// プレイヤー基本パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(PlayerBaseMaster.Param param)
    {
        LogExtensions.OutputInfo("level      = " + param.level);
        LogExtensions.OutputInfo("hp         = " + param.hp);
        LogExtensions.OutputInfo("atk        = " + param.atk);
        LogExtensions.OutputInfo("def        = " + param.def);
        LogExtensions.OutputInfo("mgc        = " + param.mgc);
        LogExtensions.OutputInfo("spd        = " + param.spd);
        LogExtensions.OutputInfo("next_exp   = " + param.next_exp);
    }
}
