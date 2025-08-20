using System;

public class LevelResultEventArgs : EventArgs
{
    public static readonly int EventId = typeof(LevelResultEventArgs).GetHashCode();

    public LevelResultData Data { get; private set; }
    public bool Show { get; private set; }
    public LevelResultEventArgs(LevelResultData data, bool show)
    {
        Data = data;
        Show = show;
    }
}
