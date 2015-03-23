using UnityEngine;
using System.Collections;

public class ExplodeHighFive : MonoBehaviour {

    public GameObject explosion;

    void Start()
    {
        this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "HandRight")
        {
            GameObject newExplosion = Instantiate(explosion, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(newExplosion, 10);
        }
    }
}
