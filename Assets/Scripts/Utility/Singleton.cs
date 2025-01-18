
using System;
public class Singleton<T> where T : class
{
    private static T instance = null;
    private static readonly object padlock = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = (T)Activator.CreateInstance(typeof(T), true);
                    }
                }
            }
            return instance;
        }
    }
}