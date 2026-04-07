-- so we're hovering/clicking on some block
-- if it's the side of a block, check if the torch can be attached to that side
-- if it isn't the side, or can't be attached, check if the non-side torch can be placed
-- if that cant be done either, block the placement

local function try_place(x, y, z, t)
	if (args.clicktype == "Right") then
		world_trysettypeat(x, y, z, t)
	else
		showpreview(x, y, z, t, "green")
	end
end

if (args.clicktype == "Left" or args.hittype ~= "Block") then return nil end

if (args.blockHitSide == "Y-") then return "used" end

if (args.blockBuildCurrentType ~= "air") then return "used" end

if (args.blockBuildPositionY == args.blockHitY) then
	-- this means we're not y+ or y-, but horizontal
	local sideResult = nil
	if (args.blockHitSide == "X+") then sideResult = "torchx-"
	elseif (args.blockHitSide == "X-") then sideResult = "torchx+"
	elseif (args.blockHitSide == "Z+") then sideResult = "torchz-"
	elseif (args.blockHitSide == "Z-") then sideResult = "torchz+" end
	
	if (world_canplaceat(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, sideResult) == "Yes") then
		try_place(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, sideResult)
		return "used"
	end
end

-- either Y+ or horizontal-but-failed, try Y+

if (world_canplaceat(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, "torchy+") == "Yes") then
	try_place(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, "torchy+")
	return "used"
end

return "used"


