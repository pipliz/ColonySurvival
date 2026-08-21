CallbackType: `OnEndReadOnly`  
=======  
Method type: System.Action  
Registered callbacks: 3  
0.	'AI.PathingManager+ExecuteManager+Callbacks1.OnEndReadOnly' -> 'AI.PathingManager+ExecuteManager+Callbacks1.OnEndReadOnly'   
1.	'Saving.SaveManager+Callbacks.OnEndReadOnly' -> 'Saving.SaveManager+Callbacks.OnEndReadOnly'   
2.	'TerrainGeneration2.TerrainGenerator2ModManager.OnEndReadOnly' -> 'TerrainGeneration2.TerrainGenerator2ModManager.OnEndReadOnly'   


CallbackType: `OnStartReadOnly`  
=======  
Method type: System.Action  
Registered callbacks: 3  
0.	'AI.PathingManager+ExecuteManager+Callbacks1.OnStartReadOnly' -> 'AI.PathingManager+ExecuteManager+Callbacks1.OnStartReadOnly'   
1.	'Saving.SaveManager+Callbacks.OnStartReadOnly' -> 'Saving.SaveManager+Callbacks.OnStartReadOnly'   
2.	'TerrainGeneration2.TerrainGenerator2ModManager.OnStartReadOnly' -> 'TerrainGeneration2.TerrainGenerator2ModManager.OnStartReadOnly'   


CallbackType: `OnStartMainthread`  
=======  
Method type: System.Action  
No registered uses  


CallbackType: `OnEndMainthread`  
=======  
Method type: System.Action  
Registered callbacks: 1  
0.	'TerrainGeneration2.TerrainGenerator2ModManager.OnEndMainthread' -> 'TerrainGeneration2.TerrainGenerator2ModManager.OnEndMainthread'   


CallbackType: `AfterModsLoaded`  
=======  
Method type: System.Action<System.Collections.Generic.List<ModLoader.ModDescription>>  
Called after parsing all modinfo files  
Registered callbacks: 3  
0.	'create_filetable' -> 'ServerManager.AfterModsLoadedCreateFiletable'   
1.	'start_loading_lua' -> 'LuaScripting.LuaManager+Callbacks.AfterModsLoaded'   
		 Parent @ 0 : 'create_filetable'  
2.	'start_loading_modinfo_custom_files' -> 'ModLoader+ModInfoFileCallbacks.AfterModsLoaded'   
		 Parent @ 0 : 'create_filetable'  


CallbackType: `OnItemTypeRegistered`  
=======  
Method type: System.Action<ItemTypes.ItemType>  
Called once for each type that is being registered to ItemTypes  
Registered callbacks: 3  
0.	'BlockEntities.Implementations.Astrolabe+Callbacks.OnItemTypeRegistered' -> 'BlockEntities.Implementations.Astrolabe+Callbacks.OnItemTypeRegistered'   
1.	'pipliz.server.itemtypesserver' -> 'ItemTypesServer.OnRegisteredItemType'   
2.	'register_tools' -> 'ItemTypesServer+Callbacks.OnItemTypeRegistered'   


CallbackType: `OnUpdateStart`  
=======  
Method type: System.Action  
Called early on in unity's Update method  
Registered callbacks: 5  
0.	'startmainthreadonly' -> 'ThreadPhase+Callbacks.OnUpdateStart' index: -1000000  
1.	'pipliz.server.setsecondsthisframe' -> 'Pipliz.Time.SetThisFrame' index: -1000  
2.	'servertime.startframe' -> 'ServerTime+ServerCallbacks.OnUpdateStart' index: -1000  
3.	'mainthreadactions' -> 'ThreadManager+Callbacks.OnUpdateStart' index: -100  
4.	'monstertracker.applythreadresults' -> 'Monsters.MonsterTracker+Callbacks.OnUpdateStart'   


CallbackType: `OnUpdate`  
=======  
Method type: System.Action  
In the middle of unity's Update method.  
Registered callbacks: 22  
0.	'steammanager' -> 'SteamManager+Callbacks.OnUpdate' index: -1000  
1.	'networkwrapper.receivemessages' -> 'NetworkWrapper+Callbacks.OnUpdate' index: -999  
2.	'BlockEntities.Implementations.Grass+Callbacks.OnUpdate' -> 'BlockEntities.Implementations.Grass+Callbacks.OnUpdate'   
3.	'Blueprints.BlueprintTracker+Callbacks.OnUpdate' -> 'Blueprints.BlueprintTracker+Callbacks.OnUpdate'   
4.	'colonytrackerupdate' -> 'ColonyTracker+Callbacks.OnUpdate'   
5.	'dotrading' -> 'ColonyTrading.Update'   
6.	'monstertracker.update' -> 'Monsters.MonsterTracker+Callbacks.OnUpdate'   
7.	'effectstracker' -> 'EffectsTracker.OnUpdate'   
		 Parent @ 6 : 'monstertracker.update'  
		 Child @ 10 : 'npctracker.update'  
8.	'Networking.NetworkSteamDiscovery+DiscoveryMetadataCacher.OnUpdate' -> 'Networking.NetworkSteamDiscovery+DiscoveryMetadataCacher.OnUpdate'   
9.	'Notifications.NPCDeathMarkerManager.OnUpdate' -> 'Notifications.NPCDeathMarkerManager.OnUpdate'   
10.	'npctracker.update' -> 'NPC.NPCTracker+Callbacks.OnUpdate'   
11.	'ChunkQueue.Update' -> 'ChunkQueue+Callbacks.OnUpdate' index: 100  
12.	'pipliz.server.chunkupdater' -> 'ChunkUpdating.Update'   
		 Parent @ 11 : 'ChunkQueue.Update'  
13.	'pipliz.server.overlaycounters.update' -> 'NetworkUI.OverlayCounterManager+Callbacks.OnUpdate'   
14.	'pipliz.server.tickscounter' -> 'Chatting.Commands.TicksPerSecond.Update'   
15.	'pipliz.server.updatetimecycle' -> 'TimeCycle.Update'   
16.	'players.update' -> 'Players+Callbacks.OnUpdate'   
17.	'savemanager.onupdate' -> 'Saving.SaveManager+Callbacks.OnUpdate'   
18.	'update_transports' -> 'Transport.TransportManager+Callbacks.OnUpdate'   
19.	'update_water' -> 'BlockEntities.Implementations.Water.Tick'   
20.	'updateblockentities' -> 'BlockEntities.BlockEntityTracker+Callbacks.OnUpdate'   
21.	'masterserver.onupdate' -> 'MasterServerPublisher.OnUpdate' index: 1000  


CallbackType: `OnUpdateEnd`  
=======  
Method type: System.Action  
At the end of unity's Update method.  
Registered callbacks: 4  
0.	'pipliz.server.senddirtystockpiles' -> 'StockpileManager.SendDirtyStockpiles'   
1.	'mainthreadactions' -> 'ThreadManager+Callbacks.OnUpdateEnd' index: 100  
2.	'networkwrapper.finishframe' -> 'NetworkWrapper+Callbacks.OnUpdateEnd' index: 500  
3.	'endmainthreadonly' -> 'ThreadPhase+Callbacks.OnUpdateEnd' index: 1000000  


CallbackType: `OnLateUpdate`  
=======  
Method type: System.Action  
Called inside unity's LateUpdate method  
Registered callbacks: 1  
0.	'servertime.endframe' -> 'ServerTime+ServerCallbacks.OnLateUpdate' index: 1000  


CallbackType: `AfterItemTypesDefined`  
=======  
Method type: System.Action  
First callback after all item types should be defined, so you can resolve types etc here  
Registered callbacks: 50  
0.	'lua.execute_post_type_bootstraps' -> 'LuaScripting.LuaManager+PostTypeCallbacks.AfterItemTypesDefined' index: -1000000  
1.	'initialize_upgrademanager' -> 'Assets.ColonyPointUpgrades.UpgradesManager+CallbacksA.AfterItemTypesDefined' index: -1001  
2.	'create_servermanager_trackers' -> 'ServerManager.CreateBlockEntityCallbacks' index: -1000  
3.	'process_smart_moulding' -> 'BlockEntities.Implementations.MouldingPlacement.AfterItemTypesDefined'   
		 Child @ 7 : 'blockentitycallback.autoloaders'  
4.	'process_smart_quarter' -> 'BlockEntities.Implementations.QuarterBlockPlacement.AfterItemTypesDefined'   
		 Child @ 7 : 'blockentitycallback.autoloaders'  
5.	'register_smart_placement' -> 'BlockEntities.SmartPlacement.AfterItemTypesDefined'   
		 Child @ 7 : 'blockentitycallback.autoloaders'  
