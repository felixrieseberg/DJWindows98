using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    public GameObject[] interactiveElements;
    public int timeTillNewEvent = 45;

    private string[] _interactiveNames;
    private float _lastEvent = 0;
    private int _randomTryCounter = 0;

    // Things we keep around
    private GameObject _startMenu;

    void Start()
    {
        // Populate _interactiveNames
        int length = interactiveElements.Length;
        string[] interactiveNames = new string[length];
        int i = 0;

        foreach (GameObject obj in interactiveElements)
        {
            interactiveNames[i] = obj.name.ToLower();
            i++;
        }
        _interactiveNames = interactiveNames;
    }
    void Update()
    {
        // Let's see if things got boring yet
        if ((Time.time > timeTillNewEvent && _lastEvent == 0) || (Time.time - _lastEvent > timeTillNewEvent))
        {
            StartRandomInteraction();
        }
    }

    public void StartRandomInteraction()
    {
        string randomInteraction = _interactiveNames[Random.Range(0, _interactiveNames.Length)];
        int i = 0;

        // Check if interaction already active
        foreach (GameObject obj in interactiveElements)
        {
            i++;

            if (obj.name.ToLower() == randomInteraction && obj.activeSelf)
            {
                if (_randomTryCounter <= _interactiveNames.Length && i == _interactiveNames.Length)
                {
                    _randomTryCounter++;
                    StartRandomInteraction();
                    break;
                }
            
            }

            if (obj.name.ToLower() == randomInteraction && !obj.activeSelf)
            {
                _randomTryCounter = 0;
                StopInteraction("all");
                StartInteraction(randomInteraction);
                break;
            }
        }
    }

    public void StopInteraction(string target, string except)
    {
        if (target == "all")
        {
            foreach (GameObject obj in interactiveElements)
            {
                if (obj.name.ToLower() != except)
                {
                    StopExtras(obj);
                    obj.SetActive(false);
                }
            }
        } else
        {
            foreach (GameObject obj in interactiveElements)
            {
                if (obj.name == target)
                {
                    StopExtras(obj);
                    obj.SetActive(false);
                }
            }
        }
    }

    public void StopInteraction(string target)
    {
        StopInteraction(target, "");
    }

    public void StopInteraction()
    {
        StopInteraction("all", "");
    }

    public void StartInteraction(string target)
    {
        foreach (GameObject obj in interactiveElements)
        {
            if (obj.name.ToLower() == target)
            {
                _lastEvent = Time.time;
                obj.SetActive(true);
                StartExtras(obj);
                break;
            }
        }
    }

    public void StartExtras(GameObject target)
    {
        switch (target.name.ToLower())
        {
            case "doom":
                Transform doomBlock = target.transform.FindChild("Doom");
                Transform doomWall = target.transform.FindChild("DoomWall");

                if (doomBlock)
                {
                    doomBlock.gameObject.SetActive(true);
                    doomBlock.GetComponent<Rigidbody>().useGravity = true;
                }

                if (doomWall)
                {
                    doomWall.gameObject.SetActive(false);
                }
                break;
            case "flowing screens":
                GameObject startMenu = GameObject.Find("start_menu");
                if (startMenu)
                {
                    _startMenu = startMenu;
                    startMenu.SetActive(false);
                }
                break;
            default:
                break;
        }
    }

    public void StopExtras(GameObject target)
    {
        switch (target.name)
        {
            case "flowing screens":
                if (_startMenu)
                {
                    _startMenu.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
}
