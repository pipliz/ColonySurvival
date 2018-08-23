using BlockEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Pipliz.APIProvider.Jobs
{
	/// <summary>
	/// A generic crafting job manager that can be instantiated using some settings
	/// Register it to ServerManager.BlockEntities or nothing happens
	/// The manager creates/removes the sub-class CraftingJobInstance
	/// IUnloadedByPosition is not implemented because a chunk with crafting jobs will not unload (the instance implements IBlockEntityKeepLoaded for it)
	/// </summary>
	public class BlockJobManager<TBlockType> : ILoadedWithDataByPositionType, IChangedWithType, IMultiBlockEntityMapping
		where TBlockType : IBlockEntity
	{
		public IBlockJobSettings Settings { get; set; }

		public Func<IBlockJobSettings, Vector3Int, ItemTypes.ItemType, ByteReader, TBlockType> Loader { get; set; }
		public Func<IBlockJobSettings, Vector3Int, ItemTypes.ItemType, Colony, TBlockType> Adder { get; set; }

		// IMultiBlockEntityMapping
		public IEnumerable<ItemTypes.ItemType> TypesToRegister { get { return Settings.BlockTypes; } }

		static Type[] LoaderTypes = new Type[] { typeof(IBlockJobSettings), typeof(Vector3Int), typeof(ItemTypes.ItemType), typeof(ByteReader) };
		static Type[] AdderTypes = new Type[] { typeof(IBlockJobSettings), typeof(Vector3Int), typeof(ItemTypes.ItemType), typeof(Colony) };

		public BlockJobManager (IBlockJobSettings settings,
			Func<IBlockJobSettings, Vector3Int, ItemTypes.ItemType, ByteReader, TBlockType> loader = null,
			Func<IBlockJobSettings, Vector3Int, ItemTypes.ItemType, Colony, TBlockType> adder = null)
		{
			if (loader == null) {
				try {
					ConstructorInfo constructor = typeof(TBlockType).GetConstructor(LoaderTypes);
					ParameterExpression[] parameters = new[] {
						Expression.Parameter(typeof(IBlockJobSettings)),
						Expression.Parameter(typeof(Vector3Int)),
						Expression.Parameter(typeof(ItemTypes.ItemType)),
						Expression.Parameter(typeof(ByteReader)),
					};
					Loader = Expression.Lambda<Func<IBlockJobSettings, Vector3Int, ItemTypes.ItemType, ByteReader, TBlockType>>(
						Expression.New(constructor, parameters), parameters
					).Compile();
				} catch (Exception e) {
					Log.WriteException($"Type <{typeof(TBlockType)}> does not implement a constructor with arguments (IBlockJobSettings, Vector3Int, ItemTypes.ItemType, ByteReader). Required to work with the BlockJobManager.", e);
				}
			} else {
				Loader = loader;
			}

			if (adder == null) {
				try {
					ConstructorInfo constructor = typeof(TBlockType).GetConstructor(AdderTypes);
					ParameterExpression[] parameters = new[] {
						Expression.Parameter(typeof(IBlockJobSettings)),
						Expression.Parameter(typeof(Vector3Int)),
						Expression.Parameter(typeof(ItemTypes.ItemType)),
						Expression.Parameter(typeof(Colony)),
					};
					Adder = Expression.Lambda<Func<IBlockJobSettings, Vector3Int, ItemTypes.ItemType, Colony, TBlockType>>(
						Expression.New(constructor, parameters), parameters
					).Compile();
				} catch (Exception e) {
					Log.WriteException($"Type <{typeof(TBlockType)}> does not implement a constructor with arguments (IBlockJobSettings, Vector3Int, ItemTypes.ItemType, Colony). Required to work with the BlockJobManager.", e);
				}
			} else {
				Adder = adder;
			}

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
			if (Loader != null) {
				ServerManager.BlockEntityTracker.OnAddedEntity(blockPosition, Loader(Settings, blockPosition, ItemTypes.GetType(type), reader));
			}
		}

		public void OnPlaced (Players.Player player, Vector3Int blockPosition, ItemTypes.ItemType type)
		{
			if (player.ActiveColony == null) {
				// need a colony to set as parent of this block!
				ThreadManager.AssertIsMainThread();
				ServerManager.TryChangeBlock(blockPosition, 0, player, ServerManager.SetBlockFlags.None);
				return;
			}
			if (Adder != null) {
				ServerManager.BlockEntityTracker.OnAddedEntity(blockPosition, Adder(Settings, blockPosition, type, player.ActiveColony));
			}
		}

		public void OnRemoved (Vector3Int blockPosition)
		{
			ServerManager.BlockEntityTracker.OnRemoveEntity(blockPosition);
		}
	}
}
