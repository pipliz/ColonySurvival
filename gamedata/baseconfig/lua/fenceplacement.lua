local function register_placement_for()
	local res = typesmanager_getallwithbehaviourrecursive("fence")
	return res;
end

local x = args.x
local y = args.y
local z = args.z

local behaviour = types_getbehaviourrecursive(args.type, "fence")

if (behaviour == nil) then
	behaviour = types_getbehaviour("fencestripped", "fence")
end

local didReadA, isXPFence = world_tryhasbehaviour(x + 1, y, z, "fence") 
local didReadB, isXNFence = world_tryhasbehaviour(x - 1, y, z, "fence")
local didReadC, isZPFence = world_tryhasbehaviour(x, y, z + 1, "fence")
local didReadD, isZNFence = world_tryhasbehaviour(x, y, z - 1, "fence")

if (not(didReadA and didReadB and didReadC and didReadD))
then
-- failed to read the world here, just use default pillar fence
	return behaviour.fencemiddlei
end

if (isXPFence and isXNFence and isZPFence and isZNFence)
then
	return behaviour.fencemiddlex
end

-- T split ones
if (isXPFence and isXNFence and isZPFence)
then
	return behaviour.fencemiddlet .. "x-"
end

if (isXPFence and isZPFence and isZNFence)
then
	return behaviour.fencemiddlet .. "z+"
end

if (isXNFence and isZPFence and isZNFence)
then
	return behaviour.fencemiddlet ..  "z-"
end

if (isXPFence and isXNFence and isZNFence)
then
	return behaviour.fencemiddlet ..  "x+"
end

-- 90 degree ones
if (isXPFence and isZPFence)
then
	return behaviour.fencemiddlecorner .. "z-"
end

if (isXPFence and isZNFence)
then
	return behaviour.fencemiddlecorner .. "x-"
end

if (isXNFence and isZPFence)
then
	return behaviour.fencemiddlecorner .. "x+"
end

if (isXNFence and isZNFence)
then
	return behaviour.fencemiddlecorner .. "z+"
end

-- check for straight ones; these also match with solid blocks
local didReadE, isZPSolid = world_tryissolid(x, y, z + 1)
local didReadF, isZNSolid = world_tryissolid(x, y, z - 1)

if (not(didReadE and didReadF))
then
	return behaviour.fencemiddlei
end

if ((isZPFence or isZNFence) and (isZPSolid or isZPFence) and (isZNSolid or isZNFence))
then
	return behaviour.fencemiddlemiddle .. "z+"
end

-- same but X axis
local didReadG, isXPSolid = world_tryissolid(x + 1, y, z)
local didReadH, isXNSolid = world_tryissolid(x - 1, y, z)

if (not(didReadG and didReadH))
then
	return behaviour.fencemiddlei
end

if ((isXPFence or isXNFence) and (isXPSolid or isXPFence) and (isXNSolid or isXNFence))
then
	return behaviour.fencemiddlemiddle .. "x+"
end


-- horizontal fence start/ends
if (isZNFence)
then
	return behaviour.fencemiddleend .. "z+"
end

if (isZPFence)
then
	return behaviour.fencemiddleend .. "z-"
end

if (isXNFence)
then
	return behaviour.fencemiddleend .. "x+"
end

if (isXPFence)
then
	return behaviour.fencemiddleend .. "x-"
end


-- multi-block fence pillar, top/middle/end entries
local didReadI, isYPFence = world_tryhasbehaviour(x, y + 1, z, "fence")
local didReadJ, isYNFence = world_tryhasbehaviour(x, y - 1, z, "fence")

if (not(didReadI and didReadJ))
then
	return behaviour.fencemiddlei
end

if (isYPFence)
then
	if (isYNFence)
	then
		return behaviour.fencesolomiddle
	else
		return behaviour.fencesolobottom
	end
else
	if (isYNFence)
	then
		return behaviour.fencesolotop
	end
end

return behaviour.fencemiddlei
