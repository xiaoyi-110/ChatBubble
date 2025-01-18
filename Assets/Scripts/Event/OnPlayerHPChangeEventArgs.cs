
using System;
using JetBrains.Annotations;

public class OnPlayerHPChangeEventArgs : EventArgs
{
    public static readonly int EventId = typeof(OnPlayerHPChangeEventArgs).GetHashCode();
    
    public OnPlayerHPChangeEventArgs(int HP)
    {
        this.HP = HP;
    }

    public int HP { get; private set; }

    public static OnPlayerHPChangeEventArgs Create(int HP)
    {
        return new OnPlayerHPChangeEventArgs(HP);
    }
}