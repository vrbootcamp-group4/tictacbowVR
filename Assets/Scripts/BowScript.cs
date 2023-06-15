using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public GameManager gameManager;
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {

        }
    }
}
