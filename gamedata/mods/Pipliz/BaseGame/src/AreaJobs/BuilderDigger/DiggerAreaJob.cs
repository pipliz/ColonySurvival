using NPC;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using BlockTypes.Builtin;
	using JSON;

	public class DiggerAreaJob : IAreaJob
	{
		protected Vector3Int positionMin;
		protected Vector3Int positionMax;

		protected Vector3Int iterationChunk0;
		protected Vector3Int iterationChunk1;
		protected Vector3Int iterationChunk2;
		protected Vector3Int iterationPosition;

		protected Players.Player owner;
		protected bool isValid = true;

		protected static DiggerAreaDefinition DefinitionInstance;

		public virtual Vector3Int Minimum { get { return positionMin; } }
		public virtual Vector3Int Maximum { get { return positionMax; } }
		public virtual NPCBase UsedNPC { get { return null; } }
		public virtual Players.Player Owner { get { return owner; } }
		public virtual Shared.EAreaType AreaType { get { return Shared.EAreaType.ThreeD; } }
		public virtual bool IsValid { get { return isValid; } }

		public virtual IAreaJobDefinition Definition
		{
			get
			{
				if (DefinitionInstance == null) {
					foreach (var instance in AreaJobTracker.RegisteredAreaJobDefinitions) {
						if (instance.GetType() == typeof(DiggerAreaDefinition)) {
							DefinitionInstance = (DiggerAreaDefinition)instance;
						}
					}
				}
				return DefinitionInstance;
			}
		}


		public DiggerAreaJob (Players.Player owner, Vector3Int min, Vector3Int max)
		{
			positionMin = min;
			positionMax = max;
			iterationChunk2 = new Vector3Int(min.x & -4, max.y & -4, min.z & -4);
			iterationChunk1 = new Vector3Int(min.x & -8, max.y & -8, min.z & -8);
			iterationChunk0 = new Vector3Int(min.x & -16, max.y & -16, min.z & -16);
			iterationPosition = Vector3Int.zero;
			this.owner = owner;
		}

		public virtual void OnRemove ()
		{
			Definition.OnRemove(this);
			isValid = false;
		}

		public virtual void SaveAreaJob ()
		{
			Definition.SaveJob(owner, new JSONNode()
				.SetAs("min", (JSONNode)positionMin)
				.SetAs("max", (JSONNode)positionMax));
		}

		public virtual void DoJob (ref NPCBase.NPCState state)
		{
			while (iterationChunk0.y >= (positionMin.y & -16)) {
				while (iterationChunk0.x <= (positionMax.x & -16)) {
					while (iterationChunk0.z <= (positionMax.z & -16)) {

						while (iterationChunk1.y >= iterationChunk0.y) {
							while (iterationChunk1.x < iterationChunk0.x + 16) {
								while (iterationChunk1.z < iterationChunk0.z + 16) {

									while (iterationChunk2.y >= iterationChunk1.y) {
										while (iterationChunk2.x < iterationChunk1.x + 8) {
											while (iterationChunk2.z < iterationChunk1.z + 8) {

												while (iterationPosition.y >= 0) {
													while (iterationPosition.x < 4) {
														while (iterationPosition.z < 4) {
															Vector3Int pos = iterationChunk2 + iterationPosition;
															if (DoJobAt(ref state, ref pos)) {
																return;
															}
														}
														iterationPosition.z = 0;
														iterationPosition.x++;
													}
													iterationPosition.x = 0;
													iterationPosition.y--;
												}

												// completed 4x4x4 box, increment z & reset iteration position
												iterationPosition = new Vector3Int(0, 4, 0);
												iterationChunk2.z += 4;
											}
											// completed 4x4x4 box row, increment x & reset z to start the new row
											iterationChunk2.x += 4;
											iterationChunk2.z = iterationChunk1.z;
										}
										// completed 4x4x4 box layer, decrement y & reset x to start the new layer
										iterationChunk2.y -= 4;
										iterationChunk2.x = iterationChunk1.x;
									}

									// completed 8x8x8 box, increment z & reset sub-boxes
									iterationChunk1.z += 8;
									iterationChunk2 = iterationChunk1.Add(0, 4, 0);
								}

								// completed 8x8x8 box row, increment x & reset z to start the new row
								iterationChunk1.x += 8;
								iterationChunk1.z = iterationChunk0.z;
								iterationChunk2 = iterationChunk1.Add(0, 4, 0);
							}

							// completed 8x8x8 box layer, restart at lower level
							iterationChunk1.x = iterationChunk0.x;
							iterationChunk1.y -= 8;
							iterationChunk2 = iterationChunk1.Add(0, 4, 0);
						}

						// completed 16x16x16 chunk, increment z & reset 8x8x8 cube position
						iterationChunk0.z += 16;
						iterationChunk1 = iterationChunk0.Add(0, 8, 0);
						iterationChunk2 = iterationChunk0.Add(0, 8 + 4, 0);
					}
					// completed 16x16x16 chunk row, increment x & reset z to start the new row
					iterationChunk0.z = positionMin.z & -16;
					iterationChunk0.x += 16;
					iterationChunk1 = iterationChunk0.Add(0, 8, 0);
					iterationChunk2 = iterationChunk0.Add(0, 8 + 4, 0);
				}
				// completed 16x16x16 chunk layer, decrement y
				iterationChunk0.x = positionMin.x & -16;
				iterationChunk0.y -= 16;
				iterationChunk1 = iterationChunk0.Add(0, 8, 0);
				iterationChunk2 = iterationChunk0.Add(0, 8 + 4, 0);
			}

			// completed area, remove it
			state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorIdle));
			AreaJobTracker.RemoveJob(this);
		}

		bool DoJobAt (ref NPCBase.NPCState state, ref Vector3Int jobPosition)
		{
			if (jobPosition.x > positionMax.x
				|| jobPosition.y > positionMax.y
				|| jobPosition.z > positionMax.z
				|| jobPosition.x < positionMin.x
				|| jobPosition.y < positionMin.y
				|| jobPosition.z < positionMin.z) {
				iterationPosition.z++;
				return false;
			}
			ushort foundType;
			if (World.TryGetTypeAt(jobPosition, out foundType)) {
				if (foundType != 0 && foundType != BuiltinBlocks.Water) {
					if (ServerManager.TryChangeBlock(jobPosition, 0, ServerManager.SetBlockFlags.DefaultAudio)) {
						iterationPosition.z++;
						state.SetIndicator(new Shared.IndicatorState(Random.NextFloat(2.5f, 3.5f), foundType));
					} else {
						state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorMissing, true, false));
					}
					return true;
				}
				iterationPosition.z++;
				return false;
			} else {
				state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorMissing, true, false));
				return true;
			}
		}
	}
}
