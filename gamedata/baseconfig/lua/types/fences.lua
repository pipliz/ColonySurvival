local function MakeBlocks (name, meshfolder)
	local blocks = {}
	
	local rootName
	if (name == "") then
		rootName = "fencestripped"
	else
		rootName = "fence" .. name 
	end
	
	table.insert(blocks, {
		baseType = {
			attachBehaviour = {
				{
					id = "fence",
					fencemiddlei = "fencemiddlei" .. name,
					fencemiddlex = "fencemiddlex" .. name,
					fencemiddlet = "fencemiddlet" .. name,
					fencemiddlecorner = "fencemiddlecorner" .. name,
					fencemiddlemiddle = "fencemiddlemiddle" .. name,
					fencemiddleend = "fencemiddleend" .. name,
					fencesolomiddle = "fencesolomiddle" .. name,
					fencesolobottom = "fencesolobottom" .. name,
					fencesolotop = "fencesolotop" .. name
				}
			},
			onRemoveType = "fence"
		},
		generateType = "type",
		typeName = rootName
	})
	
	table.insert(blocks, {
		baseType = {
			parentType = rootName,
			attachmentProviders = {
				{ min = { -0.17538, 0.45274, -0.17538 }, max = { 0.17538, 0.5, 0.17538 } },
				{ min = { -0.13261, -0.5, -0.13261 }, max = { 0.13261, -0.42914, 0.13261 } }
			},
			canBuildUpon = true,
			colliders = {
				{ min = { -0.2, -0.5, -0.2 }, max = { 0.2, 0.5, 0.2 } }
			},
			lightingTransparency = 190,
			mesh = meshfolder .. "fencemiddlei.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral"
		},
		generateType = "type",
		typeName = "fencemiddlei" .. name
	})
	
	table.insert(blocks, {
		baseType = {
			parentType = rootName,
			attachmentProviders = {
				{ min = { -0.17538, 0.45274, -0.17538 }, max = { 0.17538, 0.5, 0.17538 } }
			},
			canBuildUpon = true,
			colliders = {
				{ min = { -0.2, -0.5, -0.2 }, max = { 0.2, 0.5, 0.2 } }
			},
			lightingTransparency = 190,
			mesh = meshfolder .. "fencesolotop.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral"
		},
		generateType = "type",
		typeName = "fencesolotop" .. name
	})
	
	
	table.insert(blocks, {
		baseType = {
			parentType = rootName,
			attachmentProviders = {
				{ min = { -0.13261, -0.5, -0.13261 }, max = { 0.13261, -0.42914, 0.13261 } }
			},
			canBuildUpon = true,
			colliders = {
				{ min = { -0.2, -0.5, -0.2 }, max = { 0.2, 0.5, 0.2 } }
			},
			lightingTransparency = 190,
			mesh = meshfolder .. "fencesolobottom.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral"
		},
		generateType = "type",
		typeName = "fencesolobottom" .. name
	})

	table.insert(blocks, {
		baseType = {
			parentType = rootName,
			canBuildUpon = true,
			colliders = {
				{ min = { -0.2, -0.5, -0.2 }, max = { 0.2, 0.5, 0.2 } }
			},
			lightingTransparency = 190,
			mesh = meshfolder .. "fencesolomiddle.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral"
		},
		generateType = "type",
		typeName = "fencesolomiddle" .. name
	})

	table.insert(blocks, {
		baseType = {
			parentType = rootName,
			attachmentProviders = {
				{ min = { -0.17538, 0.45274, -0.17538 }, max = { 0.17538, 0.5, 0.17538 } },
				{ min = { -0.13261, -0.5, -0.13261 }, max = { 0.13261, -0.42914, 0.13261 } }
			},
			canBuildUpon = true,
			colliders = {
				{ min = { -0.2, -0.5, -0.5 }, max = { 0.2, 0.5, 0.5 } },
				{ min = { -0.5, -0.5, -0.2 }, max = { 0.5, 0.5, 0.2 } }
			},
			lightingTransparency = 190,
			mesh = meshfolder .. "fencemiddlex.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral"
		},
		generateType = "type",
		typeName = "fencemiddlex" .. name
	})
	
	table.insert(blocks, {
		baseType = {
			parentType = rootName,
			attachmentProviders = {
				{ min = { -0.17538, 0.45274, -0.17538 }, max = { 0.17538, 0.5, 0.17538 } },
				{ min = { -0.13261, -0.5, -0.13261 }, max = { 0.13261, -0.42914, 0.13261 } }
			},
			canBuildUpon = true,
			colliders = {
				{ min = { -0.2, -0.5, -0.2 }, max = { 0.2, 0.5, 0.5 } },
				{ min = { -0.5, -0.5, -0.2 }, max = { 0.2, 0.5, 0.2 } }
			},
			lightingTransparency = 190,
			mesh = meshfolder .. "fencemiddlecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "fencemiddlecorner" .. name
	})

	table.insert(blocks, {
		baseType = {
			parentType = rootName,
			attachmentProviders = {
				{ min = { -0.17538, 0.45274, -0.17538 }, max = { 0.17538, 0.5, 0.17538 } },
				{ min = { -0.13261, -0.5, -0.13261 }, max = { 0.13261, -0.42914, 0.13261 } }
			},
			canBuildUpon = true,
			colliders = {
				{ min = { -0.2, -0.5, -0.5 }, max = { 0.2, 0.5, 0.2 } },
				{ min = { -0.5, -0.5, -0.2 }, max = { 0.5, 0.5, 0.2 } }
			},
			lightingTransparency = 190,
			mesh = meshfolder .. "fencemiddlet.ply",
			meshRotationEuler = { x = 0, y = 90, z = 0 },
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "fencemiddlet" .. name
	})

	table.insert(blocks, {
		baseType = {
			parentType = rootName,
			attachmentProviders = {
				{ min = { -0.17538, 0.45274, -0.17538 }, max = { 0.17538, 0.5, 0.17538 } },
				{ min = { -0.13261, -0.5, -0.13261 }, max = { 0.13261, -0.42914, 0.13261 } }
			},
			canBuildUpon = true,
			colliders = {
				{ min = { -0.5, -0.5, -0.2 }, max = { 0.5, 0.5, 0.2 } }
			},
			lightingTransparency = 190,
			mesh = meshfolder .. "fencemiddlemiddle.ply",
			meshRotationEuler = { x = 0, y = 90, z = 0 },
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "fencemiddlemiddle" .. name
	})

	table.insert(blocks, {
		baseType = {
			parentType = rootName,
			attachmentProviders = {
				{ min = { -0.17538, 0.45274, -0.17538 }, max = { 0.17538, 0.5, 0.17538 } },
				{ min = { -0.13261, -0.5, -0.13261 }, max = { 0.13261, -0.42914, 0.13261 } }
			},
			canBuildUpon = true,
			colliders = {
				{ min = { -0.5, -0.5, -0.2 }, max = { 0.2, 0.5, 0.2 } }
			},
			lightingTransparency = 190,
			mesh = meshfolder .. "fencemiddleend.ply",
			meshRotationEuler = { x = 0, y = 270, z = 0 },
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "fencemiddleend" .. name
	})
	
	return blocks
end

local allBlocks = {}

local function AddBlocks(...)
	local blocks = MakeBlocks(...)
	for _, block in ipairs(blocks) do
		table.insert(allBlocks, block)
	end
end

AddBlocks("", "../meshes/meshes_fence/")
AddBlocks("red", "../meshes/meshes_fencered/")
AddBlocks("white", "../meshes/meshes_fencewhite/")
AddBlocks("blue", "../meshes/meshes_fenceblue/")
AddBlocks("green", "../meshes/meshes_fencegreen/")

return allBlocks
