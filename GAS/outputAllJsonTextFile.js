// シート名
var SHEET_NAME_PLAYER_BASE_MASTER           = "プレイヤー基本マスタ";
var SHEET_NAME_ENEMY_BASE_MASTER            = "敵基本マスタ";
var SHEET_NAME_ENEMY_GROWTH_MASTER          = "敵成長マスタ";
var SHEET_NAME_ENEMY_SPAWAN_MASTER          = "敵出現マスタ";
var SHEET_NAME_ITEM_MASTER                  = "アイテムマスタ";
var SHEET_NAME_STAGE_MASTER                 = "ステージマスタ";
var SHEET_NAME_BGM_MASTER                   = "BGMマスタ";
var SHEET_NAME_WEAPON_MASTER                = "武器マスタ";
var SHEET_NAME_CSV_OUTPUT                   = "CSV出力";
var SHEET_NAME_ENEMY_TRIBAL_VALUE_MASTER    = "敵種族値マスタ";
var SHEET_NAME_ENEMY_EXTENSION_KEY          = "敵(外部キー)";
var SHEET_NAME_ITEM_EXTENSION_KEY           = "アイテム(外部キー)";

//--------------------------------------------------------------------------------------------------
// シートのデータをテキストファイル(Json形式)に出力
//--------------------------------------------------------------------------------------------------
function toJsonTextFile(values,filename)
{
  const COL_TYPE_NUM = 0; // 型が定義されている場所
  const COL_NAME_NUM = 1; // 変数名が定義されている場所
  
  // データ開始インデックス
  const START_INDEX = 3;

  // マスターデータをjson形式で出力する
  var masterJsonStr = '';
  
  for(var i = START_INDEX; i < values.length; ++i)
  {
    masterJsonStr += '{';
    
    var strtempList = [];
    for(var j = 0; j < values[i].length; ++j)
    {
      var strtemp = "";
      // カラムが入力されていないところは表示欄として扱うため、continue
      if(values[COL_NAME_NUM][j] == "" || values[COL_NAME_NUM][j] == " ")
      {
        continue;
      }

      // 値が設定されていない場合、出力しない
      if(values[i][j]  == "")
      {
        continue;
      }

      // 変数名を出力
      strtemp = "\"" + values[COL_NAME_NUM][j] + "\":";

      // 値を出力(文字列の場合""で囲んで出力する)
      strtemp += ((values[COL_TYPE_NUM][j] == "string") ? ("\"" + values[i][j] + "\"") : values[i][j]);

      strtempList.push(strtemp);
    }

    masterJsonStr += strtempList.join(',') + '}\n';
  }
  
  // UTF8なBlobに変換
  var blob = Utilities.newBlob("","text/plain",filename).setDataFromString(masterJsonStr,"UTF8");
  
  // ファイルを作成する
  createFile(blob,filename);
}

//--------------------------------------------------------------------------------------------------
//　すべてのマスタデータをJson形式のテキストデータに出力する
//--------------------------------------------------------------------------------------------------
function outputAllJsonTextFile()
{
  // スプレットシートAPPを取得
  var spreadSheetApp =  SpreadsheetApp.getActiveSpreadsheet();
  
  // 全シートを取得
  var sheetList = spreadSheetApp.getSheets();
  
  // CSV出力シートの番号
  const CSV_OUTPUT_SHEET_NUM = 0;

  // シート名とクラス名の連想配列
  var masterParameterClassNameList =  GetOutputSheetNameList(sheetList[CSV_OUTPUT_SHEET_NUM].getDataRange().getValues());
  
  var i = CSV_OUTPUT_SHEET_NUM + 1;
  for(; i < sheetList.length; ++i)
  {   
    // シート名を取得
    var sheetName = sheetList[i].getName();
    
    // CSV出力シートに定義されていないシートはマスタ出力しない
    if(typeof masterParameterClassNameList[sheetName] == 'undefined')
    {
      continue;
    }
    
    // テキストファイルに変換する
    toJsonTextFile(sheetList[i].getDataRange().getValues(), sheetName + ".txt");
    
  }
}

//--------------------------------------------------------------------------------------------------
// 共通Jsonテキストファイル出力処理
//--------------------------------------------------------------------------------------------------
function commonOutputJsonTextFile(sheetName)
{
    // スプレットシートAPPを取得
  var spreadSheetApp =  SpreadsheetApp.getActiveSpreadsheet();
  
  // 指定したシートを取得
  var masterSheet = spreadSheetApp.getSheetByName(sheetName);
  
  // Json形式のテキストファイルに変換する
  toJsonTextFile(masterSheet.getDataRange().getValues(),sheetName);
}
//--------------------------------------------------------------------------------------------------
// プレイヤー基本マスタJsonテキストファイル出力を行う
//--------------------------------------------------------------------------------------------------
function outputJsonTextFilePlayerBaseMaster()
{
  commonOutputJsonTextFile(SHEET_NAME_PLAYER_BASE_MASTER);
}
//--------------------------------------------------------------------------------------------------
// 敵基本マスタJsonテキストファイル出力を行う
//--------------------------------------------------------------------------------------------------
function outputJsonTextFileEnemyBaseMaster()
{
  commonOutputJsonTextFile(SHEET_NAME_ENEMY_BASE_MASTER);
}
//--------------------------------------------------------------------------------------------------
// 敵成長マスタJsonテキストファイル出力を行う
//--------------------------------------------------------------------------------------------------
function outputJsonTextFileEnemyGrowthMaster()
{
  commonOutputJsonTextFile(SHEET_NAME_ENEMY_GROWTH_MASTER);
}
//--------------------------------------------------------------------------------------------------
// 敵出現マスタJsonテキストファイル出力を行う
//--------------------------------------------------------------------------------------------------
function outputJsonTextFileEnemySpawnMaster()
{
  commonOutputJsonTextFile(SHEET_NAME_ENEMY_SPAWAN_MASTER);
}
//--------------------------------------------------------------------------------------------------
// アイテムマスタJsonテキストファイル出力を行う
//--------------------------------------------------------------------------------------------------
function outputJsonTextFileItemMaster()
{
  commonOutputJsonTextFile(SHEET_NAME_ITEM_MASTER);
}
//--------------------------------------------------------------------------------------------------
// ステージマスタJsonテキストファイル出力を行う
//--------------------------------------------------------------------------------------------------
function outputJsonTextFileStageMaster()
{
  commonOutputJsonTextFile(SHEET_NAME_STAGE_MASTER);
}
//--------------------------------------------------------------------------------------------------
// BGMマスタJsonテキストファイル出力を行う
//--------------------------------------------------------------------------------------------------
function outputJsonTextFileBGMMaster()
{
  commonOutputJsonTextFile(SHEET_NAME_BGM_MASTER);
}
//--------------------------------------------------------------------------------------------------
// 武器マスタJsonテキストファイル出力を行う
//--------------------------------------------------------------------------------------------------
function outputJsonTextFileWeaponMaster()
{
  commonOutputJsonTextFile(SHEET_NAME_WEAPON_MASTER);
}
