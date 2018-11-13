using Jobs;
using NPC;
using Pipliz.JSON;

namespace Pipliz.Mods.BaseGame.Construction
{
	public class ConstructionArea : IAreaJob, IAreaJobSubArguments
	{
		protected Vector3Int positionMin;
		protected Vector3Int positionMax;

		protected bool isValid = true;

		protected IConstructionType constructionType;
		protected IIterationType iterationType;
		protected JSONNode arguments;

		protected static ConstructionAreaDefinition DefinitionInstance;

		public virtual Colony Owner { get; protected set; }

		public virtual Vector3Int Minimum { get { return positionMin; } }
		public virtual Vector3Int Maximum { get { return positionMax; } }
		public virtual NPCBase NPC { get { return null; } set { } }
		public virtual Shared.EAreaType AreaType { get { return constructionType == null ? Shared.EAreaType.Unknown : constructionType.AreaType; } }
		public virtual Shared.EAreaMeshType AreaTypeMesh { get { return constructionType == null ? Shared.EAreaMeshType.AutoSelect : constructionType.AreaTypeMesh; ; } }
		public virtual bool IsValid { get { return isValid && constructionType != null && iterationType != null; } }

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
			arguments = args;
			if (args == null) {
				Log.WriteWarning("Unexpected construction area args; null");
				return;
			}
			string type;
			if (args.TryGetAs("constructionType", out type)) {
				SetConstructionType(type, args);
			} else {
				Log.WriteWarning("Unexpected construction area args; no constructionType");
			}
		}

		public void SetConstructionType (string type, JSONNode args = null)
		{
			switch (type) {
				case "pipliz.digger":
					SetConstructionType(new Types.DiggerBasic());
					SetIterationType(new Iterators.TopToBottom(this));
					break;
				case "pipliz.diggerspecial":
					if (args != null) {
						ItemTypes.ItemType digTpe = ItemTypes.GetType(ItemTypes.IndexLookup.GetIndex(args.GetAsOrDefault("diggerBlockType", "air")));
						if (digTpe != null && digTpe.ItemIndex != 0) {
							SetConstructionType(new Types.DiggerSpecial(digTpe));
							SetIterationType(new Iterators.TopToBottom(this));
						}
					}
					break;
				case "pipliz.builder":
					if (args != null) {
						ItemTypes.ItemType buildType = ItemTypes.GetType(ItemTypes.IndexLookup.GetIndex(args.GetAsOrDefault("builderBlockType", "air")));
						if (buildType != null && buildType.ItemIndex != 0) {
							SetConstructionType(new Types.BuilderBasic(buildType));
							SetIterationType(new Iterators.BottomToTop(this));
						}
					}
					break;
				default:
					Log.WriteWarning("Unexpected construction type: {0}", type);
					break;
			}
		}

		public void SetConstructionType (IConstructionType type)
		{
			constructionType = type;
		}

		public void SetIterationType (IIterationType type)
		{
			iterationType = type;
		}

		public virtual void OnRemove ()
		{
			Definition.OnRemove(this);
			isValid = false;
		}

		public virtual void SaveAreaJob ()
		{
			JSONNode node = new JSONNode()
				.SetAs("min", (JSONNode)positionMin)
				.SetAs("max", (JSONNode)positionMax);

			if (arguments == null) {
				node.SetAs("arguments", "none");
			} else {
				node.SetAs("arguments", arguments);
			}

			Definition.SaveJob(Owner, node);
		}

		public virtual void DoJob (ConstructionJobInstance job, ref NPCBase.NPCState state)
		{
			if (constructionType != null) {
				constructionType.DoJob(iterationType, this, job, ref state);
			}
		}
	}
}
