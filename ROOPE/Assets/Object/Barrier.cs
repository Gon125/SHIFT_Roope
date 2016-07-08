﻿using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag != "Player" && other.tag != "Rope" && other.tag != "ScaleChanger") {
			Destroy (other.gameObject);
		}
	}
}
