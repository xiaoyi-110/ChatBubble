
using System;

public class OnLevelPauseChangeEventArgs : EventArgs
{
    public static readonly int EventId = typeof(OnLevelPauseChangeEventArgs).GetHashCode();
    
    public OnLevelPauseChangeEventArgs()
    {
        IsPause = false;
    }

    public bool IsPause { get; private set; }

    public static OnLevelPauseChangeEventArgs Create(bool isPause)
    {
        OnLevelPauseChangeEventArgs args = new OnLevelPauseChangeEventArgs();
        args.IsPause = isPause;
        return args;
    }

}