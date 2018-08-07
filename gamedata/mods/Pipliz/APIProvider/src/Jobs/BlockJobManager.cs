using BlockEntities;
using System;
using System.Collections.Generic;

namespace Pipliz.APIProvider.Jobs
{
	/// <summary>
	/// A generic crafting job manager that can be instantiated using some settings
	/// Register it to ServerManager.BlockEntities or nothing happens
	/// The manager creates/removes the sub-class CraftingJobInstance
	/// IUnloadedByPosition is not implemented because a chunk with crafting jobs will not unload (the instance implements IBlockEntityKeepLoaded for it)
	/// </summary>
	public class BlockJobManager<TSettings, TBlockType> : ILoadedWithDataByPositionType, IChangedWithType, IMultiBlockEntityMapping
		where TSettings : IBlockJobSettings
		where TBlockType : IBlockEntity
	{
		public TSettings Settings { get; set; }
		public Func<TSettings, Vector3Int, ItemTypes.ItemType, ByteReader, TBlockType> Loader { get; set; }
		public Func<TSettings, Vector3Int, ItemTypes.ItemType, Colony, TBlockType> Adder { get; set; }

		// ISingleBlockEntityMapping
		public IEnumerable<ItemTypes.ItemType> TypesToRegister { get { return Settings.BlockTypes; } }

		public BlockJobManager (TSettings settings,
			Func<TSettings, Vector3Int, ItemTypes.ItemType, ByteReader, TBlockType> loader,
			Func<TSettings, Vector3Int, ItemTypes.ItemType, Colony, TBlockType> adder
		) {
			Loader = loader;
			Adder = adder;
			Settings = settings;
		}

		// IChangedWithType
		public void OnChangedWithType (Players.Player player, Vector3Int blockPosition, ItemTypes.ItemType typeOld, ItemTypes.ItemType typeNew)
		{
			var settings = Settings;
			bool isAdded = settings.BlockTypes.ContainsByReference(typeNew);
			bool isRemoved = settings.BlockTypes.ContainsByReference(typeOld);

			if (isAdded) {
				if (isRemoved) {
					// changed from 1 subtype to another
				} else {
					OnPlaced(player, blockPosition, typeNew);
				}
			} else {
				if (isRemoved) {
					OnRemoved(blockPosition);
				} else {
					// passed an onchange which isnt related to the blocktype - shouldn't happen
				}
			}
		}

		// ILoadedWithDataByPositionType
		public void OnLoadedWithDataPosition (Vector3Int blockPosition, ushort type, ByteReader reader)
		{
			ServerManager.BlockEntityTracker.OnAddedEntity(blockPosition, Loader(Settings, blockPosition, ItemTypes.GetType(type), reader));
		}

		public void OnPlaced (Players.Player player, Vector3Int blockPosition, ItemTypes.ItemType type)
		{
			if (player.ActiveColony == null) {
				// need a colony to set as parent of this block!
				ThreadManager.AssertIsMainThread();
				ServerManager.TryChangeBlock(blockPosition, 0, player, ServerManager.SetBlockFlags.None);
				return;
			}
			ServerManager.BlockEntityTracker.OnAddedEntity(blockPosition, Adder(Settings, blockPosition, type, player.ActiveColony));
		}

		public void OnRemoved (Vector3Int blockPosition)
		{
			ServerManager.BlockEntityTracker.OnRemoveEntity(blockPosition);
		}
	}
}
