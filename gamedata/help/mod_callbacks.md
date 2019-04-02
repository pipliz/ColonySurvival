CallbackType: `None`
=======
No registered uses


CallbackType: `OnAssemblyLoaded`
=======
## Description
Signature: void (string a)
Arg a: the loaded assemblies' path
Called directly upon discovering a method with this attribute, during assembly parsing.
No defined order in which they are called.
No registered uses


CallbackType: `AfterModsLoaded`
=======
## Description
Signature: void (List<ModLoader.ModAssembly> a)
Arg a: the loaded assemblies
Called after parsing all modinfo files
No registered uses


CallbackType: `OnItemTypeRegistered`
=======
## Description
Signature: void (ItemTypes.ItemType a)
Arg a: a type that is being registered
Called once for each type that is being registered to ItemTypes
## Registered callbacks: 1
0.	`pipliz.server.itemtypesserver`
		_Registers parents, icons and meshes_


CallbackType: `OnLateUpdate`
=======
## Description
Signature: void ()
Called inside unity's LateUpdate method
No registered uses


CallbackType: `AfterStartup`
=======
## Description
Signature: void ()
Called somewhere after startup, in the first frame start
No registered uses


CallbackType: `AfterSelectedWorld`
=======
## Description
Signature: void ()
First callback after the world to load has been determined
## Registered callbacks: 7
0.	`pipliz.server.applytexturemappingpatches`
		_Waits for texture mapping patches to load, then merges them._
1.	`pipliz.server.loadaudiofiles`
2.	`pipliz.server.loadtimecycle`
		_Sets the TimeCycle time, loading from ServerManager.WorldSettings_
3.	`pipliz.server.registertexturemappingtextures`
		Depends On 0. `pipliz.server.applytexturemappingpatches`
		_Registers the texture mapping files to load (from the registered texturemappings)_
4.	`pipliz.startloaddifficulty`
		_Starts loading the difficulty json files_
5.	`registernpctextures`
6.	`registerwatertextures`


CallbackType: `AfterAddingBaseTypes`
=======
## Description
Signature: void (Dictionary<string, ItemTypesServer.ItemTypeRaw> a)
Arg a: Loaded items that are about to be parsed - add/remove types here
No registered uses


CallbackType: `AfterItemTypesDefined`
=======
## Description
Signature: void ()
First callback after all item types should be defined, so you can resolve types etc here
## Registered callbacks: 25
0.	`pipliz.server.loadnpctypes`
1.	`areajobs.insertattributed`
		Depends On 0. `pipliz.server.loadnpctypes`
		_Finds & registers areajobdefs marked AreaJobDefinitionAutoLoaderAttribute_
2.	`create_servermanager_trackers`
		_Starts Various trackers and singletons saved in the servermanager_
3.	`blockentitycallback.autoloaders`
		Depends On 2. `create_servermanager_trackers`
		_Calls BlockEntityCallbacks.SearchAssembliesForAutoLoaders - instantiating classes with the IBlockEntityAutoLoaderBase type interfaces_
4.	`chunk_dedupe_initializer`
5.	`pipliz.server.registerdefaultdifficulty`
		_Registers the default difficulty to the DifficultyManager. ProvidesFor loadplayers so that loading the difficulty per player works._
6.	`pipliz.endloaddifficulty`
		_Awaits loading of settings/difficulty.json from pipliz.startloaddifficulty_
7.	`pipliz.server.recipeplayerload`
		Depends On 2. `create_servermanager_trackers`
		_Waits for recipe patches to load, then merges them_
8.	`pipliz.server.recipenpcload`
		Depends On 2. `create_servermanager_trackers`
		_Waits for npc recipe patches to complete loading, then merges and registers them_
9.	`pipliz.server.loadresearchables`
		Depends On 2. `create_servermanager_trackers`
		Depends On 8. `pipliz.server.recipenpcload`
		Depends On 7. `pipliz.server.recipeplayerload`
		_Load & resolve researchable configs_
10.	`pipliz.server.loadplayers`
		_Starts loading player data_
11.	`createareajobdefinitions`
		Depends On 0. `pipliz.server.loadnpctypes`
		_Registers json area jobs_
