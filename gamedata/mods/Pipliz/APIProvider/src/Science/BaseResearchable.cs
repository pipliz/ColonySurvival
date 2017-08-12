using Server.Science;
using System;
using System.Collections.Generic;

namespace Pipliz.APIProvider.Science
{
	public class BaseResearchable : IResearchable
	{
		protected List<string> dependencies;

		public virtual string GetKey ()
		{
			throw new NotImplementedException();
		}

		public virtual IList<string> GetDependencies ()
		{
			return dependencies;
		}

		protected void AddDependency (string dependency)
		{
			if (dependencies == null) {
				dependencies = new List<string>();
			}
			dependencies.Add(dependency);
		}
	}
}
