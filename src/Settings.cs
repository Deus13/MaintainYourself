using System.Reflection;
using ModSettings;

namespace MaintainYourself
{
    internal class Settings : JsonModSettings
    {
        public static Settings instance = new Settings();
        

        [Section("Condition:")]
        [Name("Gain factor overall")]
        [Description("Multiplies every condition gain with this factor.")]
        [Slider(0, 5)]
        public float ConditionGain = 1f;

        [Name("Condition Affects Condition Gain")]
        [Description("Determins if curent condition level should have an effect on condition gain.")]
        public bool ConditionOn = true;

        [Name("Condition min gain factor")]
        [Description("How big the condition gain factor is with low condition.")]
        [Slider(0, 2)]
        public float ConditionMin = 0f;

        [Name("Condition max gain factor")]
        [Description("How big the condition gain factor is with high condition.")]
        [Slider(0, 2)]
        public float ConditionMax = 1.5f;

        [Name("Condition min threshold")]
        [Description("How low the condition has to be to reach the min gain factor.")]
        [Slider(0, 1)]
        public float ConditionMinLvl = 0f;

        [Name("Condition max threshold")]
        [Description("How high the condition has to be to reach the max gain factor.")]
        [Slider(0, 1)]
        public float ConditionMaxLvl = 1f;



        [Section("Hunger:")]
        [Name("Hunger Affects Condition Gain")]
        [Description("Determines if hunger level should have an effect on condition gain.")]
        public bool HungerOn = true;

        [Name("Hunger min gain factor")]
        [Description("How big the condition gain factor is when hungry.")]
        [Slider(0, 2)]
        public float HungerMin = 0f;

        [Name("Hunger max gain factor")]
        [Description("How big the condition gain factor is when not hungry.")]
        [Slider(0, 2)]
        public float HungerMax = 1.5f;

        [Name("Hunger min threshold")]
        [Description("How low the hunger need bar has to be to reach the min gain factor.")]
        [Slider(0, 1)]
        public float HungerMinLvl = 0f;

        [Name("Hunger max threshold")]
        [Description("How high the hunger need bar has to be to reach the max gain factor.")]
        [Slider(0, 1)]
        public float HungerMaxLvl = 1f;

        

        [Section("Thirst:")]
        [Name("Thirst Affects Condition Gain")]
        [Description("Determines if hunger level should have an effect on condition gain.")]
        public bool ThirstOn = true;

        [Name("Thirst min gain factor")]
        [Description("How big the condition gain factor is when thirsty.")]
        [Slider(0, 2)]
        public float ThirstMin = 0f;

        [Name("Thirst max gain factor")]
        [Description("How big the condition gain factor is when not thirsty.")]
        [Slider(0, 2)]
        public float ThirstMax = 1.5f;

        [Name("Thirst min threshold")]
        [Description("How low the thirst need bar has to be to reach the min gain factor.")]
        [Slider(0, 1)]
        public float ThirstMinLvl = 0f;

        [Name("Thirst max threshold")]
        [Description("How high the thirst need bar has to be to reach the max gain factor.")]
        [Slider(0, 1)]
        public float ThirstMaxLvl = 1f;



        [Section("Freezing:")]
        [Name("Break Even Temperature")]
        [Description("Temperature where condition loss from freezing is the same as the unmodded condition loss.")]
        [Slider(0, -30)]
        public float BreakEvenTemperature = -20f;
        
        [Name("Freezing Affects Condition Gain")]
        [Description("Determins if freezing level should have an effect on condition gain.")]
        public bool FreezingOn = true;

        [Name("Freezing min gain factor")]
        [Description("How big the condition gain factor is when freezing.")]
        [Slider(0, 2)]
        public float FreezingMin = 0f;

        [Name("Freezing max gain factor")]
        [Description("How big the condition gain factor is when not freezing.")]
        [Slider(0, 2)]
        public float FreezingMax = 1.5f;

        [Name("Freezing min threshold")]
        [Description("How low the freezing need bar has to be to reach the min gain factor.")]
        [Slider(0, 1)]
        public float FreezingMinLvl = 0f;

        [Name("Freezing max threshold")]
        [Description("How high the freezing need bar has to be to reach the max gain factor.")]
        [Slider(0, 1)]
        public float FreezingMaxLvl = 1f;

        

        [Section("Fatigue:")]
        [Name("Fatigue Affects Condition Gain")]
        [Description("Determins if fatigue level should have an effect on condition gain.")]
        public bool FatigueOn = true;

        [Name("Fatigue min gain factor")]
        [Description("How big the condition gain factor is when fatigued.")]
        [Slider(0, 2)]
        public float FatigueMin = 0f;

        [Name("Fatigue max gain factor")]
        [Description("How big the condition gain factor is when not fatigued.")]
        [Slider(0, 2)]
        public float FatigueMax = 1.5f;

        [Name("Fatigue min threshold")]
        [Description("How low the fatigue need bar has to be to reach the min gain factor.")]
        [Slider(0, 1)]
        public float FatigueMinLvl = 0f;

        [Name("Fatigue max threshold")]
        [Description("How high the fatigue need bar has to be to reach the max gain factor.")]
        [Slider(0, 1)]
        public float FatigueMaxLvl = 1f;
        
        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            base.OnChange(field, oldValue, newValue);
            
            if (field.Name == nameof(HungerOn))
            {
                HideHunger();
            }
            else if (field.Name == nameof(ThirstOn))
            {
                HideThirst();
            }
            else if (field.Name == nameof(FreezingOn))
            {
                HideFreezing();
            }
            else if (field.Name == nameof(FatigueOn))
            {
                HideFatigue();
            }
            else if (field.Name == nameof(ConditionOn))
            {
                HideCondition();
            }
            RefreshGUI();
        }

        private void HideHunger()
        {
            SetFieldVisible(nameof(HungerMin), HungerOn);
            SetFieldVisible(nameof(HungerMax), HungerOn);
            SetFieldVisible(nameof(HungerMinLvl), HungerOn);
            SetFieldVisible(nameof(HungerMaxLvl), HungerOn);           
        }

        private void HideThirst()
        {
            SetFieldVisible(nameof(ThirstMin), ThirstOn);
            SetFieldVisible(nameof(ThirstMax), ThirstOn);
            SetFieldVisible(nameof(ThirstMinLvl), ThirstOn);
            SetFieldVisible(nameof(ThirstMaxLvl), ThirstOn);
        }

        private void HideFreezing()
        {
            SetFieldVisible(nameof(FreezingMin), FreezingOn);
            SetFieldVisible(nameof(FreezingMax), FreezingOn);
            SetFieldVisible(nameof(FreezingMinLvl), FreezingOn);
            SetFieldVisible(nameof(FreezingMaxLvl), FreezingOn);
        }
        private void HideFatigue()
        {
            SetFieldVisible(nameof(FatigueMin), FatigueOn);
            SetFieldVisible(nameof(FatigueMax), FatigueOn);
            SetFieldVisible(nameof(FatigueMinLvl), FatigueOn);
            SetFieldVisible(nameof(FatigueMaxLvl), FatigueOn);
        }
        private void HideCondition()
        {
            SetFieldVisible(nameof(ConditionMin), ConditionOn);
            SetFieldVisible(nameof(ConditionMax), ConditionOn);
            SetFieldVisible(nameof(ConditionMinLvl), ConditionOn);
            SetFieldVisible(nameof(ConditionMaxLvl), ConditionOn);
        }
    }
}
