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
		protected JSONNode arguments;
		protected ConstructionAreaDefinition definition;
		protected bool isValid = true;

		public IConstructionType ConstructionType { get; set; }
		public IIterationType IterationType { get; set; }

		public static Dictionary<string, IConstructionLoader> constructionLoaders = new Dictionary<string, IConstructionLoader>();

		public virtual Colony Owner { get; protected set; }

		public virtual Vector3Int Minimum { get { return positionMin; } }
		public virtual Vector3Int Maximum { get { return positionMax; } }
		public virtual NPCBase NPC { get { return null; } set { } }
		public virtual Shared.EServerAreaType AreaType { get { return Shared.EServerAreaType.ConstructionArea; } }
		public virtual Shared.EAreaMeshType AreaTypeMesh { get { return Shared.EAreaMeshType.ThreeD; } }
		public virtual bool IsValid { get { return isValid && arguments != null && ConstructionType != null && IterationType != null; } }

		static ConstructionArea ()
		{
			RegisterLoader(new Loaders.BuilderLoader());
			RegisterLoader(new Loaders.DiggerLoader());
			RegisterLoader(new Loaders.DiggerSpecialLoader());
		}

		public ConstructionArea (ConstructionAreaDefinition definition, Colony owner, Vector3Int min, Vector3Int max)
		{
			this.definition = definition;
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
			definition.OnRemove(this);
			isValid = false;
		}

		public virtual void SaveAreaJob (JSONNode colonyRootNode)
		{
			if (arguments != null) {
				JSONNode identifierNode;
				if (!colonyRootNode.TryGetChild(definition.Identifier, out identifierNode)) {
					identifierNode = new JSONNode(NodeType.Array);
					colonyRootNode[definition.Identifier] = identifierNode;
				}

				JSONNode node = new JSONNode()
					.SetAs("x-", positionMin.x)
					.SetAs("y-", positionMin.y)
					.SetAs("z-", positionMin.z)
					.SetAs("xd", positionMax.x - positionMin.x)
					.SetAs("yd", positionMax.y - positionMin.y)
					.SetAs("zd", positionMax.z - positionMin.z)
					.SetAs("args", arguments);

				if (arguments.TryGetAs("constructionType", out string type) && constructionLoaders.TryGetValue(type, out IConstructionLoader jobCallbacks)) {
					jobCallbacks.SaveTypes(this, node);
				}

				identifierNode.AddToArray(node);
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
