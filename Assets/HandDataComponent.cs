using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDataComponent : MonoBehaviour
{
    public enum HandModelType {Left, Right}

    public HandModelType handType;
    public Transform root;
    public Animator anim;
    public Transform[] fingies;
}
