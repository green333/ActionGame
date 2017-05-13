using UnityEngine;
using System.Collections;

/// <summary>
/// シングルトンクラスを作成するときにはこのクラスを継承すること
/// </summary>
public class BaseSingleton<T>where T : class, new()
{
    /// <summary>
    /// インスタンス
    /// </summary>
    private static  T instance = null;

    /// <summary>
    /// インスタンスを取得
    /// </summary>
    static public T Instance { get { if (instance == null) { instance = new T(); } return instance; } }

}
