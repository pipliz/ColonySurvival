CallbackType: `AfterModsLoaded`  
=======  
Method type: System.Action<System.Collections.Generic.List<ModLoader.ModDescription>>  
Called after parsing all modinfo files  
No registered uses  


CallbackType: `OnItemTypeRegistered`  
=======  
Method type: System.Action<ItemTypes.ItemType>  
Called once for each type that is being registered to ItemTypes  
Registered callbacks: 1  
0.	'pipliz.client.processitemtype' -> 'ItemTypesClient+Callbacks.OnItemTypeRegistered'   


CallbackType: `OnUpdateStart`  
=======  
Method type: System.Action  
Called early on in unity's Update method  
Registered callbacks: 3  
0.	'threadphase.startmainthreadonly' -> 'ThreadPhase+Callbacks.OnUpdateStart' index: -1000000  
1.	'pipliz.server.setsecondsthisframe' -> 'Pipliz.Time.SetThisFrame' index: -1000  
2.	'mainthreadactions' -> 'ThreadManager+Callbacks.OnUpdateStart' index: -100  


CallbackType: `OnUpdate`  
=======  
Method type: System.Action  
Registered callbacks: 23  
0.	'network.receivepackets' -> 'ClientWrapper+Callbacks.OnUpdate' index: -1000  
1.	'process-unity-inputs' -> 'InputBehaviour+ModCallbacks.OnUpdate' index: -500  
2.	'controlledmeshes.update' -> 'Client.ControlledMeshes.ControlledMeshes+Callbacks.OnUpdate'   
		 Child @ 3 : 'controller.updatestatic'  
3.	'controller.updatestatic' -> 'PlayerController+Callbacks.OnUpdate' index: -350  
4.	'playerbody.updatestatic' -> 'PlayerBody+Callbacks.OnUpdate' index: -300  
5.	'controller.updateoffset' -> 'PlayerController+Callbacks2.OnUpdate' index: -250  
6.	'world.checkorigin' -> 'World+Callbacks.OnUpdate' index: -200  
7.	'Assets.Scripts.UI.ScrollrectAutoscroll.OnUpdate' -> 'Assets.Scripts.UI.ScrollrectAutoscroll.OnUpdate'   
8.	'AudioManager+Callbacks.OnUpdate' -> 'AudioManager+Callbacks.OnUpdate'   
9.	'update_receiveratetracker' -> 'ChunkRequests+Callbacks2.OnUpdate'   
10.	'ChunkManager+Callbacks.OnUpdate' -> 'ChunkManager+Callbacks.OnUpdate'   
		 Parent @ 9 : 'update_receiveratetracker'  
11.	'ChunkRequests+Callbacks.OnUpdate' -> 'ChunkRequests+Callbacks.OnUpdate'   
12.	'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnUpdate' -> 'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnUpdate'   
13.	'CursorWrapper+Callbacks.OnUpdate' -> 'CursorWrapper+Callbacks.OnUpdate'   
14.	'heartbeattracker.update' -> 'CongestionTracker+Callbacks.OnUpdate'   
15.	'NPC.NPCManager+Callbacks.OnUpdate' -> 'NPC.NPCManager+Callbacks.OnUpdate'   
16.	'pipliz.client.ragdollupdate' -> 'ColonySurvival.Client.RagdollTracker.Update'   
17.	'Pipliz.Tools.HoverCheck.OnUpdate' -> 'Pipliz.Tools.HoverCheck.OnUpdate'   
18.	'pipliz.updateparticles' -> 'ParticleManager.UpdateParticles'   
19.	'SteamManager+Callbacks.OnUpdate' -> 'SteamManager+Callbacks.OnUpdate'   
20.	'WorldContext+Callbacks.OnUpdate' -> 'WorldContext+Callbacks.OnUpdate'   
21.	'WorldRenderer+Callbacks.OnUpdate' -> 'WorldRenderer+Callbacks.OnUpdate'   
22.	'update_networkbody' -> 'NetworkBody.AllInterpolate' index: 100  


CallbackType: `OnUpdateEnd`  
=======  
Method type: System.Action  
Registered callbacks: 4  
0.	'update_selected_type' -> 'Inventory+Callbacks.OnUpdateEnd'   
1.	'mainthreadactions' -> 'ThreadManager+Callbacks.OnUpdateEnd' index: 100  
2.	'network.flushpackets' -> 'ClientWrapper+Callbacks.OnUpdateEnd' index: 1000  
3.	'threadphase.endmainthreadonly' -> 'ThreadPhase+Callbacks.OnUpdateEnd' index: 1000000  


CallbackType: `OnLateUpdate`  
=======  
Method type: System.Action  
Called inside unity's LateUpdate method  
Registered callbacks: 2  
0.	'flush_controlledmesh_input' -> 'Client.ControlledMeshes.ControlledMeshes.UpdateInput'   
1.	'render_areas' -> 'Pipliz.AI.Areas.AreaTracker+AreasRenderer.Render'   


