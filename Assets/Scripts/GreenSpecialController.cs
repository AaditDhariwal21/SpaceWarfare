using UnityEngine;

public class GreenSpecialController : SpecialController
{

    public GameObject clusterMissilePrefab;
    private float spawnOffsetY = 2f;
    public override void Activate(Transform ship)
    {
        SpawnMissile(ship, +spawnOffsetY);   // top
        SpawnMissile(ship, -spawnOffsetY);   // bottom

        Finish(); 
    }

    void SpawnMissile(Transform ship, float yOffset)
    {
        Vector3 pos = ship.position + Vector3.up * yOffset;
        GameObject m = Instantiate(clusterMissilePrefab, pos, ship.rotation);
        m.layer = LayerMask.NameToLayer("P" + ship.GetComponent<PlayerConfig>().GetPlayerNumber()+"special");
        m.GetComponent<ClusterMissile>().LaunchMissile(m.transform.up);
    }
}