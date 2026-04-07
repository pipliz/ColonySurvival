local function try_place(x, y, z, t)
	if (args.clicktype == "Right") then
		world_trysettypeat(x, y, z, t)
	else
		showpreview(x, y, z, t, "green")
	end
end

if (args.clicktype == "Left" or args.hittype ~= "Block") then return nil end

if (args.blockBuildCurrentType ~= "air") then return nil end

local typeToPlace

if args.blockHitSide == "Y-" then
	typeToPlace = types_applyrotation("signroof", args.rotation)
elseif args.blockHitSide == "Y+" then
	typeToPlace = types_applyrotation("signground", args.rotation)
else
	-- wall side
	typeToPlace = types_applyrotation("signwall", args.blockHitSide)
	
	if (world_canplaceat(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, typeToPlace) == "Yes") then
		try_place(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, typeToPlace)
		return "used"
	end
	-- failed to place on wall, probably missing attachment. Fallback to trying floor variant
	typeToPlace = types_applyrotation("signground", args.rotation)
end

if (world_canplaceat(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, typeToPlace) == "Yes") then
	try_place(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, typeToPlace)
	return "used"
end
	
return "used"
