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
## Registered callbacks: 2
0.	`pipliz.colonyserverwrapper.load`
		_Checks commandline args, sets up connection if required_
1.	`pipliz.blocknpcs.assemblyload`
		_Sets BaseGame gamedata directory_


CallbackType: `AfterModsLoaded`
=======
## Description
Signature: void (List<ModLoader.ModAssembly> a)
Arg a: the loaded assemblies
Called after parsing all modinfo files
## Registered callbacks: 3
0.	`pipliz.mods.apiprovider.growableblocks.findattributes`
		_Searches for types marked with GrowableBlockDefinitionAutoLoaderAttribute_
1.	`pipliz.mods.apiprovider.areajobs.findattributes`
		_Finds types marked with AreaJobDefinitionAutoLoaderAttribute_
2.	`pipliz.apiprovider.parsemods`
		_Checks loaded types for types marked with APIProvider.Science.AutoLoadedResearchableAttribute_


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
## Registered callbacks: 1
0.	`pipliz.server.ai.aimanager.queuebatchedchunks`
		_Queues the batch of dirty chunks for recalculating AI data_


CallbackType: `AfterStartup`
=======
## Description
Signature: void ()
Called somewhere after startup, in the first frame start
## Registered callbacks: 1
0.	`pipliz.server.ai.aimanager.startthread`
		_Starts AI precalculation thread_


CallbackType: `AfterSelectedWorld`
=======
## Description
Signature: void ()
First callback after the world to load has been determined
## Registered callbacks: 7
0.	`pipliz.startloaddifficulty`
		_Starts loading the difficulty json files_
1.	`pipliz.server.registerwatertextures`
		_Registers the default water texture files_
2.	`pipliz.server.registernpctextures`
		_Registers the default npc texture files_
3.	`pipliz.server.registeraudiofiles`
		_Loads the audioFiles.json file_
4.	`pipliz.server.loadaudiofiles`
		Depends On 3. `pipliz.server.registeraudiofiles`
		_Processes the ItemTypesServer.AudioFiles json_
5.	`pipliz.server.applytexturemappingpatches`
		Provides For 6. `pipliz.server.registertexturemappingtextures`
		_Waits for texture mapping patches to load, then merges them._
6.	`pipliz.server.registertexturemappingtextures`
		_Registers the texture mapping files to load (from the registered texturemappings)_


CallbackType: `AfterAddingBaseTypes`
=======
## Description
Signature: void (Dictionary<string, ItemTypesServer.ItemTypeRaw> a)
Arg a: Loaded items that are about to be parsed - add/remove types here
## Registered callbacks: 1
0.	`pipliz.blocknpcs.addlittypes`
		_Creates some lit/rotatable types - furnace/torch etc_


CallbackType: `AfterItemTypesDefined`
=======
## Description
Signature: void ()
First callback after all item types should be defined, so you can resolve types etc here
## Registered callbacks: 25
0.	`pipliz.server.terraingenerator.setdefault`
		_Sets the default terrain generator to TerrainGenerator.UsedGenerator_
1.	`pipliz.server.registerdefaultdifficulty`
		Provides For 19. `pipliz.server.loadplayers`
		_Registers the default difficulty to the DifficultyManager. ProvidesFor loadplayers so that loading the difficulty per player works._
2.	`pipliz.server.registercallbacks`
		_Registers base block callbacks (water, crate, beds, those things) to ItemTypesServer_
3.	`pipliz.server.recipeplayerload`
		Provides For 18. `pipliz.server.loadresearchables`
		_Waits for recipe patches to load, then merges them_
4.	`pipliz.server.recipenpcload`
		Provides For 18. `pipliz.server.loadresearchables`
		_Waits for npc recipe patches to complete loading, then merges and registers them_
5.	`pipliz.server.loadwater`
		_Starts loading water blocks_
6.	`pipliz.server.loadtimecycle`
		_Sets the TimeCycle time, loading from ServerManager.WorldSettings_
7.	`pipliz.server.loadpermissions`
		_Load permissions_
8.	`pipliz.server.loadnpctypes`
		Provides For 22. `pipliz.server.loadnpcs`
		_Loads gamedata/npctypes.json_
9.	`pipliz.server.registermonstertextures`
		Depends On 8. `pipliz.server.loadnpctypes`
		_Registers monster textures from registered NPCTypes_