6.	'register_smart_placement_lua' -> 'BlockEntities.SmartPlacementLua.AfterItemTypesDefined'   
		 Parent @ 0 : 'lua.execute_post_type_bootstraps'  
		 Child @ 7 : 'blockentitycallback.autoloaders'  
7.	'blockentitycallback.autoloaders' -> 'ServerManager.AutoLoadBlockEntities'   
		 Parent @ 2 : 'create_servermanager_trackers'  
8.	'chunk_dedupe_initializer' -> 'Chunk+ChunkDataDeduplicator.Initialize'   
9.	'NPC.NPCNames.AfterItemTypesDefined' -> 'NPC.NPCNames.AfterItemTypesDefined'   
		 Child @ 26 : 'pipliz.server.endloadcolonies'  
10.	'pipliz.server.loadnpctypes' -> 'NPC.NPCType.LoadNPCTypes'   
11.	'areajobs.insertattributed' -> 'AreaJobTracker.RegisterAutoDefs'   
		 Parent @ 10 : 'pipliz.server.loadnpctypes'  
12.	'parse_starterpack_patches_afteritemtype' -> 'StarterPacks.Loader.ParsePacks'   
		 Child @ 13 : 'pipliz.server.recipeload'  
13.	'pipliz.server.recipeload' -> 'Recipes.RecipeStorage+Callbacks.AfterItemTypesDefined'   
		 Parent @ 2 : 'create_servermanager_trackers'  
14.	'pipliz.server.loadresearchables' -> 'ServerManager.LoadResearchables'   
		 Parent @ 10 : 'pipliz.server.loadnpctypes'  
		 Parent @ 2 : 'create_servermanager_trackers'  
		 Parent @ 13 : 'pipliz.server.recipeload'  
15.	'pipliz.server.endloadplayers' -> 'Players.EndInitializePlayerData'   
16.	'createareajobdefinitions' -> 'AreaJobTracker+AreaJobPatches.CreateAreaJobDefinitions'   
		 Parent @ 10 : 'pipliz.server.loadnpctypes'  
17.	'pipliz.server.loadblueprints' -> 'Blueprints.BlueprintTracker+Callbacks.AfterItemTypesDefined'   
		 Parent @ 15 : 'pipliz.server.endloadplayers'  
		 Child @ 26 : 'pipliz.server.endloadcolonies'  
18.	'quests.loaddefinitions' -> 'Quests.QuestManager+QuestCallbacks.AfterItemTypesDefined'   
		 Parent @ 2 : 'create_servermanager_trackers'  
		 Parent @ 13 : 'pipliz.server.recipeload'  
		 Child @ 26 : 'pipliz.server.endloadcolonies'  
19.	'bookcase_registers' -> 'BlockEntities.Implementations.Bookcases+Callbacks.AfterItemTypesDefined'   
		 Parent @ 2 : 'create_servermanager_trackers'  
		 Child @ 21 : 'pipliz.blocknpcs.registerjobs'  
20.	'wisteriatree_register' -> 'BlockEntities.Implementations.WisteriaTreeRegistration.AfterItemTypesDefined'   
		 Parent @ 2 : 'create_servermanager_trackers'  
		 Child @ 21 : 'pipliz.blocknpcs.registerjobs'  
21.	'pipliz.blocknpcs.registerjobs' -> 'Jobs.BlockJobLoader.AfterDefiningNPCTypes'   
		 Parent @ 2 : 'create_servermanager_trackers'  
		 Parent @ 10 : 'pipliz.server.loadnpctypes'  
		 Parent @ 7 : 'blockentitycallback.autoloaders'  
22.	'creategrowabledefinitions' -> 'GrowableBlocks.GrowablePatchHandler.CreateGrowableDefinitions'   
		 Parent @ 2 : 'create_servermanager_trackers'  
23.	'parse_elevatortypes' -> 'Transport.Elevator.ElevatorManager.AfterItemTypesDefined'   
		 Child @ 28 : 'pipliz.server.completeloadmiscworld'  
24.	'parse_railtypes' -> 'Transport.Rail.RailManager.AfterItemTypesDefined'   
		 Child @ 28 : 'pipliz.server.completeloadmiscworld'  
25.	'register_upgrades' -> 'Assets.ColonyPointUpgrades.UpgradesManager+CallbacksB.AfterItemTypesDefined' index: 10  
		 Parent @ 1 : 'initialize_upgrademanager'  
		 Parent @ 14 : 'pipliz.server.loadresearchables'  
		 Child @ 26 : 'pipliz.server.endloadcolonies'  
26.	'pipliz.server.endloadcolonies' -> 'ServerManager.LoadColonies'   
		 Parent @ 10 : 'pipliz.server.loadnpctypes'  
		 Parent @ 11 : 'areajobs.insertattributed'  
		 Parent @ 2 : 'create_servermanager_trackers'  
		 Parent @ 13 : 'pipliz.server.recipeload'  
		 Parent @ 14 : 'pipliz.server.loadresearchables'  
		 Parent @ 15 : 'pipliz.server.endloadplayers'  
		 Parent @ 16 : 'createareajobdefinitions'  
27.	'load_notifications' -> 'Notifications.NotificationCallbacks.AfterItemTypesDefined'   
		 Parent @ 26 : 'pipliz.server.endloadcolonies'  
		 Child @ 28 : 'pipliz.server.completeloadmiscworld'  
28.	'pipliz.server.completeloadmiscworld' -> 'ServerManager.CompleteLoadMiscWorld'   
		 Parent @ 26 : 'pipliz.server.endloadcolonies'  
		 Child @ 29 : 'start_load_startup_chunks'  
29.	'start_load_startup_chunks' -> 'ServerManager.CreateBlockEntityTracker' index: -1000  
		 Parent @ 7 : 'blockentitycallback.autoloaders'  
		 Parent @ 8 : 'chunk_dedupe_initializer'  
		 Parent @ 26 : 'pipliz.server.endloadcolonies'  
		 Parent @ 21 : 'pipliz.blocknpcs.registerjobs'  
		 Parent @ 22 : 'creategrowabledefinitions'  
30.	'process_paintables' -> 'BlockEntities.Implementations.Paint.Paintables.AfterItemTypesDefined' index: -10  
31.	'BlockEntities.Implementations.BombFuse.AfterItemTypesDefined' -> 'BlockEntities.Implementations.BombFuse.AfterItemTypesDefined'   
32.	'BlockEntities.Implementations.Grass+Callbacks.AfterItemTypesDefined' -> 'BlockEntities.Implementations.Grass+Callbacks.AfterItemTypesDefined'   
33.	'BlockEntities.Implementations.PlayerSaplings+Callbacks.AfterItemTypesDefined' -> 'BlockEntities.Implementations.PlayerSaplings+Callbacks.AfterItemTypesDefined'   
34.	'BlockEntities.Implementations.WallPaintingPlacer.AfterItemTypesDefined' -> 'BlockEntities.Implementations.WallPaintingPlacer.AfterItemTypesDefined'   
35.	'monstertracker.load' -> 'Monsters.MonsterTracker+Callbacks.AfterItemTypesDefined'   
		 Parent @ 26 : 'pipliz.server.endloadcolonies'  
36.	'effectstracker_load' -> 'EffectsTracker.AfterItemTypesDefined'   
		 Parent @ 2 : 'create_servermanager_trackers'  
		 Parent @ 35 : 'monstertracker.load'  
37.	'find_auto_chatcommands' -> 'Chatting.CommandManager.Initialize'   
38.	'GrowableBlocks.SaplingHandler.AfterItemTypesDefined' -> 'GrowableBlocks.SaplingHandler.AfterItemTypesDefined'   
39.	'Jobs.ToolSetManager.AfterItemTypesDefined' -> 'Jobs.ToolSetManager.AfterItemTypesDefined'   
40.	'pipliz.server.asyncloadpermissions' -> 'PermissionsManager.Reload'   
41.	'pipliz.server.endblackandwhitelisting' -> 'BlackAndWhitelisting.EndReload'   
42.	'pipliz.server.endloadwater' -> 'BlockEntities.Implementations.Water.Load'   
43.	'pipliz.server.loadnpcmeshes' -> 'NPC.NPCType.LoadNPCMeshes'   
44.	'register_onserverclicklua' -> 'BlockEntities.OnServerClickLua.AfterItemTypesDefined'   
		 Parent @ 0 : 'lua.execute_post_type_bootstraps'  
45.	'Statistics.AchievementGathering.AfterItemTypesDefined' -> 'Statistics.AchievementGathering.AfterItemTypesDefined'   
46.	'trading.doublelinkrules' -> 'ColonyTrading.LoadColonies'   
		 Parent @ 26 : 'pipliz.server.endloadcolonies'  
47.	'wait_complete_startup_chunks' -> 'ServerManager.WaitForCompletedStartupChunks' index: 1000  
		 Parent @ 29 : 'start_load_startup_chunks'  
