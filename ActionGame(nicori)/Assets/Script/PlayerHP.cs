using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField, Header("HPÉAÉCÉRÉì")]
    private GameObject playerIcon;

    private Player player;
    private int beforeHP;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        beforeHP = player.GetHP();
        CreateHPIcon();
    }

    private void CreateHPIcon()
    {
        for(int i = 0; i < player.GetHP(); i++)
        {
            GameObject playerHPObj = Instantiate(playerIcon);
            playerHPObj.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShowHPIcon();
    }

    private void ShowHPIcon()
    {
        if (beforeHP == player.GetHP()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();
        for(int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < player.GetHP());
        }
        beforeHP = player.GetHP();
    }
}
