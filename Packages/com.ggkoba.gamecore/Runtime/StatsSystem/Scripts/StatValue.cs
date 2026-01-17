using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Изменяемый параметр с базовым значением и списком модификаторов.
/// </summary>
[Serializable]
public class StatValue
{
    public float BaseValue;             // Базовое значение параметра
    public StatTarget Target;           // Целевой параметр, который модифицируется

    private List<StatModifier> _modifiers = new();

    public IReadOnlyList<StatModifier> Modifiers => _modifiers;

    public StatValue(StatTarget target, float baseValue)
    {
        Target = target;
        BaseValue = baseValue;
    }

    public void AddModifier(StatModifier modifier)
    {
        if (modifier != null && modifier.Target == Target)
            _modifiers.Add(modifier);
    }

    public void RemoveModifier(string id)
    {
        _modifiers.RemoveAll(m => m.Id == id);
    }

    public void RemoveModifiersBySource(string sourceId)
    {
        _modifiers.RemoveAll(m => m.SourceId == sourceId);
    }

    public void ClearModifiers()
    {
        _modifiers.Clear();
    }

    /// <summary>
    /// Возвращает итоговое значение с учётом Flat и Percent модификаторов.
    /// </summary>
    public float GetFinalValue()
    {
        float flatBonus = 0f;
        float percentBonus = 0f;

        foreach (var mod in _modifiers)
        {
            if (mod.Type == StatModifierType.Flat)
                flatBonus += mod.Value;
            else if (mod.Type == StatModifierType.Percent)
                percentBonus += mod.Value;
        }

        float basePlusFlat = BaseValue + flatBonus;
        return basePlusFlat * (1 + percentBonus / 100f);
    }

    public List<StatModifier> GetSpecialModifiers()
    {
        return _modifiers.Where(m => m.Type == StatModifierType.Special).ToList();
    }
}