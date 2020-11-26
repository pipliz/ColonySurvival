CallbackType: `OnConstructInventoryManageColonyUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, System.ValueTuple<NetworkUI.Items.Table, NetworkUI.Items.Table>>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnConstructInventoryManageColonyUI'   


CallbackType: `OnConstructColonyRecruitmentUI`  
=======  
Method type: System.Action<Players.Player, Pipliz.JSON.JSONNode, System.Collections.Generic.List<NetworkUI.IItem>>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnConstructColonyRecruitmentUI'   


CallbackType: `OnConstructColonySettingsUI`  
=======  
Method type: System.Action<Players.Player, Pipliz.JSON.JSONNode, System.Collections.Generic.List<NetworkUI.IItem>>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnConstructColonySettingsUI'   


CallbackType: `OnRegisteringEntityManagers`  
=======  
Method type: System.Action<System.Collections.Generic.List<object>>  
No registered uses  


CallbackType: `AfterModsLoaded`  
=======  
Method type: System.Action<System.Collections.Generic.List<ModLoader.ModDescription>>  
Called after parsing all modinfo files  
Registered callbacks: 3  
0.	'create_filetable' -> 'ServerManager.AfterModsLoadedCreateFiletable'   
1.	'examplemod.detect_flat_world' -> 'ExampleMod.FlatWorld.FlatWorld.DetectFlatWorld'   
2.	'Init TraversabilityBuffer.BurstFunctions' -> 'AI.NavMeshBaker+BurstFunctions+InitializeCallback.AfterModsLoaded'   


CallbackType: `OnItemTypeRegistered`  
=======  
Method type: System.Action<ItemTypes.ItemType>  
Called once for each type that is being registered to ItemTypes  
Registered callbacks: 1  
0.	'pipliz.server.itemtypesserver' -> 'ItemTypesServer.OnRegisteredItemType'   


CallbackType: `OnUpdateStart`  
=======  
Method type: System.Action  
Called early on in unity's Update method  
Registered callbacks: 2  
0.	'servertime.startframe' -> 'ServerTime+ServerCallbacks.OnUpdateStart' index: -1000  
1.	'pipliz.server.setsecondsthisframe' -> 'Pipliz.Time.SetThisFrame'   


CallbackType: `OnUpdate`  
=======  
Method type: System.Action  
In the middle of unity's Update method.  
Registered callbacks: 6  
0.	'dotrading' -> 'ColonyTrading.Update'   
1.	'pipliz.server.chunkupdater' -> 'ChunkUpdating.Update'   
2.	'pipliz.server.tickscounter' -> 'Chatting.Commands.TicksPerSecond.Update'   
3.	'pipliz.server.updatetimecycle' -> 'TimeCycle.Update'   
4.	'update_transports' -> 'Transport.TransportManager.Update'   
5.	'update_water' -> 'BlockEntities.Implementations.Water.Tick'   


CallbackType: `OnLateUpdate`  
=======  
Method type: System.Action  
Called inside unity's LateUpdate method  
Registered callbacks: 2  
0.	'Colony+SendDirtiedColonies.OnLateUpdate' -> 'Colony+SendDirtiedColonies.OnLateUpdate'   
1.	'servertime.endframe' -> 'ServerTime+ServerCallbacks.OnLateUpdate' index: 1000  


CallbackType: `AfterItemTypesDefined`  
=======  
Method type: System.Action  
First callback after all item types should be defined, so you can resolve types etc here  
Registered callbacks: 32  
0.	'initialize_upgrademanager' -> 'Assets.ColonyPointUpgrades.UpgradesManager+CallbacksA.AfterItemTypesDefined' index: -1001  
1.	'create_servermanager_trackers' -> 'ServerManager.CreateBlockEntityCallbacks' index: -1000  
2.	'blockentitycallback.autoloaders' -> 'ServerManager.AutoLoadBlockEntities'   
		 Parent @ 1 : 'create_servermanager_trackers'  
3.	'chunk_dedupe_initializer' -> 'Chunk+ChunkDataDeduplicator.Initialize'   
4.	'pipliz.server.loadnpctypes' -> 'NPC.NPCType.LoadNPCTypes'   
5.	'areajobs.insertattributed' -> 'AreaJobTracker.RegisterAutoDefs'   
		 Parent @ 4 : 'pipliz.server.loadnpctypes'  
6.	'pipliz.server.registerdefaultdifficulty' -> 'Difficulty.DifficultyManager.RegisterDefault'   
7.	'parse_starterpack_patches_afteritemtype' -> 'StarterPacks.Loader.ParsePacks'   
		 Child @ 8 : 'pipliz.server.recipeplayerload'  
8.	'pipliz.server.recipeplayerload' -> 'Recipes.RecipeStorage.ReadRecipesPlayer'   
		 Parent @ 1 : 'create_servermanager_trackers'  
9.	'pipliz.server.recipenpcload' -> 'Recipes.RecipeStorage.ReadRecipesNPC'   
		 Parent @ 1 : 'create_servermanager_trackers'  
10.	'pipliz.server.loadresearchables' -> 'ServerManager.LoadResearchables'   
		 Parent @ 4 : 'pipliz.server.loadnpctypes'  
		 Parent @ 1 : 'create_servermanager_trackers'  
		 Parent @ 9 : 'pipliz.server.recipenpcload'  
		 Parent @ 8 : 'pipliz.server.recipeplayerload'  
11.	'pipliz.server.endloadplayers' -> 'Players.EndInitializePlayerData'   
12.	'createareajobdefinitions' -> 'AreaJobTracker+AreaJobPatches.CreateAreaJobDefinitions'   
		 Parent @ 4 : 'pipliz.server.loadnpctypes'  
13.	'pipliz.blocknpcs.registerjobs' -> 'Jobs.BlockJobLoader.AfterDefiningNPCTypes'   
		 Parent @ 1 : 'create_servermanager_trackers'  
		 Parent @ 4 : 'pipliz.server.loadnpctypes'  
14.	'creategrowabledefinitions' -> 'GrowableBlocks.GrowablePatchHandler.CreateGrowableDefinitions'   
		 Parent @ 1 : 'create_servermanager_trackers'  
15.	'register_upgrades' -> 'Assets.ColonyPointUpgrades.UpgradesManager+CallbacksB.AfterItemTypesDefined' index: 10  
		 Parent @ 0 : 'initialize_upgrademanager'  
		 Parent @ 10 : 'pipliz.server.loadresearchables'  
		 Child @ 17 : 'pipliz.server.endloadcolonies'  
16.	'pipliz.endloaddifficulty' -> 'Difficulty.ColonyDifficultySetting.CompleteLoading' index: 1000  
17.	'pipliz.server.endloadcolonies' -> 'ServerManager.LoadColonies'   
		 Parent @ 4 : 'pipliz.server.loadnpctypes'  
		 Parent @ 5 : 'areajobs.insertattributed'  
		 Parent @ 1 : 'create_servermanager_trackers'  
		 Parent @ 6 : 'pipliz.server.registerdefaultdifficulty'  
		 Parent @ 16 : 'pipliz.endloaddifficulty'  
		 Parent @ 8 : 'pipliz.server.recipeplayerload'  
		 Parent @ 9 : 'pipliz.server.recipenpcload'  
		 Parent @ 10 : 'pipliz.server.loadresearchables'  
		 Parent @ 11 : 'pipliz.server.endloadplayers'  
		 Parent @ 12 : 'createareajobdefinitions'  
18.	'pipliz.server.completeloadmiscworld' -> 'ServerManager.CompleteLoadMiscWorld'   
		 Parent @ 17 : 'pipliz.server.endloadcolonies'  
		 Child @ 19 : 'create_savemanager'  
19.	'create_savemanager' -> 'ServerManager.CreateBlockEntityTracker' index: -1000  
		 Parent @ 2 : 'blockentitycallback.autoloaders'  
		 Parent @ 3 : 'chunk_dedupe_initializer'  
		 Parent @ 17 : 'pipliz.server.endloadcolonies'  
		 Parent @ 13 : 'pipliz.blocknpcs.registerjobs'  
		 Parent @ 14 : 'creategrowabledefinitions'  
20.	'pipliz.server.registermonstertextures' -> 'NPC.NPCType.RegisterMonsterTextures' index: -100  
		 Parent @ 4 : 'pipliz.server.loadnpctypes'  
21.	'find_auto_chatcommands' -> 'Chatting.CommandManager.Initialize'   
22.	'insert_banner_radius_legacy_upgrade_check' -> 'Assets.ColonyPointUpgrades.Implementations.SafeZoneUpgrade+WorldUpgradeCheck.AfterItemTypesDefined'   
		 Parent @ 1 : 'create_servermanager_trackers'  
23.	'insert_builder_size_legacy_upgrade_check' -> 'Assets.ColonyPointUpgrades.Implementations.BuilderSizeUpgrade+WorldUpgradeCheck.AfterItemTypesDefined'   
		 Parent @ 1 : 'create_servermanager_trackers'  
24.	'insert_colonist_capacity_legacy_upgrade_check' -> 'Assets.ColonyPointUpgrades.Implementations.ColonistCapacityUpgrade+WorldUpgradeCheck.AfterItemTypesDefined'   
		 Parent @ 1 : 'create_servermanager_trackers'  
25.	'insert_digger_size_legacy_upgrade_check' -> 'Assets.ColonyPointUpgrades.Implementations.DiggerSizeUpgrade+WorldUpgradeCheck.AfterItemTypesDefined'   
		 Parent @ 1 : 'create_servermanager_trackers'  
26.	'pipliz.server.asyncloadpermissions' -> 'PermissionsManager.Reload'   
27.	'pipliz.server.endblackandwhitelisting' -> 'BlackAndWhitelisting.EndReload'   
28.	'pipliz.server.endloadwater' -> 'BlockEntities.Implementations.Water.Load'   
29.	'trading.doublelinkrules' -> 'ColonyTrading.LoadColonies'   
		 Parent @ 17 : 'pipliz.server.endloadcolonies'  
30.	'wait_complete_startup_chunks' -> 'ServerManager.WaitForCompletedStartupChunks' index: 1000  
		 Parent @ 19 : 'create_savemanager'  
31.	'set_colony_sciencemask' -> 'Science.ScienceManager.SetScienceMask' index: 1  
		 Parent @ 30 : 'wait_complete_startup_chunks'  
		 Parent @ 17 : 'pipliz.server.endloadcolonies'  
		 Parent @ 1 : 'create_servermanager_trackers'  


CallbackType: `OnQuit`  
=======  
Method type: System.Action  
Called in the quit method queue  
Registered callbacks: 8  
0.	'pipliz.shared.waitforasyncquitsearly' -> 'Pipliz.Application.WaitForQuits' index: -1000  
1.	'pipliz.server.savecolonies' -> 'ServerManager.SaveColonies'   
2.	'pipliz.server.savemiscworld' -> 'ServerManager.SaveMiscWorld'   
3.	'pipliz.server.saveplayers' -> 'Players.SavePlayers'   
4.	'pipliz.server.savewater' -> 'BlockEntities.Implementations.Water.Save'   
5.	'pipliz.server.saveworldsettings' -> 'ServerManager.SaveWorldSettings'   
6.	'pipliz.jointhreads' -> 'Pipliz.Threading.ThreadSafeQuitWrapper.JoinThread' index: 500  
7.	'pipliz.shared.waitforasyncquitslate' -> 'Pipliz.Application.WaitForQuits' index: 1000  


CallbackType: `AfterSelectedWorld`  
=======  
Method type: System.Action  
First callback after the world to load has been determined  
Registered callbacks: 14  
0.	'pipliz.server.startloadcolonies' -> 'ServerManager.StartLoadColonies' index: -1000  
1.	'pipliz.server.startloadplayers' -> 'Players.StartInitializePlayerData' index: -1000  
2.	'pipliz.server.loadaudiofiles' -> 'AudioManager.LoadAudioFiles' index: -100  
3.	'pipliz.server.applytexturemappingpatches' -> 'ItemTypesServer.ApplyTextureMappingPatches'   
4.	'pipliz.server.registertexturemappingtextures' -> 'ItemTypesServer.RegisterTextures' index: -100  
		 Parent @ 3 : 'pipliz.server.applytexturemappingpatches'  
5.	'pipliz.server.startloadmiscworld' -> 'ServerManager.StartLoadMiscWorld' index: -100  
6.	'pipliz.server.startloadwater' -> 'BlockEntities.Implementations.Water.LoadStart' index: -100  
7.	'pipliz.startloaddifficulty' -> 'Difficulty.ColonyDifficultySetting.StartLoading' index: -100  
8.	'register_glider_settings' -> 'Transport.Glider.Initialize' index: -100  
9.	'registernpctextures' -> 'ItemTypesServer.RegisterNPCTextures' index: -100  
10.	'registerwatertextures' -> 'ItemTypesServer.RegisterWaterTextures' index: -100  
11.	'pipliz.imagemanager.loadingimages' -> 'Pipliz.ImageManager.AfterWorldLoad'   
12.	'pipliz.server.loadtimecycle' -> 'TimeCycle.Initialize'   
13.	'pipliz.server.startblackandwhitelisting' -> 'BlackAndWhitelisting.StartReload'   


CallbackType: `AfterAddingBaseTypes`  
=======  
Method type: System.Action<System.Collections.Generic.Dictionary<string, ItemTypesServer.ItemTypeRaw>>  
Callback after AddItemTypes but before parsing those types; intended usecase is to allow changing the raw json  
No registered uses  


CallbackType: `AfterWorldLoad`  
=======  
Method type: System.Action  
Most things should be initialized by now, does not have to include chunks and pathing data though (those may be ongoing on other threads)  
Registered callbacks: 6  
0.	'start_generator' -> 'TerrainGeneration.TerrainModManager.StartGenerator' index: -1  
1.	'check_colonies_version_upgradable' -> 'ColonyTracker+Callbacks.AfterWorldLoad'   
2.	'pipliz.server.localization.convert' -> 'Localization.Convert'   
3.	'pipliz.server.monsterspawner.fetchnpctypes' -> 'Monsters.MonsterSpawner.Fetch'   
4.	'pipliz.server.monsterspawner.register' -> 'Monsters.MonsterSpawner.Register'   
5.	'save_recipemapping' -> 'Recipes.RecipeStorage.SaveMapping'   


CallbackType: `AfterNetworkSetup`  
=======  
Method type: System.Action  
After the networkwrapper clas is ready to send data. Can take a few seconds for steam servers to connect.  
No registered uses  


CallbackType: `OnUpdateEnd`  
=======  
Method type: System.Action  
At the end of unity's Update method.  
Registered callbacks: 1  
0.	'pipliz.server.senddirtystockpiles' -> 'StockpileManager.SendDirtyStockpiles'   


CallbackType: `OnFixedUpdate`  
=======  
Method type: System.Action  
Unity's OnFixedUpdate method. See unity documentation.  
Registered callbacks: 1  
0.	'collisionchecker' -> 'Transport.CollisionChecker.FixedUpdate'   


CallbackType: `OnApplicationFocus`  
=======  
Method type: System.Action<bool>  
Unity's OnApplicationFocus method. See unity documentation.  
No registered uses  


CallbackType: `OnApplicationPause`  
=======  
Method type: System.Action<bool>  
Unity's OnApplicationPause method. See unity documentation.  
No registered uses  


CallbackType: `OnPlayerConnectedEarly`  
=======  
Method type: System.Action<Players.Player>  
Early on in the player connection process - the player is probably not ready to receive messages yet  
Registered callbacks: 3  
0.	'pipliz.imagemanager.sendimagesettings' -> 'Pipliz.ImageManager.OnPlayerConnectedEarly'   
1.	'pipliz.server.sendinitialtime' -> 'TimeCycle.SendHeartBeat'   
2.	'pipliz.server.start_loadsurroundings' -> 'ChunkQueue.ResetSurroundings'   


CallbackType: `OnPlayerDisconnected`  
=======  
Method type: System.Action<Players.Player>  
No registered uses  


CallbackType: `OnSavingPlayer`  
=======  
Method type: System.Action<Pipliz.JSON.JSONNode, Players.Player>  
Allows saving custom data into the player save file  
Registered callbacks: 1  
0.	'pipliz.server.savesetflight' -> 'Chatting.Commands.SetFlight.SavePlayer'   


CallbackType: `OnLoadingPlayer`  
=======  
Method type: System.Action<Pipliz.JSON.JSONNode, Players.Player>  
Allows loading custom data saved with OnSavingPlayer  
Registered callbacks: 1  
0.	'pipliz.server.loadsetflight' -> 'Chatting.Commands.SetFlight.LoadPlayer'   


CallbackType: `OnSavedChunk`  
=======  
Method type: System.Action<Chunk>  
No registered uses  


CallbackType: `OnLoadedChunk`  
=======  
Method type: System.Action<Chunk>  
No registered uses  


CallbackType: `OnPlayerMoved`  
=======  
Method type: System.Action<Players.Player, UnityEngine.Vector3>  
Probably called about 6/second/player. New data is on the player already.  
Registered callbacks: 3  
0.	'pipliz.server.loadsurroundings' -> 'ChunkQueue.LoadSurroundings'   
1.	'send_audiobiome' -> 'AudioManager.OnMoveSendBiome'   
2.	'send_sciencebiome' -> 'TerrainGeneration.TerrainModManager.OnMoveSendBiome'   


CallbackType: `OnModifyResearchables`  
=======  
Method type: System.Action<System.Collections.Generic.Dictionary<string, Science.DefaultResearchable>>  
Called inside of OnAddResearchables - allows modifying researches added through jsonFiles before they're registered  
No registered uses  


CallbackType: `OnChangedBlock`  
=======  
Method type: System.Action<ModLoader.OnTryChangeBlockData>  
Called after OnTryChangeBlock, if the block was actually succesfully changed.  
Registered callbacks: 1  
0.	'update_collisions' -> 'Transport.CollisionChecker.UpdateCollisions'   


CallbackType: `OnTryChangeBlock`  
=======  
Method type: System.Action<ModLoader.OnTryChangeBlockData>  
Callback triggered upon a call to ServerManager.TryChangeBlock - used by various code and the client to edit blocks  
Nothing changed yet when this callback happens and the change can be blocked.  
You can block is by setting CallbackState to Cancelled  
Registered callbacks: 1  
0.	'preventaccidentalbannerremoval' -> 'ServerManager.TempBannerFix'   


CallbackType: `OnPlayerConnectedLate`  
=======  
Method type: System.Action<Players.Player>  
Messages send here will work unlike with OnPlayerConnectedEarly. May be delayed till after the client is done loading.  
Registered callbacks: 5  
0.	'pipliz.server.meshedobjects.sendtable' -> 'MeshedObjects.MeshedObjectType.OnPlayerConnectedLate'   
1.	'pipliz.server.sendnpctypes' -> 'NPC.NPCType.SendNPCTypes'   
2.	'pipliz.server.sendsetflight' -> 'Chatting.Commands.SetFlight.OnPlayerSendFlight'   
3.	'send_attached_mesh' -> 'MeshedObjects.MeshedObjectManager.SendAttachedMesh'   
		 Parent @ 0 : 'pipliz.server.meshedobjects.sendtable'  
4.	'send_audiobiome' -> 'AudioManager.OnConnectSendBiome'   


CallbackType: `OnAddResearchables`  
=======  
Method type: System.Action  
The place to add researchables to Server.Science.ScienceManager  
Registered callbacks: 1  
0.	'registerresearchables' -> 'Science.ScienceManager.RegisterAutoResearchables'   


CallbackType: `OnConstructTooltipUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.ConstructTooltipUIData>  
Registered callbacks: 2  
0.	'Jobs.CommandToolManager.OnConstructTooltipUI' -> 'Jobs.CommandToolManager.OnConstructTooltipUI'   
1.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager.ConstructTooltip'   


