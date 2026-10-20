using System.Collections;
using UnityEngine;

public class BlueSpecialController : SpecialController
{
    public GameObject cometParticleSystem;
    public GameObject cometPrefab;
    public override void Activate(Transform ship)
    {
        StartCoroutine(SpawnComets(ship));
    }

    IEnumerator SpawnComets(Transform ship)
    {   
       float posX = ship.position.x;

        for (int i = 0; i < 10; i=i+2)
        {
            Vector3 spawn1 = new Vector3(posX, Random.Range(-4f, 0f), 0f);
            Vector3 spawn2 = new Vector3(posX, Random.Range(0f, 4f), 0f);

            GameObject pS1 = Instantiate(cometParticleSystem,spawn1,ship.rotation);
            GameObject pS2 = Instantiate(cometParticleSystem,spawn2,ship.rotation);

            yield return new WaitForSeconds(0.8f);

            Destroy(pS1);
            Destroy(pS2);

            GameObject c1 = Instantiate(cometPrefab, spawn1, ship.rotation);
            GameObject c2 = Instantiate(cometPrefab, spawn2, ship.rotation);

            if(posX > 0)
            {
                c1.GetComponent<Rigidbody2D>().angularVelocity = 1080f;
                c2.GetComponent<Rigidbody2D>().angularVelocity = 1080f;
            }
            else
            {
                c1.GetComponent<Rigidbody2D>().angularVelocity = -1080f;
                c2.GetComponent<Rigidbody2D>().angularVelocity = -1080f;
            }
            
            c1.layer = LayerMask.NameToLayer("P" + ship.GetComponent<PlayerConfig>().GetPlayerNumber()+"special");
            c2.layer = LayerMask.NameToLayer("P" + ship.GetComponent<PlayerConfig>().GetPlayerNumber()+"special");

            if (c1 != null) c1.GetComponent<Projectile>().Launch(ship.up);
            if (c2 != null) c2.GetComponent<Projectile>().Launch(ship.up);
 
            if (i < 9) yield return new WaitForSeconds(0.5f);
        }

        Finish();
    }
}
