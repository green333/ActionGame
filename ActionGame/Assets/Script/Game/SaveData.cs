using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// セーブデータクラス
/// </summary>
[Serializable]
public class SaveData
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    private SaveData() {}

    /// <summary>
    /// シングルトン
    /// </summary>
    private static SaveData instance = new SaveData();
    public static SaveData Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// セーブ用キー定数
    /// </summary>
    //private const string KEY_CHAPTER = "chapter";
    //private const string KEY_STAGE_ID = "stageId";
    public const string KEY_SLOT_1 = "slot_1";
    public const string KEY_SLOT_2 = "slot_2";
    public const string KEY_SLOT_3 = "slot_3";

    /// <summary> 章 </summary>
    public int chapter { get; set; }
    /// <summary> ステージID </summary>
    public int stageId { get; set; }

    /// <summary> プレイヤーステータス </summary>
    public PlayerBaseMaster.Param playerParam { get; set; }
    /// <summary> プレイヤーの武器パラメーター </summary>
    public WeaponMaster.Param playerWeaponParam { get; set; }

    /// <summary>
    /// セーブ
    /// </summary>
    /// <param name="keySlot"> セーブするスロット </param>
    public void Save(string keySlot)
    {
        PlayerPrefsWrap.Instance.SaveGeneric<SaveData>(keySlot, instance);
    }

    /// <summary>
    /// ロード
    /// </summary>
    /// <param name="keySlot"> セーブするスロット </param>
    /// <returns> デシリアライズされたクラスデータ </returns>
    public SaveData Load(string keySlot)
    {
        return PlayerPrefsWrap.Instance.LoadGeneric<SaveData>(keySlot);
    }

    ////  ------------ ロードとりあえず個々に ------------
    ///// <summary>
    ///// 章ロード
    ///// </summary>
    ///// <returns></returns>
    //public int LoadChapter()
    //{
    //    return PlayerPrefsWrap.Instance.LoadInt(KEY_CHAPTER);
    //}

    ///// <summary>
    ///// ステージIDロード
    ///// </summary>
    ///// <returns></returns>
    //public int LoadStageID()
    //{
    //    return PlayerPrefsWrap.Instance.LoadInt(KEY_STAGE_ID);
    //}
}
