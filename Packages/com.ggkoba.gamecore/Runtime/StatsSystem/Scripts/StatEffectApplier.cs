using System;
using System.Collections.Generic;
using UnityEngine;

public static class StatEffectApplier
{
    public static void ApplyEffect(CharacterData character, StatEffect newEffect)
    {
        switch (newEffect.DurationType)
        {
            case StatEffectDurationType.Instant:
                foreach (var mod in newEffect.Modifiers)
                    ApplyInstantModifier(character, mod);
                break;

            case StatEffectDurationType.PerMinute:
                var existing = character.ActiveEffects.Find(e => e.Id == newEffect.Id);

                if (!newEffect.AllowMultiple && existing != null)
                {
                    existing.RemainingTicks = newEffect.RemainingTicks;
                    existing.Modifiers = newEffect.Modifiers;
                }
                else
                {
                    character.ActiveEffects.Add(newEffect);
                }
                break;

            case StatEffectDurationType.Permanent:
                foreach (var mod in newEffect.Modifiers)
                    ApplyPermanentModifier(mod, character.Mood);
                break;
        }
    }

    public static void OnMinutePassed(CharacterData character)
    {
        var toRemove = new List<StatEffect>();

        foreach (var effect in character.ActiveEffects)
        {
            if (effect.DurationType != StatEffectDurationType.PerMinute)
                continue;

            foreach (var mod in effect.Modifiers)
                ApplyTickModifier(character, mod);

            effect.RemainingTicks--;
            if (effect.RemainingTicks <= 0)
                toRemove.Add(effect);
        }

        foreach (var expired in toRemove)
            character.ActiveEffects.Remove(expired);
    }

    public static void ApplyInstantModifier(CharacterData character, StatModifier mod)
    {
        switch (mod.Target)
        {
            case StatTarget.Health:
                ApplyValueModifier(mod, character.Health, character.AddReduceHealth);
                break;
            case StatTarget.Fatigue:
                ApplyValueModifier(mod, character.Fatigue, character.AddReduceFatigue);
                break;
            default:
                Debug.LogWarning($"[Instant] Target '{mod.Target}' не поддерживается.");
                break;
        }
    }

    public static void ApplyTickModifier(CharacterData character, StatModifier mod)
    {
        switch (mod.Target)
        {
            case StatTarget.Health:
                ApplyValueModifier(mod, character.Health, character.AddReduceHealth);
                break;
            case StatTarget.Fatigue:
                ApplyValueModifier(mod, character.Fatigue, character.AddReduceFatigue);
                break;
            default:
                Debug.LogWarning($"[Tick] Target '{mod.Target}' не поддерживается.");
                break;
        }
    }

    public static void ApplyValueModifier(StatModifier mod, int currentValue, Action<int> applyFunc)
    {
        switch (mod.Type)
        {
            case StatModifierType.Flat:
                applyFunc((int)mod.Value);
                break;
            case StatModifierType.Percent:
                int delta = (int)(currentValue * mod.Value / 100f);
                applyFunc(delta);
                break;
            default:
                Debug.LogWarning($"[Value] Тип модификатора '{mod.Type}' не поддерживается.");
                break;
        }
    }

    public static void ApplyPermanentModifier(StatModifier mod, StatValue targetStat)
    {
        switch (mod.Type)
        {
            case StatModifierType.Flat:
            case StatModifierType.Percent:
            case StatModifierType.Special:
                targetStat.AddModifier(mod);
                break;
            default:
                Debug.LogWarning($"[Permanent] Тип модификатора '{mod.Type}' не поддерживается.");
                break;
        }
    }
}