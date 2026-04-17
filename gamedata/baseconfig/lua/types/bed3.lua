local function MakeBed3(suffix, mesh)
	local typeName = "bed3" .. suffix
	local block = {
		baseType = {
			attachBehaviour = {
				{
					id = "ambientOcclusionFactor",
					strength = 0.4
				},
				{
					id = "buildableMultiblock",
					positions = { { 0, 0, 0 }, { -1, 0, 0 } }
				},
				{
					id = "bed",
					sleepForward = { 0, 1, 0 },
					sleepUp = { -1, 0, 0 },
					sleepOffset = { -0.35, -0.20, 0 },
					sleepAnimationState = "bed"
				},
				{
					id = "dependentBlocks",
					instances = {
						{ pos = { 1, 0, 0 }, type = "npcblockerair" },
						{ pos = { -1, 0, 0 }, type = "npcblockerair" }
					}
				}
			},
			attachmentProviders = {
				{
					min = { -1.5, -0.5, -0.5 },
					max = { -1.35, 0.74, 0.5 }
				}
			},
			attachmentRequirements = {
				{
					axis = { "Y" },
					min = { -0.48, -0.51, -0.48 },
					max = { 0.48, -0.49, 0.48 }
				},
				{
					axis = { "Y" },
					min = { 0.52, -0.51, -0.48 },
					max = { 1.48, -0.49, 0.48 }
				},
				{
					axis = { "Y" },
					min = { -1.48, -0.51, -0.48 },
					max = { -0.52, -0.49, 0.48 }
				}
			},
			colliders = {
				{ min = { -1.343, -0.5, -0.5 }, max = { 0.733, 0.1, 0.5 }, impactsPhysics = false, impactsBlockPlacement = true },
				{ min = { -1.499, -0.5, -0.5 }, max = { -1.335, 0.739, 0.5 }, impactsBlockPlacement = true },
				{ min = { 0.733, -0.5, -0.5 }, max = { 0.884, 0.47, 0.5 }, impactsBlockPlacement = true },
				{ min = { 0.944, -0.5, -0.372 }, max = { 1.471, 0.051, 0.372 }, impactsBlockPlacement = true }
			},
			isSolid = false,
			lightingTransparency = 160,
			mesh = mesh,
			movementCost = 4,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = "bed3",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = typeName
	}

	if suffix == "" then
		table.insert(block.baseType.attachBehaviour, 1, "builder_usable")
		table.insert(block.baseType.attachBehaviour, {
			id = "hint_paintable",
			types = { "paintstripped", "paintwhite", "paintred", "paintgreen", "paintblue" }
		})
		block.baseType.categories = {
			"decorative"
		}
		block.baseType.icon = "../textures/icons/bed3.png"
	end

	return block
end

return {
	MakeBed3("", "../meshes/bed3.glb"),
	MakeBed3("red", "../meshes/deco/bed3red.glb"),
	MakeBed3("green", "../meshes/deco/bed3green.glb"),
	MakeBed3("blue", "../meshes/deco/bed3blue.glb"),
	MakeBed3("white", "../meshes/deco/bed3white.glb")
}
