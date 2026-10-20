using System.Collections;
using UnityEngine;

public class RedSpecialController : SpecialController
{
   public GameObject meteorPrefab;
   public override void Activate(Transform ship)
    {
        StartCoroutine(SpawnMeteors(ship));
    }

    IEnumerator SpawnMeteors(Transform ship)
    {   
       float posX = ship.position.x < 0 ? ship.position.x + 2f : ship.position.x - 2f;

        for (int i = 0; i < 6; i++)
        {
            Vector3 spawn = new Vector3(posX, Random.Range(-4f, 4f), 0f);
            GameObject m = Instantiate(meteorPrefab, spawn, ship.rotation);
            m.layer = LayerMask.NameToLayer("P" + ship.GetComponent<PlayerConfig>().GetPlayerNumber()+"special");

            yield return new WaitForSeconds(0.3f);  

            if (m != null) m.GetComponent<Projectile>().Launch(ship.up);

            if (i < 5) yield return new WaitForSeconds(0.7f);
        }

        Finish();
    }
}
