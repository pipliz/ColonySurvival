local rotation = args.rotation; -- "xMin", "xPlus", "zMin", "zPlus"; the blueprint area rotation
local type = args.type; -- type name
local flipX = args.flipX; -- whether the area is flipped in X axis (post-rotation)
local flipZ = args.flipZ; -- whether the area is flipped in Z axis (post-rotation)

local tableMap = {
	"x+x-", "x+z+", "x+x+", "x+z-",
	"z-z-", "z-x+", "z-z+", "z-x-",
	"x-x-", "x-z-", "x-x+", "x-z+",
	"z+z-", "z+x-", "z+z+", "z+x+"
}

local typeEnd = string.sub(type, -4)

for i, v in ipairs(tableMap) do
    if v == typeEnd then
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
		
        local typeEndNew = tableMap[row * 4 + column + 1] -- more 1-indexed
        
		if flipX then
			if string_contains_at(typeEndNew, "x", 1) then
				-- main axis is x- or x+
				if string_contains_at(typeEndNew, "+", 2) then
					typeEndNew = "x-" .. string.sub(typeEndNew, 3, 4) 
				else
					-- negative sign
					typeEndNew = "x+" .. string.sub(typeEndNew, 3, 4)
				end
			else 
				if string_contains_at(typeEndNew, "x", 3) then
					-- secondary axis is x- or x+
					if string_contains_at(typeEndNew, "+", 4) then
						typeEndNew = string.sub(typeEndNew, 1, 2) .. "x-" 
					else
						-- negative sign
						typeEndNew = string.sub(typeEndNew, 1, 2) .. "x+"
					end
				end
			end
		end
		
		if flipZ then
			if string_contains_at(typeEndNew, "z", 1) then
				-- main axis is z- or z+
				if string_contains_at(typeEndNew, "+", 2) then
					typeEndNew = "z-" .. string.sub(typeEndNew, 3, 4) 
				else
					-- negative sign
					typeEndNew = "z+" .. string.sub(typeEndNew, 3, 4)
				end
			else 
				if string_contains_at(typeEndNew, "z", 3) then
					-- secondary axis is z- or z+
					if string_contains_at(typeEndNew, "+", 4) then
						typeEndNew = string.sub(typeEndNew, 1, 2) .. "z-" 
					else
						-- negative sign
						typeEndNew = string.sub(typeEndNew, 1, 2) .. "z+"
					end
				end
			end
		end
		
		local newTypeResult = string_replace(type, typeEnd, typeEndNew)
        return newTypeResult
    end
end
