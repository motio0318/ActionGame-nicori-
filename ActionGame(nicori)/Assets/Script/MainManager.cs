using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバー")]
    private GameObject gameOverUI;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ShowGameOverUI();
    }

    private void ShowGameOverUI()
    {
        if (player != null) return;
        gameOverUI.SetActive(true);
    }
}