48.	'set_colony_sciencemask' -> 'Science.ScienceManager+Callbacks.AfterItemTypesDefined' index: 1  
		 Parent @ 47 : 'wait_complete_startup_chunks'  
		 Parent @ 26 : 'pipliz.server.endloadcolonies'  
		 Parent @ 2 : 'create_servermanager_trackers'  
49.	'prepare_network_packets' -> 'ItemTypesServer+Callbacks.AfterItemTypesDefined' index: 10000  


CallbackType: `OnQuit`  
=======  
Method type: System.Action  
Called in the quit method queue  
Registered callbacks: 16  
0.	'pipliz.shared.waitforasyncquitsearly' -> 'Pipliz.Application.WaitForQuits' index: -1000  
1.	'AreaJobTracker+Callbacks.OnQuit' -> 'AreaJobTracker+Callbacks.OnQuit'   
2.	'BlockEntities.BlockEntityCallbacks+Callbacks.OnQuit' -> 'BlockEntities.BlockEntityCallbacks+Callbacks.OnQuit'   
3.	'close_filetable' -> 'FileTable+Callbacks.OnQuit'   
4.	'EffectsTracker.OnQuit' -> 'EffectsTracker.OnQuit'   
5.	'LargeBoundsAwarenessWrapper+Callbacks.OnQuit' -> 'LargeBoundsAwarenessWrapper+Callbacks.OnQuit'   
6.	'Monsters.MonsterSpawner+Callbacks.OnQuit' -> 'Monsters.MonsterSpawner+Callbacks.OnQuit'   
7.	'TerrainGeneration2.TerrainGenerator2ModManager.OnQuit' -> 'TerrainGeneration2.TerrainGenerator2ModManager.OnQuit'   
8.	'trigger_autosave' -> 'Saving.SaveManager+Callbacks2.OnQuit'   
9.	'steamnetworking.close' -> 'Pipliz.Networking.SteamNetworking+ModRegistering.OnQuit' index: 1  
10.	'masterserver.onquit' -> 'MasterServerPublisher.OnQuit' index: 10  
11.	'pipliz.jointhreads' -> 'Pipliz.Threading.ThreadSafeQuitWrapper.JoinThread' index: 500  
12.	'close_savemanager' -> 'Saving.SaveManager+Callbacks.OnQuit' index: 750  
13.	'pipliz.shared.waitforasyncquitslate' -> 'Pipliz.Application.WaitForQuits' index: 1000  
14.	'LZ4DecoderFreeing' -> 'Pipliz.LZ4.LZ4Codec+Callbacks.OnQuit' index: 10000  
15.	'modloader.dispose' -> 'ModLoader+ModloaderCallbacks.OnQuit' index: 1000000  


CallbackType: `AfterSelectedWorld`  
=======  
Method type: System.Action  
First callback after the world to load has been determined  
Registered callbacks: 12  
0.	'pipliz.server.loadaudiofiles' -> 'AudioManager.LoadAudioFiles' index: -100  
1.	'pipliz.server.applytexturemappingpatches' -> 'ItemTypesServer.ApplyTextureMappingPatches'   
2.	'pipliz.server.registertexturemappingtextures' -> 'ItemTypesServer.RegisterTextures' index: -100  
		 Parent @ 1 : 'pipliz.server.applytexturemappingpatches'  
3.	'start_load_vehicle_meshes' -> 'Transport.TransportManager+Callbacks.AfterSelectedWorld' index: -100  
4.	'allocate_chunkqueue' -> 'ChunkQueue+Callbacks.AfterSelectedWorld'   
5.	'create_savemanager' -> 'ServerManager.CreateSaveManager'   
6.	'load_npc_mapping' -> 'NPC.NPCTypeID+Callbacks.AfterSelectedWorld'   
7.	'load_worldsettings' -> 'ServerManager.LoadWorldSettings'   
		 Parent @ 5 : 'create_savemanager'  
8.	'pipliz.imagemanager.loadingimages' -> 'Pipliz.ImageManager.AfterWorldLoad'   
9.	'pipliz.server.loadtimecycle' -> 'TimeCycle.Initialize'   
		 Parent @ 7 : 'load_worldsettings'  
10.	'pipliz.server.startblackandwhitelisting' -> 'BlackAndWhitelisting.StartReload'   
11.	'ServerManager+InitBurstDelegate.AfterSelectedWorld' -> 'ServerManager+InitBurstDelegate.AfterSelectedWorld'   


CallbackType: `AfterAddingBaseTypes`  
=======  
Method type: System.Action<System.Collections.Generic.Dictionary<string, ItemTypesServer.ItemTypeRaw>>  
Callback after AddItemTypes but before parsing those types; intended usecase is to allow changing the raw json  
No registered uses  


CallbackType: `AfterWorldLoad`  
=======  
Method type: System.Action  
Most things should be initialized by now, does not have to include chunks and pathing data though (those may be ongoing on other threads)  
Registered callbacks: 2  
0.	'pipliz.server.localization.convert' -> 'Localization.Convert'   
1.	'pipliz.server.monsterspawner.register' -> 'Monsters.MonsterSpawner+Callbacks.AfterWorldLoad'   


CallbackType: `AfterInitialChunkLoad`  
=======  
Method type: System.Action  
After startup chunks queued during server startup have fully loaded and CompletedInitialLoad flips to true.  
Registered callbacks: 1  
0.	'quests.afterinitialchunkload' -> 'Quests.QuestManager+QuestCallbacks.AfterInitialChunkLoad'   


CallbackType: `AfterNetworkSetup`  
=======  
Method type: System.Action  
After the networkwrapper clas is ready to send data. Can take a few seconds for steam servers to connect.  
Registered callbacks: 1  
0.	'masterserver.after-network-setup' -> 'MasterServerPublisher.AfterNetworkSetup' index: 1000  


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
Registered callbacks: 4  
0.	'MasterServerPublisher.OnPlayerConnectedEarly' -> 'MasterServerPublisher.OnPlayerConnectedEarly'   
1.	'Networking.NetworkSteam+SteamConnectionWrapper+SteamRichPresenceMetadataSync.OnPlayerConnectedEarly' -> 'Networking.NetworkSteam+SteamConnectionWrapper+SteamRichPresenceMetadataSync.OnPlayerConnectedEarly'   
2.	'pipliz.server.sendinitialtime' -> 'TimeCycle.SendHeartBeat'   
3.	'pipliz.server.start_loadsurroundings' -> 'ChunkQueue+Callbacks.OnPlayerConnectedEarly'   


CallbackType: `OnPlayerDisconnected`  
=======  
Method type: System.Action<Players.Player>  
Registered callbacks: 10  
0.	'BlockEntities.Implementations.Astrolabe+Callbacks.OnPlayerDisconnected' -> 'BlockEntities.Implementations.Astrolabe+Callbacks.OnPlayerDisconnected'   
1.	'BlockEntities.Implementations.Sign.SignTracker+SignSender.OnPlayerDisconnected' -> 'BlockEntities.Implementations.Sign.SignTracker+SignSender.OnPlayerDisconnected'   
2.	'Jobs.CommandToolManager+ConstructionMenuHelper.OnPlayerDisconnected' -> 'Jobs.CommandToolManager+ConstructionMenuHelper.OnPlayerDisconnected'   
3.	'MasterServerPublisher.OnPlayerDisconnected' -> 'MasterServerPublisher.OnPlayerDisconnected'   
4.	'Networking.NetworkSteam+SteamConnectionWrapper+SteamRichPresenceMetadataSync.OnPlayerDisconnected' -> 'Networking.NetworkSteam+SteamConnectionWrapper+SteamRichPresenceMetadataSync.OnPlayerDisconnected'   
5.	'NetworkUI.NetworkMenuManager+NPCMenuCallbacks.OnPlayerDisconnected' -> 'NetworkUI.NetworkMenuManager+NPCMenuCallbacks.OnPlayerDisconnected'   
6.	'Notifications.NPCDeathMarkerManager.OnPlayerDisconnected' -> 'Notifications.NPCDeathMarkerManager.OnPlayerDisconnected'   
7.	'pipliz.server.overlaycounters.disconnected' -> 'NetworkUI.OverlayCounterManager+Callbacks.OnPlayerDisconnected'   
8.	'Reset control mode to firstperson' -> 'Players+Callbacks.OnPlayerDisconnected'   
9.	'Statistics.AchievementGathering.OnPlayerDisconnected' -> 'Statistics.AchievementGathering.OnPlayerDisconnected'   


