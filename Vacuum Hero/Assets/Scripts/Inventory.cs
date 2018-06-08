using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	private ItemStack[] ie;
	private int MAXSTACK = 250;

	public Inventory() {
		ie = new ItemStack[25];
	}

	public ItemStack[] Ie {
		get {
			return this.ie;
		}
		set {
			ie = value;
		}
	}

	public ItemStack getItemSlot(int p) {
		return ie [p];
	}

	public void setItemSlot(int p, ItemStack item){
		this.ie [p] = item;
	}
}
