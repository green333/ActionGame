using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

/// <summary>
/// CSV読み込みテストクラス
/// </summary>
[TestFixture]
public class TextMasterManagerTest : TextMasterManager
{
    /// <summary> テストに使用するマスタ </summary>
    string filename = "Assets/Script/Editor/TextMasterManager/fixture_アイテムマスタ.txt";

    /// <summary> Search()のテスト結果 </summary>
    public static TestCaseData[] serachProvider = new[]
    {
        // 値がstringのパラメータの検索を行い、一致するデータがあった時のテスト
        new TestCaseData("name","薬草",null,"{\"name\":\"薬草\",\"id\":4,\"kind\":\"消費アイテム\",\"effect\":100,\"desc\":\"体力を１００回復する\"}").SetName("値がstringのパラメータの検索を行い、一致するデータがあった時のテスト"),
       
        // 値がintのパラメーターの検索を行い、一致するデータがあった時のテスト
        new TestCaseData("id",string.Empty,4,"{\"name\":\"薬草\",\"id\":4,\"kind\":\"消費アイテム\",\"effect\":100,\"desc\":\"体力を１００回復する\"}").SetName("値がintのパラメーターの検索を行い、一致するデータがあった時のテスト"),
       
        // 値がstringのパラメータの検索を行い、一致するデータが無かったときのテスト
        new TestCaseData("name","そんなもんねーよ",null,string.Empty).SetName("値がstringのパラメータの検索を行い、一致するデータが無かったときのテスト"),

        // 値がintのパラメータの検索を行い、一致するデータが無かったときのテスト
        new TestCaseData("id",string.Empty,99999999,string.Empty).SetName("値がintのパラメータの検索を行い、一致するデータが無かったときのテスト"),
    
    };

    /// <summary>
    /// Search()のテストを行う
    /// </summary>
    [TestCaseSource("serachProvider")]
    public void TestSearch(string param,string strVar,int iVar, string funcRetResult)
    {
        base.Open(filename);
        string val = string.Empty;

        if (strVar == string.Empty)
        {
            val = base.VariableToJson(param, iVar);
        }
        else
        {
            val = base.VariableToJson(param, strVar);
        }
      
        Assert.AreEqual(base.Search(val), funcRetResult);
        base.Close();

    }
}
