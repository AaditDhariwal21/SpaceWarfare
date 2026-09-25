using UnityEngine;

[CreateAssetMenu(menuName = "Ships/ShipData")]
public class ShipData : ScriptableObject
{
    public string shipName;
    public Sprite playerSprite;
    public Sprite laserSprite;
    public Sprite missileSprite;
    public Sprite healthBarBorder;
    public Color fillColor;
    public string specialName;
    public SpecialController specialControllerPrefab;

}
