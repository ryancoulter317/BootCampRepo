using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionRadius = 5.0f;
    public float explosionForce = 500.0f;

    private void Start()
    {
        gameObject.transform.parent = null;
        Physics.IgnoreLayerCollision(9, 9); //FX ignore FX

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb!=null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 2.0f);
        }
        
        StartCoroutine(Killer());
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator Killer()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
