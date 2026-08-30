using UnityEngine;

[CreateAssetMenu(menuName = "Ships/ShipData")]
public class ShipData : ScriptableObject
{
    public string shipName;
    public Sprite sprite;
    public GameObject laserPrefab;
    public GameObject missilePrefab;
}
