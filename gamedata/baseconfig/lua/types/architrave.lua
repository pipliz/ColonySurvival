local function MakePillarBlocks (name, meshfolder)

	local blocks = {}
	local block = {
		name = "architrave" .. name,
		colliders = {
			{ min = { -0.5, -0.5, -0.5 }, max = { 0.5, 0.464, 0.5 } },
			{ min = { -0.65, 0.464, -0.65 }, max = { 0.65, 0.50, 0.65 } }
		},
		canBuildUpon = true,
		icon = "../textures/icons/architrave.png",
		lightingTransparency = 110,
		mesh = meshfolder .. "architrave.ply",
		isSolid = true,
		onPlaceAudio = "stonePlace",
		onRemoveAudio = "stoneDelete",
		onRemoveType = "architrave", 
		pathingImpact = "AsSolid",
		sideall = "neutral"
	}
	
	if name == "" then
		block.categories = {
			"decorative",
			"decorative_mesh"
		}
	end
	
	table.insert(blocks, block)
	
	return blocks
end

local allBlocks = {}

local function AddPillars(...)
	local blocks = MakePillarBlocks(...)
	for _, block in ipairs(blocks) do
		table.insert(allBlocks, block)
	end
end

AddPillars("", "../meshes/architrave/")
AddPillars("red", "../meshes/architravered/")
AddPillars("white", "../meshes/architravewhite/")
AddPillars("blue", "../meshes/architraveblue/")
AddPillars("green", "../meshes/architravegreen/")

return allBlocks
