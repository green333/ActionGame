using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 敵成長マスタパラメーター
/// </summary>
public class EnemyGrowthMaster : ScriptableObject{
    public class Param{
        [SerializeField]
        private int id;    /// <summary> ID </summary>
        [SerializeField]
        private int level;    /// <summary> レベル </summary>
        [SerializeField]
        private int hp;    /// <summary> 体力 </summary>
        [SerializeField]
        private int atk;    /// <summary> 攻撃力 </summary>
        [SerializeField]
        private int def;    /// <summary> 防御力 </summary>
        [SerializeField]
        private int spd;    /// <summary> 素早さ </summary>
        [SerializeField]
        private int exp;    /// <summary> 取得経験値 </summary>
        public int Id{ get { return id; } }    /// <summary> 取得プロパティ: ID </summary>
        public int Level{ get { return level; } }    /// <summary> 取得プロパティ: レベル </summary>
        public int Hp{ get { return hp; } }    /// <summary> 取得プロパティ: 体力 </summary>
        public int Atk{ get { return atk; } }    /// <summary> 取得プロパティ: 攻撃力 </summary>
        public int Def{ get { return def; } }    /// <summary> 取得プロパティ: 防御力 </summary>
        public int Spd{ get { return spd; } }    /// <summary> 取得プロパティ: 素早さ </summary>
        public int Exp{ get { return exp; } }    /// <summary> 取得プロパティ: 取得経験値 </summary>
    }
}

/// <summary>
/// 敵基本マスタパラメーター
/// </summary>
public class EnemyBaseMaster : ScriptableObject{
    public class Param{
        [SerializeField]
        private string name;    /// <summary> 名前 </summary>
        [SerializeField]
        private int id;    /// <summary> ID </summary>
        [SerializeField]
        private string path;    /// <summary> リソース </summary>
        [SerializeField]
        private string tribe_name;    /// <summary> 種族 </summary>
        [SerializeField]
        private string class_name;    /// <summary> 階級 </summary>
        [SerializeField]
        private int drop_item_id1;    /// <summary> ドロップアイテム1 </summary>
        [SerializeField]
        private int drop_item_num1;    /// <summary> 個数 </summary>
        [SerializeField]
        private int drop_item_add_rnd1;    /// <summary> 個数増加乱数 </summary>
        [SerializeField]
        private int drop_item_id2;    /// <summary> ドロップアイテム2 </summary>
        [SerializeField]
        private int drop_item_num2;    /// <summary> 個数 </summary>
        [SerializeField]
        private int drop_item_add_rnd2;    /// <summary> 個数増加乱数 </summary>
        [SerializeField]
        private int rare_drop_item_id1;    /// <summary> レアドロップアイテム </summary>
        [SerializeField]
        private int rare_drop_item_num1;    /// <summary> 個数 </summary>
        [SerializeField]
        private int rare_drop_item_add_rnd1;    /// <summary> 個数増加乱数 </summary>
        public string Name{ get { return name; } }    /// <summary> 取得プロパティ: 名前 </summary>
        public int Id{ get { return id; } }    /// <summary> 取得プロパティ: ID </summary>
        public string Path{ get { return path; } }    /// <summary> 取得プロパティ: リソース </summary>
        public string Tribe_name{ get { return tribe_name; } }    /// <summary> 取得プロパティ: 種族 </summary>
        public string Class_name{ get { return class_name; } }    /// <summary> 取得プロパティ: 階級 </summary>
        public int Drop_item_id1{ get { return drop_item_id1; } }    /// <summary> 取得プロパティ: ドロップアイテム1 </summary>
        public int Drop_item_num1{ get { return drop_item_num1; } }    /// <summary> 取得プロパティ: 個数 </summary>
        public int Drop_item_add_rnd1{ get { return drop_item_add_rnd1; } }    /// <summary> 取得プロパティ: 個数増加乱数 </summary>
        public int Drop_item_id2{ get { return drop_item_id2; } }    /// <summary> 取得プロパティ: ドロップアイテム2 </summary>
        public int Drop_item_num2{ get { return drop_item_num2; } }    /// <summary> 取得プロパティ: 個数 </summary>
        public int Drop_item_add_rnd2{ get { return drop_item_add_rnd2; } }    /// <summary> 取得プロパティ: 個数増加乱数 </summary>
        public int Rare_drop_item_id1{ get { return rare_drop_item_id1; } }    /// <summary> 取得プロパティ: レアドロップアイテム </summary>
        public int Rare_drop_item_num1{ get { return rare_drop_item_num1; } }    /// <summary> 取得プロパティ: 個数 </summary>
        public int Rare_drop_item_add_rnd1{ get { return rare_drop_item_add_rnd1; } }    /// <summary> 取得プロパティ: 個数増加乱数 </summary>
    }
}