12.	`pipliz.server.loadcolonies`
		Depends On 1. `areajobs.insertattributed`
		Depends On 2. `create_servermanager_trackers`
		Depends On 11. `createareajobdefinitions`
		Depends On 6. `pipliz.endloaddifficulty`
		Depends On 0. `pipliz.server.loadnpctypes`
		Depends On 10. `pipliz.server.loadplayers`
		Depends On 9. `pipliz.server.loadresearchables`
		Depends On 8. `pipliz.server.recipenpcload`
		Depends On 7. `pipliz.server.recipeplayerload`
		Depends On 5. `pipliz.server.registerdefaultdifficulty`
		_Starts loading the colony data; also trigger OnLoadingColony_
13.	`pipliz.blocknpcs.registerjobs`
		Depends On 2. `create_servermanager_trackers`
		Depends On 0. `pipliz.server.loadnpctypes`
		_Adds all the job block implementations to BlockJobManagerTracker_
14.	`creategrowabledefinitions`
		Depends On 2. `create_servermanager_trackers`
		_Registers growable block json types_
15.	`register.basegame.blockjobs`
		Depends On 2. `create_servermanager_trackers`
		Depends On 0. `pipliz.server.loadnpctypes`
		Provides For 16. `create_savemanager`
16.	`create_savemanager`
		Depends On 3. `blockentitycallback.autoloaders`
		Depends On 4. `chunk_dedupe_initializer`
		Depends On 14. `creategrowabledefinitions`
		Depends On 13. `pipliz.blocknpcs.registerjobs`
		Depends On 12. `pipliz.server.loadcolonies`
		_Starts ServerManager.SaveManager_
		_Starts loading the index of the chunk storage_
17.	`find_auto_chatcommands`
18.	`pipliz.server.blackandwhitelistingreload`
		_Loads the black & whitelist settings_
19.	`pipliz.server.loadpermissions`
		_Load permissions_
20.	`pipliz.server.loadwater`
		_Starts loading water blocks_
21.	`pipliz.server.registermonstertextures`
		Depends On 0. `pipliz.server.loadnpctypes`
		_Registers monster textures from registered NPCTypes_
22.	`wait_complete_startup_chunks`
		Depends On 16. `create_savemanager`
		_Waits for the savemanager to complete loading its index & the startup chunks_
23.	`set_colony_sciencemask`
		Depends On 2. `create_servermanager_trackers`
		Depends On 12. `pipliz.server.loadcolonies`
		Depends On 22. `wait_complete_startup_chunks`
		_Will disable missing science and set the correct biome dependent science for loaded colonies_
24.	`trading.doublelinkrules`
		Depends On 12. `pipliz.server.loadcolonies`
		_Copies all outgoing rules to the respective goal colonies' incoming rules._
		_It seems not essential to have this happen before npc's are loaded:_
		_worst case trader npcs will idle for a few second upon loading (which they do anyway @ AI gen)_


CallbackType: `AfterWorldLoad`
=======
## Description
Signature: void ()
After all misc data is loaded (structures, npcs, player data, etc)
Does not mean chunks are loaded though
## Registered callbacks: 5
0.	`pipliz.server.localization.convert`
		_Waits for locale patches to have loaded, then merges them and prepares the network packages_
1.	`pipliz.server.monsterspawner.fetchnpctypes`
		_Caches the default monster types_
2.	`pipliz.server.monsterspawner.register`
		_Registers the default monsterspawner to MonsterTracker.MonsterSpawner_
3.	`save_recipemapping`
		_Saves the recipe mapping table if it's changed_
4.	`start_generator`


CallbackType: `AfterNetworkSetup`
=======
## Description
Signature: void ()
After the networkwrapper clas is ready to send data. Can take a few seconds for steam servers.
No registered uses


CallbackType: `OnUpdateStart`
=======
## Description
Signature: void ()
Called early on in unity's Update method
## Registered callbacks: 1
0.	`pipliz.server.setsecondsthisframe`
		_Updates the 'ThisFrame' fields from Pipliz.Time_


CallbackType: `OnUpdate`
=======
## Description
Signature: void ()
In the middle of unity's Update method.
## Registered callbacks: 4
0.	`pipliz.server.chunkupdater`
		_Checks if chunks can be unloaded_
1.	`pipliz.server.tickscounter`
		_Counts framerate for /tps_
