using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : ItemStack {

	private int armor, health;

	public Helmet(int stackID, int itemID, string name, int amount, bool unique, string itemAssetName, int armor, int health) : base(stackID, itemID, name, amount, unique, itemAssetName) {
		this.armor = armor;
		this.health = health;
	}

	public int Armor {
		get {
			return this.armor;
		}
		set {
			armor = value;
		}
	}

	public int Health {
		get {
			return this.health;
		}
		set {
			health = value;
		}
	}

	public override string Hover() {
		return name + "/+" + armor + " Armor/+" + health + " Health";
	}
}
