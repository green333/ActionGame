//using UnityEngine;
//using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;


/// <summary>
/// テキストを読み込むクラス
/// </summary>
public class TexLoader{

    private StreamReader _stream = null;
    protected StreamReader stream { get { return _stream; } }

    protected void Open(string filename,string open_encode = "UTF-8")
    {
        _stream = new StreamReader(filename, System.Text.Encoding.GetEncoding(open_encode));
    }

    protected void Close()
    {
        stream.Close();
    }

    protected string GetLine()
    {
        if(stream.EndOfStream)
        {
            return null;
        }
        return stream.ReadLine();
    }

}

/// <summary>
/// テキストに書き出したマスタから必要なデータを探したりするクラス
/// </summary>
public class TextMasterManager : TexLoader
{
    // このクラスの存在理由
    // マスタデータ参照の速度を上げるために、同じﾃﾞｰﾀの検索を行ったりした際、
    // 検索値と見つけたﾃﾞｰﾀ変数に一時保存し、一時保存された変数に一致する検索値が来た場合
    // その時のﾃﾞｰﾀを返す。
    // といったコードを書く場合などがあるため。

    /// <summary>
    /// 指定した文字列に一致する文字列が見つかった場合、見つけた行の文字列を返す(なければnull)
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    protected string Search(string val)
    {
        string ret = null;

        string getLine  = base.GetLine();

        while(getLine != null)
        {
            // -1以外の場合、指定した文字列に一致するデータが見つかったので
            // 見つかった行の文字列を戻り値に格納しwhileを抜ける
            if(getLine.IndexOf(val) != 1)
            {
                ret =  getLine;
                break;
            }

            getLine = base.GetLine();
        }

        return ret;
    }
}