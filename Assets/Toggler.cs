using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Toggler : MonoBehaviour
{
    InputDevice left;
    InputDevice right;
    bool isPressed;
    bool visible;
    public GameObject toggle;

    void Start()
    {
        isPressed = false;
        visible = false;
        toggle.transform.localScale = new Vector3(0, 0, 0);
    }


    void Update()
    {
        // needs to be in Update
        right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // left = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        // more https://docs.unity3d.com/ScriptReference/XR.XRNode.html

        // assigns button value to out variable, if expecting Vector3 replace bool
        right.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isPressed);

        // gameObject.SetActive(isPressed);
        if (isPressed && !visible)
        {
            toggle.transform.localScale = new Vector3(1, 1, 1);
            visible = true;
        }
        else if (!isPressed && visible)
        {
            toggle.transform.localScale = new Vector3(0, 0, 0);
            visible = false;
        }
        Debug.Log("isPressed: " + isPressed);
        Debug.Log("visible: " + visible);
    }
}
