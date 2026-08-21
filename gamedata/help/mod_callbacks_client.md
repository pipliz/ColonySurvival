CallbackType: `OnEndReadOnly`  
=======  
Method type: System.Action  
Registered callbacks: 3  
0.	'ChunkManager+Callbacks.OnEndReadOnly' -> 'ChunkManager+Callbacks.OnEndReadOnly'   
1.	'Lighting.LightingManager+Callbacks.OnEndReadOnly' -> 'Lighting.LightingManager+Callbacks.OnEndReadOnly'   
2.	'WorldRenderer+Callbacks.OnEndReadOnly' -> 'WorldRenderer+Callbacks.OnEndReadOnly'   


CallbackType: `OnStartReadOnly`  
=======  
Method type: System.Action  
Registered callbacks: 5  
0.	'start_instanced_render' -> 'WorldRenderer+Callbacks.OnStartReadOnly' index: -1000  
1.	'Lighting.LightingManager+Callbacks.OnStartReadOnly' -> 'Lighting.LightingManager+Callbacks.OnStartReadOnly'   
2.	'Meshing.ChunkMesherLightmap+Callbacks.OnStartReadOnly' -> 'Meshing.ChunkMesherLightmap+Callbacks.OnStartReadOnly'   
3.	'mesher.startjobs' -> 'ChunkManager+Callbacks.OnStartReadOnly' index: 100  
4.	'finish_instanced_render' -> 'WorldRenderer+Callbacks.FinishRenderLater' index: 1000  


CallbackType: `OnStartMainthread`  
=======  
Method type: System.Action  
No registered uses  


CallbackType: `OnEndMainthread`  
=======  
Method type: System.Action  
No registered uses  


CallbackType: `OnBlockChange`  
=======  
Method type: System.Action<Callbacks.BlockChangeData>  
Registered callbacks: 2  
0.	'ColonySurvival.Client.Physics.ColliderGeneration+Callbacks.OnBlockChange' -> 'ColonySurvival.Client.Physics.ColliderGeneration+Callbacks.OnBlockChange'   
1.	'Lighting.LightingManager+Callbacks.OnBlockChange' -> 'Lighting.LightingManager+Callbacks.OnBlockChange'   


CallbackType: `AfterModsLoaded`  
=======  
Method type: System.Action<System.Collections.Generic.List<ModLoader.ModDescription>>  
Called after parsing all modinfo files  
No registered uses  


CallbackType: `OnItemTypeRegistered`  
=======  
Method type: System.Action<ItemTypes.ItemType>  
Called once for each type that is being registered to ItemTypes  
Registered callbacks: 2  
0.	'LargeBoundsAwarenessWrapper+Callbacks.OnItemTypeRegistered' -> 'LargeBoundsAwarenessWrapper+Callbacks.OnItemTypeRegistered'   
1.	'pipliz.client.processitemtype' -> 'ItemTypesClient+Callbacks.OnItemTypeRegistered'   


CallbackType: `OnUpdateStart`  
=======  
Method type: System.Action  
Called early on in unity's Update method  
Registered callbacks: 4  
0.	'threadphase.startmainthreadonly' -> 'ThreadPhase+Callbacks.OnUpdateStart' index: -1000000  
1.	'pipliz.server.setsecondsthisframe' -> 'Pipliz.Time.SetThisFrame' index: -1000  
2.	'mainthreadactions' -> 'ThreadManager+Callbacks.OnUpdateStart' index: -100  
3.	'ServerTime+ClientCallbacks.OnUpdateStart' -> 'ServerTime+ClientCallbacks.OnUpdateStart'   


CallbackType: `OnUpdate`  
=======  
Method type: System.Action  
Registered callbacks: 25  
0.	'network.receivepackets' -> 'ClientWrapper+Callbacks.OnUpdate' index: -1000  
1.	'process-unity-inputs' -> 'InputBehaviour+ModCallbacks.OnUpdate' index: -500  
2.	'controlledmeshes.update' -> 'Client.ControlledMeshes.ControlledMeshes+Callbacks.OnUpdate'   
		 Child @ 3 : 'playerbody.updatestatic'  
