using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster {

	private int health, damage;

	public Monster(int health, int damge) {
		this.health = health;
		this.damage = damage;
	}

	public int Health {
		get {
			return this.health;
		}
		set {
			health = value;
		}
	}

	public int Damage {
		get {
			return this.damage;
		}
		set {
			damage = value;
		}
	}
}