2.	`pipliz.server.updatetimecycle`
		_Updates TimeCycle_
3.	`update_water`


CallbackType: `OnUpdateEnd`
=======
## Description
Signature: void ()
At the end of unity's Update method.
## Registered callbacks: 1
0.	`pipliz.server.senddirtystockpiles`


CallbackType: `OnFixedUpdate`
=======
## Description
Signature: void ()
Unity's OnFixedUpdate method. See unity documentation.
No registered uses


CallbackType: `OnConstructWorldSettingsUI`
=======
## Description
Signature: void (Players.Player p, NetworkUI.NetworkMenu m)
Arg p: The player that'll receive this menu
Arg m: The menu that can be edited / made, will be send after the callback completes
## Registered callbacks: 1
0.	`pipliz.buildbase`


CallbackType: `OnApplicationFocus`
=======
## Description
Signature: void (bool a)
Unity's OnApplicationFocus method. See unity documentation.
No registered uses


CallbackType: `OnApplicationPause`
=======
## Description
Signature: void (bool a)
Unity's OnApplicationPause method. See unity documentation.
No registered uses


CallbackType: `OnPlayerConnectedEarly`
=======
## Description
Signature: void (Players.Player a)
Arg a: The player that is connecting.
Early on in the player connection process - the player is probably not ready to receive messages yet
## Registered callbacks: 2
0.	`pipliz.server.sendinitialtime`
		_Sends the time and heartbeat to the player early on_
1.	`pipliz.server.start_loadsurroundings`


CallbackType: `OnPlayerDisconnected`
=======
## Description
Signature: void (Players.Player a)
Arg a: The player that disconnected.
No registered uses


CallbackType: `OnSavingPlayer`
=======
## Description
Signature: void (JSONNode a, Players.Player b)
Arg a: The json data that'll be saved for the player.
Arg b: said player
## Registered callbacks: 1
0.	`pipliz.server.savesetflight`
		_Save player flight state_


CallbackType: `OnLoadingPlayer`
=======
## Description
Signature: void (JSONNode a, Players.Player b)
Arg a: The json data that got loaded for the player.
Arg b: said player
## Registered callbacks: 1
0.	`pipliz.server.loadsetflight`
		_Load player flight state_


CallbackType: `OnQuitEarly`
=======
## Description
Signature: void ()
Called early on in the quit method queue (Application.OnQuit -200)
## Registered callbacks: 1
0.	`pipliz.shared.waitforasyncquitsearly`
		_Waits for async items to complete (mostly from autosaving)_


CallbackType: `OnQuit`
=======
## Description
Signature: void ()
Called in the quit method queue (Application.OnQuit 0)
## Registered callbacks: 6
0.	`pipliz.jointhreads`
1.	`pipliz.server.savecolonies`
		_Start saving colony data_
2.	`pipliz.server.saveplayers`
		_Starts saving all dirty-marked players_
3.	`pipliz.server.savetimecycle`
		_Saves the time to ServerManager.WorldSettings_
4.	`pipliz.server.savewater`
		_Saves water data_
5.	`pipliz.server.saveworldsettings`
		Depends On 3. `pipliz.server.savetimecycle`
		_Saves ServerManager.WorldSettings_


CallbackType: `OnQuitLate`
=======
## Description
Signature: void ()
Called late in the quit method queue (Application.OnQuit 100)
## Registered callbacks: 1
0.	`pipliz.shared.waitforasyncquits`
		_Waits for async items to complete (mostly from autosaving)_


CallbackType: `OnSavedChunk`
=======
## Description
Signature: void (Chunk a)
Saved chunk x
No registered uses


CallbackType: `OnLoadedChunk`
=======
## Description
Signature: void (Chunk a)
Loaded chunk x from the save manager
No registered uses


CallbackType: `OnPlayerMoved`
=======
## Description
Signature: void (Players.Player a, Vector3 oldPosition)
Called approx 6 times per second per player. New position/rotation is set on the Players.Player argument.
## Registered callbacks: 2
0.	`pipliz.server.loadsurroundings`
		_Queues up chunks to load if the player moves to other chunks_
1.	`send_audiobiome`