CallbackType: `OnSavingPlayer`  
=======  
Method type: System.Action<Newtonsoft.Json.Linq.JObject, Players.Player>  
Allows saving custom data into the player save file  
Registered callbacks: 2  
0.	'pipliz.server.overlaycounters.saveplayer' -> 'NetworkUI.OverlayCounterManager+Callbacks.OnSavingPlayer'   
1.	'save_notification_reads' -> 'Notifications.NotificationCallbacks.OnSavingPlayer'   


CallbackType: `OnLoadingPlayer`  
=======  
Method type: System.Action<Newtonsoft.Json.Linq.JObject, Players.Player>  
Allows loading custom data saved with OnSavingPlayer  
Registered callbacks: 2  
0.	'load_notification_reads' -> 'Notifications.NotificationCallbacks.OnLoadingPlayer'   
1.	'pipliz.server.overlaycounters.loadplayer' -> 'NetworkUI.OverlayCounterManager+Callbacks.OnLoadingPlayer'   


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
No registered uses  


CallbackType: `OnPlayerMoved2`  
=======  
Method type: System.Action<Players.Player, UnityEngine.Vector3, UnityEngine.Vector3>  
Registered callbacks: 5  
0.	'BlockEntities.Implementations.NPCBlockerAirFixer.OnPlayerMoved2' -> 'BlockEntities.Implementations.NPCBlockerAirFixer.OnPlayerMoved2'   
1.	'BlockEntities.Implementations.Sign.SignTracker+SignSender.OnPlayerMoved2' -> 'BlockEntities.Implementations.Sign.SignTracker+SignSender.OnPlayerMoved2'   
2.	'Call old onPlayerMoved' -> 'Players+Callbacks.OnPlayerMoved2'   
3.	'Notifications.NPCDeathMarkerManager.OnPlayerMoved2' -> 'Notifications.NPCDeathMarkerManager.OnPlayerMoved2'   
4.	'pipliz.server.loadsurroundings' -> 'ChunkQueue+Callbacks.OnPlayerMoved2'   


CallbackType: `OnModifyResearchables`  
=======  
Method type: System.Action<System.Collections.Generic.Dictionary<string, Science.IResearchable>>  
Called inside of OnAddResearchables - allows modifying researches added through jsonFiles before they're registered  
Registered callbacks: 1  
0.	'Recipes.RecipeStorage+Callbacks.OnModifyResearchables' -> 'Recipes.RecipeStorage+Callbacks.OnModifyResearchables'   


CallbackType: `OnChangedBlock`  
=======  
Method type: System.Action<ModLoader.OnTryChangeBlockData>  
Called after OnTryChangeBlock, if the block was actually succesfully changed.  
Registered callbacks: 3  
0.	'BlockEntities.Implementations.AttachmentChecker.OnChangedBlock' -> 'BlockEntities.Implementations.AttachmentChecker.OnChangedBlock'   
1.	'quests.blockchange' -> 'Quests.QuestManager+QuestCallbacks.OnChangedBlock'   
2.	'update_collisions' -> 'Transport.CollisionChecker.UpdateCollisions'   


CallbackType: `OnTryChangeBlock`  
=======  
Method type: System.Action<ModLoader.OnTryChangeBlockData>  
Callback triggered upon a call to ServerManager.TryChangeBlock - used by various code and the client to edit blocks  
Nothing changed yet when this callback happens and the change can be blocked.  
You can block is by setting CallbackState to Cancelled  
Registered callbacks: 6  
0.	'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnTryChangeBlock' -> 'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnTryChangeBlock'   
1.	'BlockEntities.Implementations.BedTracker+Callbacks.OnTryChangeBlock' -> 'BlockEntities.Implementations.BedTracker+Callbacks.OnTryChangeBlock'   
2.	'BlockEntities.Implementations.DependentBlockPlacement+Callbacks.OnTryChangeBlock' -> 'BlockEntities.Implementations.DependentBlockPlacement+Callbacks.OnTryChangeBlock'   
3.	'GrowableBlocks.SaplingHandler.OnTryChangeBlock' -> 'GrowableBlocks.SaplingHandler.OnTryChangeBlock'   
4.	'LargeBoundsAwarenessWrapper+Callbacks.OnTryChangeBlock' -> 'LargeBoundsAwarenessWrapper+Callbacks.OnTryChangeBlock'   
5.	'preventaccidentalbannerremoval' -> 'ServerManager.TempBannerFix'   


CallbackType: `OnPlayerConnectedLate`  
=======  
Method type: System.Action<Players.Player>  
Messages send here will work unlike with OnPlayerConnectedEarly. May be delayed till after the client is done loading.  
Registered callbacks: 9  
0.	'pipliz.imagemanager.sendimagesettings' -> 'Pipliz.ImageManager.OnPlayerConnectedEarly' index: -1000  
1.	'BlockEntities.Implementations.Sign.SignTracker+SignSender.OnPlayerConnectedLate' -> 'BlockEntities.Implementations.Sign.SignTracker+SignSender.OnPlayerConnectedLate'   
2.	'Blueprints.BlueprintTracker+Callbacks.OnPlayerConnectedLate' -> 'Blueprints.BlueprintTracker+Callbacks.OnPlayerConnectedLate'   
3.	'Notifications.NPCDeathMarkerManager.OnPlayerConnectedLate' -> 'Notifications.NPCDeathMarkerManager.OnPlayerConnectedLate'   
4.	'pipliz.server.meshedobjects.sendtable' -> 'MeshedObjects.MeshedObjectType.OnPlayerConnectedLate'   
5.	'pipliz.server.overlaycounters.playerconnected' -> 'NetworkUI.OverlayCounterManager+Callbacks.OnPlayerConnectedLate'   
6.	'pipliz.server.sendcheatstate' -> 'Chatting.Commands.DisableAchievements+Callbacks.OnPlayerConnectedLate'   
7.	'pipliz.server.sendnpctypes' -> 'NPC.NPCType.SendNPCTypes'   
8.	'send_attached_mesh' -> 'MeshedObjects.MeshedObjectManager.SendAttachedMesh'   
		 Parent @ 4 : 'pipliz.server.meshedobjects.sendtable'  


CallbackType: `OnAddResearchables`  
=======  
Method type: System.Action  
The place to add researchables to Server.Science.ScienceManager  
Registered callbacks: 1  
0.	'registerresearchables' -> 'Science.ScienceManager+Callbacks.OnAddResearchables'   


