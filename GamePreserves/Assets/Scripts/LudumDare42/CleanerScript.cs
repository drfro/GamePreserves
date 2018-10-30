using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerScript : MonoBehaviour {

    public int timeToDie;

    // Use this for initialization
    void Start()
    {
      
    }

    public GameObject FindClosestGoo()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("GooPile");
        GameObject closest = null;
        float distance = 2.0f;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    // Update is called once per frame
    void Update ()
    {
      GameObject nearGoo = FindClosestGoo();
        if (nearGoo != null)
        {
//            nearGoo.GetComponent<GooScript>().TakeDamage(105);
        }

        timeToDie--;

        if(timeToDie <= 0)
        {
            Destroy(gameObject);
        }
    }
}
