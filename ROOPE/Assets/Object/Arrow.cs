﻿using UnityEngine;
using System.Collections;

public class Arrow : Obstacle {

	void Start ()
	{
		move (-1, 0);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			Destroy (gameObject);
			collideWithCharacter ();
		}
	}

	public override void collideWithCharacter()
	{

	}

}