using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
    /// 指定した文字列に一致する行データを返す。
    /// </summary>
    /// <param name="selectStr">検索を行う文字列(複数ある場合はカンマ区切り)</param>
    /// <returns>
    /// 一致するデータがあった時:   一致した文字列データ 
    /// 一致するデータが無かった時: string.Empty
    /// </returns>
    protected string Search(string selectStr)
    {
        string ret = string.Empty;

        string getLine = base.GetLine();

        // カンマ区切りで検索文字列を分割する
        string[] selectStrList = selectStr.Split(',');

        // 検索を行う文字列の対象数
        int selectStrCount = selectStrList.Length;
        
        // 一致したデータ数と検索を行う文字列の対象数の比較チェックを行うための変数
        int matchCount = 0;

        while (getLine != string.Empty)
        {
            // カンマ区切りで検索を行い、見つからなかった場合isFoundフラグをにfalseを設定する。
            // カンマ区切りでの検索を行った後、isFoundがtrueのままなら
            foreach (string str in selectStrList)
            {
                if (getLine.IndexOf(str) != -1)
                {
                    ++matchCount;
                }
            }

            if (selectStrCount == matchCount)
            {
                ret = getLine;
                break;
            }
            matchCount = 0;
            getLine = base.GetLine();
        }

        return ret;
    }

    /// <summary>
    /// 複数のデータを取得
    /// 指定した行数で配列生成するが指定した行数分読み込めなかった場合、配列の中にはstring.Emptyが格納されている。
    /// </summary>
    /// <param name="valList"></param>
    /// <returns></returns>
    protected string[] SearchList(string[] valList)
    {
        // 長さ valList.Lengthの配列をstring.Emptyで初期化
        string[] ret =  Enumerable.Repeat<string>(string.Empty, valList.Length).ToArray();

        string getLine = base.GetLine();

        int cheked = 0;
        while (getLine != null)
        {
            foreach(string str in valList)
            {
                if (getLine.IndexOf(str) != -1)
                {
                    ret[cheked++] = getLine;
                    break;
                }
            }

            // 検索したい数と同じ数だけ取得できたらbreakする
            if(cheked == valList.Length)
            {
                break;
            }

            getLine = base.GetLine();
        }

        return ret;
    }


    /// <summary>
    /// 変数名と値をjson文字列に変換する
    /// </summary>
    /// <param name="var"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    protected string VariableToJson(string var, int val)
    {
        return ("\"" + var + "\":" + val.ToString());
    }

    /// <summary>
    /// 変数名と値をjson文字列に変換する
    /// </summary>
    /// <param name="var"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    protected string VariableToJson(string var, string val)
    {
        return ("\"" + var + "\":" + "\"" + val.ToString() + "\"");
    }
}