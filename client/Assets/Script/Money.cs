using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{

    public const int maxHealth = 100;
    public bool destroyOnDeath;

    public int currentHealth = maxHealth;
    public bool isEnemy = false;

     public Text money;
  
    private bool isLocalPlayer;

    // Use this for initialization
    void Start()
    {
 
        if (!isEnemy)
        {
            money = money.GetComponent<Text>();
            money.text = currentHealth.ToString();
        }
      
        PlayerController pc = GetComponent<PlayerController>();
        isLocalPlayer = pc.isLocalPlayer;
    }

    public void TakeDamage(string playerFrom, int amount)
    {
        currentHealth -= amount;
        OnChangeHealth();
        NetworkManager n = NetworkManager.instance.GetComponent<NetworkManager>();
        n.CommandHealthChange(playerFrom, this.gameObject, amount, isEnemy);
    }
    
    public void OnChangeHealth()
    {
        if (!isEnemy)
        {
            money.text = currentHealth.ToString();
        }
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                print(gameObject.name);
                Destroy(gameObject);
            }
            else
            {

                //  healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
                //   Respawn();
                
            }
        }
    } 

    void Respawn()
    {
        if (isLocalPlayer)
        {
            print("RE");
            /*
            Vector3 spawnPoint = Vector3.zero;
            Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);
            transform.position = spawnPoint;
            transform.rotation = spawnRotation;*/
        }
    }
}
