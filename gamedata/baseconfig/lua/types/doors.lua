local function MakeDoor (suffix, meshOpenLeft, meshOpenRight, meshClosedLeft, meshClosedRight, meshOpenSingle, meshClosedSingle)
	-- open left door
	local blocks = {}
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{
					id = "door",
					onOpenClip = "dooropen",
					onCloseClip = "doorclose",
					closedType = "doordoubleclosedleft" .. suffix,
					npcUsable = true,
					autoclose = 30,
					health = 175,
					healthRegen = 2
				}
			},
			colliders = {
				{
					max = {0.35, 1.4, -0.30},
					min = {-0.6, -0.5, -0.45},
					impactsBlockPlacement = true
				}
			},
			lightingTransparency = 230,
			mesh = meshOpenLeft,
			needsBase = true,
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = "doorclosed",
			pathingImpact = "AsDoor",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "doordoubleopenleft" .. suffix
	})
	
	-- open right door
	table.insert(blocks,
	{
		baseType = {
			attachBehaviour = {
				{
					id = "door",
					onOpenClip = "dooropen",
					onCloseClip = "doorclose",
					closedType = "doordoubleclosedright" .. suffix,
					npcUsable = true,
					autoclose = 30,
					health = 175,
					healthRegen = 2
				}
			},
			colliders = {
				{
					max = {0.35, 1.4, 0.45},
					min = {-0.6, -0.5, 0.3},
					impactsBlockPlacement = true
				}
			},
			lightingTransparency = 230,
			mesh = meshOpenRight,
			needsBase = true,
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = "doorclosed",
			pathingImpact = "AsDoor",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "doordoubleopenright" .. suffix
	})
	
	-- closed left door
	table.insert(blocks,
	{
		baseType = {
			colliders = {
				{
					max = {0.4, 1.5, 0.5},
					min = {0.3, -0.5, -0.5},
					impactsBlockPlacement = true
				}
			},
			lightingTransparency = 230,
			mesh = meshClosedLeft,
			needsBase = true,
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = "doorclosed",
			pathingImpact = "AsDoor",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "doordoubleclosedleft" .. suffix
	})
	
	-- closed right door
	table.insert(blocks,
	{
		baseType = {
			colliders = {
				{
					max = {0.4, 1.5, 0.5},
					min = {0.3, -0.5, -0.5},
					impactsBlockPlacement = true
				}
			},
			lightingTransparency = 230,
			mesh = meshClosedRight,
			needsBase = true,
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = "doorclosed",
			pathingImpact = "AsDoor",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "doordoubleclosedright" .. suffix
	})

	-- open door single
	local baseOpen = {
		baseType = {
			attachBehaviour = {
				{
					id = "door",
					onOpenClip = "dooropen",
					onCloseClip = "doorclose",
					closedType = "doorclosed" .. suffix,
					npcUsable = true,
					autoclose = 30,
					health = 175,
					healthRegen = 2
				},
				{
					id = "door_smart",
					group = "default",
					typesingleclosed = "doorclosed" .. suffix,
					typesingleopen = "dooropen" .. suffix,
					typedoubleleftopen = "doordoubleopenleft" .. suffix,
					typedoubleleftclosed = "doordoubleclosedleft" .. suffix,
					typedoublerightopen = "doordoubleopenright" .. suffix,
					typedoublerightclosed = "doordoubleclosedright" .. suffix
				}
			},
			colliders = {
				{
					max = { 0.35, 1.4, -0.30 },
					min = { -0.5, -0.5, -0.45 },
					impactsBlockPlacement = true
				}
			},
			lightingTransparency = 230,
			mesh = meshOpenSingle,
			needsBase = true,
			isSolid = false,
			onRemoveType = "doorclosed",
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsDoor",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "dooropen" .. suffix
	}
	
	if (suffix == "") then
		baseOpen.baseType.icon = "../textures/icons/door.png"
	end
	
	table.insert(blocks, baseOpen)
	
	-- closed door single
	local baseClosed = {
		baseType = {
			colliders = {
				{
					max = { 0.4, 1.5, 0.5 },
					min = { 0.3, -0.5, -0.5 },
					impactsBlockPlacement = true
				}
			},
			lightingTransparency = 230,
			mesh = meshClosedSingle,
			needsBase = true,
			isSolid = false,
			onRemoveType = "doorclosed",
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsDoor",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "doorclosed" .. suffix
	}
	
	if suffix == "" then
		baseClosed.baseType.attachBehaviour = {
			{
				id = "hint_paintable",
				types = { "paintstripped", "paintwhite", "paintred", "paintgreen", "paintblue" }
			}
		}
		baseClosed.baseType.categories = { "essential" }
		baseClosed.baseType.icon = "../textures/icons/door.png"
	end
	
	table.insert(blocks, baseClosed)
	
	return blocks
end

local allBlocks = {}

local function AddDoors(...)
	local blocks = MakeDoor(...)
	for _, block in ipairs(blocks) do
		table.insert(allBlocks, block)
	end
end

AddDoors("", "../meshes/meshes_doordouble/doordoubleopenleft.ply", "../meshes/meshes_doordouble/doordoubleopenright.ply", "../meshes/meshes_doordouble/doordoubleclosedleft.ply","../meshes/meshes_doordouble/doordoubleclosedright.ply", "../meshes/meshes_doorsingle/dooropen.ply", "../meshes/meshes_doorsingle/doorclosed.ply")

AddDoors("red", "../meshes/meshes_doordoublered/doordoubleopenleft.ply", "../meshes/meshes_doordoublered/doordoubleopenright.ply", "../meshes/meshes_doordoublered/doordoubleclosedleft.ply","../meshes/meshes_doordoublered/doordoubleclosedright.ply", "../meshes/meshes_doorsingle/dooropen_red.ply", "../meshes/meshes_doorsingle/doorclosed_red.ply")

AddDoors("blue", "../meshes/meshes_doordoubleblue/doordoubleopenleft.ply", "../meshes/meshes_doordoubleblue/doordoubleopenright.ply", "../meshes/meshes_doordoubleblue/doordoubleclosedleft.ply","../meshes/meshes_doordoubleblue/doordoubleclosedright.ply", "../meshes/meshes_doorsingle/dooropen_blue.ply", "../meshes/meshes_doorsingle/doorclosed_blue.ply")

AddDoors("white", "../meshes/meshes_doordoublewhite/doordoubleopenleft.ply", "../meshes/meshes_doordoublewhite/doordoubleopenright.ply", "../meshes/meshes_doordoublewhite/doordoubleclosedleft.ply","../meshes/meshes_doordoublewhite/doordoubleclosedright.ply", "../meshes/meshes_doorsingle/dooropen_white.ply", "../meshes/meshes_doorsingle/doorclosed_white.ply")

AddDoors("green", "../meshes/meshes_doordoublegreen/doordoubleopenleft.ply", "../meshes/meshes_doordoublegreen/doordoubleopenright.ply", "../meshes/meshes_doordoublegreen/doordoubleclosedleft.ply","../meshes/meshes_doordoublegreen/doordoubleclosedright.ply", "../meshes/meshes_doorsingle/dooropen_green.ply", "../meshes/meshes_doorsingle/doorclosed_green.ply")

return allBlocks