CallbackType: `OnConstructTooltipUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.ConstructTooltipUIData>  
Registered callbacks: 3  
0.	'button_startcolony' -> 'NetworkUI.NetworkMenuManager+BannerPlacementCallbacks.OnConstructTooltipUI'   
1.	'Jobs.CommandToolManager.OnConstructTooltipUI' -> 'Jobs.CommandToolManager.OnConstructTooltipUI'   
2.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+BuildBaseTooltipCallbacks.OnConstructTooltipUI'   


CallbackType: `OnNPCCraftedRecipe`  
=======  
Method type: System.Action<Jobs.IJob, Recipes.Recipe, System.Collections.Generic.List<Recipes.RecipeResult>>  
The results list is re-used, don't store it.  
Results can be edited. After the callback they'll be added to the npc/block's inventory  
If the results are not empty, the npc will show a npc indicator with a weighted random type from the non-optional results  
Registered callbacks: 3  
0.	'resolve.chances' -> 'Jobs.CallbackImplementations+GatherItemResolver.OnNPCCraftedRecipe' index: 10  
1.	'registerproduction' -> 'Jobs.CallbackImplementations+RegisterProductionStats.OnNPCCraftedRecipe'   
		 Parent @ 0 : 'resolve.chances'  
2.	'quests.npccrafted' -> 'Quests.QuestManager+QuestCallbacks.OnNPCCraftedRecipe' index: 100  
		 Parent @ 0 : 'resolve.chances'  


CallbackType: `OnAutoCrafterCraftedRecipe`  
=======  
Method type: System.Action<BlockEntities.Implementations.AutocrafterTracker.AutocrafterInstance, Recipes.Recipe, System.Collections.Generic.List<ItemTypes.ItemTypeDrops>>  
Called after an autocrafter completes a craft.  
The first argument is the autocrafter instance that produced the results.  
The results list is re-used and can be edited before the items are added.  
Registered callbacks: 2  
0.	'resolve_chance' -> 'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnAutoCrafterCraftedRecipe' index: -100  
1.	'quests.autocraftercrafted' -> 'Quests.QuestManager+QuestCallbacks.OnAutoCrafterCraftedRecipe' index: 100  
		 Parent @ 0 : 'resolve_chance'  


CallbackType: `OnPlayerDeath`  
=======  
Method type: System.Action<Players.Player>  
Registered callbacks: 1  
0.	'pipliz.server.onplayerdeath' -> 'Players.OnDeath'   


CallbackType: `OnPlayerRespawn`  
=======  
Method type: System.Action<Players.Player>  
Registered callbacks: 2  
0.	'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnPlayerRespawn' -> 'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnPlayerRespawn'   
1.	'pipliz.server.onplayerrespawn' -> 'Players.OnDeathReset'   


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
Registered callbacks: 2  
0.	'pipliz.server.jobfinderdirty' -> 'Jobs.JobFinder+Callbacks.OnNPCRecruited'   
1.	'quests.recruited' -> 'Quests.QuestManager+QuestCallbacks.OnNPCRecruited'   


CallbackType: `OnNPCDied`  
=======  
Method type: System.Action<NPC.NPCBase>  
No registered uses  


CallbackType: `OnNPCDied2`  
=======  
Method type: System.Action<NPC.NPCBase, Shared.Notifications.NPCDeath.EReason>  
Registered callbacks: 2  
0.	'Notifications.NPCDeathMarkerManager.OnNPCDied2' -> 'Notifications.NPCDeathMarkerManager.OnNPCDied2'   
1.	'pipliz.server.jobfinderdirty' -> 'Jobs.JobFinder+Callbacks.OnNPCDied2'   


CallbackType: `OnNPCJobChanged`  
=======  
Method type: System.Action<System.ValueTuple<NPC.NPCBase, Jobs.IJob, Jobs.IJob>>  
Registered callbacks: 2  
0.	'pipliz.server.refundrecruitement' -> 'Jobs.JobFinder+Callbacks.OnNPCJobChanged'   
1.	'quests.npcjobchanged' -> 'Quests.QuestManager+QuestCallbacks.OnNPCJobChanged'   


CallbackType: `OnNPCHit`  
=======  
Method type: System.Action<NPC.NPCBase, ModLoader.OnHitData>  
Registered callbacks: 1  
0.	'NPC.NPCBase+Callbacks.OnNPCHit' -> 'NPC.NPCBase+Callbacks.OnNPCHit'   


CallbackType: `OnPlayerClicked`  
=======  
Method type: System.Action<Players.Player, Shared.PlayerClickedData>  
Registered callbacks: 19  
0.	'BlockEntities.Implementations.AlarmbellTracker+Callbacks.OnPlayerClicked' -> 'BlockEntities.Implementations.AlarmbellTracker+Callbacks.OnPlayerClicked'   
1.	'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnPlayerClicked' -> 'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnPlayerClicked'   
2.	'BlockEntities.Implementations.BombFuse.OnPlayerClicked' -> 'BlockEntities.Implementations.BombFuse.OnPlayerClicked'   
3.	'BlockEntities.Implementations.Door.DoorTracker+Callbacks.OnPlayerClicked' -> 'BlockEntities.Implementations.Door.DoorTracker+Callbacks.OnPlayerClicked'   
4.	'BlockEntities.Implementations.Failsafes+Callbacks.OnPlayerClicked' -> 'BlockEntities.Implementations.Failsafes+Callbacks.OnPlayerClicked'   
5.	'BlockEntities.Implementations.Sign.SignTracker+Callbacks.OnPlayerClicked' -> 'BlockEntities.Implementations.Sign.SignTracker+Callbacks.OnPlayerClicked'   
6.	'BlockEntities.Implementations.TopdownTriggerManager+Callbacks.OnPlayerClicked' -> 'BlockEntities.Implementations.TopdownTriggerManager+Callbacks.OnPlayerClicked'   
7.	'BlockEntities.Implementations.WallPaintingPlacer.OnPlayerClicked' -> 'BlockEntities.Implementations.WallPaintingPlacer.OnPlayerClicked'   
8.	'BlockEntities.OnServerClickLua.OnPlayerClicked' -> 'BlockEntities.OnServerClickLua.OnPlayerClicked'   
9.	'check_banner_click' -> 'NetworkUI.NetworkMenuManager+BannerClickCallbacks.OnPlayerClicked'   
10.	'clicked_glider' -> 'Transport.Glider+Callbacks.OnPlayerClicked'   
		 Child @ 11 : 'clicked_transport'  
11.	'clicked_transport' -> 'Transport.TransportManager+Callbacks.OnPlayerClicked'   
12.	'Jobs.CommandToolManager.OnPlayerClicked' -> 'Jobs.CommandToolManager.OnPlayerClicked'   
13.	'pipliz.server.players.hitnpc' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnPlayerClicked'   
14.	'Transport.Elevator.ElevatorManager.OnPlayerClicked' -> 'Transport.Elevator.ElevatorManager.OnPlayerClicked'   
15.	'Transport.Rail.RailManager.OnPlayerClicked' -> 'Transport.Rail.RailManager.OnPlayerClicked'   
16.	'use_paint' -> 'BlockEntities.Implementations.Paint.Paintables.OnPlayerClicked'   
17.	'use_smart_moulding' -> 'BlockEntities.Implementations.MouldingPlacement.OnPlayerClicked'   
18.	'use_smart_quarterblock' -> 'BlockEntities.Implementations.QuarterBlockPlacement.OnPlayerClicked'   


CallbackType: `OnPlayerHit`  
=======  
Method type: System.Action<Players.Player, ModLoader.OnHitData>  
No registered uses  


CallbackType: `OnAutoSaveWorld`  
=======  
Method type: System.Action  
Triggers an autosave every x minutes, to begin autosaving non-block data (jobs, npc's, players)  
Registered callbacks: 14  
0.	'start_world_transaction' -> 'Saving.SaveManager+Callbacks2.OnAutoSaveWorld' index: -100  
1.	'BlockEntities.Implementations.Grass+Callbacks.OnAutoSaveWorld' -> 'BlockEntities.Implementations.Grass+Callbacks.OnAutoSaveWorld'   
2.	'effectstracker' -> 'EffectsTracker.OnAutoSaveWorld'   
3.	'pipliz.server.autosaveplayers' -> 'Players+Callbacks.OnAutoSaveWorld'   
4.	'pipliz.server.autosavewater' -> 'BlockEntities.Implementations.Water.Save'   
5.	'pipliz.server.saveareajobs' -> 'ServerManager.SaveAreaJobs'   
6.	'pipliz.server.savecolonies' -> 'ServerManager.SaveColonies'   
7.	'pipliz.server.savemiscworld' -> 'ServerManager.SaveMiscWorld'   
8.	'pipliz.server.savemonsters' -> 'ServerManager.SaveMonsters'   
9.	'pipliz.server.savenpcs' -> 'ServerManager.SaveNPCs'   
10.	'pipliz.server.saveworldsettings' -> 'ServerManager.SaveWorldSettings'   
11.	'save_dirty_notifications' -> 'Notifications.NotificationCallbacks.OnAutoSaveWorld'   
12.	'save_jobconfig' -> 'JobConfigManager+Callbacks.OnAutoSaveWorld'   
13.	'end_world_transaction' -> 'Saving.SaveManager+Callbacks.OnAutoSaveWorld' index: 100  


CallbackType: `OnNPCGathered`  
=======  
Method type: System.Action<Jobs.IJob, Pipliz.Vector3Int, System.Collections.Generic.List<ItemTypes.ItemTypeDrops>>  
Can edit the results; don't store them - the list is re-used.  
After the callback, results will be added to the npc's inventory.  
The location does not have to be the job/npc's position - see the construction jobs.  
Registered callbacks: 3  
0.	'resolve.chances' -> 'Jobs.CallbackImplementations+GatherItemResolver.OnNPCGathered' index: 10  
1.	'registerproduction' -> 'Jobs.CallbackImplementations+RegisterProductionStats.OnNPCGathered'   
		 Parent @ 0 : 'resolve.chances'  
2.	'quests.npcgathered' -> 'Quests.QuestManager+QuestCallbacks.OnNPCGathered' index: 100  
		 Parent @ 0 : 'resolve.chances'  


CallbackType: `OnPointRecipeMade`  
=======  
Method type: System.Action<ColonyGroup, Recipes.Recipe, System.Collections.Generic.List<ItemTypes.ItemTypeDrops>>  
Registered callbacks: 2  
0.	'resolve_chance' -> 'ColonyGroup+PointRecipeCallback.OnPointRecipeMade' index: -100  
1.	'quests.merchanthubmade' -> 'Quests.QuestManager+QuestCallbacks.OnPointRecipeMade' index: 100  
		 Parent @ 0 : 'resolve_chance'  


CallbackType: `OnShouldKeepChunkLoaded`  
=======  
Method type: System.Action<ChunkUpdating.KeepChunkLoadedData>  
!!! Will be called from multiple threads, simultaneously !!!  
Periodically triggered for every chunk loaded. Use it to keep chunks loaded - and to indicate how long you expect them to stay loaded.  
{data.CheckedChunk} -> the chunk  
{data.MillisecondsTillNextCheck} -> the minimum time until another callback will be fired. Defaults to random between 24000 and 64000  
{data.Result} -> bool indicating whether or not to keep this chunk. Defaults to false (set to true to keep it)  
{data.ChunkLoadedSource} -> source for this callback. If loadedstorage / loadedgenerator, the chunk is already locked for writing. if Updater, it is not locked.  
Registered callbacks: 5  
0.	'bannercheck' -> 'BlockEntities.Implementations.BannerTracker.CheckKeepChunkLoaded'   
1.	'pipliz.server.playercheck' -> 'Players+Callbacks.OnShouldKeepChunkLoaded'   
		 Parent @ 0 : 'bannercheck'  
2.	'check_blockentities' -> 'ServerManager.KeepBlockEntitiesLoaded'   
		 Parent @ 1 : 'pipliz.server.playercheck'  
3.	'blueprint_keepsloaded' -> 'Blueprints.BlueprintTracker+Callbacks.OnShouldKeepChunkLoaded' index: 1000  
4.	'constructionarea.check' -> 'Jobs.Implementations.Construction.ConstructionManager+Callbacks.OnShouldKeepChunkLoaded' index: 1000  


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
Method type: System.Action<System.ValueTuple<Players.Player, Newtonsoft.Json.Linq.JObject, string>>  
Called when a player closes a networkmenu while some of its state was changed  
Registered callbacks: 1  
0.	'pipliz.parsenetui' -> 'NetworkUI.NetworkMenuManager+WorldSettingsCallbacks.OnPlayerChangedNetworkUIStorage'   


CallbackType: `OnPlayerPushedNetworkUIButton`  
=======  
Method type: System.Action<NetworkUI.ButtonPressCallbackData>  
Registered callbacks: 13  
0.	'Assets.UIGeneration.ColonyManageJobs.OnPlayerPushedNetworkUIButton' -> 'Assets.UIGeneration.ColonyManageJobs.OnPlayerPushedNetworkUIButton'   
1.	'Assets.UIGeneration.PointsUpgrades.OnPlayerPushedNetworkUIButton' -> 'Assets.UIGeneration.PointsUpgrades.OnPlayerPushedNetworkUIButton'   
2.	'BlockEntities.Implementations.Failsafes+Callbacks.OnPlayerPushedNetworkUIButton' -> 'BlockEntities.Implementations.Failsafes+Callbacks.OnPlayerPushedNetworkUIButton'   
3.	'BlockEntities.Implementations.Sign.SignTracker+Callbacks.OnPlayerPushedNetworkUIButton' -> 'BlockEntities.Implementations.Sign.SignTracker+Callbacks.OnPlayerPushedNetworkUIButton'   
4.	'button_opencolonytab' -> 'NetworkUI.NetworkMenuManager+BannerPlacementCallbacks.OnPlayerPushedNetworkUIButton'   
5.	'handle_colony_management' -> 'NetworkUI.NetworkMenuManager+ColonyManagementButtonCallbacks.OnPlayerPushedNetworkUIButton'   
6.	'Jobs.CommandToolManager.OnPlayerPushedNetworkUIButton' -> 'Jobs.CommandToolManager.OnPlayerPushedNetworkUIButton'   
7.	'Jobs.CommandToolManager+ConstructionMenuHelper.OnPlayerPushedNetworkUIButton' -> 'Jobs.CommandToolManager+ConstructionMenuHelper.OnPlayerPushedNetworkUIButton'   
8.	'NetworkUI.NetworkMenuManager+NPCMenuCallbacks.OnPlayerPushedNetworkUIButton' -> 'NetworkUI.NetworkMenuManager+NPCMenuCallbacks.OnPlayerPushedNetworkUIButton'   
9.	'NetworkUI.NetworkMenuManager+ToolshopCallbacks.OnPlayerPushedNetworkUIButton' -> 'NetworkUI.NetworkMenuManager+ToolshopCallbacks.OnPlayerPushedNetworkUIButton'   
10.	'Notifications.NPCDeathMarkerManager.OnPlayerPushedNetworkUIButton' -> 'Notifications.NPCDeathMarkerManager.OnPlayerPushedNetworkUIButton'   
11.	'pipliz.server.overlaycounters.button' -> 'NetworkUI.OverlayCounterManager+Callbacks.OnPlayerPushedNetworkUIButton'   
12.	'quests.buttons' -> 'Quests.QuestManager+QuestCallbacks.OnPlayerPushedNetworkUIButton'   


CallbackType: `OnSendAreaHighlights`  
=======  
Method type: System.Action<Players.Player, System.Collections.Generic.List<AreaJobTracker.AreaHighlight>, System.Collections.Generic.List<ushort>>  
Edit the highlights list, adding desired area highlights to be sent to the player.  
Edit the showWhileHoldingTypes to add/remove types that will show <all> areas when selected in the inventory  
You can manually trigger this callback through AreaJobTracker.SendData(player)  
Registered callbacks: 2  
0.	'pipliz.defaultholdingtypes' -> 'AreaJobTracker.GatherShowWhileHoldingTypes'   
1.	'pipliz.sendjobareas' -> 'AreaJobTracker.GatherJobHighlights'   


CallbackType: `OnActiveColonyChanges`  
=======  
Method type: System.Action<Players.Player, Colony, Colony>  
Registered callbacks: 7  
0.	'onchange' -> 'ServerManager.OnColonyChange' index: -1000  
1.	'Blueprints.BlueprintTracker+Callbacks.OnActiveColonyChanges' -> 'Blueprints.BlueprintTracker+Callbacks.OnActiveColonyChanges'   
2.	'pipliz.server.overlaycounters.activecolony' -> 'NetworkUI.OverlayCounterManager+Callbacks.OnActiveColonyChanges'   
3.	'resend_areajobs' -> 'AreaJobTracker.OnActiveColonyChange'   
4.	'sendconstructiondata' -> 'Jobs.Implementations.Construction.ConstructionManager.SendData'   
5.	'sendresearch' -> 'Science.ScienceManager+Callbacks.OnActiveColonyChanges'   
6.	'sendthreat' -> 'Difficulty.ColonyThreatLevel+Callbacks.OnActiveColonyChanges'   


CallbackType: `OnSavingColony`  
=======  
Method type: System.Action<Colony, Newtonsoft.Json.Linq.JObject>  
Registered callbacks: 2  
0.	'savebuiltin' -> 'ColonyTracker+Callbacks.OnSavingColony'   
1.	'savejobfinder' -> 'Jobs.JobFinder+Callbacks.OnSavingColony'   


CallbackType: `OnLoadingColony`  
=======  
Method type: System.Action<Colony, Newtonsoft.Json.Linq.JObject>  
Registered callbacks: 2  
0.	'loadbuiltin' -> 'ColonyTracker+Callbacks.OnLoadingColony'   
1.	'loadjobfinder' -> 'Jobs.JobFinder+Callbacks.OnLoadingColony'   


CallbackType: `OnSavingColonyGroup`  
=======  
Method type: System.Action<ColonyGroup, Newtonsoft.Json.Linq.JObject>  
Registered callbacks: 5  
0.	'quests.savegroup' -> 'Quests.QuestManager+QuestCallbacks.OnSavingColonyGroup'   
1.	'save_cheatstate' -> 'ColonyGroup+CheatCallbacks.OnSavingColonyGroup'   
2.	'save_trade' -> 'ColonyTrading+Callbacks.OnSavingColonyGroup'   
3.	'save_upgradestate' -> 'Assets.ColonyPointUpgrades.ColonyUpgradeState+Callbacks.OnSavingColonyGroup'   
4.	'savebuiltin' -> 'ColonyTracker+Callbacks.OnSavingColonyGroup'   


CallbackType: `OnLoadingColonyGroup`  
=======  
Method type: System.Action<ColonyGroup, Newtonsoft.Json.Linq.JObject>  
Registered callbacks: 5  
0.	'load_cheatstate' -> 'ColonyGroup+CheatCallbacks.OnLoadingColonyGroup'   
1.	'load_trade' -> 'ColonyTrading+Callbacks.OnLoadingColonyGroup'   
2.	'load_upgradestate' -> 'Assets.ColonyPointUpgrades.ColonyUpgradeState+Callbacks.OnLoadingColonyGroup'   
3.	'loadbuiltin' -> 'ColonyTracker+Callbacks.OnLoadingColonyGroup'   
4.	'quests.loadgroup' -> 'Quests.QuestManager+QuestCallbacks.OnLoadingColonyGroup'   


CallbackType: `OnPlayerEditedNetworkInputfield`  
=======  
Method type: System.Action<NetworkUI.InputfieldEditCallbackData>  
Registered callbacks: 1  
0.	'handle_builtin' -> 'NetworkUI.NetworkMenuManager+BuiltinCallbacks.OnPlayerEditedNetworkInputfield'   


CallbackType: `OnCreatedColony`  
=======  
Method type: System.Action<Colony>  
Called when a colony is created, does not trigger on loading colonies (!)  
Registered callbacks: 1  
0.	'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnCreatedColony' -> 'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnCreatedColony'   


CallbackType: `OnCreatedColonyGroup`  
=======  
Method type: System.Action<ColonyGroup>  
Registered callbacks: 3  
0.	'quests.createdgroup' -> 'Quests.QuestManager+QuestCallbacks.OnCreatedColonyGroup' index: -10  
1.	'disable_some_science' -> 'Science.ScienceManager+Callbacks.OnCreatedColonyGroup'   
2.	'spread_cheatstate' -> 'ColonyGroup+CheatCallbacks.OnCreatedColonyGroup'   


CallbackType: `OnConstructDiplomacyUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, Colony>  
No registered uses  