10.	`pipliz.server.blackandwhitelistingreload`
		_Loads the black & whitelist settings_
11.	`pipliz.mods.apiprovider.growableblocks.insertattributed`
		Provides For 12. `pipliz.server.growableblocks.loadblocks`
		_Creates instance of registered IGrowableBlockDefinitions and registers those to GrowableBlockManager_
12.	`pipliz.server.growableblocks.loadblocks`
		_Load all registered growable block types' files_
13.	`pipliz.mods.apiprovider.areajobs.insertattributed`
		Provides For 23. `pipliz.server.loadareajobs`
		_Creates instance of registered IAreaJobDefinition and registers those to AreaJobTracker_
14.	`pipliz.endloaddifficulty`
		Provides For 19. `pipliz.server.loadplayers`
		_Awaits the difficulty json files to be loaded_
		_You can edit the difficulties on DifficultySetting.Cooldowns / Keys after this callback (but before loadplayers)_
15.	`pipliz.blocknpcs.registerjobs`
		Provides For 17. `pipliz.apiprovider.jobs.resolvetypes`
		_Adds all the job block implementations to BlockJobManagerTracker_
16.	`pipliz.blocknpcs.registerchangetypes`
		_Registers changetypes for created blocks from pipliz.blocknpcs.addlittypes_
17.	`pipliz.apiprovider.jobs.resolvetypes`
		Depends On 8. `pipliz.server.loadnpctypes`
		Provides For 18. `pipliz.server.loadresearchables`
		_Activates the blockjobmanagers, and registers block job provided npc types_
18.	`pipliz.server.loadresearchables`
		Provides For 19. `pipliz.server.loadplayers`
		_Load & resolve researchable configs_
19.	`pipliz.server.loadplayers`
		_Starts loading player data_
20.	`pipliz.server.loadstockpileblocks`
		Depends On 19. `pipliz.server.loadplayers`
		_Load stockpile blocks_
21.	`pipliz.server.loadbanners`
		Depends On 19. `pipliz.server.loadplayers`
		_Loads banners_
22.	`pipliz.server.loadnpcs`
		Depends On 21. `pipliz.server.loadbanners`
		_Starts loading the npc data_
23.	`pipliz.server.loadareajobs`
		Depends On 22. `pipliz.server.loadnpcs`
		_Load all registered areajobs' files_
24.	`pipliz.server.loadbedblocks`
		Depends On 21. `pipliz.server.loadbanners`
		_Load bed blocks_


CallbackType: `AfterWorldLoad`
=======
## Description
Signature: void ()
After all misc data is loaded (structures, npcs, player data, etc)
Does not mean chunks are loaded though
## Registered callbacks: 6
0.	`pipliz.server.monsterspawner.register`
		_Registers the default monsterspawner to MonsterTracker.MonsterSpawner_
1.	`pipliz.server.monsterspawner.fetchnpctypes`
		_Caches the default monster types_
2.	`pipliz.server.localization.convert`
		_Waits for locale patches to have loaded, then merges them and prepares the network packages_
3.	`pipliz.server.ai.aimanager.defaultpathfinder`
		_Registers the default pathfinder to AIManager.ZombiePathFinder and AIManager.NPCPathFinder_
4.	`pipliz.apiprovider.jobs.registercallbacks`
		_Registers callbacks for block job trackers_
5.	`pipliz.apiprovider.jobs.load`
		_Loads files for registered block job trackers_


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
## Registered callbacks: 6
0.	`pipliz.server.updatetimecycle`
		_Updates TimeCycle_
1.	`pipliz.server.tickscounter`
		_Counts framerate for /tps_
2.	`pipliz.server.senddirtyscience`
		_Send dirty-marked science managers to players_
3.	`pipliz.server.growables.update`
		_Updates growable blocks_
4.	`pipliz.server.chunkupdater`
		_Checks if chunks can be unloaded_
5.	`pipliz.colonyserverwrapper.process`
		_Processes packets from external socket_


CallbackType: `OnUpdateEnd`
=======
## Description
Signature: void ()
At the end of unity's Update method.
## Registered callbacks: 1
0.	`pipliz.server.senddirtystockpiles`


