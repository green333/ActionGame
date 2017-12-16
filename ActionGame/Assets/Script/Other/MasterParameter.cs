using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 敵成長マスタパラメーター
/// </summary>
public class EnemyGrowthMaster : ScriptableObject{
    [System.SerializableAttribute]
    public class Param{
        public int id;    /// <summary> ID </summary>
        public int level;    /// <summary> レベル </summary>
        public int hp;    /// <summary> 体力 </summary>
        public int atk;    /// <summary> 攻撃力 </summary>
        public int def;    /// <summary> 防御力 </summary>
        public int spd;    /// <summary> 素早さ </summary>
        public int exp;    /// <summary> 取得経験値 </summary>
    }
}

/// <summary>
/// 敵基本マスタパラメーター
/// </summary>
public class EnemyBaseMaster : ScriptableObject{
    [System.SerializableAttribute]
    public class Param{
        public string name;    /// <summary> 名前 </summary>
        public int id;    /// <summary> ID </summary>
        public string path;    /// <summary> リソース </summary>
        public string tribe_name;    /// <summary> 種族 </summary>
        public string class_name;    /// <summary> 階級 </summary>
        public int drop_item_id1;    /// <summary> ドロップアイテム1 </summary>
        public int drop_item_num1;    /// <summary> 個数 </summary>
        public int drop_item_add_rnd1;    /// <summary> 個数増加乱数 </summary>
        public int drop_item_id2;    /// <summary> ドロップアイテム2 </summary>
        public int drop_item_num2;    /// <summary> 個数 </summary>
        public int drop_item_add_rnd2;    /// <summary> 個数増加乱数 </summary>
        public int rare_drop_item_id1;    /// <summary> レアドロップアイテム </summary>
        public int rare_drop_item_num1;    /// <summary> 個数 </summary>
        public int rare_drop_item_add_rnd1;    /// <summary> 個数増加乱数 </summary>
    }
}

/// <summary>
/// プレイヤー基本マスタパラメーター
/// </summary>
public class PlayerBaseMaster : ScriptableObject{
    [System.SerializableAttribute]
    public class Param{
        public int level;    /// <summary> レベル </summary>
        public int hp;    /// <summary> 体力 </summary>
        public int atk;    /// <summary> 攻撃力 </summary>
        public int def;    /// <summary> 防御力 </summary>
        public int spd;    /// <summary> 素早さ </summary>
        public int next_exp;    /// <summary> 経験値 </summary>
    }
}

/// <summary>
/// アイテムマスタパラメーター
/// </summary>
public class ItemMaster : ScriptableObject{
    [System.SerializableAttribute]
    public class Param{
        public string name;    /// <summary> 名前 </summary>
        public int id;    /// <summary> ID </summary>
        public string kind;    /// <summary> 種類 </summary>
        public int effect;    /// <summary> 効果 </summary>
        public int desc;    /// <summary> 説明 </summary>
    }
}

/// <summary>
/// ステージマスタパラメーター
/// </summary>
public class StageMaster : ScriptableObject{
    [System.SerializableAttribute]
    public class Param{
        public string name;    /// <summary> 名前 </summary>
        public int id;    /// <summary> ID </summary>
        public int chapter;    /// <summary> 章 </summary>
        public string normal_bgm_name;    /// <summary> 通常BGM </summary>
        public string battle_bgm_name;    /// <summary> 戦闘BGM </summary>
        public string boss_bgm_name;    /// <summary> BOSS戦BGM </summary>
        public string event_bgm_name;    /// <summary> イベントBGM </summary>
    }
}

/// <summary>
/// 敵出現マスタパラメーター
/// </summary>
public class EnemySpawnMaster : ScriptableObject{
    [System.SerializableAttribute]
    public class Param{
        public int stage_id;    /// <summary> ステージID </summary>
        public int chapter_id;    /// <summary> 章 </summary>
        public int stage_detail_id;    /// <summary> ステージ詳細マスタID </summary>
        public int respawn_max;    /// <summary> 出現する敵の最大数 </summary>
        public int enemy1_id;    /// <summary> 敵１ </summary>
        public int enemy1_lv;    /// <summary> 出現レベル </summary>
        public int enemy1_respawn_time;    /// <summary> 沸き時間(秒) </summary>
        public int enemy1_frequency;    /// <summary> 出現確率(％) </summary>
        public int enemy2_id;    /// <summary> 敵２ </summary>
        public int enemy2_lv;    /// <summary> 出現レベル </summary>
        public int enemy2_respawn_time;    /// <summary> 沸き時間(秒) </summary>
        public int enemy2_frequency;    /// <summary> 出現確率(％) </summary>
        public int enemy3_id;    /// <summary> 敵３ </summary>
        public int enemy3_lv;    /// <summary> 出現レベル </summary>
        public int enemy3_respawn_time;    /// <summary> 沸き時間(秒) </summary>
        public int enemy3_frequency;    /// <summary> 出現確率(％) </summary>
    }
}

/// <summary>
/// BGMマスタパラメーター
/// </summary>
public class BGMMaster : ScriptableObject{
    [System.SerializableAttribute]
    public class Param{
        public string name;    /// <summary> 名前 </summary>
        public int id;    /// <summary> ID </summary>
    }
}

/// <summary>
/// 武器マスタパラメーター
/// </summary>
public class WeaponMaster : ScriptableObject{
    [System.SerializableAttribute]
    public class Param{
        public string name;    /// <summary> 名前 </summary>
        public int id;    /// <summary> ID </summary>
        public string type;    /// <summary> 種類 </summary>
        public int atk;    /// <summary> 攻撃力 </summary>
        public int def;    /// <summary> 防御力 </summary>
        public int spd;    /// <summary> 素早さ </summary>
    }
}

