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
        // 検索対象が一つで、一致するデータがあった時のテスト
        new TestCaseData("\"name\":\"薬草\"","{\"name\":\"薬草\",\"id\":4,\"kind\":\"消費アイテム\",\"effect\":100,\"desc\":\"体力を１００回復する\"}").SetName("値がstringのパラメータの検索を行い、一致するデータがあった時のテスト"),
       
        // 検索対象が複数で、一致するデータがあった時のテスト
        new TestCaseData("\"name\":\"薬草\",\"effect\":100","{\"name\":\"薬草\",\"id\":4,\"kind\":\"消費アイテム\",\"effect\":100,\"desc\":\"体力を１００回復する\"}").SetName("値がstringのパラメータの検索を行い、一致するデータがあった時のテスト"),
       
        // 一致するデータが無かったときのテスト
        new TestCaseData("",string.Empty).SetName("値がstringのパラメータの検索を行い、一致するデータがなかった時のテスト"),
    };

    /// <summary>
    /// Search()のテストを行う
    /// </summary>
    [TestCaseSource("serachProvider")]
    public void TestSearch(string selectStr, string funcRetResult)
    {
        base.Open(filename);
        Assert.AreEqual(base.Search(selectStr), funcRetResult);
        base.Close();

    }
}
