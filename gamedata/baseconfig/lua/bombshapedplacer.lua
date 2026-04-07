local clicktype = args.clicktype; -- "Left" or "Right" or "Hover"
local hittype = args.hittype; -- "Missed", "Block", "NPC" or "ControlledMesh"

if (clicktype == "Left") then return nil end

-- only care about "Block" hits
if (hittype ~= "Block") then return nil end

if (args.blockBuildCurrentType ~= "air") then return nil end

-- depending on the voxel side we're aiming at, preview & place the appropriate shaped charge block
local hitSide = args.blockHitSide;

local buildType

if (hitSide == "Y+") then
	buildType = "bombshapedchargedown"
elseif (hitSide == "X+") then
	buildType = "bombshapedchargesidex-"
elseif (hitSide == "X-") then
	buildType = "bombshapedchargesidex+"
elseif (hitSide == "Z+") then
	buildType = "bombshapedchargesidez-"
elseif (hitSide == "Z-") then
	buildType = "bombshapedchargesidez+"
elseif (hitSide == "Y-") then
	buildType = "bombshapedchargeup"
else
	return nil
end

local buildx = args.blockBuildPositionX;
local buildy = args.blockBuildPositionY;
local buildz = args.blockBuildPositionZ;

if world_canplaceat(buildx, buildy, buildz, buildType) ~= "Yes" then return "used" end

if clicktype == "Right" then
	world_trysettypeat(buildx, buildy, buildz, buildType)
else
	showpreview(buildx, buildy, buildz, buildType, "green")
end

return "used"