CallbackType: `OnModifyResearchables`
=======
## Description
Signature: void (Dictionary<string, BaseResearchable> researches)
Called inside of OnAddResearchables - allows modifying researches added through jsonFiles before they're registered
## Registered callbacks: 3
0.	`addbannercallbacks`
1.	`addhealthcallbacks`
2.	`farmingresults`


CallbackType: `OnTryChangeBlock`
=======
## Description
Signature: void (ModLoader.OnTryChangeBlockData a)
Callback triggered upon a call to ServerManager.TryChangeBlock - used by various code and the client to edit blocks
Nothing changed yet when this callback happens and the change can be blocked.
Arg a contains the following fields:
RequestedByPlayer - the player (in)directly calling TryChangeBlock. Can be null.
Position - the position where the change will happen
TypeOld - the current world type at that position
TypeNew - the new world type after the desired change. Can be 0 to indicate block removal
CallbackState - enum to communicate what to do with the change after this callback:
	-Default - callback doesn't require any change to the desired outcome
	-CallbackConsumed - TryChangeBlock won't do anything more (nothing happens, but it's also not cleanly cancelled)
	-Cancelled - TryChangeBlock won't do anything more and cleanly return false. Use this for anti-grief and similar uses.
CallbackOrigin - enum to indicate the origin of this callback
	-ClientPlayerManual - the player building/digging
	-ClientScript - the player using tools (command tool placing jobs etc)
	-Server - server side changes, like water spread or farmers
InventoryItemResults - Inventory results the player will receive, only used if the origin is ClientPlayerManual. Don't store or change the reference - but you can change the contents.
CallbackConsumedResult - Return value of TryChangeBlock if you set callbackState to Consumed
PlayerClickedData - only set if callback origin is from the client. Same data as in the OnPlayerClicked callback
## Registered callbacks: 1
0.	`TODO_FIX_BANNER_MOVING`


CallbackType: `OnPlayerConnectedLate`
=======
## Description
Signature: void (Players.Player a)
Arg a: The Player that is connecting
Messages send here will work unlike with OnPlayerConnectedEarly. May be delayed till after the client is done loading.
## Registered callbacks: 4
0.	`pipliz.server.meshedobjects.sendtable`
		_Sends the meshed object settings data_
1.	`pipliz.server.sendnpctypes`
		_Sends the registered NPCType settings to the player_
2.	`pipliz.server.sendsetflight`
		_Send player flight state_
3.	`send_audiobiome`


CallbackType: `OnAddResearchables`
=======
## Description
Signature: void ()
The place to add researchables to Server.Science.ScienceManager
## Registered callbacks: 1
0.	`registerresearchables`
		_Registers AutoLoadedResearchableAttribute marked IResearchable types_


CallbackType: `OnConstructTooltipUI`
=======
## Description
Signature: void(NetworkUI.ConstructTooltipUIData data)
Constructs the networkMenu of type hovertype that'll be send to the player
If hoverType is PlayerRecipe, NPCRecipe or Science the hoverKey is set to their respective keys.
If hoverType is Item, the item is in hoverItem
## Registered callbacks: 1
0.	`pipliz.buildbase`
		_Builds base of tooltip ui_


