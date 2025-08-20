using System.Collections.Generic;
using UnityEngine;

public static class EntityRegistry
{
    private static Dictionary<EntityType, Entity> entities = new Dictionary<EntityType, Entity>();

    public static void Register(EntityType type, Entity entity)
    {
        if (!entities.ContainsKey(type))
            entities.Add(type, entity);
        else
            entities[type] = entity; 
    }

    public static T Get<T>(EntityType type) where T : Entity
    {
        if (entities.TryGetValue(type, out var entity))
            return entity as T;
        Debug.LogError($"Entity of type {type} not found in registry!");
        return null;
    }
}
