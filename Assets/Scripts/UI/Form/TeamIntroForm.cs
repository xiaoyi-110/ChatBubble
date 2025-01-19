using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TeamIntroForm : UIForm
{

    private bool m_IsTryExit => Input.anyKey;


    

    private void Update() {
        if(m_IsTryExit) {
            UIManager.Instance.ShowUIForm("MenuForm");
        }
    }



}
