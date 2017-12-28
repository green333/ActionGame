using UnityEngine;
using System.Collections;

public class SceneMain : MonoBehaviour
{
    private GameObject playerPrefab;
    private Player player;

    private void Awake()
    {
        // 各マスタの読み込みを行う。
        bool isMasterLoadSuccess = false;
        do
        {
            if (!LoadEnemyBaseMaster.Instance.Init())
            {
                break;
            }
            if (!LoadEnemySpawnMaster.Instance.Init())
            {
                break;
            }
            if (!LoadEnemyGrowthMaster.Instance.Init())
            {
                break;
            }
            if (!LoadPlayerBaseMaster.Instance.Init())
            {
                break;
            }
            if(!LoadItemMaster.Instance.Init())
            {
                break;
            }

            isMasterLoadSuccess = true;
        } while (false);
       
        // マスタデータの読み込みに失敗した場合
        if(!isMasterLoadSuccess)
        {
            // TODO:タイトル画面に戻すとかの処理が必要
        }
        playerPrefab = Resources.Load<GameObject>("Prefab/unitychan");
    }

    // Use this for initialization
    void Start ()
    {
        gameObject.AddComponent<PlayerCamera>();
        player = Instantiate(playerPrefab).GetComponent<Player>();
        player.Init();
    }

    private void FixedUpdate()
    {
        player.VelocityUpdate();
    }

    // Update is called once per frame
    void Update ()
    {
        player.PlayerUpdate();
	}
}
