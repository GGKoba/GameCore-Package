using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Реестр всех доступных StatEffect. Хранит шаблоны эффектов и позволяет создавать их копии с SourceId.
/// Instant - Мгновенный эффект, применяется один раз и сразу исчезает.
/// PerMinute - Повторяющийся эффект — применяется каждую игровую минуту.
/// Permanent - Постоянный эффект, действует всё время, пока не удалён вручную.
/// </summary>
public static class StatEffectRegistry
{
    // Все шаблоны эффектов. Хранятся без SourceId.
    private static readonly Dictionary<string, StatEffect> effects = new()
    {
        {
            "event_campfire_story",
            new StatEffect
            {
                Id = "event_campfire_story",
                DurationType = StatEffectDurationType.Permanent,
                RemainingTicks = 0,
                AllowMultiple = false,
                Modifiers = new List<StatModifier>
                {
                    StatModifierRegistry.Get("mod_mood_10")
                }
            }
        },
        {
            "event_poisoned_food",
            new StatEffect
            {
                Id = "event_poisoned_food",
                DurationType = StatEffectDurationType.PerMinute,
                RemainingTicks = 5,
                AllowMultiple = false,
                Modifiers = new List<StatModifier>
                {
                    StatModifierRegistry.Get("mod_health_-10"),
                }
            }
        },
        {
            "blessing_of_rest",
            new StatEffect
            {
                Id = "blessing_of_rest",
                DurationType = StatEffectDurationType.Instant,
                RemainingTicks = 0,
                AllowMultiple = false,
                Modifiers = new List<StatModifier>
                {
                    StatModifierRegistry.Get("mod_fatigue_-5")
                }
            }
        }
    };

    /// <summary>
    /// Создаёт копию эффекта по его ID и задаёт SourceId для всех его модификаторов.
    /// </summary>
    public static StatEffect CreateEffect(string effectId, string sourceId)
    {
        if (!effects.TryGetValue(effectId, out var template))
        {
            Debug.LogWarning($"[StatEffectRegistry] Эффект с ID '{effectId}' не найден.");
            return null;
        }

        var cloned = new StatEffect
        {
            Id = template.Id,
            SourceId = sourceId,
            DurationType = template.DurationType,
            RemainingTicks = template.RemainingTicks,
            AllowMultiple = template.AllowMultiple,
            Modifiers = new List<StatModifier>()
        };

        foreach (var mod in template.Modifiers)
        {
            var clone = new StatModifier
            {
                Id = mod.Id,
                Target = mod.Target,
                Type = mod.Type,
                Value = mod.Value,
                KeyDescription = mod.KeyDescription,
                SourceId = sourceId
            };
            cloned.Modifiers.Add(clone);
        }

        return cloned;
    }

    /// <summary>
    /// Возвращает список всех ID зарегистрированных эффектов.
    /// </summary>
    public static IEnumerable<string> GetAllEffectIds() => effects.Keys;
}
