using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : ItemStack {

	private int damage, flatLifeSteal, percentLifeSteal, bonusFlatDamage, bonusPercentDamage, flatBurnChance, percentBurnChance, flatPoisonChance, percentPoisonChance;

	public Vacuum(int stackID, int itemID, string name, int amount, bool unique, string itemAssetName, int damage, int flatLifeSteal, int percentLifeSteal, int bonusFlatDamage,
		int bonusPercentDamage, int flatBurnChance, int percentBurnChance, int flatPoisonChance, int percentPoisonChance) : base(stackID, itemID, name, amount, unique, itemAssetName) {
		this.damage = damage;
		this.flatLifeSteal = flatLifeSteal;
		this.percentLifeSteal = percentLifeSteal;
		this.bonusFlatDamage = bonusFlatDamage;
		this.bonusPercentDamage = bonusPercentDamage;
		this.flatBurnChance = flatBurnChance;
		this.percentBurnChance = percentBurnChance;
		this.flatPoisonChance = flatPoisonChance;
		this.percentPoisonChance = percentPoisonChance;
	}

	public int Damage {
		get {
			return this.damage;
		}
		set {
			damage = value;
		}
	}

	public int FlatLifeSteal {
		get {
			return this.flatLifeSteal;
		}
		set {
			flatLifeSteal = value;
		}
	}

	public int PercentLifeSteal {
		get {
			return this.percentLifeSteal;
		}
		set {
			percentLifeSteal = value;
		}
	}

	public int BonusFlatDamage {
		get {
			return this.bonusFlatDamage;
		}
		set {
			bonusFlatDamage = value;
		}
	}

	public int BonusPercentDamage {
		get {
			return this.bonusPercentDamage;
		}
		set {
			bonusPercentDamage = value;
		}
	}

	public int FlatBurnChance {
		get {
			return this.flatBurnChance;
		}
		set {
			flatBurnChance = value;
		}
	}

	public int PercentBurnChance {
		get {
			return this.percentBurnChance;
		}
		set {
			percentBurnChance = value;
		}
	}

	public int FlatPoisonChance {
		get {
			return this.flatPoisonChance;
		}
		set {
			flatPoisonChance = value;
		}
	}

	public int PercentPoisonChance {
		get {
			return this.percentPoisonChance;
		}
		set {
			percentPoisonChance = value;
		}
	}

	public override string Hover() {
		return name + "/+" + damage + " Damage" + "/+" + flatLifeSteal + " Life Steal" + "/+" + percentLifeSteal + "% LifeSteal" +
			"/+" + bonusFlatDamage + " Bonus Damage" + "/+" + bonusPercentDamage + "% Bonus Damage" + "/+" + flatBurnChance + " Burn Chance" + "/+" + percentBurnChance + "% Burn Chance";
	}
}
