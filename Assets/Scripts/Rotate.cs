using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Time.deltaTime, 0, 0);
        transform.Rotate(0, Time.deltaTime, 0, Space.World);
    }
}