CallbackType: `AfterItemTypesDefined`  
=======  
Method type: System.Action  
Registered callbacks: 4  
0.	'create_lut_texture' -> 'Rendering.CreateLUTTexture'   
1.	'GameAction+Callbacks.AfterItemTypesDefined' -> 'GameAction+Callbacks.AfterItemTypesDefined'   
2.	'InventoryStatistics+Callbacks.AfterItemTypesDefined' -> 'InventoryStatistics+Callbacks.AfterItemTypesDefined'   
3.	'register_smart_placement' -> 'SmartPlacement.AfterItemTypesDefined'   


CallbackType: `OnQuit`  
=======  
Method type: System.Action  
Called in the quit method queue  
Registered callbacks: 17  
0.	'pipliz.shared.waitforasyncquitsearly' -> 'Pipliz.Application.WaitForQuits' index: -1000  
1.	'ChunkManager+Callbacks.OnQuit' -> 'ChunkManager+Callbacks.OnQuit'   
2.	'ChunkRequests+Callbacks.OnQuit' -> 'ChunkRequests+Callbacks.OnQuit'   
3.	'Clear temp file cache' -> 'GameManager.ClearCache'   
4.	'ColonySurvival.Client.Physics.ColliderGeneration+Callbacks.OnQuit' -> 'ColonySurvival.Client.Physics.ColliderGeneration+Callbacks.OnQuit'   
5.	'dispose gamemanager' -> 'GameManager.Dispose'   
6.	'ItemTypesClient+Callbacks.OnQuit' -> 'ItemTypesClient+Callbacks.OnQuit'   
7.	'NPC.NPCManagerInternal+NPCUpdating+Callbacks.OnQuit' -> 'NPC.NPCManagerInternal+NPCUpdating+Callbacks.OnQuit'   
8.	'Rendering+Callbacks.OnQuit' -> 'Rendering+Callbacks.OnQuit'   
9.	'VoxelPhysics+Callbacks.OnQuit' -> 'VoxelPhysics+Callbacks.OnQuit'   
10.	'WaterMeshTracker+ModCallbacks.OnQuit' -> 'WaterMeshTracker+ModCallbacks.OnQuit'   
11.	'WorldRenderer+Callbacks.OnQuit' -> 'WorldRenderer+Callbacks.OnQuit'   
12.	'steamnetworking.close' -> 'Pipliz.Networking.SteamNetworking+ModRegistering.OnQuit' index: 1  
13.	'pipliz.jointhreads' -> 'Pipliz.Threading.ThreadSafeQuitWrapper.JoinThread' index: 500  
14.	'pipliz.shared.waitforasyncquitslate' -> 'Pipliz.Application.WaitForQuits' index: 1000  
15.	'dispose colorbuffers' -> 'PipBurst+ColorBuffer.DisposePool' index: 10000  
16.	'LZ4DecoderFreeing' -> 'Pipliz.LZ4.LZ4Codec+Callbacks.OnQuit' index: 10000  


CallbackType: `OnConnectionScreenDone`  
=======  
Method type: System.Action  
No registered uses  