CallbackType: `OnConstructCommandTool`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, string>  
Registered callbacks: 1  
0.	'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnConstructCommandTool' -> 'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnConstructCommandTool'   


CallbackType: `OnConstructColonyOwnerManagementUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnConstructColonyOwnerManagementUI'   


CallbackType: `OnConstructBannerPlacementUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu>  
Registered callbacks: 2  
0.	'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnConstructBannerPlacementUI' -> 'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnConstructBannerPlacementUI'   
1.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+BannerPlacementCallbacks.OnConstructBannerPlacementUI'   


CallbackType: `OnConstructNPCContextMenu`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, NPC.NPCBase>  
Registered callbacks: 1  
0.	'NetworkUI.NetworkMenuManager+NPCMenuCallbacks.OnConstructNPCContextMenu' -> 'NetworkUI.NetworkMenuManager+NPCMenuCallbacks.OnConstructNPCContextMenu'   


CallbackType: `OnConstructBannerClickedUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, BlockEntities.Implementations.BannerTracker.Banner>  
Registered callbacks: 2  
0.	'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnConstructBannerClickedUI' -> 'BlockEntities.Implementations.AutocrafterTracker+Callbacks.OnConstructBannerClickedUI'   
1.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+BannerPlacementCallbacks.OnConstructBannerClickedUI'   


CallbackType: `OnHandleColonySelected`  
=======  
Method type: System.Action<NetworkUI.ButtonPressCallbackData, ColonyID>  
Result of the colony selection menu  
Registered callbacks: 2  
0.	'handle_builtin' -> 'NetworkUI.NetworkMenuManager+BuiltinCallbacks.OnHandleColonySelected'   
1.	'selected_outpost_parent' -> 'NetworkUI.NetworkMenuManager+BannerPlacementCallbacks.OnHandleColonySelected'   


CallbackType: `OnPlayerSelectedTypePopup`  
=======  
Method type: System.Action<Players.Player, ushort, Newtonsoft.Json.Linq.JObject>  
Result of the item selection menu  
Registered callbacks: 1  
0.	'traderule' -> 'NetworkUI.NetworkMenuManager+TradingRuleCallbacks.OnPlayerSelectedTypePopup'   


CallbackType: `OnPlayerSelectedTypePopupMulti`  
=======  
Method type: System.Action<Players.Player, System.Collections.Generic.List<ushort>, Newtonsoft.Json.Linq.JObject>  
Result of the multi item selection menu  
Registered callbacks: 1  
0.	'pipliz.server.overlaycounters.multiselect' -> 'NetworkUI.OverlayCounterManager+Callbacks.OnPlayerSelectedTypePopupMulti'   


CallbackType: `OnPlayerSelectedTypePopupCancel`  
=======  
Method type: System.Action<Players.Player, Newtonsoft.Json.Linq.JObject>  
No registered uses  


CallbackType: `OnSaveWorldMisc`  
=======  
Method type: System.Action<Newtonsoft.Json.Linq.JObject>  
called on autosave/quit, node is the json that'll be saved to {world}/world.json  
Registered callbacks: 4  
0.	'Blueprints.BlueprintTracker+Callbacks.OnSaveWorldMisc' -> 'Blueprints.BlueprintTracker+Callbacks.OnSaveWorldMisc'   
1.	'Notifications.NPCDeathNotification+Callbacks.OnSaveWorldMisc' -> 'Notifications.NPCDeathNotification+Callbacks.OnSaveWorldMisc'   
2.	'save_transports' -> 'Transport.TransportManager+Callbacks.OnSaveWorldMisc'   
3.	'savenpcid' -> 'NPC.NPCTracker+Callbacks.OnSaveWorldMisc'   


CallbackType: `OnLoadWorldMisc`  
=======  
Method type: System.Action<Newtonsoft.Json.Linq.JObject>  
called in AfterItemTypesDefined, node is the json that was saved aerlier ({world}/world.json)  
Registered callbacks: 6  
0.	'Blueprints.BlueprintTracker+Callbacks.OnLoadWorldMisc' -> 'Blueprints.BlueprintTracker+Callbacks.OnLoadWorldMisc'   
1.	'load_elevators' -> 'Transport.Elevator.ElevatorTransport+Callbacks.OnLoadWorldMisc'   
2.	'load_gliders' -> 'Transport.Glider+Callbacks.OnLoadWorldMisc'   
3.	'load_rails' -> 'Transport.Rail.RailTransport+Callbacks.OnLoadWorldMisc'   
4.	'loadnpcid' -> 'NPC.NPCTracker+Callbacks.OnLoadWorldMisc'   
5.	'Notifications.NPCDeathNotification+Callbacks.OnLoadWorldMisc' -> 'Notifications.NPCDeathNotification+Callbacks.OnLoadWorldMisc'   


CallbackType: `OnConstructColonyStartSettingsUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnConstructColonyStartSettingsUI'   


