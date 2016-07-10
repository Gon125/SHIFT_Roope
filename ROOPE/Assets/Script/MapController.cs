﻿using UnityEngine;
using System.Collections;
using System.IO;

public class MapController : MonoBehaviour {
	private string MapPath;
	private Hashtable MapObjects;
	private int stage = 1;
	private Player player;
	int pattern_num=0;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
		MapPath = "Maps/";
		Debug.Log (MapPath);
		MapObjects = new Hashtable ();
		readMapFromFile ();

	}
	
	// Update is called once per frame
	void Update () {
		float map_position = player.transform.position.x;

		if (map_position >= 50*(pattern_num+1)) {
			pattern_num++;
			Map_Create (pattern_num + 2);
		}

	}

	void WriteFile()
	{
		StreamWriter fileWriter = new StreamWriter (MapPath);

		// write file
		fileWriter.WriteLine ("Hello world");
		fileWriter.Flush ();
		fileWriter.Close ();
	}


	void readMapFromFile()
	{
		int height = 0, pattern = 0;
		int random_pattern = 0;
		string[] TILE = new string[] {
			"map_1A",
			"map_2A",
			"map_3A",
			"map_4A",
			"map_17A",
			"map_6A",
			"map_7A",
			"map_8A",
			"map_9A",
			"map_10A",
			"map_11A",
			"map_12A",
			"map_13A",
			"map_14A",
			"map_15A",
			"map_16A",
			"map_5A",
			"map_1B",
			"map_2B",
			"map_3B",
			"map_4B",
			"map_6B",
			"map_7B",
			"map_8B",
			"map_9B",
			"map_10B",
			"map_11B",
			"map_12B",
			"map_13B",
			"map_14B",
			"map_15B",
			"map_16B",
			"map_17B",
			"map_5B",
			"map_1C", 
			"map_2C",
			"map_3C",
			"map_4C",
			"map_6C",
			"map_7C",
			"map_8C",
			"map_9C",
			"map_10C",
			"map_11C",
			"map_12C",
			"map_13C",
			"map_14C",
			"map_15C",
			"map_16C",
			"map_7C",
			"map_A",
			"map_B",
			"map_C",
			"map_D"
		};

		for (pattern = 0; pattern < TILE.Length; pattern++) {
//			if (pattern % 25 == 0 && pattern != 0)
//				stage++;
//			if (stage == 1)
//				random_pattern = Random.Range (0, 16);
//			else if (stage == 2)
//				random_pattern = Random.Range (17, 33);
//			else
//				random_pattern = Random.Range (33, 49);
			random_pattern = Random.Range (50,53);
			TextAsset map = Resources.Load (MapPath + TILE[random_pattern]) as TextAsset;
			StreamReader fileReader = new StreamReader (new MemoryStream(map.bytes)); 
			height = 0;
			// read file
			while (!fileReader.EndOfStream) {
				string line = fileReader.ReadLine ();
				string[] temp = line.Split(' ');
				for (int i = 0; i < 50; i++) {
						MapObjects.Add ((pattern * 50 + i) + "," + height, int.Parse (temp[i] + ""));
				}
				height++;
			}
			//MapTerm (pattern);
			fileReader.Close ();
		}
		Map_Create (1);
		Map_Create (2);
	}

//	void MapTerm(int pattern) // making safe zone
//	{
//		int height = 0;
//		TextAsset map = Resources.Load<TextAsset> (MapPath + "map_TERM");
//		StreamReader mapTerm = new StreamReader (new MemoryStream(map.bytes));
//		while (!mapTerm.EndOfStream) {
//			string line = mapTerm.ReadLine ();
//			for (int i = 0; i < 5; i++) {
//				MapObjects.Add (((pattern + 1) * 30 - 5 + i) + "," + height, int.Parse (line.ToCharArray () [i] + ""));
//			}
//			height++;
//		}
//	}

	void Map_Create(int pattern)
	{
		string path = "Prefabs/";
		Quaternion rotate = new Quaternion ();
		for (int j = 0; j < 11; j++) {
			for (int i = 50*(pattern-1); i < 50 * pattern; i++) {
				Vector3 position = new Vector3 (i, -j + 4.5f, 0);
				switch ((int)MapObjects [i + "," + j]) {
				case (int)RObjectType.BLANK:
					break;

				case (int)RObjectType.STANDARD:
					Instantiate (Resources.Load (path + "Wall"), position, rotate);
					break;

				case (int)RObjectType.POINT:
					Instantiate (Resources.Load (path + "Score_Item"), position, rotate);
					break;

				case (int)RObjectType.FALLING:
					Instantiate (Resources.Load (path + "Drop_Wall"), position, rotate);
					break;

				case (int)RObjectType.SLIP:
					Instantiate (Resources.Load (path + "Slip_Wall"), position, rotate);
					break;

				case (int)RObjectType.ITEM:
					Instantiate (Resources.Load (path + "Scale_Change"), position, rotate);
					break;

				case (int)RObjectType.ARROW:
					position.y = 0;
					Instantiate (Resources.Load(path + "ArrowCollider"), position, rotate);
					break;

				case (int)RObjectType.WIND_0:
				case (int)RObjectType.WIND_UP:
				case (int)RObjectType.WIND_DOWN:
					position.y = 0;
					WindControl a = (WindControl) Instantiate (Resources.Load (path + "WindCollider"), position, rotate);
					if ((int)MapObjects [i + "," + j] == (int)RObjectType.WIND_UP) {
						a.x_Strength = 0;
						a.y_Strength = 66;
					}
					else if ((int)MapObjects [i + "," + j] == (int)RObjectType.WIND_DOWN) {
						a.x_Strength = 0;
						a.y_Strength = -33;
					}	
					break;
				case (int)RObjectType.STOP:
					Instantiate (Resources.Load (path + "WallStopper"), position, rotate);
					break;

				case (int)RObjectType.MOVE_UP:
				case (int)RObjectType.MOVE_RIGHT:
				case (int)RObjectType.MOVE_DOWN:
				case (int)RObjectType.MOVE_LEFT:
					Wall move = ((GameObject)Instantiate (Resources.Load (path + "Wall"), position, rotate)).GetComponent<Wall> ();
					move.movable = true;
					if ((int)MapObjects [i + "," + j] == (int)RObjectType.MOVE_UP) {
						move.direction = 1f;
						move.speed = 1.5f;
					} else if ((int)MapObjects [i + "," + j] == (int)RObjectType.MOVE_RIGHT) {
						move.direction = 2f;
						move.speed = 1.5f;
					} else if ((int)MapObjects [i + "," + j] == (int)RObjectType.MOVE_DOWN) {
						move.direction = 3f;
						move.speed = 1.5f;
					} else if ((int)MapObjects [i + "," + j] == (int)RObjectType.MOVE_LEFT) {
						move.direction = 0f;
						move.speed = 1.5f;
					}
					break;
				}
			}
		}
	}
}
