using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 敵基本マスタパラメーター
/// </summary>
public class EnemyBaseMaster : ScriptableObject{

    [System.SerializableAttribute]
    public class Param
    {
        public string name;        //    名前
        public int id;        //    ID
        public string tribe_name;        //    種族
        public string class_name;        //    階級
        public int drop_item_id1;        //    ドロップアイテム1
        public int drop_item_num1;        //    個数
        public int drop_item_add_rnd1;        //    個数増加乱数
        public int drop_item_id2;        //    ドロップアイテム2
        public int drop_item_num2;        //    個数
        public int drop_item_add_rnd2;        //    個数増加乱数
        public int rare_drop_item_id1;        //    レアドロップアイテム
        public int rare_drop_item_num1;        //    個数
        public int rare_drop_item_add_rnd1;        //    個数増加乱数
    }
}

/// <summary>
/// 敵成長マスタパラメーター
/// </summary>
public class EnemyGrowthMaster : ScriptableObject{

    [System.SerializableAttribute]
    public class Param
    {
        public int id;        //    ID
        public int level;        //    レベル
        public int hp;        //    体力
        public int atk;        //    攻撃力
        public int def;        //    防御力
        public int spd;        //    素早さ
        public int exp;        //    取得経験値
    }
}

/// <summary>
/// 敵出現マスタパラメーター
/// </summary>
public class EnemySpawnMaster : ScriptableObject{

    [System.SerializableAttribute]
    public class Param
    {
        public int stage_id;        //    ステージID
        public int chapter_id;        //    章
        public int stage_detail_id;        //    ステージ詳細マスタID
        public int respawn_max;        //    出現する敵の最大数
        public int enemy1_id;        //    敵１
        public int enemy1_lvpm;        //    レベル幅
        public int enemy1_respawn_time;        //    沸き時間(秒)
        public int enemy1_frequency;        //    出現確率(％)
        public int enemy2_id;        //    敵２
        public int enemy2_lvpm;        //    レベル幅
        public int enemy2_respawn_time;        //    沸き時間(秒)
        public int enemy2_frequency;        //    出現確率(％)
        public int enemy3_id;        //    敵３
        public int enemy3_lvpm;        //    レベル幅
        public int enemy3_respawn_time;        //    沸き時間(秒)
        public int enemy3_frequency;        //    出現確率(％)
    }
}

/// <summary>
/// プレイヤー基本マスタパラメーター
/// </summary>
public class PlayerBaseMaster : ScriptableObject{

    [System.SerializableAttribute]
    public class Param
    {
        public int level;        //    レベル
        public int hp;        //    体力
        public int atk;        //    攻撃力
        public int def;        //    防御力
        public int spd;        //    素早さ
        public int next_exp;        //    経験値
    }
}

/// <summary>
/// アイテムマスタパラメーター
/// </summary>
public class ItemMaster : ScriptableObject{

    [System.SerializableAttribute]
    public class Param
    {
        public string name;        //    名前
        public int id;        //    ID
        public string kind;        //    種類
        public int effect;        //    効果
        public string desc;        //    説明
    }
}

/// <summary>
/// ステージマスタパラメーター
/// </summary>
public class StageMaster : ScriptableObject{

    [System.SerializableAttribute]
    public class Param
    {
        public string name;        //    名前
        public int id;        //    ID
        public int chapter;        //    章
        public string normal_bgm_name;        //    通常BGM
        public string battle_bgm_name;        //    戦闘BGM
        public string boss_bgm_name;        //    BOSS戦BGM
        public string event_bgm_name;        //    イベントBGM
    }
}

/// <summary>
/// ステージ詳細マスタパラメーター
/// </summary>
public class undefined : ScriptableObject{

    [System.SerializableAttribute]
    public class Param
    {
        public int stage_id;        //    ステージID
        public int id;        //    ID
        public string name;        //    名前
        public string adjacent;        //    隣接ID
    }
}

/// <summary>
/// BGMマスタパラメーター
/// </summary>
public class BGMMaster : ScriptableObject{

    [System.SerializableAttribute]
    public class Param
    {
        public string name;        //    名前
        public int id;        //    ID
    }
}

/// <summary>
/// 武器マスタパラメーター
/// </summary>
public class WeaponMaster : ScriptableObject{

    [System.SerializableAttribute]
    public class Param
    {
        public string name;        //    名前
        public int id;        //    ID
        public string type;        //    種類
        public int atk;        //    攻撃力
        public int def;        //    防御力
        public int spd;        //    素早さ
    }
}

