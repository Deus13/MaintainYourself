using MelonLoader;
using UnityEngine;

namespace Maintainyouself
{
	internal class MaintainYoursefMod : MelonMod
	{

		public override void OnApplicationStart()
		{
			MaintainyouselfSettings.Settings.OnLoad();
			Debug.Log($"[{InfoAttribute.Name}] version {InfoAttribute.Version} loaded!");
		}
	}
}