/// <summary>
/// プレイヤー基本マスタパラメーター
/// </summary>
public class PlayerBaseMaster : ScriptableObject{
    public class Param{
        [SerializeField]
        private int level;    /// <summary> レベル </summary>
        [SerializeField]
        private int hp;    /// <summary> 体力 </summary>
        [SerializeField]
        private int atk;    /// <summary> 攻撃力 </summary>
        [SerializeField]
        private int def;    /// <summary> 防御力 </summary>
        [SerializeField]
        private int spd;    /// <summary> 素早さ </summary>
        [SerializeField]
        private int next_exp;    /// <summary> 経験値 </summary>
        public int Level{ get { return level; } }    /// <summary> 取得プロパティ: レベル </summary>
        public int Hp{ get { return hp; } }    /// <summary> 取得プロパティ: 体力 </summary>
        public int Atk{ get { return atk; } }    /// <summary> 取得プロパティ: 攻撃力 </summary>
        public int Def{ get { return def; } }    /// <summary> 取得プロパティ: 防御力 </summary>
        public int Spd{ get { return spd; } }    /// <summary> 取得プロパティ: 素早さ </summary>
        public int Next_exp{ get { return next_exp; } }    /// <summary> 取得プロパティ: 経験値 </summary>
    }
}

/// <summary>
/// アイテムマスタパラメーター
/// </summary>
public class ItemMaster : ScriptableObject{
    public class Param{
        [SerializeField]
        private string name;    /// <summary> 名前 </summary>
        [SerializeField]
        private int id;    /// <summary> ID </summary>
        [SerializeField]
        private string kind;    /// <summary> 種類 </summary>
        [SerializeField]
        private int effect;    /// <summary> 効果 </summary>
        [SerializeField]
        private string desc;    /// <summary> 説明 </summary>
        public string Name{ get { return name; } }    /// <summary> 取得プロパティ: 名前 </summary>
        public int Id{ get { return id; } }    /// <summary> 取得プロパティ: ID </summary>
        public string Kind{ get { return kind; } }    /// <summary> 取得プロパティ: 種類 </summary>
        public int Effect{ get { return effect; } }    /// <summary> 取得プロパティ: 効果 </summary>
        public string Desc{ get { return desc; } }    /// <summary> 取得プロパティ: 説明 </summary>
    }
}

/// <summary>
/// ステージマスタパラメーター
/// </summary>
public class StageMaster : ScriptableObject{
    public class Param{
        [SerializeField]
        private string name;    /// <summary> 名前 </summary>
        [SerializeField]
        private int id;    /// <summary> ID </summary>
        [SerializeField]
        private int chapter;    /// <summary> 章 </summary>
        [SerializeField]
        private string normal_bgm_name;    /// <summary> 通常BGM </summary>
        [SerializeField]
        private string battle_bgm_name;    /// <summary> 戦闘BGM </summary>
        [SerializeField]
        private string boss_bgm_name;    /// <summary> BOSS戦BGM </summary>
        [SerializeField]
        private string event_bgm_name;    /// <summary> イベントBGM </summary>
        public string Name{ get { return name; } }    /// <summary> 取得プロパティ: 名前 </summary>
        public int Id{ get { return id; } }    /// <summary> 取得プロパティ: ID </summary>
        public int Chapter{ get { return chapter; } }    /// <summary> 取得プロパティ: 章 </summary>
        public string Normal_bgm_name{ get { return normal_bgm_name; } }    /// <summary> 取得プロパティ: 通常BGM </summary>
        public string Battle_bgm_name{ get { return battle_bgm_name; } }    /// <summary> 取得プロパティ: 戦闘BGM </summary>
        public string Boss_bgm_name{ get { return boss_bgm_name; } }    /// <summary> 取得プロパティ: BOSS戦BGM </summary>
        public string Event_bgm_name{ get { return event_bgm_name; } }    /// <summary> 取得プロパティ: イベントBGM </summary>
    }
}

