using Pipliz.JSON;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pipliz.BlockNPCs
{
	[ModLoader.ModManager]
	public static class BlockRotator
	{
		static List<Action> RegisteringChangeTypes = new List<Action>();

		public struct RotatorSettings
		{
			public ItemTypesServer.ItemTypeRaw BaseType;
			public string sideTopLit;
			public string sideTopUnlit;
			public string rotatedSideLit;
			public string rotatedSideUnlit;

			public RotatorSettings (
				ItemTypesServer.ItemTypeRaw BaseType,
				string sideTopLit,
				string sideTopUnlit,
				string rotatedSideLit,
				string rotatedSideUnlit
			)
			{
				this.BaseType = BaseType;
				this.sideTopLit = sideTopLit;
				this.sideTopUnlit = sideTopUnlit;
				this.rotatedSideLit = rotatedSideLit;
				this.rotatedSideUnlit = rotatedSideUnlit;
			}
		}

		static ItemTypesServer.ItemTypeRaw RotatedTypeUnlit (string name, string suffix, string sideType)
		{
			return new ItemTypesServer.ItemTypeRaw(
				name + suffix,
				new JSONNode()
					.SetAs("parentType", name)
					.SetAs("side" + suffix, sideType)
			);
		}

		static ItemTypesServer.ItemTypeRaw RotatedTypeLit (string name, string suffix, string sideType, JSONNode copy)
		{
			if (copy.HasChild("torches")) {
				if (copy["torches"].HasChild("a")) {
					RotateOffset(copy["torches"]["a"], suffix);
				}
				if (copy["torches"].HasChild("b")) {
					RotateOffset(copy["torches"]["b"], suffix);
				}
			}

			return new ItemTypesServer.ItemTypeRaw(
				name + suffix,
				new JSONNode()
					.SetAs("parentType", name)
					.SetAs("side" + suffix, sideType)
					.SetAs("customData", copy)
			);
		}

		static void RotateOffset (JSONNode node, string suffix)
		{
			Vector3 position = new Vector3(node.GetAs<float>("offsetx"), node.GetAs<float>("offsety"), node.GetAs<float>("offsetz"));
			switch (suffix) {
				case "x+":
					position = new Vector3(position.x, position.y, position.z);
					break;
				case "x-":
					position = new Vector3(-position.x, position.y, -position.z);
					break;
				case "z+":
					position = new Vector3(-position.z, position.y, position.x);
					break;
				case "z-":
					position = new Vector3(position.z, position.y, -position.x);
					break;
				default:
					Assert.IsTrue(false, "Suffix not x+, x-, z+ or z-");
					throw new System.ArgumentException();
			}
			node.SetAs("offsetx", position.x);
			node.SetAs("offsety", position.y);
			node.SetAs("offsetz", position.z);
		}

		static void ItemRotator (Dictionary<string, ItemTypesServer.ItemTypeRaw> items, RotatorSettings settings, JSONNode customDataLitOnly)
		{
			ItemTypesServer.ItemTypeRaw unlitXP = RotatedTypeUnlit(settings.BaseType.name, "x+", settings.rotatedSideUnlit);
			ItemTypesServer.ItemTypeRaw unlitXN = RotatedTypeUnlit(settings.BaseType.name, "x-", settings.rotatedSideUnlit);
			ItemTypesServer.ItemTypeRaw unlitZP = RotatedTypeUnlit(settings.BaseType.name, "z+", settings.rotatedSideUnlit);
			ItemTypesServer.ItemTypeRaw unlitZN = RotatedTypeUnlit(settings.BaseType.name, "z-", settings.rotatedSideUnlit);

			settings.BaseType.description
				.SetAs("isRotatable", true)
				.SetAs("rotatablex+", unlitXP.name)
				.SetAs("rotatablex-", unlitXN.name)
				.SetAs("rotatablez+", unlitZP.name)
				.SetAs("rotatablez-", unlitZN.name)
				.SetAs("sidey+", settings.sideTopUnlit);

			items[unlitXP.name] = unlitXP;
			items[unlitXN.name] = unlitXN;
			items[unlitZP.name] = unlitZP;
			items[unlitZN.name] = unlitZN;
			items[settings.BaseType.name] = settings.BaseType;


			if (customDataLitOnly != null) {
				string litBaseName = settings.BaseType.name + "lit";

				ItemTypesServer.ItemTypeRaw litXP = RotatedTypeLit(litBaseName, "x+", settings.rotatedSideLit, customDataLitOnly.DeepClone());
				ItemTypesServer.ItemTypeRaw litXN = RotatedTypeLit(litBaseName, "x-", settings.rotatedSideLit, customDataLitOnly.DeepClone());
				ItemTypesServer.ItemTypeRaw litZP = RotatedTypeLit(litBaseName, "z+", settings.rotatedSideLit, customDataLitOnly.DeepClone());
				ItemTypesServer.ItemTypeRaw litZN = RotatedTypeLit(litBaseName, "z-", settings.rotatedSideLit, customDataLitOnly.DeepClone());

				ItemTypesServer.ItemTypeRaw baseLitType = new ItemTypesServer.ItemTypeRaw(
					litBaseName,
					new JSONNode()
						.SetAs("parentType", settings.BaseType.name)
						.SetAs("isRotatable", true)
						.SetAs("rotatablex+", litXP.name)
						.SetAs("rotatablex-", litXN.name)
						.SetAs("rotatablez+", litZP.name)
						.SetAs("rotatablez-", litZN.name)
						.SetAs("sidey+", settings.sideTopLit)
				);

				items[litXP.name] = litXP;
				items[litXN.name] = litXN;
				items[litZP.name] = litZP;
				items[litZN.name] = litZN;
				items[baseLitType.name] = baseLitType;

				RegisteringChangeTypes.Add(() => 
					ItemTypesServer.RegisterChangeTypes(settings.BaseType.name, new List<string>()
					{
						litXP.name, litXN.name, litZP.name, litZN.name,
						unlitXP.name, unlitXN.name, unlitZP.name, unlitZN.name
					})
				);
			} else {
				RegisteringChangeTypes.Add(() =>
					ItemTypesServer.RegisterChangeTypes(settings.BaseType.name, new List<string>()
					{
						unlitXP.name, unlitXN.name, unlitZP.name, unlitZN.name
					})
				);
			}
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterAddingBaseTypes, "pipliz.blocknpcs.addlittypes")]
		static void AddLitTypes (Dictionary<string, ItemTypesServer.ItemTypeRaw> items)
		{
			ItemRotator (
				items,
				new RotatorSettings (
					new ItemTypesServer.ItemTypeRaw("furnace", new JSONNode()
						.SetAs("icon", "gamedata/textures/icons/furnace.png")
						.SetAs("onPlaceAudio", "stonePlace")
						.SetAs("onRemoveAudio", "stoneDelete")
						.SetAs("sideall", "furnaceside")
						.SetAs("destructionTime", 800)
					),
					"furnacelittop",
					"furnaceunlittop",
					"furnacelitfront",
					"furnaceunlitfront"
				),
				new JSONNode()
					.SetAs("torches", new JSONNode()
						.SetAs("a", new JSONNode()
							.SetAs("volume", 0.3f)
							.SetAs("intensity", 2.5f)
							.SetAs("range", 8)
							.SetAs("red", 195f)
							.SetAs("green", 135f)
							.SetAs("blue", 46f)
							.SetAs("offsetx", 0f)
							.SetAs("offsety", 0.55f)
							.SetAs("offsetz", 0f)
						)
						.SetAs("b", new JSONNode()
							.SetAs("volume", 0.2f)
							.SetAs("intensity", 1f)
							.SetAs("range", 8)
							.SetAs("red", 195f)
							.SetAs("green", 135f)
							.SetAs("blue", 46f)
							.SetAs("offsetx", 0.55f)
							.SetAs("offsety", -0.25f)
							.SetAs("offsetz", 0f)
						)
					)
			);

			ItemRotator(
				items,
				new RotatorSettings(
					new ItemTypesServer.ItemTypeRaw("oven", new JSONNode()
						.SetAs("icon", "gamedata/textures/icons/oven.png")
						.SetAs("onPlaceAudio", "stonePlace")
						.SetAs("onRemoveAudio", "stoneDelete")
						.SetAs("sideall", "stonebricks")
						.SetAs("destructionTime", 800)
					),
					"stonebricks",
					"stonebricks",
					"ovenlitfront",
					"ovenunlitfront"
				),
				new JSONNode()
					.SetAs("torches", new JSONNode()
						.SetAs("a", new JSONNode()
							.SetAs("volume", 0.2f)
							.SetAs("intensity", 2.0f)
							.SetAs("range", 8)
							.SetAs("red", 195f)
							.SetAs("green", 135f)
							.SetAs("blue", 46f)
							.SetAs("offsetx", 0.55f)
							.SetAs("offsety", -0.25f)
							.SetAs("offsetz", 0f)
						)
					)
			);

			ItemRotator(
				items,
				new RotatorSettings(
					new ItemTypesServer.ItemTypeRaw("bloomery", new JSONNode()
						.SetAs("icon", "gamedata/textures/icons/bloomery.png")
						.SetAs("onPlaceAudio", "stonePlace")
						.SetAs("onRemoveAudio", "stoneDelete")
						.SetAs("sideall", "bricks")
						.SetAs("destructionTime", 800)
					),
					"bricks",
					"bricks",
					"bloomerylit",
					"bloomery"
				),
				new JSONNode()
					.SetAs("torches", new JSONNode()
						.SetAs("a", new JSONNode()
							.SetAs("volume", 0.2f)
							.SetAs("intensity", 2.0f)
							.SetAs("range", 8)
							.SetAs("red", 195f)
							.SetAs("green", 135f)
							.SetAs("blue", 46f)
							.SetAs("offsetx", 0.55f)
							.SetAs("offsety", -0.25f)
							.SetAs("offsetz", 0f)
						)
					)
			);

			ItemRotator(
				items,
				new RotatorSettings(
					new ItemTypesServer.ItemTypeRaw("fineryforge", new JSONNode()
						.SetAs("icon", "gamedata/textures/icons/fineryforge.png")
						.SetAs("onPlaceAudio", "stonePlace")
						.SetAs("onRemoveAudio", "stoneDelete")
						.SetAs("sideall", "ironblock")
						.SetAs("destructionTime", 1500)
					),
					"ironblock",
					"ironblock",
					"fineryforgelit",
					"fineryforge"
				),
				new JSONNode()
					.SetAs("torches", new JSONNode()
						.SetAs("a", new JSONNode()
							.SetAs("volume", 0.2f)
							.SetAs("intensity", 2.0f)
							.SetAs("range", 8)
							.SetAs("red", 195f)
							.SetAs("green", 135f)
							.SetAs("blue", 46f)
							.SetAs("offsetx", 0.55f)
							.SetAs("offsety", -0.25f)
							.SetAs("offsetz", 0f)
						)
					)
			);

			ItemRotator(
				items,
				new RotatorSettings(
					new ItemTypesServer.ItemTypeRaw("kiln", new JSONNode()
						.SetAs("icon", "gamedata/textures/icons/kiln.png")
						.SetAs("onPlaceAudio", "dirtPlace")
						.SetAs("onRemoveAudio", "grassDelete")
						.SetAs("sideall", "dirt")
						.SetAs("destructionTime", 800)
					),
					null,
					"dirt",
					null,
					"kiln"
				),
				null
			);
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.blocknpcs.registerchangetypes")]
		static void RegisterChangeTypes ()
		{
			for (int i = 0; i < RegisteringChangeTypes.Count; i++) {
				RegisteringChangeTypes[i].Invoke();
			}
			RegisteringChangeTypes = null;
		}
	}
}
