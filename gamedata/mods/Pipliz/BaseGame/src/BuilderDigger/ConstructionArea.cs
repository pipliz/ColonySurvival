using Jobs;
using NPC;
using Pipliz.JSON;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.Construction
{
	public class ConstructionArea : IAreaJob, IAreaJobSubArguments
	{
		protected Vector3Int positionMin;
		protected Vector3Int positionMax;

		protected bool isValid = true;

		public IConstructionType ConstructionType { get; set; }
		public IIterationType IterationType { get; set; }

		protected JSONNode arguments;

		protected static ConstructionAreaDefinition DefinitionInstance;

		public static Dictionary<string, IConstructionLoader> constructionLoaders = new Dictionary<string, IConstructionLoader>();

		public virtual Colony Owner { get; protected set; }

		public virtual Vector3Int Minimum { get { return positionMin; } }
		public virtual Vector3Int Maximum { get { return positionMax; } }
		public virtual NPCBase NPC { get { return null; } set { } }
		public virtual Shared.EAreaType AreaType { get { return ConstructionType == null ? Shared.EAreaType.Unknown : ConstructionType.AreaType; } }
		public virtual Shared.EAreaMeshType AreaTypeMesh { get { return ConstructionType == null ? Shared.EAreaMeshType.AutoSelect : ConstructionType.AreaTypeMesh; ; } }
		public virtual bool IsValid { get { return isValid && arguments != null && ConstructionType != null && IterationType != null; } }

		public virtual IAreaJobDefinition Definition
		{
			get
			{
				if (DefinitionInstance == null) {
					foreach (var instance in AreaJobTracker.RegisteredAreaJobDefinitions) {
						if (instance.GetType() == typeof(ConstructionAreaDefinition)) {
							DefinitionInstance = (ConstructionAreaDefinition)instance;
						}
					}
				}
				return DefinitionInstance;
			}
		}

		static ConstructionArea ()
		{
			RegisterLoader(new Loaders.BuilderLoader());
			RegisterLoader(new Loaders.DiggerLoader());
			RegisterLoader(new Loaders.DiggerSpecialLoader());
		}

		public ConstructionArea (Colony owner, Vector3Int min, Vector3Int max)
		{
			min.y = Math.Max(1, min.y);
			positionMin = min;
			positionMax = max;
			isValid = max != Vector3Int.invalidPos;
			Owner = owner;
		}

		public void SetArgument (JSONNode args)
		{
			if (args == null) {
				Log.WriteWarning("Unexpected construction area args; null");
				return;
			}
			arguments = args;
			if (args.TryGetAs("constructionType", out string type)) {
				if (constructionLoaders.TryGetValue(type, out IConstructionLoader jobCallbacks)) {
					jobCallbacks.ApplyTypes(this, args);
				} else {
					Log.WriteWarning("Unexpected construction type: {0}", type);
				}
			} else {
				Log.WriteWarning("Unexpected construction area args; no constructionType set");
			}
		}

		public static void RegisterLoader (IConstructionLoader type)
		{
			constructionLoaders[type.JobName] = type;
		}

		public virtual void OnRemove ()
		{
			Definition.OnRemove(this);
			isValid = false;
		}

		public virtual void SaveAreaJob ()
		{
			if (arguments != null) {
				JSONNode node = new JSONNode()
					.SetAs("min", (JSONNode)positionMin)
					.SetAs("max", (JSONNode)positionMax)
					.SetAs("arguments", arguments);

				if (arguments.TryGetAs("constructionType", out string type) && constructionLoaders.TryGetValue(type, out IConstructionLoader jobCallbacks)) {
					jobCallbacks.SaveTypes(this, node);
				}

				Definition.SaveJob(Owner, node);
			}
		}

		public virtual void DoJob (ConstructionJobInstance job, ref NPCBase.NPCState state)
		{
			if (ConstructionType != null) {
				ConstructionType.DoJob(IterationType, this, job, ref state);
			}
		}
	}
}