CallbackType: `OnPlayerRecipeSettingChanged`
=======
## Description
Signature: void (RecipeStorage.PlayerRecipeStorage storage, Recipe recipe, Box<RecipeSetting> recipeSetting)
Arg storage: the players' recipe settings storage
Arg recipe: the recipe for which the setting is being changed
Arg recipeSetting: the new setting for the recipe
Triggered every time a recipe limit is changed.
Don't store the Box<>, it's re-used.
See storage.Player for the owner of the recipe
This callback is also called upon loading the settings from json - but only for non-default values (defaults aren't stored)
No registered uses


CallbackType: `OnNPCCraftedRecipe`
=======
## Description
Signature: void (IJob job, Recipe recipe, List<Recipes.RecipeResult> results)
Triggered when an npc doing {job} crafts {recipe}, creating {results}
The results are re-used, don't store it.
Results can be edited. After the callback they'll be added to the npc/block's inventory
If the results are not empty, the npc will show a npc indicator with a weighted random type from the non-optional results
No registered uses


CallbackType: `OnPlayerDeath`
=======
## Description
Signature: void (Players.Player a)
Player died
## Registered callbacks: 1
0.	`pipliz.server.onplayerdeath`
		_Sets health to 0, sends audio, clears inventory_


CallbackType: `OnPlayerRespawn`
=======
## Description
Signature: void (Players.Player a)
Player respawns
## Registered callbacks: 1
0.	`pipliz.server.onplayerrespawn`
		_Teleports the player to their banner, resets health_


CallbackType: `OnMonsterSpawned`
=======
## Description
Signature: void (IMonster a)
Monster {a} was spawned
No registered uses


CallbackType: `OnMonsterHit`
=======
## Description
Signature: void (IMonster monster, ModLoader.OnHitData d)
{monster} will be hit with {d}
d.ResultDamage - the resulting damage that'll be applied
d.HitDamage - the damage that'll be applied without modifiers
d.HitSourceObjectType - enum to indicate what d.HitSourceObject is
	None -> unknown, shouldn't really be used
	Misc -> mod use?
	PlayerProjectile -> hit by a players projectile, sourceObject is a Players.Player
	PlayerClick -> hit by a players melee, sourceObject is a Players.Player
	NPC -> hit be some npc's actions, sourceObject is a NPCBase
	Monster -> hit by some monster, sourceObject is a IMonster
	FallDamage -> damage from falling. SourceObject is who caused the fall? :)
No registered uses


CallbackType: `OnMonsterDied`
=======
## Description
Signature: void (IMonster a)
Monster {a} died
No registered uses


CallbackType: `OnNPCRecruited`
=======
## Description
Signature: void (NPC.NPCBase a)
A npc got recruited
No registered uses


CallbackType: `OnNPCLoaded`
=======
## Description
Signature: void (NPC.NPCBase a, JSONNode n)
A npc got loaded from said jsonnode
No registered uses


CallbackType: `OnNPCDied`
=======
## Description
Signature: void (NPC.NPCBase a)
A npc died
## Registered callbacks: 1
0.	`pipliz.server.refundrecruitment`
		_Refunds the old jobs' recruitment item to the colony stockpile_


CallbackType: `OnNPCSaved`
=======
## Description
Signature: void (NPC.NPCBase a, JSONNode n)
A npc gets saved into said jsonnode
Should be safe to add custom data here
No registered uses


CallbackType: `OnNPCJobChanged`
=======
## Description
Signature: void (TupleStruct<NPC.NPCBase, IJob, IJob> data)
{data.item1} changed job from {data.item2} to {data.item3}
## Registered callbacks: 1
0.	`pipliz.server.refundrecruitement`
		_Refunds the old jobs' recruitment item to the colony stockpile_


CallbackType: `OnNPCHit`
=======
## Description
Signature: void (NPC.NPCBase npc, ModLoader.OnHitData d)
{npc} gets hit by {d}, see OnMonsterHit for {d} documentation
No registered uses


CallbackType: `OnPlayerClicked`
=======
## Description
Signature: void (Players.Player player, Pipliz.Box<Shared.PlayerClickedData> boxedData)
{player} clicked, resulting in {boxedData.item1}. Don't store the Box<>, it's reused.
fields in {boxedData.item1}:
	-consumedType -> indicates if a callback consumed this click
	-clickType -> whether its a left or right click
	-rayCastHit.rayHitType -> indicates which other data is valid:
	--if it's Block - distanceToHit, voxelHit, voxelSideHit and typeHit are valid
	--if it's NPC - distanceToHit and hitNPCID become valid
## Registered callbacks: 2
0.	`check_banner_click`
1.	`pipliz.server.players.hitnpc`
		_Manages players punching npcs/monsters_


CallbackType: `OnPlayerHit`
=======
## Description
Signature: void (Players.Player player, ModLoader.OnHitData d)
{player} gets hit by {d}, see OnMonsterHit for {d} documentation
No registered uses


