using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    GameObject _shopPanel;

    public int currentlySelectedItem, currentlySelectedItemCost;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player _player = other.GetComponent<Player>();

            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }

            _shopPanel.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        { _shopPanel.SetActive(false); }
    }

    public void SelectItem(int item)
    {
        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(69);
                currentlySelectedItem = 0;
                currentlySelectedItemCost = 200;
                break;

            case 1:
                UIManager.Instance.UpdateShopSelection(-36);
                currentlySelectedItem = 1;
                currentlySelectedItemCost = 400;   

                break;

            case 2:
                UIManager.Instance.UpdateShopSelection(-144);
                currentlySelectedItem = 2;
                currentlySelectedItemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        Player _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player.diamonds >= currentlySelectedItemCost)
        {
            if (currentlySelectedItem == 2)
            { GameManager.Instance.HasKeyToCastle = true; }

            _player.diamonds -= currentlySelectedItemCost;
        }
        else
        {
            Debug.Log("Player don't have enough Diamonds, Shop is closing");
            _shopPanel.SetActive(false);
        }
    }
}
