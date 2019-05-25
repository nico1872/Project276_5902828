using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public GameObject playerFrom;

   [SerializeField]
    GameObject Net;

    [HideInInspector]
    public Vector3 topoint;
    private void Update()
    {
        if (Vector3.Distance(transform.position, topoint) <= 0.1)
        {
            print("End");
          GameObject net =   Instantiate(Net, transform.position, Quaternion.identity);
            net.GetComponent<Net>().from = playerFrom.name;
            Destroy(net, 1);
            Destroy(gameObject);

        }
        else
        {

            transform.position = Vector3.MoveTowards(transform.position, topoint, 6 * Time.deltaTime);
        }
     
    }
   
  
}
