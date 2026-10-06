using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetupController : MonoBehaviour
{
    public GameObject playerPrefab;
    Transform spawnP1;
    Transform spawnP2;

    [SerializeField] private HUDController hud;

    void Awake()
    {
        spawnP1 = GameObject.FindGameObjectWithTag("SpawnP1").transform;
        spawnP2 = GameObject.FindGameObjectWithTag("SpawnP2").transform;
    }

    void Start()
    {
        SetupPlayers(GameManager.gmInstance.GetShipData1(), GameManager.gmInstance.GetShipData2());
    }

    public void SetupPlayers(ShipData shipData1 ,  ShipData shipData2)
    {
        PlayerConfig player1Config = Instantiate(playerPrefab, spawnP1.position, spawnP1.rotation).GetComponent<PlayerConfig>();
        player1Config.SetPlayerNumber(1);
        player1Config.SetShipData(shipData1);
        player1Config.GetComponent<PlayerInput>().SwitchCurrentControlScheme("KeyboardP1",Keyboard.current);
        player1Config.SetShipLayer();
        player1Config.Init();

        PlayerConfig player2Config = Instantiate(playerPrefab, spawnP2.position, spawnP2.rotation).GetComponent<PlayerConfig>();
        player2Config.SetPlayerNumber(2);
        player2Config.SetShipData(shipData2);
        player2Config.GetComponent<PlayerInput>().SwitchCurrentControlScheme("KeyboardP2",Keyboard.current);
        player2Config.SetShipLayer();
        player2Config.Init();

        hud.BindPlayers(player1Config, player2Config);
    }
}
