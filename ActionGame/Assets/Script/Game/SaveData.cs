using UnityEngine;
using System.Collections;

/// <summary>
/// セーブデータクラス
/// </summary>
public class SaveData
{
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
    private const string KEY_CHAPTER = "chapter";
    private const string KEY_STAGE_ID = "stageId";

    /// <summary> 章 </summary>
    public int chapter { get; set; }
    /// <summary> ステージID </summary>
    public int stageId { get; set; }

    /// <summary>
    /// セーブ
    /// </summary>
    public void Save()
    {
        PlayerPrefsWrap.Instance.SaveInt(KEY_CHAPTER, chapter);
        PlayerPrefsWrap.Instance.SaveInt(KEY_STAGE_ID, stageId);
    }

    //  ------------ ロードとりあえず個々に ------------
    /// <summary>
    /// 章ロード
    /// </summary>
    /// <returns></returns>
    public int LoadChapter()
    {
        return PlayerPrefsWrap.Instance.LoadInt(KEY_CHAPTER);
    }

    /// <summary>
    /// ステージIDロード
    /// </summary>
    /// <returns></returns>
    public int LoadStageID()
    {
        return PlayerPrefsWrap.Instance.LoadInt(KEY_STAGE_ID);
    }
}
