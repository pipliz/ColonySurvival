using BlockTypes.Builtin;
using Pipliz.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;

namespace Pipliz.BlockNPCs.Implementations
{
	public class BloomeryJob : RotatedCraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.bloomeryjob"; } }

		public override float TimeBetweenJobs { get { return 6f; } }

		public override int MaxRecipeCraftsPerHaul { get { return 3; } }

		public override void OnStartCrafting ()
		{
			base.OnStartCrafting();

			ushort litType;
			if (blockType == BuiltinBlocks.BloomeryXP) {
				litType = BuiltinBlocks.BloomeryLitXP;
			} else if (blockType == BuiltinBlocks.BloomeryXN) {
				litType = BuiltinBlocks.BloomeryLitXN;
			} else if (blockType == BuiltinBlocks.BloomeryZP) {
				litType = BuiltinBlocks.BloomeryLitZP;
			} else if (blockType == BuiltinBlocks.BloomeryZN) {
				litType = BuiltinBlocks.BloomeryLitZN;
			} else {
				Log.Write("Unexpect blocktype {0} for job {1} at {2}", ItemTypes.IndexLookup.GetName(blockType), NPCTypeKey, position);
				return;
			}
			ServerManager.TryChangeBlock(position, litType);
		}

		public override Vector3Int GetPositionNPC (Vector3Int position)
		{
			if (blockType == BuiltinBlocks.BloomeryLitXP || blockType == BuiltinBlocks.BloomeryXP) {
				return position.Add(1, 0, 0);
			} else if (blockType == BuiltinBlocks.BloomeryLitXN || blockType == BuiltinBlocks.BloomeryXN) {
				return position.Add(-1, 0, 0);
			} else if (blockType == BuiltinBlocks.BloomeryLitZP || blockType == BuiltinBlocks.BloomeryZP) {
				return position.Add(0, 0, 1);
			} else if (blockType == BuiltinBlocks.BloomeryLitZN || blockType == BuiltinBlocks.BloomeryZN) {
				return position.Add(0, 0, -1);
			} else {
				Log.Write("Unexpect blocktype {0} for job {1} at {2}", ItemTypes.IndexLookup.GetName(blockType), NPCTypeKey, position);
				return position;
			}
		}

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Iron smelter",
				maskColor1 = new UnityEngine.Color32(140, 72, 49, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		public override List<string> GetCraftingLimitsTriggers ()
		{
			return new List<string>()
			{
				"bloomeryx+",
				"bloomeryx-",
				"bloomeryz+",
				"bloomeryz-",
				"bloomerylitx+",
				"bloomerylitx-",
				"bloomerylitz+",
				"bloomerylitz-"
			};
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "bloomery.json");
		}
	}
}