CallbackType: `OnMainMenuLoaded`  
=======  
Method type: System.Action  
Registered callbacks: 35  
0.	'AnimationManager+Callbacks.OnMainMenuLoaded' -> 'AnimationManager+Callbacks.OnMainMenuLoaded'   
1.	'ChunkManager+Callbacks.OnMainMenuLoaded' -> 'ChunkManager+Callbacks.OnMainMenuLoaded'   
2.	'ChunkRequests+Callbacks.OnMainMenuLoaded' -> 'ChunkRequests+Callbacks.OnMainMenuLoaded'   
3.	'clear_label_cache' -> 'colonyclient.Assets.Scripts.UI.Ingame.UIGeneration.UILabel.ClearCache'   
4.	'clear_lut_texture' -> 'Rendering.ClearLUTTexture'   
5.	'clear_uiimage_cache' -> 'colonyclient.Assets.Scripts.UI.Ingame.UIGeneration.UIImage.ClearCache'   
6.	'clear_worldmarker_cache' -> 'colonyclient.Assets.Scripts.UI.Ingame.UIGeneration.WorldMarker.ClearCache'   
7.	'collidergen.reset' -> 'ColonySurvival.Client.Physics.ColliderGeneration.Reset'   
8.	'ColonySurvival.Client.Physics.ColliderGeneration+Callbacks.OnMainMenuLoaded' -> 'ColonySurvival.Client.Physics.ColliderGeneration+Callbacks.OnMainMenuLoaded'   
9.	'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnMainMenuLoaded' -> 'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnMainMenuLoaded'   
10.	'GameAction+Callbacks.OnMainMenuLoaded' -> 'GameAction+Callbacks.OnMainMenuLoaded'   
11.	'heartbeattracker.reset' -> 'CongestionTracker+Callbacks.OnMainMenuLoaded'   
12.	'InventoryStatistics+Callbacks.OnMainMenuLoaded' -> 'InventoryStatistics+Callbacks.OnMainMenuLoaded'   
13.	'NPC.NPCBase+Callbacks.OnMainMenuLoaded' -> 'NPC.NPCBase+Callbacks.OnMainMenuLoaded'   
14.	'NPC.NPCManagerInternal+NPCUpdating+Callbacks.OnMainMenuLoaded' -> 'NPC.NPCManagerInternal+NPCUpdating+Callbacks.OnMainMenuLoaded'   
15.	'NPC.NPCNames+Callbacks.OnMainMenuLoaded' -> 'NPC.NPCNames+Callbacks.OnMainMenuLoaded'   
16.	'pipliz.client.clearactionqueue' -> 'Shared.ItemManagementAction+ActionQueue.Clear'   
17.	'pipliz.client.clearaudiomanager' -> 'AudioManager.Clear'   
18.	'pipliz.client.clearnpcmaterial' -> 'NPCMaterial.ClearNPCMaterial'   
19.	'pipliz.client.clearterrainmaterial' -> 'TerrainMaterial.ClearTerrainMaterial'   
20.	'pipliz.client.gamemanagerclear' -> 'GameManager.OnMainMenuLoaded'   
21.	'pipliz.client.refreshlocale' -> 'GameManager.RefreshLocale'   
22.	'pipliz.client.resetareatracker' -> 'Pipliz.AI.Areas.AreaTracker+FilledAreaRenderer.Reset'   
23.	'pipliz.client.resetbanners' -> 'Client.Rendering.BannerRenderer.Reset'   
24.	'pipliz.client.resetinventory' -> 'Inventory.Release'   
25.	'pipliz.client.resetprojectiles' -> 'Projectile.Clear'   
26.	'pipliz.client.servercontrolledmeshes.resetsettingstable' -> 'Client.ControlledMeshes.MeshTypeSettings.OnReloaded'   
27.	'pipliz.resetparticlemanager' -> 'ParticleManager.Reset'   
28.	'reset_controlledmeshes' -> 'Client.ControlledMeshes.ControlledMeshes.Reset'   
29.	'resetcrafting' -> 'Crafting.Reset'   
30.	'signs.clear' -> 'NetworkUI.SignText+Callbacks.OnMainMenuLoaded'   
31.	'SteamManager+StatsManager+Callbacks.OnMainMenuLoaded' -> 'SteamManager+StatsManager+Callbacks.OnMainMenuLoaded'   
32.	'VoxelPhysics+Callbacks.OnMainMenuLoaded' -> 'VoxelPhysics+Callbacks.OnMainMenuLoaded'   
33.	'WaterMeshTracker+ModCallbacks.OnMainMenuLoaded' -> 'WaterMeshTracker+ModCallbacks.OnMainMenuLoaded'   
34.	'WorldContext+Callbacks.OnMainMenuLoaded' -> 'WorldContext+Callbacks.OnMainMenuLoaded'   


CallbackType: `OnOriginShift`  
=======  
Method type: System.Action<Pipliz.Vector3Int>  
Registered callbacks: 10  
0.	'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnOriginShift' -> 'ColonySurvival.Client.UI.Ingame.NPCIndicator+Callbacks.OnOriginShift'   
1.	'NetworkUI.SignText+Callbacks.OnOriginShift' -> 'NetworkUI.SignText+Callbacks.OnOriginShift'   
2.	'pipliz.client.collidergenerationmove' -> 'ColonySurvival.Client.Physics.ColliderGeneration.OnShift'   
3.	'pipliz.client.npcbodyshift' -> 'NPC.NPCBase+Statics.OnOriginShift'   
4.	'pipliz.client.playeroriginshift' -> 'PlayerBody.OnShift'   
5.	'pipliz.client.projectilesshift' -> 'Projectile.OnShift'   
6.	'pipliz.client.ragdollmove' -> 'ColonySurvival.Client.RagdollTracker.OnShift'   
7.	'pipliz.client.shiftaudio' -> 'AudioManager.Shift'   
8.	'pipliz.shiftparticles' -> 'ParticleManager.Shift'   
9.	'Pipliz.UI.DevMenu.ShaderFeatures+CinemachineTracker+Callbacks.OnOriginShift' -> 'Pipliz.UI.DevMenu.ShaderFeatures+CinemachineTracker+Callbacks.OnOriginShift'   


CallbackType: `OnPlayerPositionReceived`  
=======  
Method type: System.Action<UnityEngine.Vector3>  
Registered callbacks: 1  
0.	'ChunkRequests+Callbacks.OnPlayerPositionReceived' -> 'ChunkRequests+Callbacks.OnPlayerPositionReceived'   


CallbackType: `OnNetworkButtonPress`  
=======  
Method type: System.Action<string, Newtonsoft.Json.Linq.JToken>  
No registered uses  


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
Registered callbacks: 1  
0.	'NPC.NPCNames+Callbacks.OnPlayerMoved' -> 'NPC.NPCNames+Callbacks.OnPlayerMoved'   


CallbackType: `OnEndReadOnly`  
=======  
Method type: System.Action  
Registered callbacks: 2  
0.	'ChunkManager+Callbacks.OnEndReadOnly' -> 'ChunkManager+Callbacks.OnEndReadOnly'   
1.	'Lighting.LightingManager+Callbacks.OnEndReadOnly' -> 'Lighting.LightingManager+Callbacks.OnEndReadOnly'   


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


