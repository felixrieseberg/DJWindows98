using UnityEngine;
using System.Collections;

public class PulseSize : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        float scale = Mathf.PingPong(Time.time, 1) + 2.5f;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
