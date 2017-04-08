//using UnityEngine;
//using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// CSV読み込みクラス
/// </summary>
public sealed class CSVLoader {// : MonoBehaviour {

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private CSVLoader()
    {

    }

    /// <summary>
    /// シングルトン
    /// </summary>
    private static CSVLoader instance = new CSVLoader();
    public static CSVLoader Instance
    {
        get {
            return instance;
        }
    }

    /// <summary>
    /// 全読み込み
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public List<string> LoadCSVAll(string filePath)
    {
        List<string> res = new List<string>();
        StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8);

        //  最後まで読む
        string allData = sr.ReadToEnd();

        //  「,」と改行で分けて配列に格納
        string[] cell = allData.Split(new string[] { ",", Environment.NewLine }, System.StringSplitOptions.None);

        //  格納
        foreach(string str in cell)
        {
            res.Add(str);
        }

        return res;
    }

    /// <summary>
    /// ヘッダ部分読み飛ばし
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="headerLine"></param>
    /// <returns></returns>
    public List<string> LoadCSVExceptHeader(string filePath, int headerLine)
    {
        List<string> res = new List<string>();
        StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8);

        // 読み飛ばし
        for (int i = 0; i < headerLine; i++) {
            sr.ReadLine();
        }

        //  読み込み
        while(!sr.EndOfStream)
        {
            string str = sr.ReadLine();
            string[] cell = str.Split(new string[] { ",", Environment.NewLine }, System.StringSplitOptions.None);

            foreach(string row in cell)
            {
                res.Add(row);
            }
        }

        return res;
    }
}
