
//--------------------------------------------------------------------------------------------------
// スプレッドシート起動時に、CSV出力シートに入力されているシート名一覧に該当するシートを、
// json形式のCSVファイルに出力するためのボタンをメニューバーに追加する。
//--------------------------------------------------------------------------------------------------
function executeStartingSpreadsheet()
{ 
  //　スプレットシートAPPを取得
  var spreadSheet = SpreadsheetApp.getActiveSpreadsheet();
  
  // 「追加メニュー」ボタンに追加するボタンリスト
  var entries = 
  [
    // ボタン名とボタンが押されたときに呼ばれる関数名
    {name : "ALL_JSONファイル出力",  functionName : "outputAllJsonTextFile"},
    {name : "マスタパラメーター出力",  functionName : "outputMasterParameter"},
  ];
  
  // 全シートを取得
  var sheetList = spreadSheet.getSheets();
  
  // CSV出力シートの番号
  const CSV_OUTPUT_SHEET_NUM = 0;
  
  // シート名とクラス名の連想配列
  var masterParameterClassNameList =  GetOutputSheetNameList(sheetList[CSV_OUTPUT_SHEET_NUM].getDataRange().getValues());
  
  
  var i = CSV_OUTPUT_SHEET_NUM + 1;
  for(;i < sheetList.length; ++i)
  {
    // シート名を取得
    var sheetName = sheetList[i].getName();
    
    // CSV出力シートに定義されていないシートはマスタ出力しない
    if(typeof masterParameterClassNameList[sheetName] == 'undefined')
    {
      continue;
    }

    var className = masterParameterClassNameList[sheetName];
    entries.push({name:sheetName + "CSV出力",functionName: "outputJsonTextFile" + className});
    
  }

  // メニューバーに追加メニューボタンを作成する
  spreadSheet.addMenu("追加メニュー", entries);
 
}

//--------------------------------------------------------------------------------------------------
// ファイルを作成する(同名ファイルがあれば、そのファイルをゴミ箱に移動させる)
//--------------------------------------------------------------------------------------------------
function createFile(blob,filename)
{
  // MasterDataフォルダを取得する
  var folderIte = DriveApp.getFoldersByName("MasterData");
  
  // 実際にフォルダがあるかをチェック
  if(folderIte.hasNext())
  {
     // フォルダを取得
    var folder = folderIte.next();
    
    // 指定したファイルを取得する
    var fileIte =  folder.getFilesByName(filename); 
    
    // 実際にファイルがあるかをチェック
    if(fileIte.hasNext())
    {
      // MasterData以下にある同名ファイルを削除する
      folder.removeFile(fileIte.next());
    }
    
  }else{
    // フォルダがないので作成する
    folder = DriveApp.createFolder("MasterData");
  }
 
  // MasterData以下に指定ファイルを作成する
  folder.createFile(blob);
}

