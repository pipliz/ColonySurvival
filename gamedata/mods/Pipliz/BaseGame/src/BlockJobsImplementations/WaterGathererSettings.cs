using BlockTypes;
using Jobs;
using NPC;
using Recipes;
using System.Collections.Generic;
using UnityEngine;
using static NPC.NPCBase;

namespace Pipliz.Mods.BaseGame
{
	public class WaterGathererSettings : IBlockJobSettings
	{
		public ItemTypes.ItemType[] BlockTypes { get; }
		public NPCType NPCType { get; }
		public string NPCTypeString { get; }

		public int MaxGatheredBeforeCrate { get; set; } = 5;
		public float Cooldown { get; set; } = 8f;
		public InventoryItem RecruitmentItem { get { return InventoryItem.Empty; } }
		public float NPCShopGameHourMinimum { get { return TimeCycle.Settings.SleepTimeEnd; } }
		public float NPCShopGameHourMaximum { get { return TimeCycle.Settings.SleepTimeStart; } }
		public bool ToSleep { get { return TimeCycle.ShouldSleep; } }

		// buffer for onnpcgathered npc code should be threadsafe
		static protected List<ItemTypes.ItemTypeDrops> GatherResults = new List<ItemTypes.ItemTypeDrops>();

		public WaterGathererSettings ()
		{
			BlockTypes = new ItemTypes.ItemType[] {
				ItemTypes.GetType("watergathererjob"),
				ItemTypes.GetType("watergathererjobx+"),
				ItemTypes.GetType("watergathererjobx-"),
				ItemTypes.GetType("watergathererjobz+"),
				ItemTypes.GetType("watergathererjobz-")
			};
			NPCTypeString = "pipliz.watergatherer";
			NPCType = NPCType.GetByKeyNameOrDefault(NPCTypeString);
		}

		public void OnGoalChanged (BlockJobInstance instance, NPCGoal oldGoal, NPCGoal newGoal) { }

		public Vector3Int GetJobLocation (BlockJobInstance instance)
		{
			return instance.Position;
		}

		public void OnNPCAtJob (BlockJobInstance blockJobInstance, ref NPCState state)
		{
			WaterGathererInstance instance = (WaterGathererInstance)blockJobInstance;
			Colony owner = instance.Owner;
			state.JobIsDone = true;

			if (!CheckWater(instance)) {
				state.SetCooldown(0.3);
				return;
			}

			if (BlockTypes.ContainsByReference(instance.BlockType, out int index)) {
				Vector3 rotate = instance.NPC.Position.Vector;
				switch (index) {
					case 1:
						rotate.x -= 1f;
						break;
					case 2:
						rotate.x += 1f;
						break;
					case 3:
						rotate.z -= 1f;
						break;
					case 4:
						rotate.z += 1f;
						break;
				}
				instance.NPC.LookAt(rotate);
			}

			var recipes = owner.RecipeData.GetAvailableRecipes(NPCTypeString);

			Recipe.RecipeMatch match = Recipe.MatchRecipe(recipes, owner);
			switch (match.MatchType) {
				case Recipe.RecipeMatchType.AllDone:
				case Recipe.RecipeMatchType.FoundMissingRequirements:
					if (state.Inventory.IsEmpty) {
						state.JobIsDone = true;
						if (match.MatchType == Recipe.RecipeMatchType.AllDone) {
							state.SetIndicator(new Shared.IndicatorState(Cooldown, BuiltinBlocks.ErrorIdle));
						} else {
							state.SetIndicator(new Shared.IndicatorState(Cooldown, match.FoundRecipe.FindMissingType(owner.Stockpile), true, false));
						}
					} else {
						instance.ShouldTakeItems = true;
					}
					break;
				case Recipe.RecipeMatchType.FoundCraftable:
					var recipe = match.FoundRecipe;
					if (owner.Stockpile.TryRemove(recipe.Requirements)) {
						// should always succeed, as the match above was succesfull
						GatherResults.Clear();
						for (int i = 0; i < recipe.Results.Count; i++) {
							GatherResults.Add(recipe.Results[i]);
						}

						ModLoader.TriggerCallbacks(ModLoader.EModCallbackType.OnNPCGathered, this as IJob, instance.Position, GatherResults);

						InventoryItem toShow = ItemTypes.ItemTypeDrops.GetWeightedRandom(GatherResults);
						if (toShow.Amount > 0) {
							state.SetIndicator(new Shared.IndicatorState(Cooldown, toShow.Type));
						} else {
							state.SetCooldown(Cooldown);
						}

						state.Inventory.Add(GatherResults);

						instance.GatheredCount++;
						if (instance.GatheredCount >= MaxGatheredBeforeCrate) {
							instance.GatheredCount = 0;
							instance.ShouldTakeItems = true;
						}
					}
					break;
			}
		}

		bool CheckWater (WaterGathererInstance instance)
		{
			if (instance.WaterPosition != Vector3Int.invalidPos) {
				if (World.TryGetTypeAt(instance.WaterPosition, out ushort type)) {
					if (type == BuiltinBlocks.Water) {
						return true;
					} else {
						instance.WaterPosition = Vector3Int.invalidPos;
					}
				} else {
					return false; // not loaded
				}
			}
			UnityEngine.Assertions.Assert.IsTrue(instance.WaterPosition == Vector3Int.invalidPos, "waterpos wasn't invalid");
			for (int x = -1; x <= 1; x++) {
				for (int y = -1; y <= 1; y++) {
					for (int z = -1; z <= 1; z++) {
						if (World.TryGetTypeAt(instance.Position.Add(x, y, z), out ushort type)) {
							if (type == BuiltinBlocks.Water) {
								instance.WaterPosition = instance.Position.Add(x, y, z);
								return true;
							}
						} else {
							return false; // not loaded
						}
					}
				}
			}

			// no water found
			ServerManager.TryChangeBlock(instance.Position, instance.BlockType, ItemTypes.Air, new BlockChangeRequestOrigin(instance.Owner), ESetBlockFlags.Default);
			return false;
		}

		public void OnNPCAtStockpile (BlockJobInstance blockJobInstance, ref NPCState state)
		{
			state.Inventory.Dump(blockJobInstance.Owner.Stockpile);
			state.SetCooldown(0.3);
			state.JobIsDone = true;
		}
	}
}
