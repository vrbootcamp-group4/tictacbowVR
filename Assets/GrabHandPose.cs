using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabHandPose : MonoBehaviour
{
    public HandDataComponent leftHandPose;
    bool setPose;
    private HandDataComponent handData;
    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;

    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;
    // Start is called before the first frame update
    void Start() 
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        
        grabInteractable.selectEntered.AddListener(SetupPose);

        leftHandPose.gameObject.SetActive(false);
    }

    void Update()
    {
        if (setPose && handData != null)
        {
            handData.anim.enabled = false;
            SetHandDataValues(handData, leftHandPose);
            SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations);
        }
    }

    public void SetupPose(BaseInteractionEventArgs arg) 
    {
        if (arg.interactorObject is XRDirectInteractor) 
        {
            handData = arg.interactorObject.transform.GetComponent<HandDataComponent>();
            
            setPose = true;
        }
    }

    public void SetHandDataValues(HandDataComponent h1, HandDataComponent h2)
    {
        startingHandPosition = h1.root.localPosition;
        finalHandPosition = h2.root.localPosition;

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingies.Length];
        finalFingerRotations = new Quaternion[h2.fingies.Length];

        for (int i = 0; i < h1.fingies.Length; i++)
        {
            startingFingerRotations[i] = h1.fingies[i].localRotation;
            finalFingerRotations[i] = h2.fingies[i].localRotation;
        }
    }

    public void SetHandData(HandDataComponent h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation) 
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++) 
        {
            h.fingies[i].localRotation = newBonesRotation[i];
        }
    }
}
