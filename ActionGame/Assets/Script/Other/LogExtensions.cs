using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class LogExtensions
{
    /// <summary>
    /// 赤色のログを出力
    /// </summary>
    /// <param name="message"></param>
    [Conditional("UNITY_EDITOR")]
    public static void Red(string message)
    {
        Debug.Log("<color=red>" + message + "</color>");
    }

    /// <summary>
    /// 青色のログを出力
    /// </summary>
    /// <param name="message"></param>
    [Conditional("UNITY_EDITOR")]
    public static void Blue(string message)
    {
        Debug.Log("<color=blue>" + message + "</color>");
    }

    /// <summary>
    /// 黄色のログを出力
    /// </summary>
    /// <param name="message"></param>
    [Conditional("UNITY_EDITOR")]
    public static void Yellow(string message)
    {
        Debug.Log("<color=yellow>" + message + "</color>");
    }

    /// <summary>
    /// 緑色のログを出力
    /// </summary>
    /// <param name="message"></param>
    [Conditional("UNITY_EDITOR")]
    public static void Green(string message)
    {
        Debug.Log("<color=green>" + message + "</color>");
    }

    /// <summary>
    /// 黒色のログを出力
    /// </summary>
    /// <param name="message"></param>
    [Conditional("UNITY_EDITOR")]
    public static void Black(string message)
    {
        Debug.Log("<color=black>" + message + "</color>");
    }
}
