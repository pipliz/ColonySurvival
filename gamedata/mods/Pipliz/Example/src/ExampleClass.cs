using Pipliz;
using Pipliz.Chatting;
using Pipliz.JSON;
using Pipliz.Threading;

namespace ExampleMod
{
	[ModLoader.ModManager]
	public class ExampleClassModManager
	{
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterAddingBaseTypes)]
		public static void AfterAddingBaseTypes ()
		{
			ItemTypesServer.AddTextureMapping("ExampleBlock1", new JSONNode()
				.SetAs("albedo", "grassTemperate")
				.SetAs("normal", "stoneblock")
				.SetAs("emissive", "ovenLitFront")
				.SetAs("height", "oreCoal")
			);

			ItemTypesServer.AddTextureMapping("ExampleBlock2", new JSONNode()
				.SetAs("albedo", "grindstone")
				.SetAs("normal", "berrybush")
				.SetAs("emissive", "torch")
				.SetAs("height", "snow")
			);
			
			ItemTypes.AddRawType("ExampleBlock1", 
				new JSONNode(NodeType.Object)
					.SetAs("onPlaceAudio", "stonePlace")
					.SetAs("onRemoveAudio", "woodDeleteLight")
					.SetAs("sideall", "SELF")
					.SetAs("onRemoveAmount", 2)
					.SetAs("maxStackSize", 500)
					.SetAs("npcLimit", 1000)
			);

			ItemTypes.AddRawType("ExampleBlock2",
				new JSONNode(NodeType.Object)
					.SetAs("onPlaceAudio", "stonePlace")
					.SetAs("onRemoveAudio", "woodDeleteLight")
					.SetAs("sideall", "SELF")
					.SetAs("onRemoveAmount", 2)
					.SetAs("maxStackSize", 500)
					.SetAs("npcLimit", 1000)
			);

		}
		
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesServer)]
		public static void AfterItemTypesServer ()
		{
			ItemTypesServer.RegisterType("ExampleBlock1",
				new ItemTypesServer.ItemActionBuilder()
				.SetOnAdd(ExampleClassCodeManager.OnAdd)
				.SetOnRemove(ExampleClassCodeManager.OnRemove)
				.SetOnChange(ExampleClassCodeManager.OnChange)
				.SetChangeTypes("ExampleBlock1", "ExampleBlock2")
			);

			ItemTypesServer.RegisterParent("ExampleBlock2", "ExampleBlock1");
		}
	}

	static class ExampleClassCodeManager
	{
		public static void OnAdd (Vector3Int position, ushort newType, NetworkID causedBy)
		{
			Chat.Send(causedBy, string.Format("You placed block {0} at {1}", ItemTypes.IndexLookup.GetName(newType), position));
			ThreadManager.InvokeOnMainThread(delegate ()
		   {
			   ServerManager.TrySetBlock(position, ItemTypes.IndexLookup.GetIndex("ExampleBlock2"), causedBy, false);
		   }, 5.0);
		}

		public static void OnRemove (Vector3Int position, ushort wasType, NetworkID causedBy)
		{
			Chat.Send(causedBy, string.Format("You removed block {0} at {1}", ItemTypes.IndexLookup.GetName(wasType), position));
		}

		public static void OnChange (Vector3Int position, ushort wasType, ushort newType, NetworkID causedBy)
		{
			Chat.SendToAll(string.Format("Block type {0} changed to {1} at {2}",
				ItemTypes.IndexLookup.GetName(wasType),
				ItemTypes.IndexLookup.GetName(newType),
				position)
			);
		}
	}
}
