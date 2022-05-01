using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject takeFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
            print(PlayerPrefs.GetInt("Coins") + " coins");
            Instantiate(takeFX, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySound("Coin collect");
            Destroy(gameObject);
        }
    }
}
