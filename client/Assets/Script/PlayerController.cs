using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public bool isLocalPlayer = false;

    Vector3 oldPosition;
    Vector3 currentPosition;
    Quaternion oldRotation;
    Quaternion currentRotation;

    Vector3 lookAt;

    public  Transform Gun;
    public GameObject Reload;
    float fishgo ;
    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            Reload.SetActive(false);
        }
       
        oldPosition = transform.position;
        currentPosition = oldPosition;
        oldRotation = transform.rotation;
        currentRotation = oldRotation;
    
    }
    public void ReLoads()
    {
        GetComponent<Money>().TakeDamage(transform.name, -100);
       
    }

    // Update is called once per frame
    void Update()
    {
        if ( isLocalPlayer)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (GetComponent<Money>().currentHealth <= 0)
                {
                    Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "Re")
                        {
                            print("hitREE");
                            Reload.SetActive(false);
                            ReLoads();
                            return;
                        }
                    }

                 
                     
                Reload.SetActive(true);
                    return;
                }
                    


                print("Click");

                Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 direction = (dir - (Vector2)Gun.transform.position).normalized;
                Gun.transform.up = direction;

                //shoot
                GetComponent<Money>().TakeDamage(transform.name, 5);
                NetworkManager.instance.GetComponent<NetworkManager>().CommandTurn(Gun.transform.rotation);

                NetworkManager n = NetworkManager.instance.GetComponent<NetworkManager>();
                n.CommandShoot(dir);


            }

            currentPosition = transform.position;
            currentRotation = transform.rotation;

            if (currentPosition != oldPosition)
            {
                NetworkManager.instance.GetComponent<NetworkManager>().CommandMove(transform.position);
                oldPosition = currentPosition;
            }

            if (currentRotation != oldRotation)
            {
                NetworkManager.instance.GetComponent<NetworkManager>().CommandTurn(transform.rotation);
                oldRotation = currentRotation;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                GameObject[] f = GameObject.FindGameObjectsWithTag("fish");
                print(f.Length);
                f = null;
            }
        }
       /* else
        {
            if (transform.position.x >= 12)
            {

            }
            else if (transform.position.x >= 12)
            {

            }
            transform.position += -transform.up * Time.deltaTime * 2;
        }*/


    }
   
    public void CmdFire(float Topoint_x, float Topoint_y, float Topoint_z)
    {
        var bullet = Instantiate(bulletPrefab,
                                 bulletSpawn.position,
                                 bulletSpawn.rotation) as GameObject;

        Bullet b = bullet.GetComponent<Bullet>();
        b.playerFrom = this.gameObject;
        b.topoint = new Vector3(Topoint_x, Topoint_y, bulletSpawn.position.z);
 
    }
}
