using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// PlayerPrefsラッパー
/// </summary>
public class PlayerPrefsWrap  {

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
        return PlayerPrefs.GetInt(key);
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
        return PlayerPrefs.GetFloat(key);
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
        return PlayerPrefs.GetString(key);
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
        //string json = JsonUtility.ToJson(obj);
        //PlayerPrefs.SetString(key, json);
        //PlayerPrefs.Save();
    }

    /// <summary>
    /// 任意の型のロード
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T LoadGeneric<T>(string key)
    {
        //if (PlayerPrefs.HasKey(key)) {
            
        //}

        string str = PlayerPrefs.GetString(key);
        BinaryFormatter bif = new BinaryFormatter();
        MemoryStream memory = new MemoryStream(Convert.FromBase64String(str));

        return (T)bif.Deserialize(memory);
        //string json = PlayerPrefs.GetString(key);
        //T ret = JsonUtility.FromJson<T>(json);
        //return ret;
    }
}
