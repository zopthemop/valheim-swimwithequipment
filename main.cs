using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace SwimWithEquipment;

[BepInPlugin(ModGUID, ModName, ModVersion)]
public class Plugin : BaseUnityPlugin
{
	public const string ModGUID = "zopthemop.swimwithequipment";
	public const string ModName = "Swim With Equipment";
	public const string ModVersion = "1.0.0";
	public const string ModDescription = "Swim with equipment (e.g. pickaxe, hoe, etc)";

    private void Awake()
    {
        Harmony harmony = new(ModGUID);
        harmony.PatchAll();
    }

	[HarmonyPatch(typeof(Player), "Update")]
	public static class Player_Update_Patch
	{
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			MethodInfo targetMethod = AccessTools.Method(typeof(Character), nameof(Character.IsSwimming));

			foreach (CodeInstruction instruction in instructions)
			{
				if (instruction.Calls(targetMethod))
				{
					yield return new CodeInstruction(OpCodes.Pop); // remove Character object from stack
					yield return new CodeInstruction(OpCodes.Ldc_I4_0); // replace IsSwimming with false
					continue;
				}

				yield return instruction;
			}
		}
	}

	[HarmonyPatch(typeof(Humanoid), "EquipItem")]
	public static class Humanoid_EquipItem_Patch
	{
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			MethodInfo targetMethod = AccessTools.Method(typeof(Character), nameof(Character.IsSwimming));

			foreach (CodeInstruction instruction in instructions)
			{
				if (instruction.Calls(targetMethod))
				{
					yield return new CodeInstruction(OpCodes.Pop); // remove Character object from stack
					yield return new CodeInstruction(OpCodes.Ldc_I4_0); // replace IsSwimming with false
					continue;
				}

				yield return instruction;
			}
		}
	}

	[HarmonyPatch(typeof(Humanoid), "UpdateEquipment")]
	public static class Humanoid_UpdateEquipment_Patch
	{
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			MethodInfo targetMethod = AccessTools.Method(typeof(Character), nameof(Character.IsSwimming));

			foreach (CodeInstruction instruction in instructions)
			{
				if (instruction.Calls(targetMethod))
				{
					yield return new CodeInstruction(OpCodes.Pop); // remove Character object from stack
					yield return new CodeInstruction(OpCodes.Ldc_I4_0); // replace IsSwimming with false
					continue;
				}

				yield return instruction;
			}
		}
	}
}