3.	'playerbody.updatestatic' -> 'PlayerBody+Callbacks.OnUpdate' index: -300  
4.	'world.checkorigin' -> 'World+Callbacks.OnUpdate' index: -200  
5.	'Assets.Scripts.General.Physics.CubeBit+Callbacks.OnUpdate' -> 'Assets.Scripts.General.Physics.CubeBit+Callbacks.OnUpdate'   
6.	'Assets.Scripts.UI.ScrollrectAutoscroll.OnUpdate' -> 'Assets.Scripts.UI.ScrollrectAutoscroll.OnUpdate'   
7.	'AudioManager+Callbacks.OnUpdate' -> 'AudioManager+Callbacks.OnUpdate'   
8.	'update_receiveratetracker' -> 'ChunkRequests+Callbacks2.OnUpdate'   
9.	'ChunkManager+Callbacks.OnUpdate' -> 'ChunkManager+Callbacks.OnUpdate'   
		 Parent @ 8 : 'update_receiveratetracker'  
10.	'ChunkRequests+Callbacks.OnUpdate' -> 'ChunkRequests+Callbacks.OnUpdate'   
11.	'colonyclient.Assets.Scripts.UI.Ingame.UIGeneration.WorldMarker+Callbacks.OnUpdate' -> 'colonyclient.Assets.Scripts.UI.Ingame.UIGeneration.WorldMarker+Callbacks.OnUpdate'   
12.	'ColonySurvival.Client.UI.Ingame.InventoryBar+Callbacks.OnUpdate' -> 'ColonySurvival.Client.UI.Ingame.InventoryBar+Callbacks.OnUpdate'   
13.	'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnUpdate' -> 'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnUpdate'   
14.	'ColonySurvival.Client.UI.Ingame.TopDownOverlay+Callbacks.OnUpdate' -> 'ColonySurvival.Client.UI.Ingame.TopDownOverlay+Callbacks.OnUpdate'   
15.	'CursorWrapper+Callbacks.OnUpdate' -> 'CursorWrapper+Callbacks.OnUpdate'   
16.	'heartbeattracker.update' -> 'CongestionTracker+Callbacks.OnUpdate'   
17.	'NetworkUI.SignText+Callbacks.OnUpdate' -> 'NetworkUI.SignText+Callbacks.OnUpdate'   
18.	'pipliz.client.ragdollupdate' -> 'ColonySurvival.Client.RagdollTracker.Update'   
19.	'Pipliz.Tools.HoverCheck.OnUpdate' -> 'Pipliz.Tools.HoverCheck.OnUpdate'   
20.	'pipliz.updateparticles' -> 'ParticleManager+Callbacks.OnUpdate'   
21.	'SteamManager+Callbacks.OnUpdate' -> 'SteamManager+Callbacks.OnUpdate'   
22.	'WorldContext+Callbacks.OnUpdate' -> 'WorldContext+Callbacks.OnUpdate'   
23.	'WorldRenderer+Callbacks.OnUpdate' -> 'WorldRenderer+Callbacks.OnUpdate'   
24.	'update_networkbody' -> 'NetworkBody.AllInterpolate' index: 100  


CallbackType: `OnUpdateEnd`  
=======  
Method type: System.Action  
Registered callbacks: 7  
0.	'pipliz.culling' -> 'Culling.CullingManager+Callbacks.OnUpdateEnd' index: -100  
1.	'Meshing.MeshTrackerSimple+Callbacks.OnUpdateEnd' -> 'Meshing.MeshTrackerSimple+Callbacks.OnUpdateEnd'   
		 Parent @ 0 : 'pipliz.culling'  
2.	'NPC.NPCManager+Callbacks.OnUpdateEnd' -> 'NPC.NPCManager+Callbacks.OnUpdateEnd'   
3.	'update_selected_type' -> 'Inventory+Callbacks.OnUpdateEnd'   
4.	'mainthreadactions' -> 'ThreadManager+Callbacks.OnUpdateEnd' index: 100  
5.	'network.flushpackets' -> 'ClientWrapper+Callbacks.OnUpdateEnd' index: 1000  
6.	'threadphase.endmainthreadonly' -> 'ThreadPhase+Callbacks.OnUpdateEnd' index: 1000000  