CallbackType: `OnGUI`
=======
## Description
Signature: void ()
Unity's OnGUI method. See unity documentation.
No registered uses


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
		_Builds base of the world settings ui_


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
1.	`pipliz.server.resetsciencedeltaupdates`
		_Reset players' science managers to send initial data again_


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
## Registered callbacks: 4
0.	`pipliz.server.savesetflight`
		_Save player flight state_
1.	`pipliz.server.saveplayerscience`
		_Save players' science state_
2.	`pipliz.server.saveplayerlimits`
		_Save players' recipe limits_
3.	`pipliz.server.savedifficulty`
		_Saved the active difficulty setting to the players' file_


CallbackType: `OnLoadingPlayer`
=======
## Description
Signature: void (JSONNode a, Players.Player b)
Arg a: The json data that got loaded for the player.
Arg b: said player
## Registered callbacks: 4
0.	`pipliz.server.loadsetflight`
		_Load player flight state_
1.	`pipliz.server.loadplayerscience`
		_Load players' science state_
2.	`pipliz.server.loadplayerlimits`
		_Load players' recipe limits_
3.	`pipliz.server.loaddifficulty`
		_Loads the saved player difficulty using DifficultyManager.DifficultyLoaders_


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
## Registered callbacks: 11
0.	`pipliz.server.savewater`
		_Saves water data_
1.	`pipliz.server.savetimecycle`
		_Saves the time to ServerManager.WorldSettings_
2.	`pipliz.server.savestockpileblocks`
		_Save stockpile blocks_
3.	`pipliz.server.saveplayers`
		_Starts saving all dirty-marked players_
4.	`pipliz.server.savenpc`
		_Start saving npc data_
5.	`pipliz.server.savebedblocks`
		_Save bed blocks_
6.	`pipliz.server.savebanners`
		_Saves banners_
7.	`pipliz.server.saveareajobs`
		_Save all registered areajobs' files_
8.	`pipliz.server.growableblocks.save`
		_Start saving growable blocks_
9.	`pipliz.server.ai.aimanager.quitthread`
		_Marks the AIManager thread to stop_
10.	`pipliz.apiprovider.jobs.save`
		_Saves files for registered block job trackers_


CallbackType: `OnQuitLate`
=======
## Description
Signature: void ()
Called late in the quit method queue (Application.OnQuit 100)
## Registered callbacks: 3
0.	`pipliz.shared.waitforasyncquits`
		_Waits for async items to complete (mostly from autosaving)_
1.	`pipliz.server.saveworldsettings`
		_Saves ServerManager.WorldSettings_
2.	`pipliz.colonyserverwrapper.dispose`
		Depends On 1. `pipliz.server.saveworldsettings`
		Depends On 0. `pipliz.shared.waitforasyncquits`
		_Close external socket_


CallbackType: `OnSavedChunkToRegion`
=======
## Description
Signature: void (FileRegion a, Chunk b)
Arg a: The region the chunk got saved into.
Arg b: said chunk
The chunk is still locked for reading at this point
No registered uses


CallbackType: `OnSavedRegionToDisk`
=======
## Description
Signature: void (FileRegion a)
Arg a: The region that got saved to disk.
This callback may be called from multiple threads at once (at shutdown)
No registered uses


CallbackType: `OnPlayerMoved`
=======
## Description
Signature: void (Players.Player a)
Arg a: The player that moved.
Called approx 6 times per second per player. New position/rotation is set on the Players.Player argument.
## Registered callbacks: 1
0.	`pipliz.server.loadsurroundings`
		_Queues up chunks to load if the player moves to other chunks_


CallbackType: `OnLoadedRegionFromDisk`
=======
## Description
Signature: void (FileRegion a)
Arg a: The region that got loaded from disk.
No registered uses


CallbackType: `OnLoadedChunkFromRegion`
=======
## Description
Signature: void (FileRegion a, Chunk b)
Arg a: The region the chunk got loaded from.
Arg b: said chunk
The chunk is still locked for writing at this point
No registered uses


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
No registered uses


