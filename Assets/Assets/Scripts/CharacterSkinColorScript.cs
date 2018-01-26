using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.FantasyHeroes.Scripts;

public class CharacterSkinColorScript : MonoBehaviour {

	public int value;
	private string targetBody = "Body";
	private string targetHead = "Head";
	private string targetEars = "Ears";
	public Text label;
	public Button nextButton;
	public Button prevButton;
	public CharacterEditor characterScript;
	private List<Color> colors = new List<Color>(){
		new Color(1f, 0.875f, 0.769f, 1.0f),
		new Color(0.941f, 0.835f, 0.745f, 1.0f),
		new Color(0.933f, 0.808f, 0.702f, 1.0f),
		new Color(0.882f, 0.722f, 0.6f, 1.0f),
		new Color(0.898f, 0.761f, 0.596f, 1.0f),
		new Color(1f, 0.863f, 0.698f, 1.0f),
		new Color(0.898f, 0.722f, 0.529f, 1.0f),
		new Color(0.898f, 0.627f, 0.451f, 1.0f),
		new Color(0.906f, 0.62f, 0.427f, 1.0f),
		new Color(0.859f, 0.565f, 0.396f, 1.0f),
		new Color(0.808f, 0.588f, 0.486f, 1.0f),
		new Color(0.776f, 0.471f, 0.337f, 1.0f),
		new Color(0.729f, 0.424f, 0.286f, 1.0f),
		new Color(0.647f, 0.447f, 0.341f, 1.0f),
		new Color(0.941f, 0.784f, 0.788f, 1.0f),
		new Color(0.867f, 0.659f, 0.627f, 1.0f),
		new Color(0.725f, 0.486f, 0.427f, 1.0f),
		new Color(0.659f, 0.459f, 0.424f, 1.0f),
		new Color(0.678f, 0.392f, 0.322f, 1.0f),
		new Color(0.361f, 0.22f, 0.212f, 1.0f),
		new Color(0.796f, 0.518f, 0.259f, 1.0f),
		new Color(0.741f, 0.447f, 0.235f, 1.0f),
		new Color(0.439f, 0.255f, 0.224f, 1.0f),
		new Color(0.639f, 0.525f, 0.416f, 1.0f)
	};

	void Start(){
		nextButton.onClick.AddListener(nextColor);
		prevButton.onClick.AddListener(prevColor);
		nextColor();
	}
	public void nextColor(){
		characterScript.setTarget(targetBody);
		value = (value + 1) == colors.Count ? 0 : value + 1;
		characterScript.PickColor(colors[value]);
		characterScript.setTarget(targetHead);
		characterScript.PickColor(colors[value]);
		characterScript.setTarget(targetEars);
		characterScript.PickColor(colors[value]);
		label.text = "Skin " + value.ToString();
	}

	public void prevColor(){
		characterScript.setTarget(targetBody);
		value = (value - 1) < 0 ? colors.Count - 1 : value - 1;
		characterScript.PickColor(colors[value]);
		characterScript.setTarget(targetHead);
		characterScript.PickColor(colors[value]);
		characterScript.setTarget(targetEars);
		characterScript.PickColor(colors[value]);
		label.text = "Skin " + value.ToString();
	}
}
