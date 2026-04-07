if (args.clicktype ~= "Hover" or args.hittype ~= "Block") then return nil end

if (args.blockHitSide == "Y-" or args.blockHitSide == "Y+") then
	return "used"
end

if (args.blockBuildCurrentType ~= "air") then return "used" end

local sideResult = nil
if (args.blockHitSide == "X+") then sideResult = "wallpainting1x-"
elseif (args.blockHitSide == "X-") then sideResult = "wallpainting1x+"
elseif (args.blockHitSide == "Z+") then sideResult = "wallpainting1z-"
elseif (args.blockHitSide == "Z-") then sideResult = "wallpainting1z+" end

if (world_canplaceat(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, sideResult) == "Yes") then
	showpreview(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, sideResult, "green")
end

return "used"
