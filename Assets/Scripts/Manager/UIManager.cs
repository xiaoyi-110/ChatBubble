using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoSingleton<UIManager>
{
    public List<GameObject> UIFormList;

    private void Awake() {
        
    }

    private void Start()
    {

    }

    public void ShowUIForm(string name)
    {
        foreach (var item in UIFormList)
        {
            if (item.name == name)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }

    public void StartTransitionFadeIn(ProcedureChangeScene procedureChangeScene)
    { 
        //m_Transition.FadeIn(procedureChangeScene);
    }   

    public void StartTransitionFadeOut()
    {
        //m_Transition.FadeOut();
    }


    

    
}