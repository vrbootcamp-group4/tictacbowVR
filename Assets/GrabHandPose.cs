using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabHandPose : MonoBehaviour
{
    public HandDataComponent leftHandPose;
    // Start is called before the first frame update
    void Start() 
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        
        grabInteractable.selectEntered.AddListener(SetupPose);

        leftHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg) 
    {
        if (arg.interactorObject is XRDirectInteractor) 
        {
            HandDataComponent handData = arg.interactorObject.transform.GetComponentInChildren<HandDataComponent>();
            
            Debug.Log(handData == null);
            handData.anim.enabled = false;
        }
    }
}
