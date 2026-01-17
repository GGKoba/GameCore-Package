using System.Collections.Generic;
using UnityEngine;

public static class StatModifierRegistry
{
    // Реестр всех шаблонных модификаторов
    // Хранит только базовые данные (без SourceId — он задаётся отдельно)
    public static readonly Dictionary<string, StatModifier> Modifiers = new()
    {
        // === ЗДОРОВЬЕ ===
        { "mod_health_5", new StatModifier {
            Id = "mod_health_5",
            Target = StatTarget.Health,
            Type = StatModifierType.Flat,
            Value = 5,
            KeyDescription = "mod.health.plus"
        }},
        { "mod_health_-5", new StatModifier {
            Id = "mod_health_-5",
            Target = StatTarget.Health,
            Type = StatModifierType.Flat,
            Value = -5,
            KeyDescription = "mod.health.minus"
        }},
        { "mod_health_10", new StatModifier {
            Id = "mod_health_10",
            Target = StatTarget.Health,
            Type = StatModifierType.Flat,
            Value = 10,
            KeyDescription = "mod.health.plus"
        }},
        { "mod_health_-10", new StatModifier {
            Id = "mod_health_-10",
            Target = StatTarget.Health,
            Type = StatModifierType.Flat,
            Value = -10,
            KeyDescription = "mod.health.minus"
        }},

        // === УСТАЛОСТЬ ===
        { "mod_fatigue_5", new StatModifier {
            Id = "mod_fatigue_5",
            Target = StatTarget.Fatigue,
            Type = StatModifierType.Flat,
            Value = 5,
            KeyDescription = "mod.fatigue.plus"
        }},
        { "mod_fatigue_-5", new StatModifier {
            Id = "mod_fatigue_-5",
            Target = StatTarget.Fatigue,
            Type = StatModifierType.Flat,
            Value = -5,
            KeyDescription = "mod.fatigue.minus"
        }},
        { "mod_fatigue_10", new StatModifier {
            Id = "mod_fatigue_10",
            Target = StatTarget.Fatigue,
            Type = StatModifierType.Flat,
            Value = 10,
            KeyDescription = "mod.fatigue.plus"
        }},
        { "mod_fatigue_-10", new StatModifier {
            Id = "mod_fatigue_-10",
            Target = StatTarget.Fatigue,
            Type = StatModifierType.Flat,
            Value = -10,
            KeyDescription = "mod.fatigue.minus"
        }},

        // === НАСТРОЕНИЕ ===
        { "mod_mood_10", new StatModifier {
            Id = "mod_mood_10",
            Target = StatTarget.Mood,
            Type = StatModifierType.Flat,
            Value = 10,
            KeyDescription = "mod.mood.plus"
        }},
        { "mod_mood_-10", new StatModifier {
            Id = "mod_mood_-10",
            Target = StatTarget.Mood,
            Type = StatModifierType.Flat,
            Value = -10,
            KeyDescription = "mod.mood.minus"
        }},
    };

    // Возвращает копию модификатора по его ID
    // Копия нужна, чтобы можно было задать SourceId отдельно
    public static StatModifier Get(string id)
    {
        // Пытаемся найти модификатор по заданному ID в словаре
        // Если найден — сохраняем в переменную mod и возвращаем его копию
        // Если не найден — выполнение перейдёт к return null ниже
        if (Modifiers.TryGetValue(id, out var mod))
            return Clone(mod);

        Debug.LogWarning($"[StatModifierRegistry] Модификатор с ID '{id}' не найден.");
        return null;
    }

    // Возвращает копию модификатора по его ID, используется в перменной var mod
    // Копия нужна, чтобы можно было задать SourceId отдельно
    private static StatModifier Clone(StatModifier original)
    {
        return new StatModifier
        {
            Id = original.Id,
            Target = original.Target,
            Type = original.Type,
            Value = original.Value,
            KeyDescription = original.KeyDescription
            // SourceId не копируется — задаётся отдельно
        };
    }

    // Возвращает все ID доступных модификаторов (например, для редактора или отладки)
    public static IEnumerable<string> GetAllIds() => Modifiers.Keys;
}