CallbackType: `OnLateUpdate`  
=======  
Method type: System.Action  
Called inside unity's LateUpdate method  
Registered callbacks: 3  
0.	'flush_controlledmesh_input' -> 'Client.ControlledMeshes.ControlledMeshes.UpdateInput'   
1.	'PlayerCamera+Callbacks.OnLateUpdate' -> 'PlayerCamera+Callbacks.OnLateUpdate'   
2.	'render_areas' -> 'Pipliz.AI.Areas.AreaTracker+Callbacks.OnLateUpdate'   


CallbackType: `AfterItemTypesDefined`  
=======  
Method type: System.Action  
Registered callbacks: 7  
0.	'lua.execute_post_type_bootstraps' -> 'LuaScripting.LuaManager+PostTypeCallbacks.AfterItemTypesDefined' index: -1000000  
1.	'create_lut_texture' -> 'Rendering.CreateLUTTexture'   
2.	'GameAction+Callbacks.AfterItemTypesDefined' -> 'GameAction+Callbacks.AfterItemTypesDefined'   
3.	'parse_railtypes' -> 'Rail.RailManager.AfterItemTypesDefined'   
4.	'register_onclientclicklua' -> 'OnClickLua.AfterItemTypesDefined'   
		 Parent @ 0 : 'lua.execute_post_type_bootstraps'  
5.	'register_smart_placement' -> 'SmartPlacement.AfterItemTypesDefined'   
6.	'register_smart_placement_lua' -> 'SmartPlacementLua.AfterItemTypesDefined'   
		 Parent @ 0 : 'lua.execute_post_type_bootstraps'  


CallbackType: `OnQuit`  
=======  
Method type: System.Action  
Called in the quit method queue  
Registered callbacks: 24  
0.	'pipliz.shared.waitforasyncquitsearly' -> 'Pipliz.Application.WaitForQuits' index: -1000  
1.	'Assets.Scripts.General.Physics.CubeBit+Callbacks.OnQuit' -> 'Assets.Scripts.General.Physics.CubeBit+Callbacks.OnQuit'   
2.	'ChunkManager+Callbacks.OnQuit' -> 'ChunkManager+Callbacks.OnQuit'   
3.	'ChunkRequests+Callbacks.OnQuit' -> 'ChunkRequests+Callbacks.OnQuit'   
4.	'Clear temp file cache' -> 'GameManager.ClearCache'   
5.	'ColonySurvival.Client.Physics.ColliderGeneration+Callbacks.OnQuit' -> 'ColonySurvival.Client.Physics.ColliderGeneration+Callbacks.OnQuit'   
6.	'ColonySurvival.Client.UI.Ingame.OverlayCounterClientState+Callbacks.OnQuit' -> 'ColonySurvival.Client.UI.Ingame.OverlayCounterClientState+Callbacks.OnQuit'   
7.	'Culling.CullingManager+Callbacks.OnQuit' -> 'Culling.CullingManager+Callbacks.OnQuit'   
8.	'dispose gamemanager' -> 'GameManager.Dispose'   
9.	'FileTable+Callbacks.OnQuit' -> 'FileTable+Callbacks.OnQuit'   
10.	'ItemTypesClient+Callbacks.OnQuit' -> 'ItemTypesClient+Callbacks.OnQuit'   
11.	'LargeBoundsAwarenessWrapper+Callbacks.OnQuit' -> 'LargeBoundsAwarenessWrapper+Callbacks.OnQuit'   
12.	'NPC.NPCManagerInternal+NPCUpdating+Callbacks.OnQuit' -> 'NPC.NPCManagerInternal+NPCUpdating+Callbacks.OnQuit'   
13.	'Pipliz.Tools.HoverCheck.OnQuit' -> 'Pipliz.Tools.HoverCheck.OnQuit'   
14.	'Rendering+Callbacks.OnQuit' -> 'Rendering+Callbacks.OnQuit'   
15.	'VoxelPhysics+Callbacks.OnQuit' -> 'VoxelPhysics+Callbacks.OnQuit'   
16.	'WaterMeshTracker+ModCallbacks.OnQuit' -> 'WaterMeshTracker+ModCallbacks.OnQuit'   
17.	'WorldRenderer+Callbacks.OnQuit' -> 'WorldRenderer+Callbacks.OnQuit'   
18.	'steamnetworking.close' -> 'Pipliz.Networking.SteamNetworking+ModRegistering.OnQuit' index: 1  
19.	'pipliz.jointhreads' -> 'Pipliz.Threading.ThreadSafeQuitWrapper.JoinThread' index: 500  
20.	'pipliz.shared.waitforasyncquitslate' -> 'Pipliz.Application.WaitForQuits' index: 1000  
21.	'dispose colorbuffers' -> 'PipBurst+ColorBuffer.DisposePool' index: 10000  
22.	'LZ4DecoderFreeing' -> 'Pipliz.LZ4.LZ4Codec+Callbacks.OnQuit' index: 10000  
23.	'modloader.dispose' -> 'ModLoader+ModloaderCallbacks.OnQuit' index: 1000000  


