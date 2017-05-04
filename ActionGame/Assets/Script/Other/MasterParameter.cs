using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 敵基本マスタパラメーター
/// </summary>
public class EnemyBaseMaster : ScriptableObject{

    public List<Param> list = new List<Param>();

    [System.SerializableAttribute]
    public class Param
    {
        public string name;        //    名前
        public int id;        //    ID
        public int level;        //    レベル
        public int hp;        //    体力
        public int atk;        //    攻撃力
        public int def;        //    防御力
        public int mgc;        //    魔法力
        public int spd;        //    素早さ
        public int exp;        //    取得経験値
    }
}

/// <summary>
/// ステージマスタパラメーター
/// </summary>
public class StageMaster : ScriptableObject{

    public List<Param> list = new List<Param>();

    [System.SerializableAttribute]
    public class Param
    {
        public string name;        //    名前
        public int id;        //    ID
        public int chapter1_id;        //    第一章
        public int chapter2_id;        //    第二章
        public int chapter3_id;        //    第三章
        public int chapter4_id;        //    第四章
    }
}

/// <summary>
/// 敵出現マスタパラメーター
/// </summary>
public class EnemySpawnMaster : ScriptableObject{

    public List<Param> list = new List<Param>();

    [System.SerializableAttribute]
    public class Param
    {
        public int stage_id;        //    ステージID
        public int chapter_id;        //    章
        public string enemy1_name;        //    敵１
        public int enemy1_lvpm;        //    レベル幅
        public string enemy2_name;        //    敵２
        public int enemy2_lvpm;        //    レベル幅
        public string enemy3_name;        //    敵３
        public int enemy3_lvpm;        //    レベル幅
    }
}

