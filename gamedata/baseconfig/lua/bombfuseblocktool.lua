local clicktype = args.clicktype; -- "Left" or "Right" or "Hover"
local hittype = args.hittype; -- "Missed", "Block", "NPC" or "ControlledMesh"

if (clicktype == "Left") then return nil end

-- only care about "Block" hits
if (hittype ~= "Block") then return nil end

local hitType = args.blockHitType

if (hitType ~= "bombfusequad" and hitType ~= "bombfusethreesides" and hitType ~= "bombfusetwosides" and hittype ~= "bombfusestraight")
then
	local hitTypeParent = types_getparent(hitType)
	if (hitTypeParent ~= nil and hitTypeParent ~= "bombfusequad" and hitTypeParent ~= "bombfusethreesides" and hitTypeParent ~= "bombfusetwosides" and hitTypeParent ~= "bombfusestraight")
	then
		return nil
	end
end
-- either the type or the parent of the type matched the ones we're looking for

local x = args.blockHitX
local y = args.blockHitY
local z = args.blockHitZ

if (not(world_hassupportsfor(x + 1, y + 1, z, "bombfusequad")) and
 not(world_hassupportsfor(x - 1, y + 1, z, "bombfusequad")) and
 not(world_hassupportsfor(x, y + 1, z + 1, "bombfusequad")) and
 not(world_hassupportsfor(x, y + 1, z - 1, "bombfusequad")))
then
	-- if none of the neighbours can have a fuse above the current fuse, there is no need to preview or place the bombfusehigh block
	return nil
end

if (clicktype == "Right") then
	world_trysettypeat(x, y, z, "bombfusehigh")
else
	-- not left, not right -> hover
	showpreview(x, y, z, "bombfusehigh", "green")
end

return "used"