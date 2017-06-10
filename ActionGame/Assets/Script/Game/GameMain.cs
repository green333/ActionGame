﻿using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour
{
    /// <summary> プレイヤー </summary>
    private BaseBehaviour player;
    /// <summary> エネミー </summary>
    private BaseBehaviour enemy;

    void Awake()
    {
        player = gameObject.AddComponent<Player>();
        enemy = gameObject.AddComponent<Enemy>();
    }

	// Use this for initialization
	void Start ()
    {
        player.BaseStart();
        enemy.BaseStart();
	}
	
	// Update is called once per frame
	void Update ()
    {
        player.BaseUpdate();
	}

    /// <summary>
    /// 固定フレーム更新
    /// 物理系はここで書く
    /// </summary>
    void FixedUpdate()
    {
        player.BaseFixedUpdate();
    }
}