CallbackType: `OnGatherExtraAreas`  
=======  
Method type: System.Action<System.Collections.Generic.List<Pipliz.AI.Areas.Area>>  
Registered callbacks: 1  
0.	'Pipliz.Tools.ToolJobs+Callbacks.OnGatherExtraAreas' -> 'Pipliz.Tools.ToolJobs+Callbacks.OnGatherExtraAreas'   


CallbackType: `OnConnectionScreenDone`  
=======  
Method type: System.Action  
No registered uses  


CallbackType: `OnMainMenuLoaded`  
=======  
Method type: System.Action  
Registered callbacks: 45  
0.	'AnimationManager+Callbacks.OnMainMenuLoaded' -> 'AnimationManager+Callbacks.OnMainMenuLoaded'   
1.	'AnimationManagerBlock+Callbacks.OnMainMenuLoaded' -> 'AnimationManagerBlock+Callbacks.OnMainMenuLoaded'   
2.	'Assets.Scripts.General.Physics.CubeBit+Callbacks.OnMainMenuLoaded' -> 'Assets.Scripts.General.Physics.CubeBit+Callbacks.OnMainMenuLoaded'   
3.	'ChunkManager+Callbacks.OnMainMenuLoaded' -> 'ChunkManager+Callbacks.OnMainMenuLoaded'   
4.	'ChunkRequests+Callbacks.OnMainMenuLoaded' -> 'ChunkRequests+Callbacks.OnMainMenuLoaded'   
5.	'clear_label_cache' -> 'colonyclient.Assets.Scripts.UI.Ingame.UIGeneration.UILabel.ClearCache'   
6.	'clear_lut_texture' -> 'Rendering.ClearLUTTexture'   
7.	'clear_uiimage_cache' -> 'colonyclient.Assets.Scripts.UI.Ingame.UIGeneration.UIImage.ClearCache'   
8.	'clear_worldmarker_cache' -> 'colonyclient.Assets.Scripts.UI.Ingame.UIGeneration.WorldMarker+Callbacks.OnMainMenuLoaded'   
9.	'collidergen.reset' -> 'ColonySurvival.Client.Physics.ColliderGeneration.Reset'   
10.	'ColonySurvival.Client.Physics.ColliderGeneration+Callbacks.OnMainMenuLoaded' -> 'ColonySurvival.Client.Physics.ColliderGeneration+Callbacks.OnMainMenuLoaded'   
11.	'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnMainMenuLoaded' -> 'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnMainMenuLoaded'   
12.	'ColonySurvival.Client.UI.Ingame.OverlayCounterClientState+Callbacks.OnMainMenuLoaded' -> 'ColonySurvival.Client.UI.Ingame.OverlayCounterClientState+Callbacks.OnMainMenuLoaded'   
13.	'Culling.CullingManager+Callbacks.OnMainMenuLoaded' -> 'Culling.CullingManager+Callbacks.OnMainMenuLoaded'   
14.	'GameAction+Callbacks.OnMainMenuLoaded' -> 'GameAction+Callbacks.OnMainMenuLoaded'   
15.	'heartbeattracker.reset' -> 'CongestionTracker+Callbacks.OnMainMenuLoaded'   
16.	'LargeBoundsAwarenessWrapper+Callbacks.OnMainMenuLoaded' -> 'LargeBoundsAwarenessWrapper+Callbacks.OnMainMenuLoaded'   
17.	'NPC.NPCBase+Callbacks.OnMainMenuLoaded' -> 'NPC.NPCBase+Callbacks.OnMainMenuLoaded'   
18.	'NPC.NPCManagerInternal+NPCUpdating+Callbacks.OnMainMenuLoaded' -> 'NPC.NPCManagerInternal+NPCUpdating+Callbacks.OnMainMenuLoaded'   
19.	'NPC.NPCNames+Callbacks.OnMainMenuLoaded' -> 'NPC.NPCNames+Callbacks.OnMainMenuLoaded'   
20.	'ParticleManager2+Callbacks.OnMainMenuLoaded' -> 'ParticleManager2+Callbacks.OnMainMenuLoaded'   
21.	'pipliz.client.clearactionqueue' -> 'Shared.ItemManagementAction+ActionQueue.Clear'   
22.	'pipliz.client.clearaudiomanager' -> 'AudioManager.Clear'   
23.	'pipliz.client.clearnpcmaterial' -> 'NPCMaterial.ClearNPCMaterial'   
24.	'pipliz.client.clearterrainmaterial' -> 'TerrainMaterial.ClearTerrainMaterial'   
25.	'pipliz.client.gamemanagerclear' -> 'GameManager.OnMainMenuLoaded'   
26.	'pipliz.client.refreshlocale' -> 'GameManager.RefreshLocale'   
27.	'pipliz.client.resetareatracker' -> 'Pipliz.AI.Areas.AreaTracker+FilledAreaRenderer.Reset'   
28.	'pipliz.client.resetbanners' -> 'Client.Rendering.BannerRenderer.Reset'   
29.	'pipliz.client.resetinventory' -> 'Inventory.Release'   
30.	'pipliz.client.resetprojectiles' -> 'Projectile.Clear'   
31.	'pipliz.client.servercontrolledmeshes.resetsettingstable' -> 'Client.ControlledMeshes.MeshTypeSettings.OnReloaded'   
32.	'pipliz.resetparticlemanager' -> 'ParticleManager+Callbacks.OnMainMenuLoaded'   
33.	'Pipliz.Tools.Blueprints.BlueprintUtilities+Callbacks.OnMainMenuLoaded' -> 'Pipliz.Tools.Blueprints.BlueprintUtilities+Callbacks.OnMainMenuLoaded'   
34.	'Pipliz.Tools.HoverCheck.OnMainMenuLoaded' -> 'Pipliz.Tools.HoverCheck.OnMainMenuLoaded'   
35.	'reset_controlledmeshes' -> 'Client.ControlledMeshes.ControlledMeshes.Reset'   
36.	'resetcrafting' -> 'Crafting.Reset'   
37.	'ServerTime+ClientCallbacks.OnMainMenuLoaded' -> 'ServerTime+ClientCallbacks.OnMainMenuLoaded'   
38.	'signs.clear' -> 'NetworkUI.SignText+Callbacks.OnMainMenuLoaded'   
39.	'SteamManager+StatsManager+Callbacks.OnMainMenuLoaded' -> 'SteamManager+StatsManager+Callbacks.OnMainMenuLoaded'   
40.	'ToolActionManager+Implementations+OnHit+Callbacks.OnMainMenuLoaded' -> 'ToolActionManager+Implementations+OnHit+Callbacks.OnMainMenuLoaded'   
41.	'VoxelPhysics+Callbacks.OnMainMenuLoaded' -> 'VoxelPhysics+Callbacks.OnMainMenuLoaded'   
42.	'WaterMeshTracker+ModCallbacks.OnMainMenuLoaded' -> 'WaterMeshTracker+ModCallbacks.OnMainMenuLoaded'   
43.	'WorldContext+Callbacks.OnMainMenuLoaded' -> 'WorldContext+Callbacks.OnMainMenuLoaded'   
44.	'WorldRenderer+Callbacks.OnMainMenuLoaded' -> 'WorldRenderer+Callbacks.OnMainMenuLoaded'   


