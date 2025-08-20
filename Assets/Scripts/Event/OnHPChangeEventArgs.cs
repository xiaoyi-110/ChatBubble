using System;

public class OnHPChangeEventArgs : EventArgs
{
    public static readonly int EventId = typeof(OnHPChangeEventArgs).GetHashCode();
    
    public OnHPChangeEventArgs(int HP, EntityType target)
    {
        this.HP = HP;
        this.target = target;
    }

    public int HP { get; private set; }
    public EntityType target { get; private set; }

    public static OnHPChangeEventArgs Create(int HP, EntityType target)
    {
        return new OnHPChangeEventArgs(HP, target);
    }
}