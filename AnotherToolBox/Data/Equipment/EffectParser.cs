using System;
using System.Linq;
using AnotherToolBox.Models.Team;

namespace AnotherToolBox.Data.Equipment;

public static class EffectParser
{
    public static StatEffect[] ParseEffect(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return [];
        
        // Split on delimiters: Comma, SemiColon, "AND"
        var effects = input.Split([",", ";", "and", "AND"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var returnEffects = new StatEffect[effects.Length];
        for (int i = 0; i < effects.Length; i++)
        {
            var effect = effects[i];
            var parts = effect.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            // try get value from last part, is either straight number or %
            double value = 0;
            var isMultiplicative = false;
            var isPercentage = false;
            var effectDescription = effect;

            if (parts.Length > 0)
            {
                var lastPart = parts[^1];
                if (lastPart.EndsWith("%"))
                {
                    if (double.TryParse(lastPart.TrimEnd('%'), out value))
                    {
                        isPercentage = true;
                        effectDescription = string.Join(" ", parts[..^1]);
                    }
                }
                else if (double.TryParse(lastPart, out value))
                {
                    effectDescription = string.Join(" ", parts[..^1]);
                }
            }

            // if no number could simply be a description
            if (value == 0 && string.IsNullOrWhiteSpace(effectDescription))
            {
                effectDescription = effect;
            }

            var unitStat = MapToUnitStat(effectDescription);
            var effectType = effectDescription.Contains("decrease", StringComparison.OrdinalIgnoreCase) || 
                           effectDescription.Contains("reduction", StringComparison.OrdinalIgnoreCase) ||
                           effectDescription.Contains("resist", StringComparison.OrdinalIgnoreCase)
                ? StatEffectType.Decrease 
                : StatEffectType.Increase;
            
            var newEffect = new StatEffect()
            {
                Effect = effectType,
                Stat = unitStat,
                Value = value,
                Multiplicative = isMultiplicative,
                Percentage = isPercentage
            };
            
            returnEffects[i] = newEffect;
        }

        return returnEffects;
    }

    private static UnitStat MapToUnitStat(string effectDescription)
    {
        var lower = effectDescription.ToLowerInvariant();

        // Direct stat matches
        if (lower.Contains("hp")) return UnitStat.Hp;
        if (lower.Contains("mp")) return UnitStat.Mp;
        if (lower.Contains("pwr") || lower.Contains("power")) return UnitStat.Pwr;
        if (lower.Contains("int") || lower.Contains("intelligence")) return UnitStat.Int;
        if (lower.Contains("end") || lower.Contains("endurance")) return UnitStat.End;
        if (lower.Contains("spr") || lower.Contains("spirit")) return UnitStat.Spr;
        if (lower.Contains("spd") || lower.Contains("speed")) return UnitStat.Spd;
        if (lower.Contains("lck") || lower.Contains("luck")) return UnitStat.Lck;

        // Attack stats
        if (lower.Contains("physical") && lower.Contains("attack")) return UnitStat.PhysAtk;
        if (lower.Contains("magic") && lower.Contains("attack")) return UnitStat.MagAtk;
        if (lower.Contains("physical") && lower.Contains("def")) return UnitStat.PhysDef;
        if (lower.Contains("magic") && lower.Contains("def")) return UnitStat.MagDef;

        // Critical stats
        if (lower.Contains("physical") && lower.Contains("crit") && (lower.Contains("damage") || lower.Contains("str"))) return UnitStat.PhyCritStr;
        if (lower.Contains("physical") && lower.Contains("crit")) return UnitStat.PhyCrit;
        if (lower.Contains("magic") && lower.Contains("crit") && (lower.Contains("damage") || lower.Contains("str"))) return UnitStat.MagCritStr;
        if (lower.Contains("magic") && lower.Contains("crit")) return UnitStat.MagCrit;
        if (lower.Contains("critical") && lower.Contains("damage")) return UnitStat.PhyCritStr;

        // Elemental attacks
        if (lower.Contains("fire") && lower.Contains("attack")) return UnitStat.FireAtk;
        if (lower.Contains("water") && lower.Contains("attack")) return UnitStat.WaterAtk;
        if (lower.Contains("earth") && lower.Contains("attack")) return UnitStat.EarthAtk;
        if (lower.Contains("wind") && lower.Contains("attack")) return UnitStat.WindAtk;
        if (lower.Contains("thunder") && lower.Contains("attack")) return UnitStat.ThunderAtk;
        if (lower.Contains("shade") && lower.Contains("attack")) return UnitStat.ShadeAtk;
        if (lower.Contains("crystal") && lower.Contains("attack")) return UnitStat.CrystalAtk;
        if (lower.Contains("null") && lower.Contains("attack")) return UnitStat.NullAtk;

        // Elemental resistance
        if (lower.Contains("fire") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.FireRes;
        if (lower.Contains("water") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.WaterRes;
        if (lower.Contains("earth") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.EarthRes;
        if (lower.Contains("wind") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.WindRes;
        if (lower.Contains("thunder") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.ThunderRes;
        if (lower.Contains("shade") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.ShadeRes;
        if (lower.Contains("crystal") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.CrystalRes;
        if (lower.Contains("null") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.NullRes;

        // Type resistance
        if (lower.Contains("slash") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.SlashRes;
        if (lower.Contains("pierce") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.PiercingRes;
        if (lower.Contains("blunt") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.BluntRes;

        // Status resistance
        if (lower.Contains("stun") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.StunRes;
        if (lower.Contains("sleep") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.SleepRes;
        if (lower.Contains("stone") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.StoneRes;
        if (lower.Contains("freeze") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.FreezeRes;
        if (lower.Contains("poison") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.PoisonRes;
        if (lower.Contains("pain") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.PainRes;
        if (lower.Contains("darkness") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.DarknessRes;
        if (lower.Contains("bind") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.BindRes;
        if (lower.Contains("confusion") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.ConfusionRes;
        if (lower.Contains("betray") && (lower.Contains("res") || lower.Contains("resist"))) return UnitStat.BetrayalRes;

        // Default fallback
        return UnitStat.Hp;
    }
}