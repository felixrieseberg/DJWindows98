using UnityEngine;
using System.Collections;

public class StartDoom : MonoBehaviour
{
    public GameObject eventManager;
    public GameObject doomWall;
    public Vector3 moveTarget;
    public bool move;

    private bool _ran = false;

    public void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, 10 * Time.deltaTime);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Start Doom Wall
        if (collision.collider.name == "HandRight" || collision.collider.name == "HandLeft" && _ran == false)
        {
            doomWall.SetActive(true);
            this.GetComponent<Rigidbody>().AddForce(new Vector3(150, 50, 0));
            _ran = true;

            // Disable All Other Crazy Elements
            if (eventManager)
            {
                eventManager.GetComponent<EventManager>().StopInteraction("all", "doom");
            }
        }
    }

    public void MoveIntoPosition(Vector3 target)
    {
        moveTarget = target;
        move = true;
    }
}
