using Pipliz.JSON;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Pipliz.WorldConverter
{
	[ModLoader.ModManager]
	public static class WorldConverter
	{
		static string pathRegionFolderOriginal = "Input Region folder here";
		static string pathRegionFolderNew = "Output folder here";
		static string pathTableOriginal = "Original itemTypeTable path here";
		static string pathTableNew = "New itemTypeTable path here";

		[ModLoader.ModCallback (ModLoader.EModCallbackType.OnGUI, "pipliz.worldconverter.ongui")]
		static void OnGUI ()
		{
			Rect rect = new Rect(Screen.width - 700, 0, 400, 30);
			pathRegionFolderOriginal = GUI.TextField(rect, pathRegionFolderOriginal);
			rect.y += 30f;
			pathRegionFolderNew = GUI.TextField(rect, pathRegionFolderNew);
			rect.y += 30f;
			pathTableOriginal = GUI.TextField(rect, pathTableOriginal);
			rect.y += 30f;
			pathTableNew = GUI.TextField(rect, pathTableNew);
			rect.y += 30f;
			if (GUI.Button(rect, "Click to convert here")) {
				Convert();
			}
		}

		static void Convert ()
		{
			if (!Directory.Exists(pathRegionFolderOriginal)) {
				Log.Write("Directory does not exist: {0}", pathRegionFolderOriginal);
			}
			if (!Directory.Exists(pathRegionFolderNew)) {
				Log.Write("Directory does not exist: {0}", pathRegionFolderNew);
			}
			if (!File.Exists(pathTableOriginal)) {
				Log.Write("File does not exist: {0}", pathTableOriginal);
			}
			if (!File.Exists(pathTableNew)) {
				Log.Write("File does not exist: {0}", pathTableNew);
			}

			JSONNode originalTable = JSON.JSON.Deserialize(pathTableOriginal);
			JSONNode newTable = JSON.JSON.Deserialize(pathTableNew);

			HashSet<ushort> existingNewIndices = new HashSet<ushort>();
			Dictionary<ushort, ushort> typeMapping = new Dictionary<ushort, ushort>();

			foreach (var pair in newTable.LoopObject()) {
				existingNewIndices.Add(pair.Value.GetAs<ushort>());
			}

			ushort firstUnUsed = 1;

			foreach (var pair in originalTable.LoopObject()) {
				ushort oldIndex = pair.Value.GetAs<ushort>();
				string itemName = pair.Key;
				ushort newIndex;
				if (!newTable.TryGetAs(itemName, out newIndex)) {
					while (firstUnUsed < ushort.MaxValue) {
						if (existingNewIndices.Contains(firstUnUsed)) {
							firstUnUsed++;
						} else {
							newTable.SetAs(itemName, firstUnUsed);
							Log.Write("Added {0} -> {1} to new table", itemName, firstUnUsed);
							typeMapping[oldIndex] = firstUnUsed;
							break;
						}
					}
				} else {
					typeMapping[oldIndex] = newIndex;
				}
			}

			JSON.JSON.Serialize(pathTableNew, newTable);

			foreach (var file in Directory.GetFiles (pathRegionFolderOriginal, "*.csregion")) {
				((Action<string, Dictionary<ushort, ushort>>)Convert).BeginInvoke(file, typeMapping, null, null);
			}
		}

		static void Convert (string file, Dictionary<ushort, ushort> mapping)
		{
			List<KeyValuePair<Vector3Int, byte[]>> chunks = new List<KeyValuePair<Vector3Int, byte[]>>();
			using (FileStream baseStream = File.OpenRead(file))
			using (var decompressor = new ICSharpCode.SharpZipLib.GZip.GZipInputStream(baseStream))
			using (BinaryReader binaryReader = new BinaryReader(decompressor)) {
				while (binaryReader.ReadBoolean()) {
					Vector3Int position = new Vector3Int(
						binaryReader.ReadInt32(),
						binaryReader.ReadInt32(),
						binaryReader.ReadInt32()
					);
					int dataLength = binaryReader.ReadInt32();
					byte[] data = binaryReader.ReadBytes(dataLength);

					chunks.Add(new KeyValuePair<Vector3Int, byte[]>(position, data));
				}
			}


			string fileName = Path.GetFileName(file);
			Log.Write("Loaded {0}", fileName);
			ushort[] raw = new ushort[4096];

			using (FileStream baseStream = File.Open(Path.Combine(pathRegionFolderNew, fileName), FileMode.Create, FileAccess.Write))
			using (var compressor = new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(baseStream))
			using (BinaryWriter binaryWriter = new BinaryWriter(compressor)) {
				foreach (var pair in chunks) {
					FileSaver.FileCompression.DecompressRLEToRaw(pair.Value, raw);
					for (int i = 0; i < 4096; i++) {
						raw[i] = mapping[raw[i]];
					}
					byte[] chunk = FileSaver.FileCompression.CompressRawToRLE(raw);
					binaryWriter.Write(true);
					binaryWriter.Write(pair.Key.x);
					binaryWriter.Write(pair.Key.y);
					binaryWriter.Write(pair.Key.z);
					binaryWriter.Write(chunk.Length);
					binaryWriter.Write(chunk);
				}
				binaryWriter.Write(false);
			}

			Log.Write("Converted {0}", fileName);
		}
	}
}
