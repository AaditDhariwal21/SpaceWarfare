using System;
using UnityEngine;

public class GreenSpecialController : SpecialController
{

    public GameObject clusterMissilePrefab;
    private float spawnOffsetY = 1f;
    public override void Activate(Transform owner)
    {
        SpawnMissile(owner, +spawnOffsetY);   // top
        SpawnMissile(owner, -spawnOffsetY);   // bottom

        Finish(); 
    }

    void SpawnMissile(Transform owner, float yOffset)
    {
        Vector3 pos = owner.position + Vector3.up * yOffset;
        GameObject m = Instantiate(clusterMissilePrefab, pos, owner.rotation);
    }
}