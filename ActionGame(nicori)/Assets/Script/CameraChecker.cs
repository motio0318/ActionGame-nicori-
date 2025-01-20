using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChecker : MonoBehaviour
{
    private enum Mode
    {
        None,
        Render,
        RenderOut,
    }

    private Mode mode;

    // Start is called before the first frame update
    void Start()
    {
        mode = Mode.None;
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
    }
    private void OnWillRenderObject()
    {
        if(Camera.current.name == "Main Camera")
        {
            mode = Mode.Render;

        }
    }

    private void Dead()
    {
        if(mode == Mode.RenderOut)
        {
            Destroy(gameObject);
        }
        if(mode == Mode.Render)
        {
            mode = Mode.RenderOut;
        }
    }
}
