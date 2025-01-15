using UnityEngine;

public class UIForm : MonoBehaviour
{

    public void Open(object userData=null)
    {
        OnOpen(userData);
    }
    public void Close(object userData=null)
    {
        OnClose(userData);
    }

    protected virtual void OnOpen(object userData) { }
    protected virtual void OnClose(object userData) { }

    protected virtual void OnResume(object userData) { }
    protected virtual void OnPause(object userData) { }

    protected virtual void OnCover(object userData) { }
    protected virtual void OnReveal(object userData) { }
    
    protected virtual void OnUpdate(object userData) { }

}