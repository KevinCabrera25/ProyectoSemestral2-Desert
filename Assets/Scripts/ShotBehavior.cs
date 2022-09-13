using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour
{

    public Vector3 target;
    public GameObject collisionExplosion;
    public float speed;


    // Update is called once per frame
    void Update()
    {
        // transform.position += transform.forward * Time.deltaTime * 300f;// The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        if (target != null)
        {
            if (transform.position == target)
            {
                Explode();
            }
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }

    }

    public void SetTarget(Vector3 actualTarget)
    {
        target = actualTarget;
    }

    void Explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }


    }

}
