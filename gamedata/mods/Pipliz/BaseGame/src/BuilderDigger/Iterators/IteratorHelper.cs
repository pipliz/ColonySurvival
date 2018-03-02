using UnityEngine.Assertions;

namespace Pipliz.Mods.BaseGame.Construction.Iterators
{
	public static class IteratorHelper 
	{
		public static Vector3Byte ZOrderToPosition (int index)
		{
			Assert.IsTrue(index >= 0 && index < 16 * 16 * 16);
			return new Vector3Byte(
				ZOrderDecoderHelper(index),
				ZOrderDecoderHelper(index >> 1),
				ZOrderDecoderHelper(index >> 2)
			);
		}

		public static int ZOrderDecoderHelper (int x)
		{
			// x = --3- -2-- 1--0
			// ~ = --3- 32-2 1-10
			// ~ = ---- ---- 3210
			Assert.IsTrue(x >= 0 && x < 16 * 16 * 16);
			x &= 0x249;
			x = (x ^ (x >> 2)) & 0x0c3;
			x = (x ^ (x >> 4)) & 0x00f;
			return x;
		}
	}
}
