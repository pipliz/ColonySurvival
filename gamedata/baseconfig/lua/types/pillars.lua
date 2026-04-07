local function MakePillarBlocks (name, meshfolder)
	local blocks = {}
	
	local behaviour = {
		{
			id = "pillar",
			top = "pillartop".. name,
			mid = "pillarmiddle" .. name,
			bot = "pillarbottom" .. name,
			full = "pillartopbottom" .. name
		}
	}
	
	table.insert(blocks, {
		name = "pillartopbottom" .. name,
		attachmentProviders = {
			{ min = { -0.5, 0.4, -0.5 }, max = { 0.5, 0.5, 0.5 } }
		},
		colliders = {
			{ min = { -0.55, -0.5, -0.55 }, max = { 0.55, 0.5, 0.55 } }
		},
		attachBehaviour = behaviour,
		canBuildUpon = true,
		isSolid = false,
		lightingTransparency = 100,
		mesh = meshfolder .. "pillartopbottom.ply",
		onPlaceAudio = "stonePlace",
		onRemoveAudio = "stoneDelete",
		onRemoveType = "pillar",
		pathingImpact = "AsSolid",
		sideall = "neutral"
	})

	table.insert(blocks, {
		name = "pillartop" .. name,
		attachmentProviders = {
			{ min = { -0.5, 0.4, -0.5 }, max = { 0.5, 0.5, 0.5 } }
		},
		colliders = {
			{ min = { -0.3, -0.5, -0.3 }, max = { 0.3, 0.4, 0.3 } },
			{ min = { -0.5, 0.3, -0.5 }, max = { 0.5, 0.5, 0.5 } }
		},
		attachBehaviour = behaviour,
		canBuildUpon = true,
		lightingTransparency = 145,
		mesh = meshfolder .. "pillartop.ply",
		isSolid = false,
		onPlaceAudio = "stonePlace",
		onRemoveAudio = "stoneDelete",
		onRemoveType = "pillar",
		pathingImpact = "AsSolid",
		sideall = "neutral"
	})

	table.insert(blocks, {
		name = "pillarbottom" .. name,
		colliders = {
			{ min = { -0.3, -0.4, -0.3 }, max = { 0.3, 0.5, 0.3 } },
			{ min = { -0.5, -0.5, -0.5 }, max = { 0.5, -0.3, 0.5 } }
		},
		attachBehaviour = behaviour,
		canBuildUpon = true,
		lightingTransparency = 145,
		mesh = meshfolder .. "pillarbottom.ply",
		isSolid = false,
		onPlaceAudio = "stonePlace",
		onRemoveAudio = "stoneDelete",
		onRemoveType = "pillar",
		pathingImpact = "AsUntouchableSolid",
		sideall = "neutral"
	})

	table.insert(blocks, {
		name = "pillarmiddle" .. name,
		colliders = {
			{ min = { -0.3, -0.5, -0.3 }, max = { 0.3, 0.5, 0.3 } }
		},
		attachBehaviour = behaviour,
		canBuildUpon = true,
		lightingTransparency = 145,
		mesh = meshfolder .. "pillarmiddle.ply",
		isSolid = false,
		onPlaceAudio = "stonePlace",
		onRemoveAudio = "stoneDelete",
		onRemoveType = "pillar",
		pathingImpact = "AsUntouchableSolid",
		sideall = "neutral"
	})
		
	return blocks
end

local allBlocks = {}

local function AddPillars(...)
	local blocks = MakePillarBlocks(...)
	for _, block in ipairs(blocks) do
		table.insert(allBlocks, block)
	end
end

AddPillars("", "../meshes/pillar/")
AddPillars("red", "../meshes/pillarred/")
AddPillars("white", "../meshes/pillarwhite/")
AddPillars("blue", "../meshes/pillarblue/")
AddPillars("green", "../meshes/pillargreen/")

return allBlocks
