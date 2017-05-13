﻿using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤー基本パラメータ
/// </summary>
public class CharacterStatus
{
    public PlayerBaseMaster.Param param;
    public int exp;
}

public class LoadPlayerBaseMaster : BaseSingleton<LoadPlayerBaseMaster>
{

    /// <summary>
    /// プレイヤー基本マスタを格納する変数
    /// </summary>
    PlayerBaseMaster master;

    /// <summary>
    /// プレイヤーのレベル上限値
    /// </summary>
    static public readonly int PLAYER_LEVEL_MAX = 100;

    /// <summary>
    /// 初期化時にプレイヤー基本マスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if (null == (master = Resources.Load<PlayerBaseMaster>("MasterData/PlayerBaseMaster")))
        {
            Debug.Log("failed to Resources.Load<PlayerBaseMaster>");
        }
    }

    /// <summary>
    /// 指定したレベルに一致するプレイヤー情報を取得する
    /// </summary>
    /// <param name="lv"></param>
    /// <returns></returns>
    public PlayerBaseMaster.Param GetPlayerInfo(int lv)
    {
        // くっそめんどくさいが、メンバ変数を一つ一つ移さないと、
        // ポインタ扱いなのかしらないが外で取得した変数をいじるとこちらの値も変わってしまうくそ仕様。
        PlayerBaseMaster.Param temp = new PlayerBaseMaster.Param();

        // 指定したレベルがレベル上限以上ならレベル上限の情報を返す
        PlayerBaseMaster.Param temp2  =((lv >= PLAYER_LEVEL_MAX) ? master.list[PLAYER_LEVEL_MAX - 1] : master.list[lv - 1]);
     
        temp.hp    = temp2.hp;
        temp.atk   = temp2.atk;
        temp.def   = temp2.def;
        temp.next_exp = temp2.next_exp;
        temp.spd   = temp2.spd;
        temp.mgc   = temp2.mgc;
        temp.level = temp2.level;

        return temp;
    }

    /// <summary>
    /// プレイヤー基本パラメーターをログに出力する
    /// </summary>
    /// <param name="param"></param>
    public void DebugLog(PlayerBaseMaster.Param param)
    {
        Debug.Log("level      = " + param.level);
        Debug.Log("hp         = " + param.hp);
        Debug.Log("atk        = " + param.atk);
        Debug.Log("def        = " + param.def);
        Debug.Log("mgc        = " + param.mgc);
        Debug.Log("spd        = " + param.spd);
        Debug.Log("next_exp   = " + param.next_exp);
    }
}
