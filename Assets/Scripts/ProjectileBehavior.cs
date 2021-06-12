using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject owningPlayer;
    public Rigidbody rigbody;
    public GameObject explosion;
    public float projectileSpeed = 100.0f;

    private void Start()
    {
        gameObject.transform.parent = null;
        rigbody.AddForce(transform.forward * projectileSpeed);
        Physics.IgnoreLayerCollision(9, 8); //FX ignore Player
        Physics.IgnoreLayerCollision(9, 9); //FX ignore FX
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, rigbody.transform);
        StartCoroutine(Killer());
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator Killer()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}
