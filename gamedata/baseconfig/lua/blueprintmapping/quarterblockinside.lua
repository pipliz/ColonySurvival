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
local flipX = args.flipX; -- whether the area is flipped in X axis (post-rotation)
local flipZ = args.flipZ; -- whether the area is flipped in Z axis (post-rotation)

local tableMap = {
	"x+x-", "x+z+", "x+x+", "x+z-",
	"z-z-", "z-x+", "z-z+", "z-x-",
	"x-x+", "x-z+", "x-x-", "x-z-",
	"z+z-", "z+x-", "z+z+", "z+x+"
}
-- 0, +3, +2, +1 -> blueprint R order


local typeEnd = string.sub(type, -4)
local typeEndNew = typeEnd

if (string_contains_at(typeEndNew, "y", 1)) then
	if rotation == "xMin" then
		typeEndNew = swap_pairs4(typeEndNew, 3, "x+", "x-", "z+", "z-")
	elseif rotation == "zMin" then
		typeEndNew = replace_at_4(typeEndNew, 3, "x+", "z-", "x-", "z+", "z+", "x+", "z-", "x-")
	elseif rotation == "zPlus" then
		typeEndNew = replace_at_4(typeEndNew, 3, "x+", "z+", "x-", "z-", "z+", "x-", "z-", "x+")
	end
else
	for i, v in ipairs(tableMap) do
		if v == typeEndNew then
			local row = math.floor((i - 1) / 4)  -- joy of lua 1-indexing
			local column = (i - 1) % 4

			-- adjust row based on rotation
			if rotation == "xMin" then
				row = (row + 2) % 4
			elseif rotation == "zMin" then
				row = (row + 1) % 4
			elseif rotation == "zPlus" then
				row = (row + 3) % 4
			end

			typeEndNew = tableMap[row * 4 + column + 1] -- more 1-indexed
			break
		end
	end
end

if flipX then
	if string_contains_at(typeEndNew, "x", 1) then
		typeEndNew = swap_pairs4(typeEndNew, 1, "x-x+", "x+z+", "x-z+", "x+x-")
		typeEndNew = swap_pairs4(typeEndNew, 1, "x-z-", "x+x+", "x-x-", "x+z-")
	else 
		if string_contains_at(typeEndNew, "y", 1) then
			typeEndNew = swap_pairs2(typeEndNew, 3, "x", "z")
		else 
			typeEndNew = swap_pairs4(typeEndNew, 1, "z+x-", "z+z-", "z+x+", "z+z+")
			typeEndNew = swap_pairs4(typeEndNew, 1, "z-x-", "z-z+", "z-x+", "z-z-")
		end
	end
end

if flipZ then
	if string_contains_at(typeEndNew, "y", 1) then
		typeEndNew = swap_pairs4(typeEndNew, 3, "x-", "z+", "x+", "z-")
	else 
		if string_contains_at(typeEndNew, "z", 1) then
			typeEndNew = swap_pairs4(typeEndNew, 1, "z+x-", "z-z-", "z+z-", "z-x+")
			typeEndNew = swap_pairs4(typeEndNew, 1, "z+x+", "z-z+", "z+z+", "z-x-")
		else
			typeEndNew = swap_pairs4(typeEndNew, 1, "x+x+", "x+z-", "x+x-", "x+z+")
			typeEndNew = swap_pairs4(typeEndNew, 1, "x-x+", "x-z+", "x-x-", "x-z-")
		end
	end
end

local newTypeResult = string_replace(type, typeEnd, typeEndNew)
return newTypeResult