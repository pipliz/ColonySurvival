using BlockTypes.Builtin;
using NPC;
using Pipliz.APIProvider.Jobs;
using Pipliz.JSON;
using Server.NPCs;
using Server.Science;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Pipliz.BlockNPCs.Implementations
{
	public class ScientistJob : BlockJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 14f;

		static double RECYCLE_CHANCE = 0.8;

		bool shouldTakeItems;
		public override string NPCTypeKey { get { return "pipliz.scientist"; } }

		public override ITrackableBlock InitializeFromJSON (Players.Player player, JSONNode node)
		{
			InitializeJob(player, (Vector3Int)node["position"], node.GetAs<int>("npcID"));
			return this;
		}

		public ITrackableBlock InitializeOnAdd (Vector3Int position, ushort type, Players.Player player)
		{
			InitializeJob(player, position, 0);
			return this;
		}

		public override bool NeedsItems { get { return shouldTakeItems; } }

		public override void OnNPCAtJob (ref NPCBase.NPCState state)
		{
			usedNPC.LookAt(position.Vector);
			ScienceManagerPlayer scienceManager;
			if (!ScienceManager.TryGetPlayerManager(owner, out scienceManager)) {
				state.SetIndicator(NPCIndicatorType.SuccessIdle, 6f);
				state.JobIsDone = false;
				return;
			}
			ResearchProgress active = scienceManager.ActiveResearch;
			if (state.Inventory.IsEmpty) {
				if (active.research != null && !active.IsCompleted) {
					// no items, but valid research -> try to get items
					IList<InventoryItem> requirements = active.research.GetScienceRequirements();
					Stockpile stockpile = Stockpile.GetStockPile(owner);
					if (Stockpile.GetStockPile(owner).Contains (requirements)) {
						shouldTakeItems = true;
						state.SetCooldown(0.3);
						state.JobIsDone = true;
					} else {
						ushort missing = 0;
						for (int i = 0; i < requirements.Count; i++) {
							if (!stockpile.Contains(requirements[i])) {
								missing = requirements[i].Type;
								break;
							}
						}
						float cooldown = Random.NextFloat(8f, 16f);
						state.SetIndicator(NPCIndicatorType.MissingItem, cooldown, missing);
						state.JobIsDone = false;
					}
				} else {
					float cooldown = Random.NextFloat(8f, 16f);
					// no items, no research -> wait for research
					state.SetIndicator(NPCIndicatorType.SuccessIdle, cooldown);
					state.JobIsDone = false;
				}
			} else {
				if (active.research == null || active.IsCompleted) {
					// items, but no valid research -> go dump items
					shouldTakeItems = true;
					state.SetCooldown(0.3);
					state.JobIsDone = true;
				} else {
					// items, valid research -> try progress
					IResearchable researchable = active.research;
					IList<InventoryItem> requirements = researchable.GetScienceRequirements();
					if (state.Inventory.TryRemove(requirements)) {
						int recycled = 0;
						for (int i = 0; i < requirements.Count; i++) {
							ushort type = requirements[i].Type;
							if (type == BuiltinBlocks.ScienceBagLife
								|| type == BuiltinBlocks.ScienceBagBasic
								|| type == BuiltinBlocks.ScienceBagMilitary) {
								recycled += requirements[i].Amount;
							}
						}
						for (int i = recycled; i > 0; i--) {
							if (Random.NextDouble() > RECYCLE_CHANCE) {
								recycled--;
							}
						}
						state.Inventory.Add(BuiltinBlocks.LinenBag, recycled);
						scienceManager.AddActiveResearchProgress(1);
						state.SetIndicator(NPCIndicatorType.ScienceProgress, StaticCraftingCooldown);
					} else {
						state.SetCooldown(0.3);
					}
					shouldTakeItems = true;
					state.JobIsDone = true;
				}
			}
		}

		public override void OnNPCAtStockpile (ref NPCBase.NPCState state)
		{
			if (state.Inventory.IsEmpty) {
				Assert.IsTrue(shouldTakeItems);
			} else {
				state.Inventory.TryDump(usedNPC.Colony.UsedStockpile);
			}
			state.SetCooldown(0.5);
			state.JobIsDone = true;
			if (shouldTakeItems) {
				shouldTakeItems = false;

				ScienceManagerPlayer scienceManager;
				if (!ScienceManager.TryGetPlayerManager(owner, out scienceManager)) {
					return;
				}

				ResearchProgress active = scienceManager.ActiveResearch;
				if (active.research != null && !active.IsCompleted) {
					var requirements = active.research.GetScienceRequirements();
					if (Stockpile.GetStockPile(owner).TryRemove (requirements)) {
						state.Inventory.Add(requirements);
					}
				}
			}
		}

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Scientist",
				maskColor1 = new UnityEngine.Color32(208, 208, 208, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
