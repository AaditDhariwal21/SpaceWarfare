using System.Collections;
using UnityEngine;
public class RedSpecialController : SpecialController
{
   public GameObject meteorPrefab;
   [SerializeField] float shiftSpeed = 15f;
   public override void Activate(Transform ship)
    {
        StartCoroutine(SpawnMeteors(ship));
    }

    IEnumerator SpawnMeteors(Transform ship)
    {   
       float posX = ship.position.x < 0 ? ship.position.x + 2f : ship.position.x - 2f;

        // GameObject vM = Instantiate(meteorPrefab, new Vector2(ship.position.x,ship.position.y + 1f), Quaternion.identity);
        // GameObject vM2 = Instantiate(meteorPrefab, new Vector2(ship.position.x,ship.position.y - 1f), Quaternion.identity);

        // vM.GetComponent<Collider2D>().enabled = false;
        // vM2.GetComponent<Collider2D>().enabled = false;
        
        // StartCoroutine(RevolveAround(vM, ship, 1f, 360f, 90f));
        // StartCoroutine(RevolveAround(vM2, ship, 1f, 360f, 270f));


        for (int i = 0; i < 15; i++)
        {
            GameObject m = Instantiate(meteorPrefab, ship.position, ship.rotation);
            Vector2 randomPos = new Vector2(posX, Random.Range(-4f, 4f));
            Rigidbody2D rb = m.GetComponent<Rigidbody2D>();

            m.layer = LayerMask.NameToLayer("P" + ship.GetComponent<PlayerConfig>().GetPlayerNumber()+"special");
            
            while (m != null && rb.position != randomPos)
            {
                rb.MovePosition(Vector2.MoveTowards(rb.position, randomPos, shiftSpeed * Time.fixedDeltaTime));
                yield return new WaitForFixedUpdate();
            }
            
             
            if (m != null) m.GetComponent<Projectile>().Launch(ship.up);
            if (i < 14) yield return new WaitForSeconds(0.25f);
        }

        // Destroy(vM);
        // Destroy(vM2);
        Finish();
    }

    // IEnumerator RevolveAround(GameObject obj, Transform center, float radius, float speed, float angle)
    // {
    //     Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

    //     while (true)
    //     {
    //         angle += speed * Time.fixedDeltaTime;
    //         float rad = angle * Mathf.Deg2Rad;

    //         Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
    //         rb.MovePosition((Vector2)center.position + offset);

    //         yield return new WaitForFixedUpdate();
    //     }
    // }
}