CallbackType: `OnPlayerRecipeSettingChanged`  
=======  
Method type: System.Action<Recipes.RecipeColony, Recipes.Recipe, Pipliz.Box<Recipes.RecipeSetting>>  
Triggered every time a recipe limit is changed.  
Don't store the Box<>, it's re-used.  
This callback is also called upon loading the settings from json - but only for non-default values (defaults aren't stored)  
No registered uses  


CallbackType: `OnNPCCraftedRecipe`  
=======  
Method type: System.Action<Jobs.IJob, Recipes.Recipe, System.Collections.Generic.List<Recipes.RecipeResult>>  
The results list is re-used, don't store it.  
Results can be edited. After the callback they'll be added to the npc/block's inventory  
If the results are not empty, the npc will show a npc indicator with a weighted random type from the non-optional results  
No registered uses  


CallbackType: `OnPlayerDeath`  
=======  
Method type: System.Action<Players.Player>  
Registered callbacks: 1  
0.	'pipliz.server.onplayerdeath' -> 'Players.OnDeath'   


CallbackType: `OnPlayerRespawn`  
=======  
Method type: System.Action<Players.Player>  
Registered callbacks: 1  
0.	'pipliz.server.onplayerrespawn' -> 'Players.OnDeathReset'   


CallbackType: `OnMonsterSpawned`  
=======  
Method type: System.Action<Monsters.IMonster>  
No registered uses  


CallbackType: `OnMonsterHit`  
=======  
Method type: System.Action<Monsters.IMonster, ModLoader.OnHitData>  
No registered uses  


CallbackType: `OnMonsterDied`  
=======  
Method type: System.Action<Monsters.IMonster>  
No registered uses  


CallbackType: `OnNPCRecruited`  
=======  
Method type: System.Action<NPC.NPCBase>  
No registered uses  


CallbackType: `OnNPCLoaded`  
=======  
Method type: System.Action<NPC.NPCBase, Pipliz.JSON.JSONNode>  
No registered uses  


CallbackType: `OnNPCDied`  
=======  
Method type: System.Action<NPC.NPCBase>  
Registered callbacks: 1  
0.	'pipliz.server.refundrecruitment' -> 'Jobs.JobFinder.OnNPCDiedRefund'   


CallbackType: `OnNPCSaved`  
=======  
Method type: System.Action<NPC.NPCBase, Pipliz.JSON.JSONNode>  
No registered uses  


CallbackType: `OnNPCJobChanged`  
=======  
Method type: System.Action<System.ValueTuple<NPC.NPCBase, Jobs.IJob, Jobs.IJob>>  
Registered callbacks: 1  
0.	'pipliz.server.refundrecruitement' -> 'Jobs.JobFinder.OnJobChanged'   


CallbackType: `OnNPCHit`  
=======  
Method type: System.Action<NPC.NPCBase, ModLoader.OnHitData>  
No registered uses  


CallbackType: `OnPlayerClicked`  
=======  
Method type: System.Action<Players.Player, Shared.PlayerClickedData>  
Registered callbacks: 5  
0.	'check_banner_click' -> 'NetworkUI.NetworkMenuManager.CheckBannerClick'   
1.	'clicked_glider' -> 'Transport.Glider.OnClicked'   
		 Child @ 2 : 'clicked_transport'  
2.	'clicked_transport' -> 'Transport.TransportManager.OnClicked'   
3.	'Jobs.CommandToolManager.OnPlayerClicked' -> 'Jobs.CommandToolManager.OnPlayerClicked'   
4.	'pipliz.server.players.hitnpc' -> 'Players.OnPlayerClicked'   


CallbackType: `OnPlayerHit`  
=======  
Method type: System.Action<Players.Player, ModLoader.OnHitData>  
No registered uses  


CallbackType: `OnAutoSaveWorld`  
=======  
Method type: System.Action  
Triggers an autosave every x minutes, to begin autosaving non-block data (jobs, npc's, players)  
Registered callbacks: 5  
0.	'pipliz.server.autosaveplayers' -> 'Players.SavePlayers'   
1.	'pipliz.server.autosavewater' -> 'BlockEntities.Implementations.Water.Save'   
2.	'pipliz.server.savecolonies' -> 'ServerManager.SaveColonies'   
3.	'pipliz.server.savemiscworld' -> 'ServerManager.SaveMiscWorld'   
4.	'pipliz.server.saveworldsettings' -> 'ServerManager.SaveWorldSettings'   


CallbackType: `OnNPCGathered`  
=======  
Method type: System.Action<Jobs.IJob, Pipliz.Vector3Int, System.Collections.Generic.List<ItemTypes.ItemTypeDrops>>  
Can edit the results; don't store them - the list is re-used.  
After the callback, results will be added to the npc's inventory.  
The location does not have to be the job/npc's position - see the construction jobs.  
No registered uses  


CallbackType: `OnShouldKeepChunkLoaded`  
=======  
Method type: System.Action<ChunkUpdating.KeepChunkLoadedData>  
!!! Will be called from multiple threads, simultaneously !!!  
Periodically triggered for every chunk loaded. Use it to keep chunks loaded - and to indicate how long you expect them to stay loaded.  
{data.CheckedChunk} -> the chunk  
{data.MillisecondsTillNextCheck} -> the minimum time until another callback will be fired. Defaults to random between 24000 and 64000  
{data.Result} -> bool indicating whether or not to keep this chunk. Defaults to false (set to true to keep it)  
{data.ChunkLoadedSource} -> source for this callback. If loadedstorage / loadedgenerator, the chunk is already locked for writing. if Updater, it is not locked.  
Registered callbacks: 3  
0.	'bannercheck' -> 'BlockEntities.Implementations.BannerTracker.CheckKeepChunkLoaded'   
1.	'pipliz.server.playercheck' -> 'Players.ShouldKeepChunkAlive'   
		 Parent @ 0 : 'bannercheck'  
2.	'check_blockentities' -> 'ServerManager.KeepBlockEntitiesLoaded'   
		 Parent @ 1 : 'pipliz.server.playercheck'  


CallbackType: `AddItemTypes`  
=======  
Method type: System.Action<System.Collections.Generic.Dictionary<string, ItemTypesServer.ItemTypeRaw>>  
Registered callbacks: 3  
0.	'blockgenerator.generateblocks' -> 'ItemTypesServer+BlockRotator.AddLitTypes'   
1.	'parse_starterpack_patches_additemtypes' -> 'StarterPacks.Loader.CreateTypes'   
2.	'pipliz.server.applymoditempatches' -> 'ItemTypesServer.ApplyPatches'   
		 Parent @ 1 : 'parse_starterpack_patches_additemtypes'  
		 Parent @ 0 : 'blockgenerator.generateblocks'  


CallbackType: `OnPlayerChangedNetworkUIStorage`  
=======  
Method type: System.Action<System.ValueTuple<Players.Player, Pipliz.JSON.JSONNode, string>>  
Called when a player closes a networkmenu while some of its state was changed  
Registered callbacks: 1  
0.	'pipliz.parsenetui' -> 'NetworkUI.NetworkMenuManager.ReceiveWorldSettings'   


CallbackType: `OnPlayerPushedNetworkUIButton`  
=======  
Method type: System.Action<NetworkUI.ButtonPressCallbackData>  
Registered callbacks: 2  
0.	'handle_colony_management' -> 'NetworkUI.NetworkMenuManager.HandleButtons'   
1.	'Jobs.CommandToolManager.OnPlayerPushedNetworkUIButton' -> 'Jobs.CommandToolManager.OnPlayerPushedNetworkUIButton'   


CallbackType: `OnSendAreaHighlights`  
=======  
Method type: System.Action<Players.Player, System.Collections.Generic.List<AreaJobTracker.AreaHighlight>, System.Collections.Generic.List<ushort>>  
Edit the highlights list, adding desired area highlights to be sent to the player.  
Edit the showWhileHoldingTypes to add/remove types that will show <all> areas when selected in the inventory  
You can manually trigger this callback through AreaJobTracker.SendData(player)  
Registered callbacks: 2  
0.	'pipliz.defaultholdingtypes' -> 'AreaJobTracker.GatherShowWhileHoldingTypes'   
1.	'pipliz.sendjobareas' -> 'AreaJobTracker.GatherJobHighlights'   


CallbackType: `OnRemoveAreaHighlight`  
=======  
Method type: System.Action<Players.Player, Pipliz.Vector3Int, Pipliz.Box<bool>>  
Request to remove an area that has said minimum corner  
Check isHandled's content to see if the callback was handled by something else  
Set said isHandled' content to prevent other callbacks from handling it.  
Registered callbacks: 1  
0.	'pipliz.removejobarea' -> 'AreaJobTracker.RemoveArea'   


CallbackType: `OnActiveColonyChanges`  
=======  
Method type: System.Action<Players.Player, Colony, Colony>  
Registered callbacks: 5  
0.	'onchange' -> 'ServerManager.OnColonyChange' index: -1000  
1.	'resend_areajobs' -> 'AreaJobTracker.OnActiveColonyChange'   
2.	'send_upgradesconfig' -> 'Assets.ColonyPointUpgrades.UpgradesManager+CallbacksA.OnActiveColonyChanges'   
3.	'sendconstructiondata' -> 'Jobs.Implementations.Construction.ConstructionManager.SendData'   
4.	'sendresearch' -> 'Science.ScienceManager.SendColonyResearch'   


CallbackType: `OnSavingColony`  
=======  
Method type: System.Action<Colony, Pipliz.JSON.JSONNode>  
Registered callbacks: 10  
0.	'save_upgradestate' -> 'Assets.ColonyPointUpgrades.ColonyUpgradeState+Callbacks.OnSavingColony'   
1.	'saveareajobs' -> 'AreaJobTracker.Save'   
2.	'savedifficulty' -> 'ColonyTracker.SaveDifficulty'   
3.	'savejobfinder' -> 'Jobs.JobFinder.Save'   
4.	'savenpcs' -> 'ColonyTracker.SaveNPCs'   
5.	'saveowners' -> 'ColonyTracker.SaveOwners'   
6.	'saverecipesettings' -> 'ColonyTracker.SaveRecipeSettings'   
7.	'savescience' -> 'ColonyTracker.SaveScience'   
8.	'savestockpile' -> 'ColonyTracker.SaveStockpile'   
9.	'savetrading' -> 'ColonyTrading.SaveTrading'   


CallbackType: `OnLoadingColony`  
=======  
Method type: System.Action<Colony, Pipliz.JSON.JSONNode>  
Registered callbacks: 10  
0.	'load_upgradestate' -> 'Assets.ColonyPointUpgrades.ColonyUpgradeState+Callbacks.OnLoadingColony'   
1.	'loadnpcs' -> 'ColonyTracker.LoadNPCs'   
2.	'loadareajobs' -> 'AreaJobTracker.Load'   
		 Parent @ 1 : 'loadnpcs'  
3.	'loaddifficulty' -> 'ColonyTracker.LoadDifficulty'   
4.	'loadjobfinder' -> 'Jobs.JobFinder.Load'   
5.	'loadowners' -> 'ColonyTracker.LoadOwners'   
6.	'loadrecipesettings' -> 'ColonyTracker.LoadRecipeSettings'   
7.	'loadstockpile' -> 'ColonyTracker.LoadStockpile'   
8.	'loadscience' -> 'ColonyTracker.LoadScience'   
		 Parent @ 1 : 'loadnpcs'  
		 Parent @ 7 : 'loadstockpile'  
9.	'loadtrading' -> 'ColonyTrading.LoadTrading'   


CallbackType: `OnLoadingTerrainGenerator`  
=======  
Method type: System.Action<TerrainGeneration.TerrainGeneratorBase>  
Called when the default terrain generator is created - during create_servermanager_trackers in AfterItemTypesDefined  
Registered callbacks: 6  
0.	'terraingen.setup' -> 'TerrainGeneration.TerrainGenerator.StaticInit' index: -500  
1.	'apply_metabiome_patches' -> 'TerrainGeneration.TerrainModManager.ApplyMetaBiomePatches'   
2.	'apply_sciencebiome_patches' -> 'TerrainGeneration.TerrainModManager.ApplyScienceBiomes'   
3.	'apply_orelayers' -> 'TerrainGeneration.TerrainModManager.ApplyOreLayers'   
		 Parent @ 2 : 'apply_sciencebiome_patches'  
4.	'load_structure_patches' -> 'TerrainGeneration.TerrainModManager.OnLoadStructures'   
5.	'load_biome_patches' -> 'TerrainGeneration.TerrainModManager.OnLoadBiomes'   
		 Parent @ 4 : 'load_structure_patches'  


CallbackType: `OnPlayerEditedNetworkInputfield`  
=======  
Method type: System.Action<NetworkUI.InputfieldEditCallbackData>  
Registered callbacks: 1  
0.	'handle_builtin' -> 'NetworkUI.NetworkMenuManager.HandleInputfields'   


CallbackType: `OnCreatedColony`  
=======  
Method type: System.Action<Colony>  
Called when a colony is created, does not trigger on loading colonies (!)  
Registered callbacks: 1  
0.	'disable_some_science' -> 'Science.ScienceManager.DisableSciences'   


CallbackType: `OnConstructManageColoniesUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, Colony>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager.ConstructManageColoniesUI'   


CallbackType: `OnConstructDiplomacyUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, Colony>  
No registered uses  


CallbackType: `OnConstructCommandTool`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, string>  
No registered uses  


CallbackType: `OnConstructManageColoniesSelectionUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager.ConstructManageColoniesSelectionUI'   


CallbackType: `OnConstructColonyOwnerManagementUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager.ConstructColonyManagement'   


CallbackType: `OnConstructBannerPlacementUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager.ConstructBannerPlacement'   


CallbackType: `OnConstructBannerClickedUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, BlockEntities.Implementations.BannerTracker.Banner>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager.ConstructBannerClicked'   


CallbackType: `OnHandleColonySelected`  
=======  
Method type: System.Action<NetworkUI.ButtonPressCallbackData, int>  
Result of the colony selection menu  
Registered callbacks: 1  
0.	'handle_builtin' -> 'NetworkUI.NetworkMenuManager.HandleSelectedColonyBuiltin'   


CallbackType: `OnPlayerSelectedTypePopup`  
=======  
Method type: System.Action<Players.Player, ushort, Pipliz.JSON.JSONNode>  
Result of the item selection menu  
Registered callbacks: 1  
0.	'traderule' -> 'NetworkUI.NetworkMenuManager.HandleSelectedTypePopup'   


CallbackType: `OnSaveWorldMisc`  
=======  
Method type: System.Action<Newtonsoft.Json.Linq.JObject>  
called on autosave/quit, node is the json that'll be saved to {world}/world.json  
Registered callbacks: 2  
0.	'save_transports' -> 'Transport.TransportManager.SaveTransports'   
1.	'savenpcid' -> 'NPC.NPCTracker.OnSaveMisc'   


CallbackType: `OnLoadWorldMisc`  
=======  
Method type: System.Action<Newtonsoft.Json.Linq.JObject>  
called in AfterItemTypesDefined, node is the json that was saved aerlier ({world}/world.json)  
Registered callbacks: 2  
0.	'load_gliders' -> 'Transport.Glider.LoadGliders'   
1.	'loadnpcid' -> 'NPC.NPCTracker.OnLoadMisc'   


CallbackType: `OnConstructColonyStartSettingsUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager.ConstructColonyStartSettings'   


CallbackType: `OnSendingStatisticsData`  
=======  
Method type: System.Action<ColonyStats.SharedStatisticsGatherer>  
Called every time the client requests statistics data;  
include your type definitions every time, so the player can select them  
if the requested key matches your given key, include your desired data that corresponds with the requested time period  
Registered callbacks: 5  
0.	'pipliz.stockpile' -> 'ColonyStats+SharedStatisticsGatherer.InsertStockpileStats' index: -6  
1.	'pipliz.npcidle' -> 'ColonyStats+SharedStatisticsGatherer.InsertIdleStats' index: -3  
2.	'pipliz.colony' -> 'ColonyStats+SharedStatisticsGatherer.InsertColonyStats' index: -2,5  
3.	'pipliz.trade-out' -> 'ColonyStats+SharedStatisticsGatherer.InsertTradeOutStats' index: -2  
4.	'pipliz.trade-in' -> 'ColonyStats+SharedStatisticsGatherer.InsertTradeInStats' index: -1  


CallbackType: `OnGatherStatisticsData`  
=======  
Method type: System.Action<Colony, int, int>  
Called with the colony and stats timeperiod index every time those stats were gathered  
First integer is the period index; 0, 1 or 2 by default  
Second integer is the time between updates for this periods (10, 40 or 240 seconds by default)  
No registered uses  


CallbackType: `OnLoadModJSONFiles`  
=======  
Method type: System.Action<System.Collections.Generic.List<ModLoader.LoadModJSONFileContext>>  
Registered callbacks: 1  
0.	'pipliz.jsonfilescallbacks' -> 'ModLoader+LoadModJSONFileCallback.OnLoadModJSONFiles'   


CallbackType: `OnPlayerRemovedFromColony`  
=======  
Method type: System.Action<Players.Player, Colony>  
Registered callbacks: 1  
0.	'ColonyTracker+Callbacks.OnPlayerRemovedFromColony' -> 'ColonyTracker+Callbacks.OnPlayerRemovedFromColony'   


CallbackType: `OnPlayerAddedToColony`  
=======  
Method type: System.Action<Players.Player, Colony>  
No registered uses  


CallbackType: `OnLoadingImages`  
=======  
Method type: System.Action<System.Collections.Generic.Dictionary<string, string>>  
No registered uses  


CallbackType: `OnRegisterUpgrades`  
=======  
Method type: System.Action<Assets.ColonyPointUpgrades.UpgradesManager>  
Registered callbacks: 1  
0.	'register_upgradeautoload' -> 'Assets.ColonyPointUpgrades.UpgradesManager+CallbacksA.OnRegisterUpgrades'   


CallbackType: `OnGetJobGroupByPosition`  
=======  
Method type: System.Action<Recipes.RecipeColony.Callbacks.GroupLimitsCallbackContext>  
Registered callbacks: 1  
0.	'pipliz.base' -> 'Recipes.RecipeColony+Callbacks.ModLoaderInterfaces.IOnGetJobGroupByPosition.OnGetJobGroupByPosition'   


CallbackType: `OnSetJobGroupByPosition`  
=======  
Method type: System.Action<Recipes.RecipeColony.Callbacks.GroupLimitsCallbackContext>  
Registered callbacks: 1  
0.	'pipliz.base' -> 'Recipes.RecipeColony+Callbacks.ModLoaderInterfaces.IOnSetJobGroupByPosition.OnSetJobGroupByPosition'   


