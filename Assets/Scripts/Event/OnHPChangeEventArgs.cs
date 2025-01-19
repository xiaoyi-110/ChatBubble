
using System;
using JetBrains.Annotations;

public class OnHPChangeEventArgs : EventArgs
{
    public static readonly int EventId = typeof(OnHPChangeEventArgs).GetHashCode();
    
    public OnHPChangeEventArgs(int HP, string target)
    {
        this.HP = HP;
        this.target = target;
    }

    public int HP { get; private set; }
    public string target { get; private set; }

    public static OnHPChangeEventArgs Create(int HP, string target)
    {
        return new OnHPChangeEventArgs(HP, target);
    }
}