CallbackType: `OnConstructOutpostStartSettingsUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnConstructOutpostStartSettingsUI'   


CallbackType: `OnSendingStatisticsData`  
=======  
Method type: System.Action<Statistics.StatisticsManager>  
Called every time the client requests statistics data;  
include your type definitions every time, so the player can select them  
if the requested key matches your given key, include your desired data that corresponds with the requested time period  
Registered callbacks: 1  
0.	'base' -> 'Statistics.StatisticModCallbacks.OnSendingStatisticsData' index: -1  


CallbackType: `OnGatherStatisticsData`  
=======  
Method type: System.Action<Colony, int, int>  
Called with the colony and stats timeperiod index every time those stats were gathered  
First integer is the period index; 0, 1 or 2 by default  
Second integer is the time between updates for this periods (10, 40 or 240 seconds by default)  
No registered uses  


CallbackType: `OnGatherGroupStatisticsData`  
=======  
Method type: System.Action<ColonyGroup, int, int>  
Registered callbacks: 1  
0.	'gather_achievement_data' -> 'Statistics.AchievementGathering.OnGatherGroupStatisticsData' index: 1000  


CallbackType: `OnLoadModJSONFiles`  
=======  
Method type: System.Action<System.Collections.Generic.List<ModLoader.LoadModJSONFileContext>>  
Registered callbacks: 1  
0.	'pipliz.jsonfilescallbacks' -> 'ModLoader+LoadModJSONFileCallback.OnLoadModJSONFiles'   


CallbackType: `OnPlayerRemovedFromColony`  
=======  
Method type: System.Action<Players.Player, ColonyGroup>  
Registered callbacks: 3  
0.	'Notifications.NotificationCallbacks.OnPlayerRemovedFromColony' -> 'Notifications.NotificationCallbacks.OnPlayerRemovedFromColony'   
1.	'Notifications.NPCDeathMarkerManager.OnPlayerRemovedFromColony' -> 'Notifications.NPCDeathMarkerManager.OnPlayerRemovedFromColony'   
2.	'removeuimarkers' -> 'ColonyTracker+Callbacks.OnPlayerRemovedFromColony'   


CallbackType: `OnPlayerAddedToColony`  
=======  
Method type: System.Action<Players.Player, ColonyGroup>  
Registered callbacks: 3  
0.	'Notifications.NotificationCallbacks.OnPlayerAddedToColony' -> 'Notifications.NotificationCallbacks.OnPlayerAddedToColony'   
1.	'Notifications.NPCDeathMarkerManager.OnPlayerAddedToColony' -> 'Notifications.NPCDeathMarkerManager.OnPlayerAddedToColony'   
2.	'spread_cheatstate' -> 'ColonyGroup+CheatCallbacks.OnPlayerAddedToColony'   


CallbackType: `OnLoadingImages`  
=======  
Method type: System.Action<System.Collections.Generic.Dictionary<string, string>>  
No registered uses  


CallbackType: `OnRegisterUpgrades`  
=======  
Method type: System.Action<Assets.ColonyPointUpgrades.UpgradesManager>  
Registered callbacks: 1  
0.	'register_upgradeautoload' -> 'Assets.ColonyPointUpgrades.UpgradesManager+CallbacksA.OnRegisterUpgrades'   


CallbackType: `OnConstructPointUpgradesMenu`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu>  
Registered callbacks: 1  
0.	'Assets.UIGeneration.PointsUpgrades.OnConstructPointUpgradesMenu' -> 'Assets.UIGeneration.PointsUpgrades.OnConstructPointUpgradesMenu'   


CallbackType: `OnConstructColonyManageJobsUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, Colony>  
Registered callbacks: 1  
0.	'Assets.UIGeneration.ColonyManageJobs.OnConstructColonyManageJobsUI' -> 'Assets.UIGeneration.ColonyManageJobs.OnConstructColonyManageJobsUI'   


