using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue {

	private Sprite portrait, background;
	private string text;
	private int portraitSide;

	public Dialogue(Sprite portrait, Sprite background, string text, int portraitSide) {
		this.portrait = portrait;
		this.background = background;
		this.text = text;
		this.portraitSide = portraitSide;
	}

	public Sprite Portrait {
		get {
			return portrait;
		}
		set {
			portrait = value;
		}
	}

	public Sprite Background {
		get {
			return background;
		}
		set {
			background = value;
		}
	}

	public string Text {
		get {
			return text;
		}
		set {
			text = value;
		}
	}

	public int PortraitSide {
		get {
			return portraitSide;
		}
		set {
			portraitSide = value;
		}
	}
}
