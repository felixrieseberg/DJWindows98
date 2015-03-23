using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, Time.deltaTime * 20, 0, Space.World);
    }
}
