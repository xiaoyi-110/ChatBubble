using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
public class UIManager : MonoSingleton<UIManager>
{

    [SerializeField]private GameObject m_UIRoot;
    [SerializeField]private Transition m_Transition;

    [Header("Level UI")]
    public GameObject m_PlayerHPBar;

    private void Start() {
        if(m_UIRoot == null)
        {
            Debug.LogError("UI Root is not assigned");
        }
    }

    public void OpenUIForm(string uiFormName, object userData)
    {
        if(HasUIForm(uiFormName))
        {
            Debug.LogWarning(string.Format("UIForm {0} is already opened", uiFormName));
            return;
        }


        string uiFormAssetName = ResoucesUtility.GetUIFormAsset(uiFormName);
        GameObject uiFormPrefab = Resources.Load<GameObject>(uiFormAssetName);
        if (uiFormPrefab == null)
        {
            Debug.LogError($"UIForm prefab not found at path: {uiFormAssetName}");
            return;
        }

        GameObject uiForm = Instantiate(uiFormPrefab);
        uiForm.name = uiFormName;
        uiForm.transform.SetParent(m_UIRoot.transform);
        uiForm.transform.localPosition = Vector3.zero;
        uiForm.transform.localScale = Vector3.one;

    
        uiForm.GetComponent<UIForm>().Open(userData);
    }

    public void CloseUIForm(string uiFormName, object userData)
    {
        Transform uiForm = m_UIRoot.transform.Find(uiFormName);
        if(uiForm != null)
        {
            uiForm.GetComponent<UIForm>().Close(userData);
            Destroy(uiForm.gameObject);
        }
        else
        {
            Debug.LogWarning(string.Format("UIForm {0} is not opened", uiFormName));
        }
    }

    public bool HasUIForm(string uiFormName)
    {
        return m_UIRoot.transform.Find(uiFormName) != null;
    }

    public void StartTransitionFadeIn(ProcedureChangeScene procedureChangeScene)
    { 
        m_Transition.FadeIn(procedureChangeScene);
    }   

    public void StartTransitionFadeOut()
    {
        m_Transition.FadeOut();
    }


    

    
}