using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string csvPath = Application.dataPath+"/CSV/Test.csv";
        List<string> strList = new List<string>(CSVLoader.Instance.LoadCSVAll(csvPath));
        //  確認
        foreach(string list in strList)
        {
            Debug.Log(list);
        }

        Debug.Log("-------------------------------------------");

        string csvPath2 = Application.dataPath + "/CSV/Test02.csv";
        List<string> strList2 = new List<string>(CSVLoader.Instance.LoadCSVExceptHeader(csvPath2, 1));
        //  確認
        foreach (string list in strList2)
        {
            Debug.Log(list);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
