local function SetOrPreview (x, y, z, t)
	if args.clicktype == "Right" then
		world_trysettypeat(x, y, z, t)
	else
		showpreview(x, y, z, t, "green")
	end
end

if (args.clicktype == "Left") then
	return nil
end

-- only care about "Block" hits
if (args.hittype ~= "Block") then
	return nil
end

-- depending on the voxel side we're aiming at, preview & place the appropriate shaped charge block
local hitSide = args.blockHitSide;

if (hitSide == "Y-") then
	-- not placing rails against the roof
	return nil
end

local buildx = args.blockBuildPositionX;
local buildy = args.blockBuildPositionY;
local buildz = args.blockBuildPositionZ;
-- only placing rails into air blocks of course
if args.blockBuildCurrentType ~= "air" then return nil end

local desiredType;

if (hitSide ~= "Y+") then
	if (hitSide == "X-") then desiredType = "elevatorshaftdiagonalx+"
	elseif (hitSide == "X+") then desiredType = "elevatorshaftdiagonalx-"
	elseif (hitSide == "Z+") then desiredType = "elevatorshaftdiagonalz-"
	elseif (hitSide == "Z-") then desiredType = "elevatorshaftdiagonalz+"
	end
	
	if (world_canplaceat(args.blockHitX, args.blockHitY + 1, args.blockHitZ, "elevatorshafthorizontalx+") == "Yes"
		and world_canplaceat(buildx, buildy, buildz, desiredType) == "Yes")
	then
		-- diagonal up onto the block we're aiming to the side of; it can have rail on top, and we can place at build pos
		SetOrPreview(buildx, buildy, buildz, desiredType)
		return "used"
	end
end

-- hitSide == "Y+", or we hit on the side but failed to make a diagonal up
desiredType = "elevatorshafthorizontalx+"

local xOffset
local zOffset

local rotation = args.rotation

if (rotation == "X+") then
	xOffset = 1
	zOffset = 0
elseif (rotation == "X-") then
	xOffset = -1
	zOffset = 0
elseif (rotation == "Z+") then
	xOffset = 0
	zOffset = 1
else
	xOffset = 0
	zOffset = -1
end

local success, ahead1 = world_trygettypeat(buildx + xOffset, buildy + 1, buildz + zOffset)
if (not(success)) then return nil end
local success, ahead2 = world_trygettypeat(buildx + xOffset, buildy + 2, buildz + zOffset)
if (not(success)) then return nil end


if (world_canplaceat(buildx + xOffset, buildy + 1, buildz + zOffset, "elevatorshafthorizontalx+") == "Yes"
	and (ahead1 == "air" or types_hasbehaviour(ahead1, "rail"))
	and (types_pathingimpact(ahead2) == "AsAir"))
then
	-- we're placing this on the ground, but ahead+up is rail(able), so make a ramp
	if (rotation == "X+") then desiredType = "elevatorshaftdiagonalx+"
	elseif (rotation == "X-") then desiredType = "elevatorshaftdiagonalx-"
	elseif (rotation == "Z+") then desiredType = "elevatorshaftdiagonalz+"
	else desiredType = "elevatorshaftdiagonalz-" end
	
	if (world_canplaceat(buildx, buildy, buildz, desiredType) == "Yes") then
		SetOrPreview(buildx, buildy, buildz, desiredType)
	end

	return "used"
end

-- flat bit, placed on y+, not matching up to a diagonal
-- so figure out if we need a corner/intersection piece

local successZP = rail_israil("railitem", buildx, buildy, buildz + 1, "ZBack");
local successZN = rail_israil("railitem", buildx, buildy, buildz - 1, "ZForward");
local successXP = rail_israil("railitem", buildx + 1, buildy, buildz, "XBack");
local successXN = rail_israil("railitem", buildx - 1, buildy, buildz, "XForward");

if (rotation == "X+") then
	successXP = true
elseif (rotation == "X-") then
	successXN = true
elseif (rotation == "Z+") then
	successZP = true
else
	successZN = true
end

local count = 0
if (successXP) then count = count + 1 end
if (successXN) then count = count + 1 end
if (successZP) then count = count + 1 end
if (successZN) then count = count + 1 end

if (count == 1) then
	if (rotation == "X+") then
		successXN = true
	elseif (rotation == "X-") then
		successXP = true
	elseif (rotation == "Z+") then
		successZN = true
	else
		successZP = true
	end
end

-- at least 2 bools are set
if (successXP) then
	if (successXN) then
		if (successZP) then
			-- xp & xn & zp
			if (successZN) then desiredType = "elevatorshafthorizontal4wayx+"
			else desiredType = "elevatorshafthorizontal3wayx+" end
		else
			-- xp & xn & !zp
			if (successZN) then desiredType = "elevatorshafthorizontal3wayx-"
			else desiredType = "elevatorshafthorizontalx+" end
		end
	else
		if (successZP) then
			-- xp & !xn & zp
			if (successZN) then desiredType = "elevatorshafthorizontal3wayz-"
			else desiredType = "elevatorshafthorizontal90x+" end
		else
			-- xp & !xn & !zp; zn must be true
			desiredType = "elevatorshafthorizontal90z-"
		end
	end
else
	if (successXN) then
		if (successZP) then
			-- !xp & xn & zp
			if (successZN) then desiredType = "elevatorshafthorizontal3wayz+"
			else desiredType = "elevatorshafthorizontal90z+" end
		else
			-- !xp & xn & !zp; zn must be true
			desiredType = "elevatorshafthorizontal90x-"
		end
	else
		-- !xp & !xn; zp and zn must be true
		desiredType = "elevatorshafthorizontalz+"
	end
end

if (world_canplaceat(buildx, buildy, buildz, desiredType) == "Yes") then
	SetOrPreview(buildx, buildy, buildz, desiredType)
end

return "used"
