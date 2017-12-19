using Server.Science;
using System;
using System.Collections.Generic;

namespace Pipliz.Mods.APIProvider.Science
{
	public static class ResearchableManager
	{
		static List<Type> toRegister = new List<Type>();

		public static void Add (Type type)
		{
			toRegister.Add(type);
		}

		public static void Register ()
		{
			for (int i = 0; i < toRegister.Count; i++) {
				var researchable = Activator.CreateInstance(toRegister[i]) as IResearchable;
				if (researchable != null) {
					ScienceManager.RegisterResearchable(researchable);
				}
			}
		}
	}
}
