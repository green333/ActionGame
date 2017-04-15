using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Unko
{
    public string name;
    public int coin;

    public Unko(string name, int coin)
    {
        this.name = name;
        this.coin = coin;
    }
}

public class ttttt : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefsWrap.Instance.DeleteAll();
        //List<int> tinko = new List<int>();
        //tinko.Add(11);
        //tinko.Add(22);
        //tinko.Add(33);
        //tinko.Add(44);
        //tinko.Add(55);
        //Debug.Log("tinko:" + tinko.Count);
        //PlayerPrefsWrap.Instance.DeleteAll();
        //PlayerPrefsWrap.Instance.SaveGeneric<List<int>>("list", tinko);
        //List<int> intList = new List<int>(PlayerPrefsWrap.Instance.LoadGeneric<List<int>>("list"));
        //Debug.Log(intList.Count);
        //for (int i = 0; i < intList.Count; i++)
        //{
        //    Debug.Log(i.ToString() + "：" + intList[i]);
        //}
        Unko unko = new Unko("名前", 1000);
        PlayerPrefsWrap.Instance.SaveGeneric<Unko>("me", unko);
        Unko tinko = PlayerPrefsWrap.Instance.LoadGeneric<Unko>("me");
        Debug.Log(tinko.name);
        Debug.Log(tinko.coin);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
