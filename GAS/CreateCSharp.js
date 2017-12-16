// 変数かクラスを判定するタイプ
var ELEMENT_TYPE = {
  VARIABLE:1,
  CLASS:2,
};

// アクセス修飾子
var ACCESS_MODIFIRES = {
  PUBLIC:'public',
  PRIVATE:'private',
  PROTECTED:'protected',
};

// 変数の型
var VARIABLE_TYPE = {
  INT:'int',
  FLOAT:'float',
  STRING:'string',
};

//--------------------------------------------------------------------------------------------------
// .csの内容の詳細情報
//--------------------------------------------------------------------------------------------------
function CSharp()
{
  this.usingList        = []; //  .csに定義するusing文のリスト
  this.classInfoList    = []; //  .csに定義するクラス情報リスト
}

//--------------------------------------------------------------------------------------------------
// .csに定義するusing文を追加する
// @param string 追加するusing文
//--------------------------------------------------------------------------------------------------
CSharp.prototype.AddUsingList = function(using)
{
  this.usingList.push(using);
}

//--------------------------------------------------------------------------------------------------
// .csに定義するクラス情報を追加する
// @param ClassInfo element 追加するクラス情報インスタンス 
//--------------------------------------------------------------------------------------------------
CSharp.prototype.AddClassInfoList = function(element)
{
  this.classInfoList.push(element);
}

//--------------------------------------------------------------------------------------------------
// 作成する変数の情報
// @param string  accessModifiers 変数のアクセスレベル(ACCESS_MODIFIRESで設定する)
// @param string  type            変数の型(VARIABLE_TYPEで設定する)
// @param string  name            変数名
// @param string  summary         変数の概要(引数指定なし、またはnull指定の場合はコメントなし)
// @param string  attribute       属性(引数指定なし、またはnull指定の場合は属性なし)
//--------------------------------------------------------------------------------------------------
function VariableInfo(accessModifiers,type,name,summary,attribute){

  // アクセス修飾子
  this.accessModifiers = accessModifiers;

  // 変数の型
  this.type = type;

  // 変数名
  this.name = name;

  // 変数の概要
  this.summary = (typeof summary !== 'undefined') ? summary : null; 

  // 変数につける属性
  this.attribute = (typeof attribute !== 'undefined') ? attribute : null;

  // 自身の属性
  this.elementType = ELEMENT_TYPE.VARIABLE;
}

//--------------------------------------------------------------------------------------------------
// 作成するクラスの情報
// @param string  accessModifiers クラスのアクセスレベル(ACCESS_MODIFIRESで設定する)
// @param string  name            クラス名
// @param string  parentName      親クラス(引数指定なし、またはnull指定の場合は継承しない)
// @param string  summary         クラスの概要(引数指定なし、またはnull指定の場合はコメントなし)
// @param string  attribute       属性(引数指定なし、またはnull指定の場合は属性なし)
//--------------------------------------------------------------------------------------------------
function ClassInfo(accessModifiers,name,parentName,summary,attribute)
{
  // アクセス修飾子
  this.accessModifiers = accessModifiers;

  // クラス名
  this.name = name;  

  // 親クラス名(nullの場合は継承なし)
  this.parentName = (typeof parentName !== 'undefined') ? parentName : null; 

  // クラスの概要
  this.summary = (typeof summary !== 'undefined') ? summary : null; 

  // 変数につける属性
  this.attribute = (typeof attribute !== 'undefined') ? attribute : null;

  // 子情報
  this.elementList = [];
  
  // 自身の属性
  this.elementType = ELEMENT_TYPE.CLASS;
}

//--------------------------------------------------------------------------------------------------
// 作成するクラス内にメンバ情報を追加する
// @param ClassInfo or VariableInfo element クラス内に追加するメンバ情報
//--------------------------------------------------------------------------------------------------
ClassInfo.prototype.AddElementList = function(element)
{
  this.elementList.push(element);
}

