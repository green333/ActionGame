using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// テキストに書き出したマスタから必要なデータを探したりするクラス
/// </summary>
public class TextMasterManager : TextLoader
{
    // TODO:検索速度を速めるために、キャッシュ処理を今度作成する
 
    /// <summary>
    /// 指定した文字列に一致する行データを取得する
    /// </summary>
    /// <param name="selectStr">検索を行う文字列(複数ある場合はカンマ区切り)</param>
    /// <returns>
    /// 一致するデータがあるば行データ
    /// 一致するデータがなければstring.Empty
    /// </returns>
    protected string Search(string selectStr)
    {
        string ret = string.Empty;

        string getLine = base.GetLine();

        // カンマ区切りで検索文字列を分割する
        string[] selectStrList = selectStr.Split(',');

        // 検索を行う文字列の対象数
        int selectStrCount = selectStrList.Length;

        while (getLine != string.Empty)
        {
            if(string.Empty != (ret = commaStrSearch(getLine, selectStrList, selectStrCount)))
            {
                break;
            }
            getLine = base.GetLine();
        }

        return ret;
    }

    /// <summary>
    /// 指定した文字列に一致するデータを複数取得する
    /// </summary>
    /// <param name="selectStr">検索を行う文字列</param>
    /// <returns></returns>
    protected List<string> SearchMultiple(string selectStr)
    {
        List<string> retList = new List<string>();

        string getLine = base.GetLine();

        // カンマ区切りで検索文字列を分割する
        string[] selectStrList = selectStr.Split(',');

        // 検索を行う文字列の対象数
        int selectStrCount = selectStrList.Length;

        string temp = string.Empty;
        while (getLine != string.Empty)
        {
            if (string.Empty != (temp = commaStrSearch(getLine, selectStrList, selectStrCount)))
            {
                retList.Add(temp);
            }
            getLine = base.GetLine();
        }

        return retList;
    }
    /// <summary>
    /// 指定した文字列リストに一致する行データを複数取得する
    /// </summary>
    /// <param name="selectStrList">検索を行う文字列リスト(複数ある場合は要素をカンマ区切りで格納する)</param>
    /// <param name="selectStrCount">検索対象の行データ取得数</param>
    /// <returns>
    /// 一致するデータがある時は行データ
    /// 一致するデータはstring.Emptyが格納される
    /// </returns>
    protected string[] SearchList(string[] selectStrList,int selectStrCount = 10)
    {
        string[] ret = Enumerable.Repeat<string>(string.Empty, selectStrList.Length).ToArray();

        // 検索対象の文字列を配列で取得する
        string[] getLineList = base.GetLine(selectStrCount);

        // 検索する文字列をListにする
        List<string> selectStr = new List<string>(selectStrList);
        
        // カンマ区切りした文字列配列を格納する
        string[] splistStr = null;

        bool isEnd = false;
        int index = 0;
        while(true)
        {
            foreach(string getLine in getLineList)
            {
                // 行データが途中で取得できてなかったときは、ファイル読み込みが終端まで行った時だけなので
                // isEndフラグをたてる
                if (getLine == string.Empty)
                {
                    isEnd = true;
                    break;
                }

                for(int i = 0; i < selectStr.Count; ++i)
                {
                    // 空文字列が入っている場合は検索せずにcontinue
                    if(selectStr[i] == "" || selectStr[i] == "0") { continue; }
                    splistStr = selectStr[i].Split(',');

                    // カンマ区切りした文字列配列に一致する行データを取得する
                    if(string.Empty != (ret[index] = commaStrSearch(getLine, splistStr, splistStr.Length)))
                    {
                        // 一致する行データを取得できたので、検索対象から外してbreakする
                        selectStr.Remove(selectStr[i]);
                        ++index;
                        break;
                    }
                }

                if(index == selectStrList.Length)
                {
                    isEnd = true;
                    break;
                }
            }

            if (isEnd)
            {
                break;
            }

            // 次の検索対象を取得する
            getLineList = base.GetLine(selectStrCount);
        }

        return ret;
    }

    /// <summary>
    /// 指定した文字列リストに一致する行データを複数取得する
    /// </summary>
    /// <param name="selectStrList">検索を行う文字列リスト(複数ある場合は要素をカンマ区切りで格納する)</param>
    /// <param name="selectStrCount">検索対象の行データ取得数</param>
    /// <returns>
    /// 一致するデータがある時は行データ
    /// 一致するデータはstring.Emptyが格納される
    /// </returns>
    protected string[] SearchList(List<string> selectStrList, int selectStrCount = 10)
    {
        string[] ret = Enumerable.Repeat<string>(string.Empty, selectStrList.Count).ToArray();

        // 検索対象の文字列を配列で取得する
        string[] getLineList = base.GetLine(selectStrCount);

        // カンマ区切りした文字列配列を格納する
        string[] splistStr = null;

        bool isEnd = false;
        int index = 0;
        int checkEndCount = selectStrList.Count;
        while (true)
        {
            foreach (string getLine in getLineList)
            {
                // 行データが途中で取得できてなかったときは、ファイル読み込みが終端まで行った時だけなので
                // isEndフラグをたてる
                if (getLine == string.Empty)
                {
                    isEnd = true;
                    break;
                }

                for (int i = 0; i < selectStrList.Count; ++i)
                {
                    // 空文字列が入っている場合は検索せずにcontinue
                    if (selectStrList[i] == "" || selectStrList[i] == "0") { continue; }
                    splistStr = selectStrList[i].Split(',');

                    // カンマ区切りした文字列配列に一致する行データを取得する
                    if (string.Empty != (ret[index] = commaStrSearch(getLine, splistStr, splistStr.Length)))
                    {
                        // 一致する行データを取得できたので、検索対象から外してbreakする
                        selectStrList.Remove(selectStrList[i]);
                        // TODO:検索対象から除外した場合、検索ヒット数===データ数ならbreakのif文が意図したとおりにならない。
                        // Remove処理を消すことで処理は成り立つが、検索回数が多くなるため、検索速度があまりにも遅くなった場青、
                        // ここの処理を作り直す必要がある
                        ++index;
                        break;
                    }
                }

                if (index == checkEndCount)
                {
                    isEnd = true;
                    break;
                }
            }

            if (isEnd)
            {
                break;
            }

            // 次の検索対象を取得する
            getLineList = base.GetLine(selectStrCount);
        }

        return ret;
    }


    /// <summary>
    /// カンマ区切りされた文字列リストに一致する行データを取得する
    /// </summary>
    /// <param name="getLine">検索対象の行データ</param>
    /// <param name="commaStr">カンマ区切りされた文字列リスト</param>
    /// <param name="commaStrLength">カンマ区切りされた文字列リストの長さ</param>
    /// <returns>
    /// 一致するデータがあれば行データ
    /// 一致するデータがなければstring.Empty
    /// </returns>
    private string commaStrSearch(string getLine,string[] commaStr,int commaStrLength)
    {
        string ret = string.Empty;

        // 一致したデータ数と検索を行う文字列の対象数の比較チェックを行うための変数
        int matchCount = 0;

        foreach (string str in commaStr)
        {
            if (getLine.IndexOf(str) != -1)
            {
                ++matchCount;
            }
        }

        if (commaStrLength == matchCount)
        {
            ret = getLine;
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