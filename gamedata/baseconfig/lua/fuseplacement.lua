local function register_placement_for()
	return "bombfusequad", "bombfusethreesidesx+", "bombfusethreesidesz-", "bombfusethreesidesz+", "bombfusethreesidesx-", "bombfusetwosidesx+", "bombfusetwosidesz-",  "bombfusetwosidesz+", "bombfusetwosidesx-", "bombfusestraightx+", "bombfusestraightz+"
end

local x = args.x
local y = args.y
local z = args.z

local didReadA, isXPFuseA = world_tryhasbehaviour(x + 1, y, z, "fuse") 
local didReadB, isXNFuseA = world_tryhasbehaviour(x - 1, y, z, "fuse")
local didReadC, isZPFuseA = world_tryhasbehaviour(x, y, z + 1, "fuse")
local didReadD, isZNFuseA = world_tryhasbehaviour(x, y, z - 1, "fuse")

local didReadE, isXPFuseB = world_tryhasbehaviour(x + 1, y - 1, z, "fusehigh") 
local didReadF, isXNFuseB = world_tryhasbehaviour(x - 1, y - 1, z, "fusehigh")
local didReadG, isZPFuseB = world_tryhasbehaviour(x, y - 1, z + 1, "fusehigh")
local didReadH, isZNFuseB = world_tryhasbehaviour(x, y - 1, z - 1, "fusehigh")

if (not(didReadA and didReadB and didReadC and didReadD and didReadE and didReadF and didReadG and didReadH))
then
-- failed to read the world here, just use default full fuse block
	return "bombfusequad"
end

local isXPFuse = isXPFuseA or isXPFuseB
local isXNFuse = isXNFuseA or isXNFuseB
local isZPFuse = isZPFuseA or isZPFuseB
local isZNFuse = isZNFuseA or isZNFuseB

if (isXPFuse and isXNFuse and isZPFuse and isZNFuse)
then
	return "bombfusequad"
end

if (isXPFuse and isXNFuse and isZPFuse)
then
	return "bombfusethreesidesx+"
end

if (isXPFuse and isZPFuse and isZNFuse)
then
	return "bombfusethreesidesz-"
end

if (isXNFuse and isZPFuse and isZNFuse)
then
	return "bombfusethreesidesz+"
end

if (isXPFuse and isXNFuse and isZNFuse)
then
	return "bombfusethreesidesx-"
end

if (isXPFuse and isZPFuse)
then
	return "bombfusetwosidesx+"
end

if (isXPFuse and isZNFuse)
then
	return "bombfusetwosidesz-"
end

if (isXNFuse and isZPFuse)
then
	return "bombfusetwosidesz+"
end

if (isXNFuse and isZNFuse)
then
	return "bombfusetwosidesx-"
end

if (isXNFuse or isXPFuse)
then
	return "bombfusestraightx+"
end

if (isZPFuse or isZNFuse)
then
	return "bombfusestraightz+"
end

return "bombfusequad"