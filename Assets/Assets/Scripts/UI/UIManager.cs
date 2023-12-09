using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            { Debug.LogError("UI Manager is null"); }
            return _instance;
        }
    }

    [SerializeField]
    Text _playerGemsCount;
    [SerializeField]
    Image _selectionImage;
    [SerializeField]
    Text _diamondCountText;
    [SerializeField]
    Image[] _lifeUnit;

    public void OpenShop(int count)
    {
        _playerGemsCount.text = count + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateDiamondsCount(int count)
    {
        _diamondCountText.text = "" + count;
    }

    public void LifeUpdate(int currentHealth)
    {
        for (int i = 0; i <= currentHealth; i++)
        {
            if (i == currentHealth)
            {
                _lifeUnit[i].enabled = false;
            }
        }
    }

    private void Awake()
    {
        _instance = this;
    }
}
