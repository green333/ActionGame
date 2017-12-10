//--------------------------------------------------------------------------------------------------
// MasterParamtere.csを出力する
//--------------------------------------------------------------------------------------------------
function outputMasterParameter()
{
    // スプレットシートAPPを取得
    var spreadSheetApp =  SpreadsheetApp.getActiveSpreadsheet();
    
    // 全シート取得
    var sheetList = spreadSheetApp.getSheets();

    // CSV出力シートの番号
    const CSV_OUTPUT_SHEET_NUM = 0;
    
    // CSV出力シートのみここで取得
    var csvOutputSheet = sheetList[CSV_OUTPUT_SHEET_NUM].getDataRange().getValues();
    
    // シート名が記載され始めてる行番号
    const BEGIN_SHEET_NAME_ROW_LINE = 3;
    
    const SHEET_NAME_COLUMN_LINE = 1; // シート名が記載されている列番号
    const CLASS_NAME_COLUMN_LINE = 2; // クラス名が記載されている列番号
    
    // シート名とクラス名の連想配列
    var masterParameterClassNameList = {};
    for(var i = BEGIN_SHEET_NAME_ROW_LINE; i < csvOutputSheet.length; ++i)
    {
        var temp = csvOutputSheet[i];

        // シート名、クラス名どちらかが定義されていなければcontinueさせる
        if(temp[SHEET_NAME_COLUMN_LINE] == "" || temp[CLASS_NAME_COLUMN_LINE] == "")
        {
            continue;
        }
        masterParameterClassNameList[temp[SHEET_NAME_COLUMN_LINE]] = temp[CLASS_NAME_COLUMN_LINE];
    }

    // CSharpインスタンスにusingを追加
    AddUsingList('UnityEngine');
    AddUsingList('System.Collections');
    AddUsingList('System.Collections.Generic');

    // 0番目のシートはCSV出力用シートなので飛ばす
    var i = CSV_OUTPUT_SHEET_NUM + 1;
    for(; i < sheetList.length; ++i)
    { 
        var sheet = sheetList[i];

        // データを取得
        var sheetDataList = sheet.getDataRange().getValues();
        
        // シート名を取得
        var sheetName = sheet.getName();
        
        // CSV出力シートに記載されてなければcontinueする
        if(typeof masterParameterClassNameList[sheetName] == 'undefined')
        {
            continue;
        }
     
        /// 1~3行目に設定されている型と変数名とコメントを読み込んでいく
        var valueTypeList       = [];   //  型
        var valueNameList       = [];   //  変数名
        var valueCommentList    = [];   //  コメント

        var VALUE_TYPE_INDEX    = 0;    // 変数の型が定義されている行 
        var VALUE_NAME_INDEX    = 1;    // 変数名が定義されている行
        var VALUE_COMMENT_INDEX = 2;    // 変数のコメントが定義されている行
        for(var j = 0; j < sheetDataList[VALUE_TYPE_INDEX].length; ++j)
        {
            var valueType       = sheetDataList[VALUE_TYPE_INDEX][j];
            var valueName       = sheetDataList[VALUE_NAME_INDEX][j];
            var valueComment    = sheetDataList[VALUE_COMMENT_INDEX][j];

            // 型名チェックを行う
            switch(valueType)
            {
                case "int":case "string":case "float":break;
                default: // 定義されている型名が間違えている、または定義されていない場合はその列に定義されている型、変数名、コメントを出力しない 
                continue;
            }
                
            // 同じ変数が定義されている場合、コンパイルエラーとなるため画面にエラーメッセージを表示して終了する。
            if(valueNameList.indexOf(valueName) != -1)
            {
                Browser.msgBox(sheetName + "に同じ変数が定義されています。変数名:" + valueName);
                return;
            }
            
            valueTypeList.push(valueType);
            valueNameList.push(valueName);
            valueCommentList.push(valueComment);
        }
        
        // シート名からクラス名を取得
        var className = masterParameterClassNameList[sheetName];
        
        // そのシートに定義された内容からマスタパラメータークラスを作成する
        var classData = new Class(ACCESS_MODIFIRES.PUBLIC,className,"ScriptableObject",sheetName + "パラメーター",null);
        var classParamData = new Class(ACCESS_MODIFIRES.PUBLIC,'Param',null,null,'System.SerializableAttribute',1);
        for(var j = 0; j < valueTypeList.length; ++j)
        {
            // クラスの要素として変数を追加する
            classParamData.elementList.push(new Variable(ACCESS_MODIFIRES.PUBLIC,valueTypeList[j],valueNameList[j],valueCommentList[j],null,1));
        }
        // クラスの要素としてクラスを追加する
        classData.elementList.push(classParamData);

        // CSharpインスタンスにクラス情報を追加
        AddElementList(classData);
    } 

    // 設定された情報からCSharpのスクリプト文を作成する
    masterParameterString = CreateCSharpString();

    // 文字列をバイトに変換する
    var blobMasterParameter         = Utilities.newBlob("","text/csv","MasterParameter.cs").setDataFromString(masterParameterString,"UTF8");
    
    // ファイルを作成する
    createFile(blobMasterParameter,"MasterParameter.cs");
}
