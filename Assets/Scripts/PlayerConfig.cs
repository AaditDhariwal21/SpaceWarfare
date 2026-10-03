using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    private int playerNumber;
    private ShipData shipData;

    public void SetPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

    public ShipData GetShipData()
    {
        return shipData;    
    }

    public void SetShipData(ShipData shipData)
    {
        this.shipData = shipData;
    }

    public void Init()
    {
        GetComponent<SpriteRenderer>().sprite = shipData.playerSprite;
        gameObject.tag = "Player" + playerNumber;
        //and HUD Changes
    }

}