//--------------------------------------------------------------------------------------------------
// 設定された情報を元に.csを作成する
// @param CSharp csharpInfo  
//--------------------------------------------------------------------------------------------------
function CreateCSharpFile(csharpInfo)
{
  var csharpStr = '';

  // using文を作成
  csharpInfo.usingList.forEach(function(usingStr){
    csharpStr += 'using ' + usingStr + ";\n";
  });

  // usingするものがあれば、using文コードの後二行ほど間隔をあける
  if(csharpStr !== '')
  {
    csharpStr += '\n\n';
  }

  // CSharpに定義されるものを作成していく
  csharpInfo.classInfoList.forEach(function(element){

    // 何が定義されているかによって処理を変える
    switch(element.elementType)
    {
      case ELEMENT_TYPE.VARIABLE:
      // namespace がないのに変数定義は不可能
      //csharpStr += CreateVariable(element);
      break;
      case ELEMENT_TYPE.CLASS:
      csharpStr += CreateClass(element);
      break;
    }

    csharpStr += '\n';
  });

  // 文字列をバイトに変換する
  var blobMasterParameter = Utilities.newBlob("","text/csv","MasterParameter.cs").setDataFromString(csharpStr,"UTF8");
    
  // ファイルを作成する
  createFile(blobMasterParameter,"MasterParameter.cs");
}

//--------------------------------------------------------------------------------------------------
// 設定された情報をもとに変数を作成する
// @param VariableInfo vriableData 変数情報をもとに、変数文を作成する
// @param string allTabSpace 作成する変数の先頭にスペースをつける(引数指定なしの場合スペースをつけない)
//--------------------------------------------------------------------------------------------------
function CreateVariable(vriableData,allTabSpace)
{
  var tabSpace = GetTabSpaceString(1);

  allTabSpace = (typeof allTabSpace != 'undefined') ?  allTabSpace : '';
  
  // アクセス修飾子 型 変数名 // コメント
  var variableString = allTabSpace + vriableData.accessModifiers + ' ' + vriableData.type + ' ' + vriableData.name + ';';
  
  if(vriableData.summary != null){
    variableString += tabSpace + '/// <summary> ' + vriableData.summary + ' </summary>';
  }

  variableString += '\n';
  return variableString;
}

//--------------------------------------------------------------------------------------------------
// 設定された情報をもとにクラスを作成する
// @param ClassInfo classData　クラス情報をもとに、クラス文を作成する
// @param string allTabSpace 作成する変数の先頭にスペースをつける(引数指定なしの場合スペースをつけない)
//--------------------------------------------------------------------------------------------------
function CreateClass(classData,allTabSpace)
{
  var classString = '';

  allTabSpace = (typeof allTabSpace != 'undefined') ?  allTabSpace : '';

  // クラス概要を作成
  if(classData.summary !== null)
  {
    classString += allTabSpace + "/// <summary>\n";
    classString += allTabSpace + "/// " + classData.summary + '\n';
    classString += allTabSpace + "/// </summary>\n" ;
  }
  
  // 属性を作成
  if(classData.attribute !== null)
  {
    classString += allTabSpace + '[' + classData.attribute + ']\n';
  }

  // クラスヘッダーを作成
  classString += allTabSpace + classData.accessModifiers + ' class ' + classData.name;
  if(classData.parentName !== null){
    classString += ' : ' + classData.parentName;
  }
  classString += '{\n';

  // クラスに設定されたメンバー情報を作成する
  classData.elementList.forEach(function(element){

    // クラス内に定義するメンバー情報の先頭にスペースを適用する
    var chileTabSpace = allTabSpace +  GetTabSpaceString(1);
    if(element.elementType ==ELEMENT_TYPE.CLASS){
      classString += CreateClass(element,chileTabSpace);
    }else{
      classString += CreateVariable(element,chileTabSpace);
    }
  }); 

  classString += allTabSpace + '}\n'

  return classString;
}

//--------------------------------------------------------------------------------------------------
// 指定した数のタブスペース文を取得する
// @param int num 取得するタブスペースの数
//--------------------------------------------------------------------------------------------------
function GetTabSpaceString(num)
{
    var ret = '';
    var tabSpace = "    ";
    for(var i = 0; i < num; ++i)
    {
        ret += tabSpace;
    }
    
    return ret;
}