CallbackType: `OnAutoSaveWorld`
=======
## Description
Signature: void ()
Triggers an autosave every x minutes, to begin autosaving non-block data (jobs, npc's, players)
## Registered callbacks: 3
0.	`pipliz.server.autosaveplayers`
		_Starts saving all dirty-marked players_
1.	`pipliz.server.autosavewater`
		_Saves water data_
2.	`pipliz.server.savecolonies`
		_Start saving colony data_


CallbackType: `OnNPCGathered`
=======
## Description
Signature: void (IJob job, Vector3Int location, List<ItemTypes.ItemTypeDrops> results)
Triggered every time {job} gathers {results} from {location}
Can edit the results; don't store them - the list is re-used.
After the callback, results will be added to the npc's inventory.
The location does not have to be the job/npc's position - see the construction jobs.
No registered uses


CallbackType: `OnShouldKeepChunkLoaded`
=======
## Description
Signature: void (ChunkUpdating.KeepChunkLoadedData data)
!!! Will be called from multiple threads, simultaneously !!!
Periodically triggered for every chunk loaded. Use it to keep chunks loaded - and to indicate how long you expect them to stay loaded.
{data.CheckedChunk} -> the chunk
{data.MillisecondsTillNextCheck} -> the minimum time until another callback will be fired. Defaults to random between 24000 and 64000
{data.Result} -> bool indicating whether or not to keep this chunk. Defaults to false (set to true to keep it)
{data.ChunkLoadedSource} -> source for this callback. If loadedstorage / loadedgenerator, the chunk is already locked for writing. if Updater, it is not locked.
## Registered callbacks: 3
0.	`bannercheck`
		_Checks to keep chunks near banners loaded_
1.	`pipliz.server.playercheck`
		Depends On 0. `bannercheck`
		_Keeps chunks near player alive_
2.	`check_blockentities`
		Depends On 1. `pipliz.server.playercheck`


CallbackType: `AddItemTypes`
=======
## Description
Signature: void (Dictionary<string, ItemTypesServer.ItemTypeRaw> dict)
Basically same as 'AfterAddingBaseTypes'
Mostly used to add...the base types, so that the other callback is correctly named :)
## Registered callbacks: 2
0.	`pipliz.blocknpcs.addlittypes`
		_Creates some lit/rotatable types - furnace/torch etc_
1.	`pipliz.server.applymoditempatches`
		Depends On 0. `pipliz.blocknpcs.addlittypes`
		_Waits for itemtype patches to load, then merges/registers them to the dict_


CallbackType: `OnPlayerChangedNetworkUIStorage`
=======
## Description
Signature: void (TupleStruct<Players.Player p, JSONNode node, string id> data)
Arg p: The player that sent changed networkUI storage data
Arg node: Said changed data
Arg id: the networkmenu ID
## Registered callbacks: 1
0.	`pipliz.parsenetui`


CallbackType: `OnPlayerPushedNetworkUIButton`
=======
## Description
Signature: void (NetworkUI.ButtonPressCallbackData data)
Data -> the storage node, button ID and player that pushed the button
## Registered callbacks: 1
0.	`handle_builtin`


CallbackType: `OnSendAreaHighlights`
=======
## Description
Signature: void(Players.Player player, List<AreaJobTracker.AreaHighlight> list, List<ushort> showWhileHoldingTypes)
Edit the highlights list, adding desired area highlights to be sent to the player.
Edit the showWhileHoldingTypes to add/remove types that will show <all> areas when selected in the inventory
You can manually trigger this callback through AreaJobTracker.SendData(player)
## Registered callbacks: 2
0.	`pipliz.defaultholdingtypes`
		_Sets default showWhileHoldingTypes_
1.	`pipliz.sendjobareas`
		_Sends the registered AreaJobs_


