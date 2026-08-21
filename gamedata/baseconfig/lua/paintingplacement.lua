local function run()
	if (args.clicktype ~= "Hover" or args.hittype ~= "Block") then return nil end

	if (args.blockHitSide == "Y-" or args.blockHitSide == "Y+") then
		return "used"
	end

	if (args.blockBuildCurrentType ~= "air") then return "used" end

	local paintingConfig = types_getbehaviour(args.selectedtype, "paintingplacer")
	if (paintingConfig == nil) then return "used" end

	local previewBase = paintingConfig["previewtype"]
	if (previewBase == nil) then previewBase = "wallpainting1" end

	local sideResult = nil
	if (args.blockHitSide == "X+") then sideResult = previewBase .. "x-"
	elseif (args.blockHitSide == "X-") then sideResult = previewBase .. "x+"
	elseif (args.blockHitSide == "Z+") then sideResult = previewBase .. "z-"
	elseif (args.blockHitSide == "Z-") then sideResult = previewBase .. "z+" end

	if (world_canplaceat(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, sideResult) == "Yes") then
		showpreview(args.blockBuildPositionX, args.blockBuildPositionY, args.blockBuildPositionZ, sideResult, "green")
	end

	return "used"
end

local bootstrap_phase = bootstrap_getphase()
if (bootstrap_phase == "after_item_types_defined") then
	register_on_client_click("wallpaintingitem", run)
	register_on_client_click("wallpaintingitemlizzy", run)
	register_on_client_click("wallpaintingitempip", run)
	return
end
