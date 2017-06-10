using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// PlayerPrefsラッパー
/// </summary>
public class PlayerPrefsWrap  {

    /// <summary> int型のデフォルト値 </summary>
    public const int DEFAULT_INT_VALUE = -1;
    /// <summary> float型のデフォルト値 </summary>
    public const float DEFAULT_FLOAT_VALUE = -1.0f;
    /// <summary> string型のデフォルト値 </summary>
    public const string DEFAULT_STRING_VALUE = "null";

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private PlayerPrefsWrap() { }

    /// <summary>
    /// シングルトン
    /// </summary>
    private static PlayerPrefsWrap instance = new PlayerPrefsWrap();
    public static PlayerPrefsWrap Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// 全削除
    /// </summary>
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    /// <summary>
    /// キー指定削除
    /// </summary>
    /// <param name="key"></param>
    public void Delete(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
    
    /// <summary>
    /// int型のセーブ
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// int型のロード
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int LoadInt(string key)
    {
        return PlayerPrefs.GetInt(key, DEFAULT_INT_VALUE);
    }

    /// <summary>
    /// float型のセーブ
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// float型のロード
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public float LoadFloat(string key)
    {
        return PlayerPrefs.GetFloat(key, DEFAULT_FLOAT_VALUE);
    }

    /// <summary>
    /// string型のセーブ
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// string型のロード
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string LoadString(string key)
    {
        return PlayerPrefs.GetString(key, DEFAULT_STRING_VALUE);
    }

    /// <summary>
    /// 任意の型のセーブ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="obj"></param>
    public void SaveGeneric<T>(string key, T obj)
    {
        BinaryFormatter bif = new BinaryFormatter();
        MemoryStream memory = new MemoryStream();
        bif.Serialize(memory, obj);
        string str = Convert.ToBase64String(memory.GetBuffer());
        PlayerPrefs.SetString(key, str);
    }

    /// <summary>
    /// 任意の型のロード
    /// 
    /// TODO:
    /// 使うときにいちいちnullチェックしないといけないのが
    /// 嫌なので使うときにnullチェックしなくていいように作り直す
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T LoadGeneric<T>(string key)
    {
        string str = PlayerPrefs.GetString(key, DEFAULT_STRING_VALUE);
        if (str == DEFAULT_STRING_VALUE)
        {
            object obj = null;
            return (T)obj;
        }
        BinaryFormatter bif = new BinaryFormatter();
        MemoryStream memory = new MemoryStream(Convert.FromBase64String(str));
        return (T)bif.Deserialize(memory);
    }
}
