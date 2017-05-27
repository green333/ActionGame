﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// テキストを読み込むクラス
/// 文字列の読み込みができなかった場合、string.Emptyを返すようにする。
/// </summary>
public class TexLoader{

    private StreamReader _stream = null;
    protected StreamReader stream { get { return _stream; } }

    /// <summary>
    /// ファイルを開く
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="open_encode"></param>
    protected void Open(string filename,string open_encode = "UTF-8")
    {
        _stream = new StreamReader(filename, System.Text.Encoding.GetEncoding(open_encode));
    }

    /// <summary>
    /// ファイルを閉じる
    /// </summary>
    protected void Close()
    {
        stream.Close();
    }

    /// <summary>
    /// テキストから一行読み込む
    /// ファイル終端ならstring.Emptyを返す
    /// </summary>
    /// <returns></returns>
    protected string GetLine()
    {
        if(stream.EndOfStream)
        {
            return string.Empty;
        }

        return stream.ReadLine();
    }

    /// <summary>
    /// 指定した行数、テキストから読み込む。
    /// 指定した行数で配列生成するが指定した行数分読み込めなかった場合、配列の中にはstring.Emptyが格納されている。
    /// ※一行も読めなかったときはnullが返るので注意。
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    protected string[] GetLine(int line)
    {
        string[] ret = null;

        do
        {
            // 一行も読めなかったときはbreak
            if (stream.EndOfStream)
            {
                break;
            }

            // 長さlineの配列をnullで初期化
            ret = Enumerable.Repeat<string>(string.Empty, line).ToArray();

            for (int i = 0; i < line;++i)
            {
                ret[i] = stream.ReadLine();
                if(stream.EndOfStream)
                {
                    break;
                }
            }
        } while (false);

        return ret;
    }
}
