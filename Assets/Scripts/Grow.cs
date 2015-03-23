using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour
{
    public float delayed = 0;

    private float currentScale = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > delayed)
        {
            if (currentScale >= 4)
            {
                transform.localScale = new Vector3(1, 1, 1);
                currentScale = 0;
            }

            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            currentScale += 0.1f;
        }
    }
}