CallbackType: `OnOriginShift`  
=======  
Method type: System.Action<Pipliz.Vector3Int>  
Registered callbacks: 11  
0.	'Assets.Scripts.General.Physics.CubeBit+Callbacks.OnOriginShift' -> 'Assets.Scripts.General.Physics.CubeBit+Callbacks.OnOriginShift'   
1.	'AudioSourceHelper+Callbacks.OnOriginShift' -> 'AudioSourceHelper+Callbacks.OnOriginShift'   
2.	'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnOriginShift' -> 'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnOriginShift'   
3.	'NetworkUI.SignText+Callbacks.OnOriginShift' -> 'NetworkUI.SignText+Callbacks.OnOriginShift'   
4.	'pipliz.client.collidergenerationmove' -> 'ColonySurvival.Client.Physics.ColliderGeneration.OnShift'   
5.	'pipliz.client.npcbodyshift' -> 'NPC.NPCBase+Statics.OnOriginShift'   
6.	'pipliz.client.playeroriginshift' -> 'PlayerBody+Callbacks.OnOriginShift'   
7.	'pipliz.client.projectilesshift' -> 'Projectile.OnShift'   
8.	'pipliz.client.ragdollmove' -> 'ColonySurvival.Client.RagdollTracker.OnShift'   
9.	'pipliz.shiftparticles' -> 'ParticleManager+Callbacks.OnOriginShift'   
10.	'Pipliz.UI.DevMenu.ShaderFeatures+CinemachineTracker+Callbacks.OnOriginShift' -> 'Pipliz.UI.DevMenu.ShaderFeatures+CinemachineTracker+Callbacks.OnOriginShift'   


