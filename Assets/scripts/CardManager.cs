using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    private HashSet<CardEffectType> activeEffects = new HashSet<CardEffectType>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void ApplyEffect(CardEffectType effect)
    {
        activeEffects.Add(effect);
    }

    public void RemoveEffect(CardEffectType effect)
    {
        activeEffects.Remove(effect);
    }

    public bool HasEffect(CardEffectType effect)
    {
        return activeEffects.Contains(effect);
    }

    public void ClearAllEffects()
    {
        activeEffects.Clear();
    }
}
