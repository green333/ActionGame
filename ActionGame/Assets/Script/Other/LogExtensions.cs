using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class LogExtensions
{
    /// <summary>
    /// エラーとしてログ出力する
    /// </summary>
    /// <param name="message"></param>
    [Conditional("UNITY_EDITOR")]
    public static void OutputError(string message)
    {
        Debug.Log("<color=red>[Error] : " + message + "</color>");
    }

    /// <summary>
    /// 確認用としてログ出力する
    /// </summary>
    /// <param name="message"></param>
    [Conditional("UNITY_EDITOR")]
    public static void OutputInfo(string message)
    {
        Debug.Log("[Info] : " + message);
    }

    /// <summary>
    /// ワーニングとして出力する
    /// </summary>
    /// <param name="message"></param>
    [Conditional("UNITY_EDITOR")]
    public static void OutputWarn(string message)
    {
        Debug.Log("<color=yellow>[Warn] : " + message + "</color>");
    }
}
