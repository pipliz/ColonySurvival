using Server.Science;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Pipliz.APIProvider.Science
{
	public class BaseResearchable : IResearchable
	{
		protected List<string> dependencies;
		protected List<InventoryItem> iterationRequirements;
		protected string key;
		protected string icon;
		protected int iterationCount = -1;

		protected void AddDependency (string dependency)
		{
			if (dependencies == null) {
				dependencies = new List<string>();
			}
			dependencies.Add(dependency);
		}

		protected void AddIterationRequirement (InventoryItem item)
		{
			if (iterationRequirements == null) {
				iterationRequirements = new List<InventoryItem>();
			}
			iterationRequirements.Add(item);
		}

		protected void AddIterationRequirement (ushort type, int amount = 1)
		{
			AddIterationRequirement(new InventoryItem(type, amount));
		}

		protected void AddIterationRequirement (string type, int amount = 1)
		{
			AddIterationRequirement(new InventoryItem(ItemTypes.IndexLookup.GetIndex(type), amount));
		}

		#region IResearchable
		public virtual string GetKey ()
		{
			Assert.IsNotNull(key, string.Format("BaseResearchable.key was not assigned for {0}", GetType()));
			return key;
		}

		public virtual IList<string> GetDependencies ()
		{
			return dependencies;
		}

		public virtual IList<InventoryItem> GetScienceRequirements ()
		{
			Assert.IsNotNull(iterationRequirements, string.Format("BaseResearchable.iterationRequirements was not assigned for {0}", key));
			Assert.IsTrue(iterationRequirements.Count > 0, string.Format("BaseResearchable.iterationRequirements was empty for {0}, not allowed", key));
			return iterationRequirements;
		}

		public virtual int GetResearchIterationCount ()
		{
			Assert.IsTrue(iterationCount > 0, string.Format("BaseResearchable.iterationCount was <= 0 for {0}", key));
			return iterationCount;
		}

		public virtual void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			Log.Write("{0} completed {1} ({2})", manager.Player, key, reason);
		}

		public virtual string GetIcon ()
		{
			Assert.IsNotNull(icon, string.Format("BaseResearch.icon was not assigned for {0}", key));
			return icon;
		}
		#endregion
	}
}