CallbackType: `OnRemoveAreaHighlight`
=======
## Description
Signature: void(Players.Player player, Vector3Int minimumCorner, Box<bool> isHandled
Request to remove an area that has said minimum corner
Check isHandled's content to see if the callback was handled by something else
Set said isHandled' content to prevent other callbacks from handling it.
## Registered callbacks: 1
0.	`pipliz.removejobarea`
		_Removes a registered areajob if it matches the position_


CallbackType: `OnActiveColonyChanges`
=======
## Description
Signature: void(Players.Player player, Colony previouslyActiveColony, Colony newActiveColony)
Called when the active colony changes
## Registered callbacks: 5
0.	`onchange`
1.	`resend_areajobs`
2.	`sendconstructiondata`
3.	`sendhappiness`
4.	`sendresearch`


CallbackType: `OnSavingColony`
=======
## Description
Signature: void(Colony colony, JSONNode json)
Called when saving a colony
## Registered callbacks: 9
0.	`saveareajobs`
1.	`savedifficulty`
		_Saved the active difficulty setting, into node['difficulty']. If the setting equals the default, does not write there_
2.	`savehappiness`
3.	`savenpcs`
4.	`saveowners`
5.	`saverecipesettings`
6.	`savescience`
7.	`savestockpile`
8.	`savetrading`


CallbackType: `OnLoadingColony`
=======
## Description
Signature: void(Colony colony, JSONNode json)
Called when loading a colony
## Registered callbacks: 9
0.	`loadnpcs`
1.	`loadareajobs`
		Depends On 0. `loadnpcs`
2.	`loaddifficulty`
		_Loads the node['difficulty'] key if present, using the node['difficulty']['key'] type of difficulty loader_
3.	`loadhappiness`
4.	`loadowners`
5.	`loadrecipesettings`
6.	`loadscience`
7.	`loadstockpile`
8.	`loadtrading`


CallbackType: `OnLoadingTerrainGenerator`
=======
## Description
Signature: void(TerrainGeneratorBase terrainGen)
Called when the terrain generator is created - during create_servermanager_trackers in AfterItemTypesDefined
## Registered callbacks: 5
0.	`apply_metabiome_patches`
1.	`apply_sciencebiome_patches`
2.	`apply_orelayers`
		Depends On 1. `apply_sciencebiome_patches`
3.	`load_structure_patches`
4.	`load_biome_patches`
		Depends On 3. `load_structure_patches`


CallbackType: `OnPlayerEditedNetworkInputfield`
=======
## Description
Signature: void (NetworkUI.InputfieldEditCallbackData data)
Data -> the storage node, inputfield ID and player that pushed the button
## Registered callbacks: 1
0.	`handle_builtin`


CallbackType: `OnCreatedColony`
=======
## Description
Signature: void(Colony colony)
Called when a colony is created, does not include loaded colonies (!)
## Registered callbacks: 1
0.	`disable_some_science`
		_Will disable any science that is missing its implementation or that requires a biome._
		_Biome dependent science will be re-enabled after adding a banner if required_


CallbackType: `OnConstructColonyOwnerManagementUI`
=======
## Description
Signature: void(Players.Player p, NetworkUI.NetworkMenu m)
Arg p: The player that'll receive this menu
Arg m: The menu that can be edited / made, will be send after the callback completes
## Registered callbacks: 1
0.	`pipliz.buildbase`


CallbackType: `OnConstructInventoryManageColonyUI`
=======
## Description
Signature: void(Players.Player p, NetworkUI.NetworkMenu m)
Arg p: The player that'll receive this menu
Arg m: The menu that can be edited / made, will be send after the callback completes
## Registered callbacks: 1
0.	`pipliz.buildbase`


CallbackType: `OnConstructColonyRecruitmentUI`
=======
## Description
Signature: void(Players.Player p, NetworkUI.NetworkMenu m)
Arg p: The player that'll receive this menu
Arg m: The menu that can be edited / made, will be send after the callback completes
## Registered callbacks: 1
0.	`pipliz.buildbase`


CallbackType: `OnConstructBannerPlacementUI`
=======
## Description
Signature: void(Players.Player p, NetworkUI.NetworkMenu m)
Arg p: The player that'll receive this menu
Arg m: The menu that can be edited / made, will be send after the callback completes
## Registered callbacks: 1
0.	`pipliz.buildbase`


CallbackType: `OnConstructBannerClickedUI`
=======
## Description
Signature: void(Players.Player p, NetworkUI.NetworkMenu m)
Arg p: The player that'll receive this menu
Arg m: The menu that can be edited / made, will be send after the callback completes
## Registered callbacks: 1
0.	`pipliz.buildbase`


CallbackType: `OnHandleColonySelected`
=======
## Description
Signature: void(NetworkUI.ButtonPressCallbackData data, int colonyID)
## Registered callbacks: 1
0.	`handle_builtin`


CallbackType: `OnPlayerSelectedTypePopup`
=======
## Description
Signature: void(Players.Player player, ushort selectedItemType, JSONNode payload)
payload is what was used as arg when first sent, to identify who sent it for what reason
## Registered callbacks: 1
0.	`traderule`


