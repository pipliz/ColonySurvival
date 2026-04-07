if (args.clicktype ~= "Hover" or args.hittype ~= "Block") then return nil end

-- only previewing for hovering against blocks
-- the real placement code lives on the server in C#

local quarterConfig = types_getbehaviour(args.selectedtype, "smart_quarterblock")
if (quarterConfig == nil) then return nil end

-- so we've got the quarterConfig
-- now we need to do the logic of finding out which of the rotations we would be placing here

local localOffsetX = args.blockHitExactX -  math.floor(args.blockHitExactX + 0.5)
local localOffsetY = args.blockHitExactY -  math.floor(args.blockHitExactY + 0.5)
local localOffsetZ = args.blockHitExactZ -  math.floor(args.blockHitExactZ + 0.5)

local localOffsetAbsX = math.abs(localOffsetX)
local localOffsetAbsY = math.abs(localOffsetY)
local localOffsetAbsZ = math.abs(localOffsetZ)

local typeToShow
	
if (args.blockHitSide == "Y+" or args.blockHitSide == "Y-") then
	local config
	if (args.blockHitSide == "Y+") then config = quarterConfig["y+"]["side"] else config = quarterConfig["y-"]["side"] end
	
	if (localOffsetAbsX > localOffsetAbsZ) then
		if (localOffsetX > 0) then typeToShow = config .. "x+" else typeToShow = config .. "x-" end
	else
		if (localOffsetZ > 0) then typeToShow = config .. "z+" else typeToShow = config .. "z-" end
	end
elseif (args.blockHitSide == "X+" or args.blockHitSide == "X-") then
	local config
	if (args.blockHitSide == "X+") then config = quarterConfig["x+"]["side"] else config = quarterConfig["x-"]["side"] end

	if (localOffsetAbsY > localOffsetAbsZ) then
		if (localOffsetY < 0) then typeToShow = config .. "x+" else typeToShow = config .. "x-" end
	else
		if (localOffsetZ > 0) then typeToShow = config .. "z+" else typeToShow = config .. "z-" end
	end
elseif (args.blockHitSide == "Z+" or args.blockHitSide == "Z-") then
	local config
	if (args.blockHitSide == "Z+") then config = quarterConfig["z+"]["side"] else config = quarterConfig["z-"]["side"] end

	if (localOffsetAbsX > localOffsetAbsY) then
		if (localOffsetX > 0) then typeToShow = config .. "x+" else typeToShow = config .. "x-" end
	else
		if (localOffsetY < 0) then typeToShow = config .. "z+" else typeToShow = config .. "z-" end
	end
end

if (typeToShow ~= nil and world_canplaceat(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, typeToShow) == "Yes") then
	showpreview(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, typeToShow, "green")
end

return nil
