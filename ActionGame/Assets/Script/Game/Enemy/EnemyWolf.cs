using UnityEngine;
using System.Collections;

public class EnemyWolf : Enemy {


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
        // TODO:仮削除処理
        if(IsDead())
        {
            Destroy(gameObject);
        }
    }
}
