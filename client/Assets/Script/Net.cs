using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    [HideInInspector]
    public string from;

    bool ishit = false;
    private void Start()
    {
        ishit = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ishit == false)
        {
            var hit = collision.gameObject;
            if (hit.tag == "fish")
            {
                var health = hit.GetComponent<Money>();
                if (health != null)
                {
                    print("hitfff");
                    health.TakeDamage(from, 10);
                }
                ishit = true;
            }
        }
      
       
    }
}
