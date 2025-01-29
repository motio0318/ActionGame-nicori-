using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバー")]
    private GameObject gameOverUI;
    [SerializeField, Header("ゲームクリア")]
    private GameObject gameClearUI;

    private GameObject player;
    private bool bShowUI;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        bShowUI = false;
        FindObjectOfType<Player>().enabled = false;
        foreach(EnemySpawner enemySpawner in FindObjectsOfType<EnemySpawner>())
        {
            enemySpawner.enabled = false;
        }
    }

    private void MainStart()
    {
        player.GetComponent<Player>().enabled = true;
        foreach(EnemySpawner enemySpawner in FindObjectsOfType<EnemySpawner>())
        {
            enemySpawner.enabled = true;
        }
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
        bShowUI = true;
    }

    public void ShowGameClearUI()
    {
        gameClearUI.SetActive(true);
        bShowUI = true;
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        if (!bShowUI || !context.performed) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
