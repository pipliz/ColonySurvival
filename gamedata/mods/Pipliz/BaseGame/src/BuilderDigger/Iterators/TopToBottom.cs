namespace Pipliz.Mods.BaseGame.Construction.Iterators
{
	public class TopToBottom : IIterationType
	{
		protected ConstructionArea area;
		protected Vector3Int positionMin;
		protected Vector3Int positionMax;

		protected Vector3Int cursor;
		protected Vector3Int iterationChunkLocation;
		protected int iterationIndex;


		public TopToBottom (ConstructionArea area)
		{
			this.area = area;

			positionMin = area.Minimum;
			positionMax = area.Maximum;

			iterationChunkLocation = new Vector3Int(positionMax.x & -16, positionMax.y & -16, positionMax.z & -16);
			iterationIndex = 16 * 16 * 16;
			MoveNext();
		}

		public Vector3Int CurrentPosition { get { return cursor; } }

		public bool IsInBounds (Vector3Int location)
		{
			return location.x >= positionMin.x && location.x <= positionMax.x
				&& location.y >= positionMin.y && location.y <= positionMax.y
				&& location.z >= positionMin.z && location.z <= positionMax.z;
		}

		public bool MoveNext ()
		{
			while (true) {
				--iterationIndex;

				if (iterationIndex < 0) {
					iterationIndex = 16 * 16 * 16 - 1;
					iterationChunkLocation.z -= 16;

					if (iterationChunkLocation.z < (positionMin.z & -16)) {
						iterationChunkLocation.z = (positionMax.z & -16);
						iterationChunkLocation.x -= 16;

						if (iterationChunkLocation.x < (positionMin.x & -16)) {
							iterationChunkLocation.x = (positionMax.x & -16);
							iterationChunkLocation.y -= 16;

							if (iterationChunkLocation.y < (positionMin.y & -16)) {
								cursor = Vector3Int.invalidPos;
								return false;
							}
						}
					}
				}

				cursor = IteratorHelper.ZOrderToPosition(iterationIndex).ToWorld(iterationChunkLocation);

				if (IsInBounds(cursor)) {
					return true;
				}
			}
		}
	}
}
