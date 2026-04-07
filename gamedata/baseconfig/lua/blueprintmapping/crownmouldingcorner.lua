-- swaps pairs (a<->b) or (c<->d) if found at index
function swap_pairs4(s, index, a, b, c, d)
    if string_contains_at(s, a, index) then
        return string.sub(s, 1, index - 1) .. b .. string.sub(s, index + #a)
    end
	if string_contains_at(s, b, index) then
        return string.sub(s, 1, index - 1) .. a .. string.sub(s, index + #b)
    end
	if string_contains_at(s, c, index) then
        return string.sub(s, 1, index - 1) .. d .. string.sub(s, index + #c)
    end
	if string_contains_at(s, d, index) then
        return string.sub(s, 1, index - 1) .. c .. string.sub(s, index + #d)
    end
	return s -- nothing matched
end

function swap_pairs2(s, index, a, b)
    if string_contains_at(s, a, index) then
        return string.sub(s, 1, index - 1) .. b .. string.sub(s, index + #a)
    end
	if string_contains_at(s, b, index) then
        return string.sub(s, 1, index - 1) .. a .. string.sub(s, index + #b)
    end
	return s
end

function replace_at_4(s, index, a, b, c, d, e, f, g, h)
    if string_contains_at(s, a, index) then
        return string.sub(s, 1, index - 1) .. b .. string.sub(s, index + #a)
    end
    if string_contains_at(s, c, index) then
        return string.sub(s, 1, index - 1) .. d .. string.sub(s, index + #c)
    end
    if string_contains_at(s, e, index) then
        return string.sub(s, 1, index - 1) .. f .. string.sub(s, index + #e)
    end
    if string_contains_at(s, g, index) then
        return string.sub(s, 1, index - 1) .. h .. string.sub(s, index + #g)
    end
	return s
end

local rotation = args.rotation; -- "xMin", "xPlus", "zMin", "zPlus"; the blueprint area rotation
local type = args.type; -- type name
local flipX = args.flipX; -- whether the area is flipped in X axis
local flipZ = args.flipZ; -- whether the area is flipped in Z axis

local typeEnd = string.sub(type, -2)
local typeEndNew = typeEnd

if rotation == "xMin" then
	typeEndNew = swap_pairs4(typeEndNew, 1, "x+", "x-", "z+", "z-")
elseif rotation == "zMin" then
	typeEndNew = replace_at_4(typeEndNew, 1, "x+", "z-", "x-", "z+", "z+", "x+", "z-", "x-")
elseif rotation == "zPlus" then
	typeEndNew = replace_at_4(typeEndNew, 1, "x+", "z+", "x-", "z-", "z+", "x-", "z-", "x+")
end

if flipX then
	typeEndNew = swap_pairs4(typeEndNew, 1, "x+", "z+", "z-", "x-")
end

if flipZ then
	typeEndNew = swap_pairs4(typeEndNew, 1, "x-", "z+", "x+", "z-")
end

local newTypeResult = string_replace(type, typeEnd, typeEndNew)
return newTypeResult