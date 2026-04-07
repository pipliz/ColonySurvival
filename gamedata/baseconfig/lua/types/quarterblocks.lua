local function MakeQuarterBlock (name, icon, itemname, color)
	local blocks = {}
	
	-- y+
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 }
			},
			attachmentRequirements = {
				{
					offset = { 0, -1, 0 },
					min = { 0.05, -0.51, -0.45 },
					max = { 0.45, -0.49, 0.45 }
				}
			},
			colliders = {
				{
					max = { 0.5, 0, 0.5 },
					min = { 0, -0.5, -0.5 }
				}
			},
			color = color,
			icon = icon,
			isSolid = false,
			lightingTransparency = 200,
			maxStackSize = 60,
			mesh = "../meshes/quarterblockdynamicx+.ply",
			movementCost = -1,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "quarterblock" .. name
	})
	
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockinside" }
			},
			attachmentRequirements = {
				{
					offset = { 0, -1, 0 },
					min = { 0.05, -0.51, -0.45 },
					max = { 0.45, -0.49, 0.45 }
				},
				{
					offset = { 0, -1, 0 },
					min = { -0.45, -0.51, 0.05 },
					max = { 0.45, -0.49, 0.45 }
				}
			},
			colliders = {
				{
					max = { 0.5, 0, 0.5 },
					min = { 0, -0.5, -0.5 }
				},
				{
					max = { 0.5, 0, 0.5 },
					min = { -0.5, -0.5, 0 }
				}
			},
			color = color,
			lightingTransparency = 200,
			mesh = "../meshes/quarterblockdynamicinsidecorner.ply",
			movementCost = -1,
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			onRemoveAmount = 2,
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "quarterblock" .. name .. "insidey+"
	})
	
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockoutside" }
			},
			attachmentRequirements = {
				{
					offset = { 0, -1, 0 },
					min = { 0.05, -0.51, 0.05 },
					max = { 0.45, -0.49, 0.45 }
				}
			},
			colliders = {
				{
					max = { 0.5, 0, 0.5 },
					min = { 0, -0.5, 0 }
				}
			},
			color = color,
			lightingTransparency = 230,
			mesh = "../meshes/quarterblockdynamicoutsidecorner.ply",
			movementCost = -1,
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveAmount = 0,
			sideall = "neutral"
		},
		generateType = "rotateBlock",
		typeName = "quarterblock" .. name .. "outsidey+"
	})
	
	-- y-
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 }
			},
			attachmentRequirements = {
				{
					offset = { 0, 1, 0 },
					min = { 0.05, 0.490000039, -0.449999958 },
					max = { 0.45, 0.509999931, 0.450000018 }
				}
			},
			colliders = {
				{
					max = { 0.5, 0.49999997, 0.50000006 },
					min = { 0.0, 4.371139E-08, -0.5 }
				}
			},
			color = color,
			isSolid = false,
			lightingTransparency = 200,
			maxStackSize = 60,
			mesh = "../meshes/quarterblockdynamicx+.ply",
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral",
			meshRotationEuler = {
				x = 0,
				y = 180.0,
				z = 180.0
			}
		},
		generateType = "rotateBlock",
		typeName = "quarterblock" .. name .. "y-"
	})
	
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockinside" }
			},
			attachmentRequirements = {
				{
					offset = { 0, 1, 0 },
					min = { -0.45, 0.48999998, 0.04999997 },
					max = { 0.449999958, 0.5099999, 0.45 }
				},
				{
					offset = { 0, 1, 0 },
					min = { 0.0499999262, 0.48999998, -0.449999958 },
					max = { 0.449999958, 0.509999931, 0.45 }
				}
			},
			colliders = {
				{
					max = { 0.5, 0.4999999, 0.5 },
					min = { -0.5, 4.371139E-08, -2.98023224E-08 }
				},
				{
					max = { 0.5, 0.49999994, 0.5 },
					min = { -7.351371E-08, 4.371139E-08, -0.49999997 }
				}
			},
			color = color,
			lightingTransparency = 200,
			mesh = "../meshes/quarterblockdynamicinsidecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			onRemoveAmount = 2,
			pathingImpact = "AsUntouchableSolid",
			sideall = "neutral",
			meshRotationEuler = {
				x = 0,
				y = 90.0,
				z = 180.0
			}
		},
		generateType = "rotateBlock",
		typeName = "quarterblock" .. name .. "insidey-"
	})
	
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockoutside" }
			},
			attachmentRequirements = {
				{
					offset = { 0, 1, 0 },
					min = { 0.049999956, 0.48999998, 0.05 },
					max = { 0.449999958, 0.509999931, 0.45 }
				}
			},
			colliders = {
				{
					max = { 0.5, 0.49999994, 0.5 },
					min = { -4.371139E-08, 4.371139E-08, 0.0 }
				}
			},
			color = color,
			lightingTransparency = 230,
			mesh = "../meshes/quarterblockdynamicoutsidecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveAmount = 0,
			pathingImpact = "AsAir",
			sideall = "neutral",
			meshRotationEuler = {
				x = 0,
				y = 90.0,
				z = 180.0
			}
		},
		generateType = "rotateBlock",
		typeName = "quarterblock" .. name .. "outsidey-"
	})
	
	-- x+
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockstraight" }
			},
			attachmentRequirements = {
				{
					offset = { -1, 0, 0 },
					min = { -0.509999931, -0.45, -0.45 },
					max = { -0.48999995, -0.0500000268, 0.45 }
				}
			},
			colliders = {
				{
					max = { 2.98023224E-08, -2.98023224E-08, 0.5 },
					min = { -0.49999997, -0.49999997, -0.5 }
				}
			},
			color = color,
			isSolid = false,
			lightingTransparency = 200,
			maxStackSize = 60,
			mesh = "../meshes/quarterblockdynamicx+.ply",
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			sideall = "neutral",
			meshRotationEuler = {
				x = 0.0,
				y = 0.0,
				z = 270.0
			}
		},
		generateType = "rotateBlockX",
		typeName = "quarterblock" .. name .. "sidex+",
		rotatedTypeJSON = {
			{ pathingImpact = "AsAir" },
			{ pathingImpact = "AsSolid" },
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsUntouchableSolid" }
		}
	})
	
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockinside" }
			},
			attachmentRequirements = {
				{
					offset = { -1, 0, 0 },
					min = { -0.509999931, -0.45, -0.45 },
					max = { -0.48999995, -0.0500000268, 0.45 }
				},
				{
					offset = { -1, 0, 0 },
					min = { -0.51, -0.45, 0.05 },
					max = { -0.48999995, 0.449999928, 0.45 }
				}
			},
			colliders = {
				{
					max = { 2.98023224E-08, -2.98023224E-08, 0.5 },
					min = { -0.49999997, -0.49999997, -0.5 }
				},
				{
					max = { 2.98023224E-08, 0.49999994, 0.5 },
					min = { -0.5, -0.49999997, 0.0 }
				}
			},
			color = color,
			lightingTransparency = 200,
			mesh = "../meshes/quarterblockdynamicinsidecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			onRemoveAmount = 2,
			sideall = "neutral",
			meshRotationEuler = {
				x = 0.0,
				y = 0.0,
				z = 270.0
			}
		},
		generateType = "rotateBlockX",
		typeName = "quarterblock" .. name .. "insidex+",
		rotatedTypeJSON = {
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsSolid" },
			{ pathingImpact = "AsSolid" },
			{ pathingImpact = "AsUntouchableSolid" }
		}
	})
	
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockoutside" }
			},
			attachmentRequirements = {
				{
					offset = { -1, 0, 0 },
					min = { -0.509999931, -0.45, -0.449999869 },
					max = { -0.4899999, -0.0500000566, -0.04999999 }
				}
			},
			colliders = {
				{
					max = { 5.96046448E-08, -5.96046448E-08, 0.0 },
					min = { -0.49999994, -0.49999994, -0.499999881 }
				}
			},
			color = color,
			lightingTransparency = 230,
			mesh = "../meshes/quarterblockdynamicoutsidecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveAmount = 0,
			pathingImpact = "AsAir",
			sideall = "neutral",
			meshRotationEuler = {
				x = 90.0,
				y = 90.0,
				z = 0.0
			}
		},
		generateType = "rotateBlockX",
		typeName = "quarterblock" .. name .. "outsidex+"
	})
	
	-- x-
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },	
				{ id = "blueprintLua", script = "blueprintmappingquarterblockstraight" }
			},
			attachmentRequirements = {
				{
					offset = { 1, 0, 0 },
					min = { 0.48999998, -0.45, -0.45 },
					max = { 0.51, -0.0500000268, 0.4499999 }
				}
			},
			colliders = {
				{
					max = { 0.5, -2.98023224E-08, 0.4999999 },
					min = { -1.39090659E-08, -0.49999997, -0.49999994 }
				}
			},
			color = color,
			isSolid = false,
			lightingTransparency = 200,
			maxStackSize = 60,
			mesh = "../meshes/quarterblockdynamicx+.ply",
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			sideall = "neutral",
			meshRotationEuler = {
				x = 0.0,
				y = 180.0,
				z = 270.0
			}
		},
		generateType = "rotateBlockX",
		typeName = "quarterblock" .. name .. "sidex-",
		rotatedTypeJSON = {
			{ pathingImpact = "AsAir" },
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsSolid" }
		}
	})
	
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockinside" }
			},
			attachmentRequirements = {
				{
					offset = {1, 0, 0},
					min = {0.49, 0.0499999672, -0.45},
					max = {0.51, 0.449999928, 0.45}
				},
				{
					offset = {1, 0, 0},
					min = {0.49, -0.45, 0.05},
					max = {0.509999931, 0.449999928, 0.45}
				}
			},
			colliders = {
				{
					max = {0.49999997, 0.49999997, 0.5},
					min = {2.98023224E-08, -2.98023224E-08, -0.5}
				},
				{
					max = {0.49999994, 0.49999997, 0.5},
					min = {2.98023224E-08, -0.5, 0.0}
				}
			},
			color = color,
			lightingTransparency = 200,
			mesh = "../meshes/quarterblockdynamicinsidecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			onRemoveAmount = 2,
			sideall = "neutral",
			meshRotationEuler = { x = 0.0, y = 0.0, z = 90.0 }
		},
		generateType = "rotateBlockX",
		typeName = "quarterblock" .. name .. "insidex-",
		rotatedTypeJSON = {
			{ pathingImpact = "AsSolid" },
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsSolid" },
			{ pathingImpact = "AsUntouchableSolid" }
		}
	})
	
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockoutside" }
			},
			attachmentRequirements = {
				{
					offset = {1, 0, 0},
					min = {0.49, -0.449999958, -0.45},
					max = {0.51, -0.0500000231, -0.05}
				}
			},
			colliders = {
				{
					max = {0.49999997, -2.98023224E-08, 0.0},
					min = {2.98023224E-08, -0.49999994, -0.49999997}
				}
			},
			color = color,
			lightingTransparency = 230,
			mesh = "../meshes/quarterblockdynamicoutsidecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveAmount = 0,
			pathingImpact = "AsAir",
			sideall = "neutral",
			meshRotationEuler = { x = -5.008957E-06, y = 180.0, z = 270.0 }
		},
		generateType = "rotateBlockX",
		typeName = "quarterblock" .. name .. "outsidex-"
	})
	
	-- z+
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockstraight" }
			},
			attachmentRequirements = {
				{
					offset = {0, 0, -1},
					min = {0.05, -0.45, -0.51},
					max = {0.45, 0.449999928, -0.48999995}
				}
			},
			colliders = {
				{
					max = {0.5, 0.49999994, 2.98023224E-08},
					min = {0.0, -0.49999997, -0.5}
				}
			},
			color = color,
			isSolid = false,
			lightingTransparency = 200,
			maxStackSize = 60,
			mesh = "../meshes/quarterblockdynamicx+.ply",
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			sideall = "neutral",
			meshRotationEuler = { x = 90.0, y = 0.0, z = 0.0 }
		},
		generateType = "rotateBlockZ",
		typeName = "quarterblock" .. name .. "sidez+",
		rotatedTypeJSON = {
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsAir" },
			{ pathingImpact = "AsSolid" }
		}

	})
	
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockinside" }
			},
			attachmentRequirements = {
				{
					offset = {0, 0, -1},
					min = {0.05, -0.45, -0.51},
					max = {0.45, 0.449999928, -0.48999995}
				},
				{
					offset = {0, 0, -1},
					min = {-0.45, -0.45, -0.509999931},
					max = {0.45, -0.0500000268, -0.48999995}
				}
			},
			colliders = {
				{
					max = {0.5, 0.49999994, 2.98023224E-08},
					min = {0.0, -0.49999997, -0.5}
				},
				{
					max = {0.5, -2.98023224E-08, 2.98023224E-08},
					min = {-0.5, -0.49999997, -0.49999997}
				}
			},
			color = color,
			lightingTransparency = 200,
			mesh = "../meshes/quarterblockdynamicinsidecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			onRemoveAmount = 2,
			sideall = "neutral",
			meshRotationEuler = { x = 90.0, y = 0.0, z = 0.0 }
		},
		generateType = "rotateBlockZ",
		typeName = "quarterblock" .. name .. "insidez+",
		rotatedTypeJSON = {
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsSolid" },
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsSolid" }
		}
	})
	
	table.insert(blocks, 
	{
		baseType = {
			attachBehaviour = {
				{ id = "ambientOcclusionFactor", strength = 0.4 },
				{ id = "blueprintLua", script = "blueprintmappingquarterblockoutside" }
			},
			attachmentRequirements = {
				{
					offset = {0, 0, -1},
					min = {0.05, 0.0499999337, -0.509999931},
					max = {0.45, 0.449999869, -0.4899999}
				}
			},
			colliders = {
				{
					max = {0.5, 0.49999994, 5.96046448E-08},
					min = {0.0, -5.96046448E-08, -0.49999994}
				}
			},
			color = color,
			lightingTransparency = 230,
			mesh = "../meshes/quarterblockdynamicoutsidecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveAmount = 0,
			pathingImpact = "AsAir",
			sideall = "neutral",
			meshRotationEuler = { x = 0.0, y = 90.0, z = 90.0 }
		},
		generateType = "rotateBlockZ",
		typeName = "quarterblock" .. name .. "outsidez+"
	})
		
	-- z-
	table.insert(blocks, {
		baseType = {
			attachBehaviour = {
				{id = "ambientOcclusionFactor", strength = 0.4},
				{ id = "blueprintLua", script = "blueprintmappingquarterblockstraight" }
			},
			attachmentRequirements = {
				{
					offset = {0,0,1},
					min = {0.05,-0.45,0.49},
					max = {0.45,0.449999928,0.509999931}
				}
			},
			colliders = {
				{
					max = {0.5,0.49999997,0.49999994},
					min = {0.0,-0.5,2.98023224E-08}
				}
			},
			color = color,
			isSolid = false,
			lightingTransparency = 200,
			maxStackSize = 60,
			mesh = "../meshes/quarterblockdynamicx+.ply",
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			sideall = "neutral",
			meshRotationEuler = {x=270.0,y=0.0,z=0.0}
		},
		generateType = "rotateBlockZ",
		typeName = "quarterblock" .. name .. "sidez-",
		rotatedTypeJSON = {
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsAir" },
			{ pathingImpact = "AsSolid" }
		}
	})

	table.insert(blocks, {
		baseType = {
			attachBehaviour = {
				{id = "ambientOcclusionFactor", strength = 0.4},
				{ id = "blueprintLua", script = "blueprintmappingquarterblockinside" }
			},
			attachmentRequirements = {
				{
					offset = {0,0,1},
					min = {0.05,-0.45,0.49},
					max = {0.45,0.449999928,0.509999931}
				},
				{
					offset = {0,0,1},
					min = {-0.45,0.0499999672,0.49},
					max = {0.45,0.449999928,0.51}
				}
			},
			colliders = {
				{
					max = {0.5,0.49999997,0.49999994},
					min = {0.0,-0.5,2.98023224E-08}
				},
				{
					max = {0.5,0.49999997,0.49999997},
					min = {-0.5,-2.98023224E-08,2.98023224E-08}
				}
			},
			color = color,
			lightingTransparency = 200,
			mesh = "../meshes/quarterblockdynamicinsidecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveType = itemname,
			onRemoveAmount = 2,
			sideall = "neutral",
			meshRotationEuler = {x=270.0,y=0.0,z=0.0}
		},
		generateType = "rotateBlockZ",
		typeName = "quarterblock" .. name .. "insidez-",
		rotatedTypeJSON = {
			{ pathingImpact = "AsSolid" },
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsUntouchableSolid" },
			{ pathingImpact = "AsSolid" }
		}

	})

	table.insert(blocks, {
		baseType = {
			attachBehaviour = {
				{id = "ambientOcclusionFactor", strength = 0.4},
				{ id = "blueprintLua", script = "blueprintmappingquarterblockoutside" }
			},
			attachmentRequirements = {
				{offset = {0,0,1}, min = {0.05,0.0499999672,0.49}, max = {0.45,0.449999928,0.51}}
			},
			colliders = {
				{max = {0.5,0.49999997,0.49999997}, min = {0.0,-2.98023224E-08,2.98023224E-08}}
			},
			color = color,
			lightingTransparency = 230,
			mesh = "../meshes/quarterblockdynamicoutsidecorner.ply",
			isSolid = false,
			onPlaceAudio = "woodPlace",
			onRemoveAudio = "woodDeleteLight",
			onRemoveAmount = 0,
			pathingImpact = "AsAir",
			sideall = "neutral",
			meshRotationEuler = {x=270.0,y=0.0,z=0.0}
		},
		generateType = "rotateBlockZ",
		typeName = "quarterblock" .. name .. "outsidez-"
	})

		
	return blocks
end

local allBlocks = {}

local function AddQuarterblocks(...)
	local blocks = MakeQuarterBlock(...)
	for _, block in ipairs(blocks) do
		table.insert(allBlocks, block)
	end
end

AddQuarterblocks("grey", "../textures/icons/quarterblockgrey.png", "quarterblockgreyitem", "#636363")
AddQuarterblocks("browndark", "../textures/icons/quarterblockbrowndark.png", "quarterblockbrowndarkitem", "#332106")
AddQuarterblocks("brownlight", "../textures/icons/quarterblockbrownlight.png", "quarterblockbrownlightitem", "#917451")

return allBlocks
