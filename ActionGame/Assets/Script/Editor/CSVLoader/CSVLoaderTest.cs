using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class CSVLoaderTest
{
    private CSVLoader csvLoader = null;

    [SetUp]
    public void TestInit()
    {
        // CSVのインスタンスを取得
        csvLoader = CSVLoader.Instance;
    }

    public static TestCaseData[] loadCSVAllProvider = new[]
    {
        // パスが正しく、日本語、英語、数字が正常に読み込まれているかのテスト
        new TestCaseData(Application.dataPath + "/Script/Editor/CSVLoader/fixture_loadVCSVAll.csv", new List<string> {"1","2","3","4","5","aiueo","あいうえお","123test" }).SetName("test loadCSVAll() SUCCEED"),
        
        // パスが間違っているときのテスト
        new TestCaseData(Application.dataPath + "/CSVLoader/fixture_loadVCSVAll.csv",null).SetName("test loadCSVAll() FAILED")
    };

    [TestCaseSource("loadCSVAllProvider")]
    public void TestLoadCSVAll(string filepath, List<string> funcRetResult)
    {
        List<string> result = csvLoader.LoadCSVAll(filepath);

        if (result == null)
        {
            Assert.AreEqual(result, funcRetResult);
        }else
        {
            Assert.AreEqual(result.ToArray(), funcRetResult.ToArray());
        }
    }


    public static TestCaseData[] loadCSVExceptHeaderProvider = new[]
    {
        // パスが正しく、日本語、英語、数字が正常に読み込まれているかのテスト
        new TestCaseData(Application.dataPath + "/Script/Editor/CSVLoader/fixture_loadVCSVAll.csv", new List<string> {"aiueo","あいうえお","123test" }).SetName("test loadCSVAll() SUCCEED"),
        
        // パスが間違っているときのテスト
        new TestCaseData(Application.dataPath + "/CSVLoader/fixture_loadVCSVAll.csv",null).SetName("test loadCSVAll() FAILED")
    };

    [TestCaseSource("loadCSVExceptHeaderProvider")]
    public void TestLoadCSVExceptHeader(string filepath, List<string> funcRetResult)
    {
        int headerLine = 1;
        List<string> result = csvLoader.LoadCSVExceptHeader(filepath, headerLine);

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
