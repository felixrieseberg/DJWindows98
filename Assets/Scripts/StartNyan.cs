using UnityEngine;
using System.Collections;

public class StartNyan : MonoBehaviour {

    public GameObject eventManager;
    public GameObject nyanWall;
    public GameObject doomBox;

    private Vector3 _startPosition;
    private bool _ran = false;

    void Start()
    {
        _startPosition = this.transform.position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Start Nyan Windows Flag
        if (collision.collider.name == "HandRight" || collision.collider.name == "HandLeft" && _ran == false)
        {
            nyanWall.SetActive(true);
            this.GetComponent<Rigidbody>().AddForce(new Vector3(150, 50, 0));
            _ran = true;

            // Disable All Other Crazy Elements
            if (eventManager)
            {
                eventManager.GetComponent<EventManager>().StopInteraction("all", "nyan");
            }
        }
    }
}
