local function MakeBunkbed(suffix, mesh)
	local typeName = "bunkbed" .. suffix
	local block = {
		baseType = {
			attachBehaviour = {
				{
					id = "ambientOcclusionFactor",
					strength = 0.4
				},
				{
					id = "bed",
					sleepForward = { 0, 1, 0 },
					sleepUp = { -1, 0, 0 },
					sleepOffset = { -0.35, -0.17, 1 },
					sleepAnimationState = "bed"
				},
				{
					id = "npcOffsetOverride",
					value = 0
				},
				{
					id = "dependentBlocks",
					instances = {
						{ pos = { -1, 0, 1 }, type = "npcblockerair" },
						{ pos = { 0, 0, 1 }, type = "npcblockerair" },
						{ pos = { 1, 0, 1 }, type = "npcblockerair" },
						{ pos = { 1, 0, 0 }, type = "bunkbedtopx+", typeCondition = typeName .. "x+" },
						{ pos = { 1, 0, 0 }, type = "bunkbedtopx-", typeCondition = typeName .. "x-" },
						{ pos = { 1, 0, 0 }, type = "bunkbedtopz+", typeCondition = typeName .. "z+" },
						{ pos = { 1, 0, 0 }, type = "bunkbedtopz-", typeCondition = typeName .. "z-" }
					}
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
					min = { -1.48, -0.51, 0.52 },
					max = { -0.52, -0.49, 1.48 }
				},
				{
					axis = { "Y" },
					min = { -0.48, -0.51, 0.52 },
					max = { 0.48, -0.49, 1.48 }
				},
				{
					axis = { "Y" },
					min = { 0.52, -0.51, 0.52 },
					max = { 1.48, -0.49, 1.48 }
				}
			},
			colliders = {
				{ min = { -1.446, -0.5, 0.547 }, max = { 0.823, 0.144, 1.453 }, impactsPhysics = false, impactsBlockPlacement = true },
				{ min = { -1.499, 1.239, 0.513 }, max = { 0.844, 1.598, 1.489 }, impactsBlockPlacement = true },
				{ min = { 0.917, -0.5, 0.025 }, max = { 1.427, 0.189, 1.435 }, impactsBlockPlacement = true },
				{ min = { 0.077, -0.5, 0.08 }, max = { 0.833, 0.251, 0.386 }, impactsBlockPlacement = true },
				{ min = { -1.455, -0.5, 0.546 }, max = { -1.371, 2.033, 0.651 }, impactsBlockPlacement = true },
				{ min = { -1.473, -0.5, 1.363 }, max = { -1.363, 2.032, 1.466 }, impactsBlockPlacement = true },
				{ min = { 0.716, -0.5, 1.357 }, max = { 0.82, 2.034, 1.463 }, impactsBlockPlacement = true },
				{ min = { 0.049, 0.285, 0.242 }, max = { 0.812, 1.251, 0.5 }, impactsBlockPlacement = true }
			},
			isSolid = false,
			lightingTransparency = 160,
			mesh = mesh,
			movementCost = 4,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = "bunkbed",
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
		block.baseType.icon = "../textures/icons/bunkbed.png"
	end

	return block
end

return {
	MakeBunkbed("", "../meshes/deco/bunkbed.glb"),
	MakeBunkbed("red", "../meshes/deco/bunkbedred.glb"),
	MakeBunkbed("green", "../meshes/deco/bunkbedgreen.glb"),
	MakeBunkbed("blue", "../meshes/deco/bunkbedblue.glb"),
	MakeBunkbed("white", "../meshes/deco/bunkbedwhite.glb")
}
