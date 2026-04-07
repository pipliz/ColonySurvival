function register_placement_for()
	local res = typesmanager_getallwithbehaviour("pillar")
	return res
end

local x = args.x
local y = args.y
local z = args.z

local behaviour = types_getbehaviour(args.type, "pillar")

if (behaviour == nil) then
	behaviour = types_getbehaviour("pillarmiddle", "pillar")
end

local didReadA, isYPPillar = world_tryhasbehaviour(x, y + 1, z, "pillar") 
local didReadB, isYNPillar = world_tryhasbehaviour(x, y - 1, z, "pillar")

if (not(didReadA and didReadB))
then
-- failed to read the world here, just use default pillar
	return behaviour.full
end

if (isYPPillar)
then
	if (isYNPillar)
	then
		return behaviour.mid
	else
		return behaviour.bot
	end
else
	if (isYNPillar)
	then
		return behaviour.top
	end
end

return behaviour.full
