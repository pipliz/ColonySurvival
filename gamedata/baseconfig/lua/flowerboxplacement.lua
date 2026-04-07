local function try_place(x, y, z, t)
	if (args.clicktype == "Right") then
		world_trysettypeat(x, y, z, t)
	else
		showpreview(x, y, z, t, "green")
	end
end

if (args.clicktype == "Left" or args.hittype ~= "Block") then return nil end

if (args.blockHitSide == "Y-") then return nil end

if (args.blockBuildCurrentType ~= "air") then return nil end

local typeToPlace;

if args.blockHitSide ~= "Y+" then
	typeToPlace = types_applyrotation("wallflowerbox", math_reverse_rotation(args.blockHitSide))
	
	if (world_canplaceat(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, typeToPlace) == "Yes") then
		try_place(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, typeToPlace)
		return "used"
	end
end

-- etiher Y+ hit or failed to attach to the aimed-at-wall, so try ground flowerbox	
typeToPlace = types_applyrotation("groundflowerbox", args.rotation)

if (world_canplaceat(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, typeToPlace) == "Yes") then
	try_place(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, typeToPlace)
	return "used"
end
	
return nil
