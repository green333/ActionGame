using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

/// <summary>
/// CSV読み込みテストクラス
/// </summary>
[TestFixture]
public class CSVLoaderTest
{
    private CSVLoader csvLoader = null;

    /// <summary>
    /// テスト開始時一番初めに呼ばれる
    /// </summary>
    [SetUp]
    public void TestInit()
    {
        csvLoader = CSVLoader.Instance;
    }

    /// <summary> LoadCSVAll()のテスト結果 </summary>
    public static TestCaseData[] loadCSVAllProvider = new[]
    {
        // パスが正しく、日本語、英語、数字が正常に読み込まれているかのテスト
        new TestCaseData(Application.dataPath + "/Script/Editor/CSVLoader/fixture_loadVCSVAll.csv", new List<string> {"1","2","3","4","5","aiueo","あいうえお","123test" }).SetName("test loadCSVAll() SUCCEED"),
        
        // パスが間違っているときのテスト
        new TestCaseData(Application.dataPath + "/CSVLoader/fixture_loadVCSVAll.csv",null).SetName("test loadCSVAll() FAILED")
    };

    /// <summary>
    /// LoadCSVAll()のテストを行う
    /// </summary>
    [TestCaseSource("loadCSVAllProvider")]
    public void TestLoadCSVAll(string filepath, List<string> funcRetResult)
    {
        List<string> result = csvLoader.LoadCSVAll(filepath);

        // 失敗した場合nullが返るため、nullの時は変数比較を行う。(ToArray()ができないため。)
        if (result == null)
        {
            Assert.AreEqual(result, funcRetResult);
        }else
        {
            Assert.AreEqual(result.ToArray(), funcRetResult.ToArray());
        }
    }

    /// <summary> LoadCSVExceptHeader()のテスト結果 </summary>
    public static TestCaseData[] loadCSVExceptHeaderProvider = new[]
    {
        // パスが正しく、日本語、英語、数字が正常に読み込まれているかのテスト
        new TestCaseData(Application.dataPath + "/Script/Editor/CSVLoader/fixture_loadVCSVAll.csv", new List<string> {"1","2","3","4","5","aiueo","あいうえお","123test" },0).SetName("test loadCSVAll() SUCCEED"),

        // headerLineが機能しているかのテスト
        new TestCaseData(Application.dataPath + "/Script/Editor/CSVLoader/fixture_loadVCSVAll.csv", new List<string> {"aiueo","あいうえお","123test" },1).SetName("work check headerLine"),
        
        // パスが間違っているときのテスト
        new TestCaseData(Application.dataPath + "/CSVLoader/fixture_loadVCSVAll.csv",null,1).SetName("test loadCSVAll() FAILED")
    };

    /// <summary>
    /// TestLoadCSVExceptHeader()のテストを行う
    /// </summary>
    [TestCaseSource("loadCSVExceptHeaderProvider")]
    public void TestLoadCSVExceptHeader(string filepath, List<string> funcRetResult,int headerLine)
    {
        List<string> result = csvLoader.LoadCSVExceptHeader(filepath, headerLine);

        // 失敗した場合nullが返るため、nullの時は変数比較を行う。(ToArray()ができないため。)
        if (result == null)
        {
            Assert.AreEqual(result, funcRetResult);
        }
        else
        {
            Assert.AreEqual(result.ToArray(), funcRetResult.ToArray());
        }
    }
}
