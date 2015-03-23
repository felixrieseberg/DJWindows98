using UnityEngine;
using System.Collections;

public class HandsUp : MonoBehaviour
{

    public GameObject handLeft;
    public GameObject handRight;
    public GameObject head;

    public Material[] headMaterials;
    private Material _defaultHeadMaterial;
    private bool switched = false;

    // Update is called once per frame
    void Update()
    {
        if (EnsureObjects())
        {
            float handLeftY = handLeft.transform.position.y;
            float handRightY = handRight.transform.position.y;
            float headY = head.transform.position.y;

            if (headY < handLeftY && headY < handRightY && headMaterials.Length > 0 && switched == false)
            {
                head.GetComponent<Renderer>().material = headMaterials[Random.Range(0, headMaterials.Length)];
                head.transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
                switched = true;
            }
            else if (headY > handLeftY && headY > handRightY)
            {
                head.GetComponent<Renderer>().material = _defaultHeadMaterial;
                head.transform.localScale = new Vector3(3f, 3f, 3f);
                switched = false;
            }
        }
    }

    bool EnsureObjects()
    {
        if (handLeft == null)
        {
            handLeft = transform.FindChild("HandLeft").gameObject;
        }
        if (handRight == null)
        {
            handRight = transform.FindChild("HandRight").gameObject;
        }
        if (head == null)
        {
            head = transform.FindChild("Head").gameObject;
            if (head != null)
            {
                _defaultHeadMaterial = head.GetComponent<Renderer>().material;
            }
        }

        if (handLeft == null || handRight == null || head == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
