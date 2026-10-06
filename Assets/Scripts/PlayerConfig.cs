using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    private int playerNumber;
    private ShipData shipData;

    public void SetPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }


    public ShipData GetShipData()
    {
        return shipData;    
    }

    public void SetShipData(ShipData shipData)
    {
        this.shipData = shipData;
    }

    public void SetShipLayer()
    {
        int layer = LayerMask.NameToLayer("P" + playerNumber + "ship");
        gameObject.layer = layer;

        foreach (var t in GetComponentsInChildren<Transform>(true)) if (t.CompareTag("Shield")) t.gameObject.layer = layer;
    }

    public void Init()
    {
        GetComponent<SpriteRenderer>().sprite = shipData.playerSprite;
        gameObject.tag = "Player" + playerNumber;
        //and HUD Changes
    }

}
