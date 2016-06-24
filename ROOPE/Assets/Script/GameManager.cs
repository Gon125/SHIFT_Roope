﻿using UnityEngine;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour {
	private string FILE_PATH = Application.persistentDataPath + "/" + "map.dat";
	private Hashtable MapObjects;

	public GameObject infoTextMesh;

	private float hitPoint;
	private int score;
	private int numberOfRope;
	private int stage;


	// Make map objects
	void Awake() {
		hitPoint = 3.0f;
		score = 0;
		numberOfRope = 0;
		stage = 1;

		MapObjects = new Hashtable ();
		readMapFromFile ();
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (infoTextMesh.GetComponent<TextMesh>() != null) {
			infoTextMesh.GetComponent<TextMesh>() = ToString();
		}
	}


	void WriteFile()
	{
		StreamWriter fileWriter = new StreamWriter (FILE_PATH);

		// write file
		fileWriter.WriteLine ("Hello world");
		fileWriter.Flush ();
		fileWriter.Close ();
	}


	void readMapFromFile()
	{
		StreamReader fileReader = new StreamReader (FILE_PATH);

		// read file
		while(!fileReader.EndOfStream)
		{
			string line = fileReader.ReadLine ();
		}

		fileReader.Close ();
	}

	public void nextStage() {
		stage++;
	}

	public float getHP() {
		return hitPoint;
	}

	public void setHP(float hitPoint) {
		this.hitPoint = hitPoint;
	}

	public void addHP(float hp) {
		setHP (getHP () + hp);
	}

	public int getScore() {
		return score;
	}

	public void setScore(int score) {
		this.score = score;
	}

	public void addScroe(int score) {
		setScore (getScore () + score);
	}

	public int getNumberOfRope() {
		return numberOfRope;
	}

	public void setNumberOfRope(int numberOfRope) {
		this.numberOfRope = numberOfRope;
	}

	public void addNumberOfRope(int numberOfRope) {
		setNumberOfRope(getNumberOfRope() + numberOfRope);
	}

	public int getStage() {
		return stage;
	}

	public override string ToString() {
		return "STAGE : " + getStage () + "HP : " + getHP () + " " +
		"SCORE : " + getScore () + "ROPE : " + getNumberOfRope ();
	}
}