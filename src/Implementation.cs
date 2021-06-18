using MelonLoader;
using System;
using UnityEngine;


namespace MaintainYourself
{
    public class Implementation : MelonMod
    {
        public static double HPnotround = 0f;
        public override void OnApplicationStart()
        {
            Debug.Log($"[{Info.Name}] version {Info.Version} loaded!");
            Settings.instance.RefreshGUI();
            Settings.instance.AddToModSettings("Maintain Yourself");
        }

        public static float NewMethod(float hp, DamageSource cause)
        {
            var setting = Settings.instance;
            if (cause == DamageSource.Freezing)
            {
                hp *= Math.Abs(GameManager.GetFreezingComponent().CalculateBodyTemperature() / setting.BreakEvenTemperature);
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
                if (HPnotround < condition.m_CurrentHP)              //float creates rounding errors so we keep track with double
                {
                    HPnotround = condition.m_CurrentHP;
                }
                else
                {
                    if (HPnotround - condition.m_CurrentHP < 0.001f && Implementation.HPnotround <= 100f) condition.m_CurrentHP = (float)Implementation.HPnotround;
                    else HPnotround = condition.m_CurrentHP;

                }
                HPnotround += hp;
                //Log((f).ToString() + "        " + condition.m_CurrentHP.ToString() + "        " + Implementation.HPnotround.ToString());
                //Log((hunger.m_CurrentReserveCalories / hunger.m_MaxReserveCalories).ToString() + "    " + (fatigue.m_CurrentFatigue / fatigue.m_MaxFatigue).ToString() + "    " + (freezing.m_CurrentFreezing / freezing.m_MaxFreezing).ToString() + "    " + (thirst.m_CurrentThirst / thirst.m_MaxThirst).ToString() + "    " + (condition.m_CurrentHP / condition.m_MaxHP).ToString());
                //Log(hp.ToString() + "        "+f.ToString());
            }

            return hp;
        }

        internal static void Log(string message)
        {
            Debug.Log( message);
        }

    }
   
}