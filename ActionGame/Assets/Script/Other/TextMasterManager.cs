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
    /// <param name="val"></param>
    /// <returns>
    /// 一致するデータがあった時:   一致した文字列データ 
    /// 一致するデータが無かった時: string.Empty
    /// </returns>
    protected string Search(string val)
    {
        string ret = string.Empty;

        string getLine = base.GetLine();

        while (getLine != string.Empty)
        {
            // -1以外の場合、指定した文字列に一致するデータが見つかったので
            // 見つかった行の文字列を戻り値に格納しwhileを抜ける
            if (getLine.IndexOf(val) != -1)
            {
                ret = getLine;
                break;
            }

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
    /// 複数の検索候補に一致したデータを取得する
    /// (一致するデータがなければnullを返す)
    /// </summary>
    /// <param name="searchList"></param>
    /// <returns></returns>
    protected string MultipleSearch(string[] searchList)
    {
        string ret = null;

        string getLine = base.GetLine();

        bool[] checkedList = new bool[searchList.Length];

        int checkIndex = 0;
        bool cheked = false;
        while (getLine != null)
        {
            // チェックリストを初期化
            for (int i = 0; i < checkedList.Length; ++i)
            {
                checkedList[i] = false;
            }

            // 
            foreach (string search in searchList)
            {
                if (getLine.IndexOf(search) != -1)
                {
                    checkedList[checkIndex] = true;
                    ++checkIndex;
                }
            }

            foreach (bool check in checkedList)
            {
                if (check == false)
                {
                    checkIndex = 0;
                    break;
                }
                cheked = true;
                break;
            }
            if (cheked)
            {
                ret = getLine;
                break;
            }
            getLine = base.GetLine();
        }
        return ret;
    }

    protected string[] MultipleSearchList()
    {
        return null;
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