using System.Collections;
using UnityEngine;

public class WeaponMelee : Weapon
{
    public Transform hitSpot;
    public float range = 1;
    public Transform gfx;

    protected override void OnActivate()
    {
        StartCoroutine(Animate());

        Collider[] hitEnemies = Physics.OverlapSphere(hitSpot.position, range);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Health h = enemy.GetComponent<Health>();
                if (h != null)
                {
                    OnHit(h);
                }
            }
        }
    }

    IEnumerator Animate()
    {
        gfx.localRotation = Quaternion.Euler(0, 0, -70);

        yield return new WaitForSeconds(maxCooldownTime * 0.9f);

        gfx.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitSpot.position, range);
    }
}