CallbackType: `OnPlayerConnectedLate`
=======
## Description
Signature: void (Players.Player a)
Arg a: The Player that is connecting
Messages send here will work unlike with OnPlayerConnectedEarly. May be delayed till after the client is done loading.
## Registered callbacks: 8
0.	`pipliz.server.terraingenerator.startsendingbiomedata`
		_Starts sending biome data to players (for background looped audio_
1.	`pipliz.server.sendsetflight`
		_Send player flight state_
2.	`pipliz.server.sendnpctypes`
		_Sends the registered NPCType settings to the player_
3.	`pipliz.server.sendaudiomapping`
		_Sends a compressed version of the audiofiles settings_
4.	`pipliz.server.queueinitialsend`
		_Send players' science_
5.	`pipliz.server.meshedobjects.sendtable`
		_Sends the meshed object settings data_
6.	`pipliz.networkmenumanager`
		_Sends some networkui menu's to the player. Triggers some callbacks internally_
7.	`pipliz.mods.basegame.sendconstructiondata`
		_Sends builder/digger limit size data_


CallbackType: `OnAddResearchables`
=======
## Description
Signature: void ()
The place to add researchables to Server.Science.ScienceManager
## Registered callbacks: 1
0.	`pipliz.apiprovider.registerresearchables`
		_Registers the found autoload researchables_


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
Signature: void (IJob job, Recipe recipe, List<InventoryItem> results)
Triggered when an npc doing {job} crafts {recipe}, creating {results}
The results are re-used, don't store it.
Results can be edited. After the callback they'll be added to the npc/block's inventory
If the results are not empty, the npc will show a npc indicator with a weighted random type from the results
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
## Registered callbacks: 1
0.	`pipliz.server.players.hitnpc`
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
## Registered callbacks: 9
0.	`pipliz.server.growableblocks.save`
		_Start saving growable blocks_
1.	`pipliz.server.autosavewater`
		_Saves water data_
2.	`pipliz.server.autosavestockpileblocks`
		_Save stockpile blocks_
3.	`pipliz.server.autosaveplayers`
		_Starts saving all dirty-marked players_
4.	`pipliz.server.autosavenpcs`
		_Start saving npc data_
5.	`pipliz.server.autosavebedblocks`
		_Save bed blocks_
6.	`pipliz.server.autosavebanners`
		_Saves banners_
7.	`pipliz.server.autosaveareajobs`
		_Save all registered areajobs' files_
8.	`pipliz.apiprovider.jobs.autosave`
		_Saves files for registered block job trackers_


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
## Registered callbacks: 2
0.	`pipliz.server.playercheck`
		_Keeps chunks near player alive_
1.	`pipliz.server.bannercheck`
		_Checks to keep chunks near banners loaded_


CallbackType: `AddItemTypes`
=======
## Description
Signature: void (Dictionary<string, ItemTypesServer.ItemTypeRaw> dict)
Basically same as 'AfterAddingBaseTypes'
Mostly used to add...the base types, so that the other callback is correctly named :)
## Registered callbacks: 1
0.	`pipliz.server.applymoditempatches`
		_Waits for itemtype patches to load, then merges/registers them to the dict_


CallbackType: `OnPlayerChangedNetworkUIStorage`
=======
## Description
Signature: void (TupleStruct<Players.Player p, JSONNode node, string id> data)
Arg p: The player that sent changed networkUI storage data
Arg node: Said changed data
Arg id: the networkmenu ID
Possible builtin values for ID: world_settings
## Registered callbacks: 1
0.	`pipliz.parsenetui`
		_Will call the methods to handle the changed data._
		_Possible identifiers:_
		_world_settings_


CallbackType: `OnPlayerPushedNetworkUIButton`
=======
## Description
Signature: void (NetworkUI.ButtonPressCallbackData data)
Data -> the storage node, button ID and player that pushed the button
No registered uses


CallbackType: `OnSendAreaHighlights`
=======
## Description
Signature: void(Players.Player player, List<AreaJobTracker.AreaHighlight> list, List<ushort> showWhileHoldingTypes)
Edit the highlights list, adding desired area highlights to be sent to the player.
Edit the showWhileHoldingTypes to add/remove types that will show <all> areas when selected in the inventory
You can manually trigger this callback through AreaJobTracker.SendData(player)
## Registered callbacks: 2
0.	`pipliz.sendjobareas`
		_Sends the registered AreaJobs_
1.	`pipliz.defaultholdingtypes`
		_Sets default showWhileHoldingTypes_


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


