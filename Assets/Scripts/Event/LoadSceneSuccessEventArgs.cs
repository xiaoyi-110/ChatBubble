
using System;

public class LoadSceneSuccessEventArgs : EventArgs
{
    public static readonly int EventId = typeof(LoadSceneSuccessEventArgs).GetHashCode();
    
    public LoadSceneSuccessEventArgs()
    {
        SceneName = null;
        UserData = null;
    }

    public string SceneName { get; private set; }
    public Object UserData { get; private set; }

    public static LoadSceneSuccessEventArgs Create(string sceneName, Object userData)
    {
        LoadSceneSuccessEventArgs args = new LoadSceneSuccessEventArgs();
        args.SceneName = sceneName;
        args.UserData = userData;
        return args;
    }

}