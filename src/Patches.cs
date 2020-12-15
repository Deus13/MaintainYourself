using System;
using Harmony;


namespace Maintainyouself
{

    [HarmonyPatch(typeof(Condition), "AddHealth", new Type[] { typeof(float), typeof(DamageSource),typeof(bool) })]
    internal static class Condition_AddHealth
    {
        private static void Prefix(Condition __instance, ref float hp, DamageSource cause)
        {
            hp = Implementation.NewMethod(hp, cause);
        }
    }
}