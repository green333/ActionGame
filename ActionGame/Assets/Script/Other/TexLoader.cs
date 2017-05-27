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
