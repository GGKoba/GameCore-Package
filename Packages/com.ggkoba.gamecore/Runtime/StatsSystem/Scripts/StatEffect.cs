using System;
using System.Collections.Generic;

/// <summary>
/// Представляет активный эффект, влияющий на характеристики персонажа.
/// </summary>
[Serializable]
public class StatEffect
{
    public string Id;                                      // Уникальный ID эффекта (например: "event_campfire_story")
    public string SourceId;                                // Источник (например: ID предмета или события)
    public StatEffectDurationType DurationType;            // Тип действия: мгновенный, постоянный, периодический и т.д.
    public int RemainingTicks;                             // Время в минутах, определяет длительность эффекта
    public bool AllowMultiple;                             // Можно ли применять несколько раз
    public List<StatModifier> Modifiers = new();           // Модификаторы, применяемые эффектом
}