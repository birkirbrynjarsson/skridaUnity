using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.FantasyHeroes.Scripts;

public class CharacterHairColorScript : MonoBehaviour {

	public int value;
	public string target;
	public Text label;
	public Button nextButton;
	public Button prevButton;
	public CharacterEditor characterScript;
	private List<Color> colors = new List<Color>(){
		new Color(0.04f, 0.04f, 0.03f, 1.0f), // Black
		new Color(0.17f, 0.13f, 0.17f, 1.0f), // Off Black
		new Color(0.44f, 0.39f, 0.35f, 1.0f), // Dark Gray
		new Color(0.72f, 0.65f, 0.62f, 1.0f), // Medium Gray
		new Color(0.84f, 0.77f, 0.76f, 1.0f), // Light Gray
		new Color(0.79f, 0.75f, 0.69f, 1.0f), // Platinum Blonde
		new Color(0.863f, 0.816f, 0.729f, 1.0f), // Bleached Blonde
		new Color(1f, 0.961f, 0.882f, 1.0f), // White Blonde
		new Color(0.902f, 0.808f, 0.659f, 1.0f), // Light Blonde
		new Color(0.898f, 0.784f, 0.659f, 1.0f),// Golden Blonde
		new Color(0.871f, 0.737f, 0.6f, 1.0f), // Ash 
		new Color(0.722f, 0.592f, 0.47f, 1.0f), // Honey Blonde
		new Color(0.647f, 0.42f, 0.275f, 1.0f), // Strawberry Blonde
		new Color(0.71f, 0.322f, 0.224f, 1.0f), // Light Red
		new Color(0.553f, 0.29f, 0.263f, 1.0f), // Dark Red
		new Color(0.569f, 0.333f, 0.239f, 1.0f), // Light Auburn
		new Color(0.325f, 0.239f, 0.196f, 1.0f), // Dark Auburn
		new Color(0.231f, 0.188f, 0.141f, 1.0f), // Dark Brown
		new Color(0.333f, 0.282f, 0.22f, 1.0f), // Golden Brown
		new Color(0.306f, 0.263f, 0.247f, 1.0f), // Medium Brown
		new Color(0.314f, 0.267f, 0.267f, 1.0f), // Chestnut Brown
		new Color(0.416f, 0.306f, 0.259f, 1.0f), // Brown
		new Color(0.655f, 0.522f, 0.416f, 1.0f), // Light Brown
		new Color(0.592f, 0.475f, 0.38f, 1.0f), // Ash Brown
	};

	private List<string> colorNames = new List<string>{
		"Black",
		"Off Black",
		"Dark Gray",
		"Medium Gray",
		"Light Gray",
		"Platinum Blonde",
		"Bleached Blonde",
		"White Blonde",
		"Light Blonde",
		"Golden Blonde",
		"Ash",
		"Honey Blonde",
		"Strawberry Blonde",
		"Light Red",
		"Dark Red",
		"Light Auburn",
		"Dark Auburn",
		"Dark Brown",
		"Golden Brown",
		"Medium Brown",
		"Chestnut Brown",
		"Brown",
		"Light Brown",
		"Ash Brown"
	};

	void Start(){
		nextButton.onClick.AddListener(nextColor);
		prevButton.onClick.AddListener(prevColor);
		nextColor();
	}
	public void nextColor(){
		characterScript.setTarget(target);
		value = (value + 1) == colors.Count ? 0 : value + 1;
		characterScript.PickColor(colors[value]);
		label.text = colorNames[value];
	}

	public void prevColor(){
		characterScript.setTarget(target);
		value = (value - 1) < 0 ? colors.Count - 1 : value - 1;
		characterScript.PickColor(colors[value]);
		label.text = colorNames[value];
	}
}
