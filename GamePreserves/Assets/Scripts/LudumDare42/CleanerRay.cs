using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CleanerRay : MonoBehaviour {

    public void doThingToGoo(GameObject goo)
    {
        //this assumes that things that can run into this thing has been limited by the collision matrix
        //you could get more intricate with like, goo hp and stuff, but I won't
        Destroy(goo);
        Debug.Log("Poof!");

    }
}
