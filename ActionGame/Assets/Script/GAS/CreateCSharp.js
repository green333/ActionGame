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
//  作成するCSharpの詳細を設定する
//--------------------------------------------------------------------------------------------------
function CSharp()
{
  this.usingList    = [];
  this.elementList  = [];
}

//--------------------------------------------------------------------------------------------------
// 作成するクラス
//--------------------------------------------------------------------------------------------------
CSharp.prototype.AddElement = function(element)
{
  this.elementList.push(element);
}
//--------------------------------------------------------------------------------------------------
// 設定された情報をもとに変数を作成する
//--------------------------------------------------------------------------------------------------
CSharp.prototype.AddUsing = function(using)
{
  this.usingList.push(using);
}

//--------------------------------------------------------------------------------------------------
// 変数データ
// @param string  accessModifiers 変数のアクセスレベル(ACCESS_MODIFIRESで設定する)
// @param string  type            変数の型(VARIABLE_TYPEで設定する)
// @param string  name            変数名
// @param string  summary         変数の概要(nullの場合はなし)
// @param string  attribute       属性(nullの場合はなし)
// @param int     tabSpaceNum     先頭につけるタブスペースの数(指定なしだと0)
//--------------------------------------------------------------------------------------------------
function Variable(accessModifiers,type,name,summary,attribute,tabSpaceNum){

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

  // 先頭につくタブスペース
  this.tabSpace =  GetTabSpaceString(typeof tabSpaceNum !== 'undefined' ? tabSpaceNum : 0); 

  // 自身の属性
  this.elementType = ELEMENT_TYPE.VARIABLE;
}

//--------------------------------------------------------------------------------------------------
// クラスデータ
// @param string  accessModifiers クラスのアクセスレベル(ACCESS_MODIFIRESで設定する)
// @param string  name            クラス名
// @param string  parentName      親クラス(継承しない場合はnull)
// @param string  summary         クラスの概要(nullの場合はなし)
// @param string  attribute       属性(nullの場合はなし)
// @param int     tabSpaceNum     先頭につけるタブスペースの数(指定なしだと0)
//--------------------------------------------------------------------------------------------------
function Class(accessModifiers,name,parentName,summary,attribute,tabSpaceNum)
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

  // 先頭につくタブスペース
  this.tabSpace =  GetTabSpaceString(typeof tabSpaceNum !== 'undefined' ? tabSpaceNum : 0); 

  // 子情報
  this.elementList = [];
  
  // 自身の属性
  this.elementType = ELEMENT_TYPE.CLASS;
}

//--------------------------------------------------------------------------------------------------
// 設定された情報をもとに変数を作成する
//--------------------------------------------------------------------------------------------------
function CreateCSharpString(csharpInfo)
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
  csharpInfo.elementList.forEach(function(element){
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

  return csharpStr;
}

//--------------------------------------------------------------------------------------------------
// 設定された情報をもとに変数を作成する
//--------------------------------------------------------------------------------------------------
function CreateVariable(vriableData)
{
  var variableString = '';

  variableString += vriableData.tabSpace + vriableData.accessModifiers + ' ' + vriableData.type + ' ' + vriableData.name + ';';
  
  if(vriableData.summary != null){
    variableString += vriableData.tabSpace + '/// <summary> ' + vriableData.summary + ' </summary>';
  }

  variableString += '\n';
  return variableString;
}

//--------------------------------------------------------------------------------------------------
// 設定された情報をもとにクラスを作成する
//--------------------------------------------------------------------------------------------------
function CreateClass(classData)
{
  var classString = '';

  // クラス概要を作成
  if(classData.summary !== null)
  {
    classString += classData.tabSpace + "/// <summary>\n";
    classString += classData.tabSpace + "/// " + classData.summary + '\n';
    classString += classData.tabSpace + "/// </summary>\n" ;
  }
  
  // 属性を作成
  if(classData.attribute !== null)
  {
    classString += classData.tabSpace + '[' + classData.attribute + ']\n';
  }

  // クラスヘッダーを作成
  classString += classData.tabSpace + classData.accessModifiers + ' class ' + classData.name;
  if(classData.parentName !== null){
    classString += ' : ' + classData.parentName;
  }
  classString += '{\n';


  classData.elementList.forEach(function(element){

    // 自身に設定されたタブスペースを子に影響させる
    element.tabSpace += classData.tabSpace;
    if(element.elementType ==ELEMENT_TYPE.CLASS){
      classString += CreateClass(element);
    }else{
      classString += CreateVariable(element);
    }
  }); 

  classString += classData.tabSpace + '}\n'

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
