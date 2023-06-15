using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrackCollisions : MonoBehaviour
{
    // Start is called before the first frame update

    public Board board;
    public int value;
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            board.UpdateOwner(value);
        }
    }
}