/// <summary>
/// 敵出現マスタパラメーター
/// </summary>
public class EnemySpawnMaster : ScriptableObject{
    public class Param{
        [SerializeField]
        private int stage_id;    /// <summary> ステージID </summary>
        [SerializeField]
        private int chapter_id;    /// <summary> 章 </summary>
        [SerializeField]
        private int stage_detail_id;    /// <summary> ステージ詳細マスタID </summary>
        [SerializeField]
        private int respawn_max;    /// <summary> 出現する敵の最大数 </summary>
        [SerializeField]
        private int enemy1_id;    /// <summary> 敵１ </summary>
        [SerializeField]
        private int enemy1_lv;    /// <summary> 出現レベル </summary>
        [SerializeField]
        private int enemy1_respawn_time;    /// <summary> 沸き時間(秒) </summary>
        [SerializeField]
        private int enemy1_frequency;    /// <summary> 出現確率(％) </summary>
        [SerializeField]
        private int enemy2_id;    /// <summary> 敵２ </summary>
        [SerializeField]
        private int enemy2_lv;    /// <summary> 出現レベル </summary>
        [SerializeField]
        private int enemy2_respawn_time;    /// <summary> 沸き時間(秒) </summary>
        [SerializeField]
        private int enemy2_frequency;    /// <summary> 出現確率(％) </summary>
        [SerializeField]
        private int enemy3_id;    /// <summary> 敵３ </summary>
        [SerializeField]
        private int enemy3_lv;    /// <summary> 出現レベル </summary>
        [SerializeField]
        private int enemy3_respawn_time;    /// <summary> 沸き時間(秒) </summary>
        [SerializeField]
        private int enemy3_frequency;    /// <summary> 出現確率(％) </summary>
        public int Stage_id{ get { return stage_id; } }    /// <summary> 取得プロパティ: ステージID </summary>
        public int Chapter_id{ get { return chapter_id; } }    /// <summary> 取得プロパティ: 章 </summary>
        public int Stage_detail_id{ get { return stage_detail_id; } }    /// <summary> 取得プロパティ: ステージ詳細マスタID </summary>
        public int Respawn_max{ get { return respawn_max; } }    /// <summary> 取得プロパティ: 出現する敵の最大数 </summary>
        public int Enemy1_id{ get { return enemy1_id; } }    /// <summary> 取得プロパティ: 敵１ </summary>
        public int Enemy1_lv{ get { return enemy1_lv; } }    /// <summary> 取得プロパティ: 出現レベル </summary>
        public int Enemy1_respawn_time{ get { return enemy1_respawn_time; } }    /// <summary> 取得プロパティ: 沸き時間(秒) </summary>
        public int Enemy1_frequency{ get { return enemy1_frequency; } }    /// <summary> 取得プロパティ: 出現確率(％) </summary>
        public int Enemy2_id{ get { return enemy2_id; } }    /// <summary> 取得プロパティ: 敵２ </summary>
        public int Enemy2_lv{ get { return enemy2_lv; } }    /// <summary> 取得プロパティ: 出現レベル </summary>
        public int Enemy2_respawn_time{ get { return enemy2_respawn_time; } }    /// <summary> 取得プロパティ: 沸き時間(秒) </summary>
        public int Enemy2_frequency{ get { return enemy2_frequency; } }    /// <summary> 取得プロパティ: 出現確率(％) </summary>
        public int Enemy3_id{ get { return enemy3_id; } }    /// <summary> 取得プロパティ: 敵３ </summary>
        public int Enemy3_lv{ get { return enemy3_lv; } }    /// <summary> 取得プロパティ: 出現レベル </summary>
        public int Enemy3_respawn_time{ get { return enemy3_respawn_time; } }    /// <summary> 取得プロパティ: 沸き時間(秒) </summary>
        public int Enemy3_frequency{ get { return enemy3_frequency; } }    /// <summary> 取得プロパティ: 出現確率(％) </summary>
    }
}

/// <summary>
/// BGMマスタパラメーター
/// </summary>
public class BGMMaster : ScriptableObject{
    public class Param{
        [SerializeField]
        private string name;    /// <summary> 名前 </summary>
        [SerializeField]
        private int id;    /// <summary> ID </summary>
        public string Name{ get { return name; } }    /// <summary> 取得プロパティ: 名前 </summary>
        public int Id{ get { return id; } }    /// <summary> 取得プロパティ: ID </summary>
    }
}

/// <summary>
/// 武器マスタパラメーター
/// </summary>
public class WeaponMaster : ScriptableObject{
    public class Param{
        [SerializeField]
        private string name;    /// <summary> 名前 </summary>
        [SerializeField]
        private int id;    /// <summary> ID </summary>
        [SerializeField]
        private string type;    /// <summary> 種類 </summary>
        [SerializeField]
        private int atk;    /// <summary> 攻撃力 </summary>
        [SerializeField]
        private int def;    /// <summary> 防御力 </summary>
        [SerializeField]
        private int spd;    /// <summary> 素早さ </summary>
        public string Name{ get { return name; } }    /// <summary> 取得プロパティ: 名前 </summary>
        public int Id{ get { return id; } }    /// <summary> 取得プロパティ: ID </summary>
        public string Type{ get { return type; } }    /// <summary> 取得プロパティ: 種類 </summary>
        public int Atk{ get { return atk; } }    /// <summary> 取得プロパティ: 攻撃力 </summary>
        public int Def{ get { return def; } }    /// <summary> 取得プロパティ: 防御力 </summary>
        public int Spd{ get { return spd; } }    /// <summary> 取得プロパティ: 素早さ </summary>
    }
}