CallbackType: `OnPlayerEditedNetworkSliderInt`  
=======  
Method type: System.Action<NetworkUI.SliderIntEditCallbackData>  
Registered callbacks: 2  
0.	'Assets.UIGeneration.ColonyManageJobs.OnPlayerEditedNetworkSliderInt' -> 'Assets.UIGeneration.ColonyManageJobs.OnPlayerEditedNetworkSliderInt'   
1.	'NetworkUI.NetworkMenuManager+ToolshopCallbacks.OnPlayerEditedNetworkSliderInt' -> 'NetworkUI.NetworkMenuManager+ToolshopCallbacks.OnPlayerEditedNetworkSliderInt'   


CallbackType: `RegisterNotificationLoaders`  
=======  
Method type: System.Action<System.Collections.Generic.Dictionary<string, Notifications.NotificationManager.NotificationLoader>>  
Registered callbacks: 4  
0.	'Notifications.NPCDeathNotification+Callbacks.RegisterNotificationLoaders' -> 'Notifications.NPCDeathNotification+Callbacks.RegisterNotificationLoaders'   
1.	'Notifications.ResearchCompletedNotification+Callbacks.RegisterNotificationLoaders' -> 'Notifications.ResearchCompletedNotification+Callbacks.RegisterNotificationLoaders'   
2.	'Notifications.SiegeModeNotification+Callbacks.RegisterNotificationLoaders' -> 'Notifications.SiegeModeNotification+Callbacks.RegisterNotificationLoaders'   
3.	'Notifications.TextNotification+Callbacks.RegisterNotificationLoaders' -> 'Notifications.TextNotification+Callbacks.RegisterNotificationLoaders'   


CallbackType: `OnRecalculateThreatLevel`  
=======  
Method type: System.Action<ColonyGroup>  
Registered callbacks: 6  
0.	'add_failsafe' -> 'BlockEntities.Implementations.Failsafes+Callbacks.OnRecalculateThreatLevel'   
1.	'add_lockboxes' -> 'BlockEntities.Implementations.Lockboxes+Callbacks.OnRecalculateThreatLevel'   
2.	'add_monsterstatues' -> 'BlockEntities.Implementations.MonsterStatues+Callbacks.OnRecalculateThreatLevel'   
3.	'add_sanctified_lockboxes' -> 'BlockEntities.Implementations.SanctifiedLockboxes+Callbacks.OnRecalculateThreatLevel'   
4.	'check_npc_count' -> 'Difficulty.ColonyThreatLevel+Callbacks2.OnRecalculateThreatLevel'   
5.	'defaultThreat' -> 'Difficulty.ColonyThreatLevel+Callbacks.OnRecalculateThreatLevel'   


CallbackType: `OnGatherMonsterParsers`  
=======  
Method type: System.Action<System.Collections.Generic.Dictionary<NPC.NPCType, System.Func<Saving.WorldDB.MonsterData, Monsters.IMonster>>>  
Registered callbacks: 1  
0.	'Monsters.MonsterSpawner+Callbacks.OnGatherMonsterParsers' -> 'Monsters.MonsterSpawner+Callbacks.OnGatherMonsterParsers'   


CallbackType: `OnRecalculatePointCapacity`  
=======  
Method type: System.Action<ColonyGroup.PointCapacityData>  
Registered callbacks: 1  
0.	'add_lockboxes' -> 'BlockEntities.Implementations.Lockboxes+Callbacks.OnRecalculatePointCapacity'   


CallbackType: `OnRecalculateSanctifiedPointCapacity`  
=======  
Method type: System.Action<ColonyGroup.SanctifiedPointCapacityData>  
Registered callbacks: 2  
0.	'add_failsafe' -> 'BlockEntities.Implementations.Failsafes+Callbacks.OnRecalculateSanctifiedPointCapacity'   
1.	'add_sanctified_lockboxes' -> 'BlockEntities.Implementations.SanctifiedLockboxes+Callbacks.OnRecalculateSanctifiedPointCapacity'   


CallbackType: `OnInventorySelectionChanged`  
=======  
Method type: System.Action<Inventory.InventorySelectionContext>  
Registered callbacks: 2  
0.	'BlockEntities.Implementations.Astrolabe+Callbacks.OnInventorySelectionChanged' -> 'BlockEntities.Implementations.Astrolabe+Callbacks.OnInventorySelectionChanged'   
1.	'NetworkUI.NetworkMenuManager+NPCMenuCallbacks.OnInventorySelectionChanged' -> 'NetworkUI.NetworkMenuManager+NPCMenuCallbacks.OnInventorySelectionChanged'   


CallbackType: `OnPlayerAttachedToVehicle`  
=======  
Method type: System.Action<Players.Player, MeshedObjects.MeshedVehicleDescription>  
Registered callbacks: 1  
0.	'Transport.TransportManager+Callbacks.OnPlayerAttachedToVehicle' -> 'Transport.TransportManager+Callbacks.OnPlayerAttachedToVehicle'   


CallbackType: `OnPlayerDetachedFromVehicle`  
=======  
Method type: System.Action<Players.Player, MeshedObjects.MeshedVehicleDescription>  
Registered callbacks: 1  
0.	'Transport.TransportManager+Callbacks.OnPlayerDetachedFromVehicle' -> 'Transport.TransportManager+Callbacks.OnPlayerDetachedFromVehicle'   


CallbackType: `OnGenerateNPCName`  
=======  
Method type: System.Action<NPC.NPCNames.NamingContext>  
Registered callbacks: 1  
0.	'NPC.NPCNames.OnGenerateNPCName' -> 'NPC.NPCNames.OnGenerateNPCName'   


CallbackType: `OnCreatedAreaJob`  
=======  
Method type: System.Action<Players.Player, Jobs.IAreaJob>  
Registered callbacks: 1  
0.	'Jobs.Implementations.Construction.DevInstantConstruction.OnCreatedAreaJob' -> 'Jobs.Implementations.Construction.DevInstantConstruction.OnCreatedAreaJob'   


CallbackType: `OnGatherAvailableBlueprints`  
=======  
Method type: System.Action<Players.Player, Colony, System.Collections.Generic.List<Blueprints.Blueprint>>  
Registered callbacks: 1  
0.	'Blueprints.BlueprintTracker+Callbacks.OnGatherAvailableBlueprints' -> 'Blueprints.BlueprintTracker+Callbacks.OnGatherAvailableBlueprints'   


CallbackType: `OnQuestCompleted`  
=======  
Method type: System.Action<Quests.QuestManager.QuestCallbackData>  
No registered uses  


CallbackType: `OnQuestLoaded`  
=======  
Method type: System.Action<Quests.QuestManager.QuestCallbackData>  
No registered uses  


CallbackType: `OnConstructInventoryManageColonyUI`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu, System.ValueTuple<NetworkUI.Items.Table, NetworkUI.Items.Table>>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnConstructInventoryManageColonyUI'   


CallbackType: `OnConstructColonyRecruitmentUI`  
=======  
Method type: System.Action<Players.Player, Newtonsoft.Json.Linq.JObject, System.Collections.Generic.List<NetworkUI.IItem>>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnConstructColonyRecruitmentUI'   


CallbackType: `OnConstructColonySettingsUI`  
=======  
Method type: System.Action<Players.Player, Newtonsoft.Json.Linq.JObject, System.Collections.Generic.List<NetworkUI.IItem>>  
Registered callbacks: 2  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnConstructColonySettingsUI'   
1.	'pipliz.server.overlaycounters.menu' -> 'NetworkUI.OverlayCounterManager+Callbacks.OnConstructColonySettingsUI'   


CallbackType: `OnConstructPlayerList`  
=======  
Method type: System.Action<Players.Player, NetworkUI.NetworkMenu>  
Registered callbacks: 1  
0.	'pipliz.buildbase' -> 'NetworkUI.NetworkMenuManager+CallbackConsumers.OnConstructPlayerList'   


CallbackType: `OnRegisteringEntityManagers`  
=======  
Method type: System.Action<BlockEntities.IEntityManager[]>  
No registered uses  


