using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawner : MonoBehaviour {

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private GameObject player, itemprevtemp;
	public bool moveTowardsPlayer = false;
	private float progress = 0, timer = 0;
	private bool timerActivated = false, previewShown = false;
	public ItemStack item = null;
	public GameObject ItemPreviewPrefab;
	private Sprite itemPreviewSprite;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player_01");
		float randx = Random.Range (-0.8f, -1.2f);
		float randy = Random.Range (4f, 6f);

		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		rb.AddForce (new Vector2(randx, randy), ForceMode2D.Impulse);
	}

	// Update is called once per frame
	void Update () {
		if(moveTowardsPlayer) {
			transform.position = Vector2.Lerp (new Vector2(transform.position.x, transform.position.y), player.transform.position, 3 * Time.deltaTime);
			if(Vector2.Distance(transform.position, player.transform.position) < 1f) {
				transform.localScale = Vector2.Lerp (transform.localScale, new Vector2(0.1f, 0.1f), progress);
				progress += Time.deltaTime * 2;
			}
			if(Vector2.Distance(transform.position, player.transform.position) < 0.25f) {
				Destroy (this.gameObject);
			}
		}

		if(itemprevtemp != null) {
			Vector3 mousepos = Input.mousePosition;
			if(Input.mousePosition.y >= 750) {
				mousepos.y = 750;
			}
			if(Input.mousePosition.y <= 290) {
				mousepos.y = 290;
			}
			if(Input.mousePosition.x <= 115) {
				mousepos.x = 115;
			}
			if(Input.mousePosition.x >= 450) {
				mousepos.x = 450;
			}
			itemprevtemp.transform.position = mousepos;
		}

		if(timerActivated) {
			timer += Time.deltaTime;
		}

		if(timer >= 0.25f) {
			if(!previewShown) {
				previewShown = true;
				preview();
			}
		}
	}

	public void preview() {
		if (item != null) {
			itemprevtemp = Instantiate(ItemPreviewPrefab, Camera.main.ScreenToViewportPoint(Input.mousePosition), Quaternion.identity);
			itemprevtemp.transform.SetParent (GameObject.Find("Canvas").transform, false);
			string hovertext = item.Hover();
	 		string[] splitString = hovertext.Split(new char[]{'/'});
			GameObject.Find("txtipName").GetComponent<Text>().text = splitString[0];
			GameObject.Find("imgipSprite").GetComponent<Image>().sprite = itemPreviewSprite;
			for(int i = 1; i <= 7; i++) {
				if(i < splitString.Length) {
					GameObject.Find("txtipStat"+i).GetComponent<Text>().text = splitString[i];
				} else {
					GameObject.Find("txtipStat"+i).GetComponent<Text>().text = " ";
				}
			}
		}
	}

	void OnMouseDown() {
		timerActivated = true;
		timer = 0;
	}

	void OnMouseUp() {
		Destroy(itemprevtemp);
		if(timer <= 0.25f) {
			if(!moveTowardsPlayer) {
				if (item != null) {
					GameManager.instance.addtoinv (item, this.gameObject);
				} else {
					Destroy (this.gameObject);
				}
			}
		}
		timerActivated = false;
		timer = 0;
		previewShown = false;
	}

	public void setItem(ItemStack i) {
		this.item = i;
		Sprite[] ss = Resources.LoadAll<Sprite>("ItemSprites");
		foreach(Sprite s in ss) {
			if(s.name == "ItemSprites_"+item.ItemAssetName) {
				GetComponent<SpriteRenderer> ().sprite = s;
				itemPreviewSprite = s;
			}
		}
	}
}
