using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualBtn : MonoBehaviour, IVirtualButtonEventHandler {

    /// <summary>
    /// 虛擬按鈕
    /// </summary>
    public GameObject _vButton;

    /// <summary>
    /// 按下與放開的貼圖
    /// </summary>
    public Texture _btnDown, _btnUp;

    /// <summary>
    /// 按鈕的渲染，來改變圖片
    /// </summary>
    public Renderer _btnImageRenderer;

    /// <summary>
    /// 按下事件
    /// </summary>
    /// <param name="vb"></param>
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //throw new NotImplementedException();
        Debug.Log("Pressed");

        // 改變圖片，按下的圖片
        _btnImageRenderer.material.mainTexture = _btnDown;
    }

    /// <summary>
    /// 放開事件
    /// </summary>
    /// <param name="vb"></param>
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        //throw new NotImplementedException();
        Debug.Log("Released");

        // 改變圖片，放開的圖片
        _btnImageRenderer.material.mainTexture = _btnUp;
    }

    // Use this for initialization
    void Start () {

        // 註冊按鈕事件
        _vButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
