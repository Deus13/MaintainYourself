using Harmony;
using System;
using UnityEngine;

namespace Maintainyouself
{

    [HarmonyPatch(typeof(Condition), "AddHealth", new Type[] { typeof(float), typeof(DamageSource),typeof(bool) })]
    internal static class Condition_AddHealth
    {
        private static void Prefix(Condition __instance, ref float hp, DamageSource cause)
        {
            var setting = MaintainyouselfSettings.Instance;
            if (cause == DamageSource.Freezing)
            {

                hp *= Math.Abs(GameManager.GetFreezingComponent().CalculateBodyTemperature() / setting.BreackevenTemperture);

            }
            if (hp > 0)
            {
                Hunger hunger = GameManager.GetHungerComponent();
                Thirst thirst = GameManager.GetThirstComponent();
                Freezing freezing = GameManager.GetFreezingComponent();
                Fatigue fatigue = GameManager.GetFatigueComponent();
                Condition condition = GameManager.GetConditionComponent();

                float f = setting.ConditionGain;

                if (setting.HungerOn)
                    if (setting.HungerMaxLvl > setting.HungerMinLvl)
                        f *= Mathf.Lerp(setting.HungerMin, setting.HungerMax, Mathf.Clamp(hunger.m_CurrentReserveCalories / hunger.m_MaxReserveCalories, setting.HungerMinLvl, setting.HungerMaxLvl) / (setting.HungerMaxLvl - setting.HungerMinLvl));
                    
                if (setting.ThirstOn)
                    if (setting.ThirstMaxLvl > setting.ThirstMinLvl)
                        f *= Mathf.Lerp(setting.ThirstMin, setting.ThirstMax, Mathf.Clamp(1 - (thirst.m_CurrentThirst / thirst.m_MaxThirst), setting.ThirstMinLvl, setting.ThirstMaxLvl) / (setting.ThirstMaxLvl - setting.ThirstMinLvl));


                if (setting.FreezingOn)
                    if (setting.FreezingMaxLvl > setting.FreezingMinLvl)
                        f *= Mathf.Lerp(setting.FreezingMin, setting.FreezingMax, Mathf.Clamp(1 - (freezing.m_CurrentFreezing / freezing.m_MaxFreezing), setting.FreezingMinLvl, setting.FreezingMaxLvl) / (setting.FreezingMaxLvl - setting.FreezingMinLvl));

                if (setting.FatigueOn)
                    if (setting.FatigueMaxLvl > setting.FatigueMinLvl)
                        f *= Mathf.Lerp(setting.FatigueMin, setting.FatigueMax, Mathf.Clamp(1 - (fatigue.m_CurrentFatigue / fatigue.m_MaxFatigue), setting.FatigueMinLvl, setting.FatigueMaxLvl) / (setting.FatigueMaxLvl - setting.FatigueMinLvl));

                if (setting.ConditionOn)
                    if (setting.ConditionMaxLvl > setting.ConditionMinLvl)
                        f *= Mathf.Lerp(setting.ConditionMin, setting.ConditionMax, Mathf.Clamp(condition.m_CurrentHP / condition.m_MaxHP, setting.ConditionMinLvl, setting.ConditionMaxLvl) / (setting.ConditionMaxLvl - setting.ConditionMinLvl));
               
                
                hp *= f;
                if (Implementation.HPnotround < condition.m_CurrentHP)              //float creates rounding errors so we keep track with double
                {
                    Implementation.HPnotround = condition.m_CurrentHP;
                }
                else
                {
                    if (Implementation.HPnotround - condition.m_CurrentHP < 0.001f && Implementation.HPnotround <= 100f) condition.m_CurrentHP = (float)Implementation.HPnotround;
                    else Implementation.HPnotround = condition.m_CurrentHP;

                }
                Implementation.HPnotround += hp;
                // Implementation.Log((f).ToString() + "        " + condition.m_CurrentHP.ToString() + "        " + Implementation.HPnotround.ToString());
                //Implementation.Log((hunger.m_CurrentReserveCalories / hunger.m_MaxReserveCalories).ToString() + "    " + (fatigue.m_CurrentFatigue / fatigue.m_MaxFatigue).ToString() + "    " + (freezing.m_CurrentFreezing / freezing.m_MaxFreezing).ToString() + "    " + (thirst.m_CurrentThirst / thirst.m_MaxThirst).ToString() + "    " + (condition.m_CurrentHP / condition.m_MaxHP).ToString());
                //Implementation.Log(hp.ToString() + "        "+f.ToString());
            }

        }
    }
    [HarmonyPatch(typeof(Condition), "Update")]
    internal static class Condition_Update
    {
        private static void Prefix(Condition __instance)
        {
            Implementation.Log("?");
            if(GameManager.GetWillpowerComponent()==null)
            {
                Implementation.Log("null");
            }
            GameManager.GetWillpowerComponent().Update();
            Implementation.Log(GameManager.GetWillpowerComponent().m_TimeRemainingSeconds.ToString());
        }
    }

    [HarmonyPatch(typeof(Willpower), "Update")]
    internal static class Willpower_Update
    {
        private static void Postfix(Willpower __instance)
        {

            Implementation.Log(__instance.m_TimeRemainingSeconds.ToString());
        }
    }
    [HarmonyPatch(typeof(Willpower), "Start")]
    internal static class Willpower_Start
    {
        private static void Postfix(Willpower __instance)
        {

            __instance.m_TimeRemainingSeconds = 1000;
        }
    }


}