using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour
{
    /// <summary> プレイヤー </summary>
    [SerializeField] private Player player;
    /// <summary> エネミー </summary>
    [SerializeField] private Enemy enemy;

	// Use this for initialization
	void Start ()
    {
        player.Init();
        enemy.Init();
	}
	
	// Update is called once per frame
	void Update ()
    {
        player.PlayerUpdate();
	}

    /// <summary>
    /// 固定フレーム更新
    /// 物理系はここで書く
    /// </summary>
    void FixedUpdate()
    {
        player.Move();
        player.Rotate();
    }
}
