using System;

/// <summary>
/// Представляет модификатор одного из параметров.
/// </summary>
[Serializable]
public class StatModifier
{
    public string Id;              // Уникальный ID модификатора (например: "mod_crafting_1")
    public string SourceId;        // Источник модификатора (например: предмет)
    public StatTarget Target;      // Целевой параметр, на который влияет модификатор
    public StatModifierType Type;  // Тип модификатора: Flat, Percent, Special
    public float Value;            // Значение модификатора
    public string KeyDescription;  // Ключ для локализации описания
}