using UnityEngine;
using UnityEditor.Sprites;
using UnityEngine.UI;
using System.Collections;

public class MainTextScript : MonoBehaviour 
{

	public Text MainText;
	public Text LocationText;
	public Image MainImage;

	private enum Location { ScienceModule, MainHabitat, TubeNorth, TubeSouth, EscapePod1, EscapePod2, BioLab };
	
	private enum HatchState { Open, Closed, Stuck };
	
	private HatchState ScienceModuleHatch;
	private HatchState BiolabHatch;
	
	private Location currentLocation;

	// Use this for initialization
	void Start () 
	{
		this.ScienceModuleHatch = HatchState.Closed;
		this.BiolabHatch = HatchState.Stuck;
		currentLocation = Location.ScienceModule;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( currentLocation )
		{
			case Location.ScienceModule:
				LocationScienceModule();
				break;
				
			case Location.BioLab:
				LocationBioLab();
				break;
		}
	}
	
	string GetHatchDescription(HatchState hatch)
	{
		if( hatch == HatchState.Stuck )
			return "There is a stuck hatch.";
	
		return "There is " + ((hatch == HatchState.Open ) ? "an open" : "a closed") + " hatch.";
	}
	
	void LocationScienceModule()
	{
		string body = "You are in a science module.";
		
	
		string choices = "\nO to open the hatch";
	
		this.MainText.text = body + "\n" + GetHatchDescription(this.ScienceModuleHatch) + choices;
		
		this.LocationText.text = "Science Module";
		
		if( Input.GetKeyDown(KeyCode.O) )
		{
			if( this.ScienceModuleHatch == HatchState.Open ) 
				this.ScienceModuleHatch = HatchState.Closed;
			else if( this.ScienceModuleHatch == HatchState.Closed ) 
				this.ScienceModuleHatch = HatchState.Open;			
		}
	
	}
	
	void LocationBioLab()
	{
		this.MainText.text = "You are in a bio lab that is heavily damaged.  There are broken components all around the module.";
	
		this.LocationText.text = "Bio Lab";
	}
	
}
