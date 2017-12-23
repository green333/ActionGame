using UnityEngine;
using System.Collections;

public class SceneMain : MonoBehaviour
{
    private GameObject playerPrefab;
    private Player player;

    private void Awake()
    {
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
