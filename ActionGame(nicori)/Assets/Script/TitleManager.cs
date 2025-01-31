using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField, Header("���艹")]
    private GameObject SubmitSE;

    private bool bStart;
    private Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        bStart = false;
        fade = FindObjectOfType<Fade>();
        fade.FadeStart(TitleStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TitleStart()
    {
        bStart = true;
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnSpaceClick(InputAction.CallbackContext context)
    {
        if(!context.performed && bStart)
        {
            fade.FadeStart(ChangeScene);
            bStart = false;
            Instantiate(SubmitSE);
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed) return;
        Application.Quit();
    }
}