CallbackType: `OnPlayerPositionReceived`  
=======  
Method type: System.Action<UnityEngine.Vector3>  
Registered callbacks: 2  
0.	'ChunkRequests+Callbacks.OnPlayerPositionReceived' -> 'ChunkRequests+Callbacks.OnPlayerPositionReceived'   
1.	'PlayerCamera+Callbacks.OnPlayerPositionReceived' -> 'PlayerCamera+Callbacks.OnPlayerPositionReceived'   


CallbackType: `OnNetworkButtonPress`  
=======  
Method type: System.Action<string, Newtonsoft.Json.Linq.JToken>  
Registered callbacks: 3  
0.	'ColonySurvival.Client.UI.Ingame.InventoryQuests+Callbacks.OnNetworkButtonPress' -> 'ColonySurvival.Client.UI.Ingame.InventoryQuests+Callbacks.OnNetworkButtonPress'   
1.	'ColonySurvival.Client.UI.Ingame.TopDownOverlay+Callbacks.OnNetworkButtonPress' -> 'ColonySurvival.Client.UI.Ingame.TopDownOverlay+Callbacks.OnNetworkButtonPress'   
2.	'InventoryNotifications+Callbacks.OnNetworkButtonPress' -> 'InventoryNotifications+Callbacks.OnNetworkButtonPress'   


CallbackType: `OnPlayerActivated`  
=======  
Method type: System.Action  
Registered callbacks: 1  
0.	'Assets.Scripts.SkySettingsManager+Callbacks.OnPlayerActivated' -> 'Assets.Scripts.SkySettingsManager+Callbacks.OnPlayerActivated'   


CallbackType: `OnFileLoaded`  
=======  
Method type: System.Action<FileDatabase.Entry, Pipliz.ByteBuilder>  
Registered callbacks: 1  
0.	'ClientWrapper+Callbacks.OnFileLoaded' -> 'ClientWrapper+Callbacks.OnFileLoaded'   


CallbackType: `OnPlayerMoved`  
=======  
Method type: System.Action<UnityEngine.Vector3, UnityEngine.Vector3>  
Registered callbacks: 2  
0.	'NPC.NPCNames+Callbacks.OnPlayerMoved' -> 'NPC.NPCNames+Callbacks.OnPlayerMoved'   
1.	'PlayerCamera+Callbacks.OnPlayerMoved' -> 'PlayerCamera+Callbacks.OnPlayerMoved'   


