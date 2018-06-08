using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack{

	protected int stackID, itemID, amount;
	protected string name, itemAssetName;
	protected bool unique;

	public ItemStack(int stackID, int itemID, string name, int amount, bool unique, string itemAssetName) {
		this.stackID = stackID;
		this.itemID = itemID;
		this.name = name;
		this.amount = amount;
		this.unique = unique;
		this.itemAssetName = itemAssetName;
	}

	public int StackID {
		get {
			return this.stackID;
		}
		set {
			stackID = value;
		}
	}

	public int ItemID {
		get {
			return this.itemID;
		}
		set {
			itemID = value;
		}
	}

	public string Name {
		get {
			return this.name;
		}
		set {
			name = value;
		}
	}

	public int Amount {
		get {
			return this.amount;
		}
		set {
			amount = value;
		}
	}

	public bool Unique {
		get {
			return this.unique;
		}
		set {
			unique = value;
		}
	}

	public string ItemAssetName {
		get {
			return this.itemAssetName;
		}
		set {
			itemAssetName = value;
		}
	}

	public virtual string Hover() {
		return "lol";
	}
}
