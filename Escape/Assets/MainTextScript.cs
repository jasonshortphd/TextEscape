using UnityEngine;
using UnityEditor.Sprites;
using UnityEngine.UI;
using System.Collections;

public class MainTextScript : MonoBehaviour 
{

	public Text MainText;
	public Text LocationText;
	public Image MainImage;

	private enum Location { ScienceModule, MainHabitat, TubeNorth, TubeSouth, EscapePod1, EscapePod2, BioLab, Outside };
	
	private enum HatchState { Open, Closed };
	
	private HatchState ScienceModuleHatch;
	private HatchState BiolabHatch;
	
	private Location currentLocation;

	// Use this for initialization
	void Start () 
	{
		this.ScienceModuleHatch = HatchState.Closed;
		this.BiolabHatch = HatchState.Closed;
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
				
			case Location.MainHabitat:
				LocationMainHabitat();
				break;
				
			case Location.Outside:
				LocationOutside();
				break;
		}
	}
	
	string GetHatchDescription(HatchState hatch, string name)
	{	
		return "There is " + ((hatch == HatchState.Open ) ? "an open " : "a closed ") + name + " hatch.";
	}
	
	void LocationScienceModule()
	{
		string body = "You are in a science module.";
	
		string choices = "\n\ns to " + ((this.ScienceModuleHatch == HatchState.Open) ? "close" : "open")  + " the science module hatch";
		
		if( this.ScienceModuleHatch == HatchState.Open )
			choices += "\nE to move east into main module";
	
		this.MainText.text = body + "\n" + GetHatchDescription(this.ScienceModuleHatch, "science module") + choices;
		
		this.LocationText.text = "Science Module";
		
		if( Input.GetKeyDown(KeyCode.S) )
		{
			if( this.ScienceModuleHatch == HatchState.Open ) 
				this.ScienceModuleHatch = HatchState.Closed;
			else if( this.ScienceModuleHatch == HatchState.Closed ) 
				this.ScienceModuleHatch = HatchState.Open;			
		}
		else if (Input.GetKeyDown(KeyCode.E))
		{
			this.currentLocation = Location.MainHabitat;
		}
	
	}
	
	void LocationBioLab()
	{
		this.MainText.text = "You are in a bio lab that is heavily damaged.  There are broken components all around the module.";
	
		this.LocationText.text = "Bio Lab";
	}
	
	void LocationMainHabitat()
	{
		string body = "The main module is a mess.  There has clearly been a fire here recently.  Maybe that is why you can't remember what happened?";
		
		string choices = "\n\nS to " + ((this.ScienceModuleHatch == HatchState.Open) ? "close" : "open")  + " the science module hatch" +
			"\nb to " + ((this.BiolabHatch == HatchState.Open) ? "close" : "open")  + " the biolab hatch";
		
		if( this.ScienceModuleHatch == HatchState.Open )
			choices += "\nW to move west into the science module";

		if( this.BiolabHatch == HatchState.Open )
			choices += "\nE to move east into the biolab module";
		
		this.MainText.text = body + "\n" + GetHatchDescription(this.ScienceModuleHatch, "science module") +
			GetHatchDescription(this.BiolabHatch, "biolab module") + choices;
		
		this.LocationText.text = "Main Module";
		
		if( Input.GetKeyDown(KeyCode.S) )
		{
			if( this.ScienceModuleHatch == HatchState.Open ) 
				this.ScienceModuleHatch = HatchState.Closed;
			else if( this.ScienceModuleHatch == HatchState.Closed ) 
				this.ScienceModuleHatch = HatchState.Open;			
		}
		else if( Input.GetKeyDown(KeyCode.B) )
		{		
			if( this.BiolabHatch == HatchState.Open ) 
				this.BiolabHatch = HatchState.Closed;
			else if( this.BiolabHatch == HatchState.Closed ) 
				this.BiolabHatch = HatchState.Open;			
				
			// Opening both hatches at the same time means death
			if(( this.ScienceModuleHatch == HatchState.Open ) && ( this.BiolabHatch == HatchState.Open ))
			{
				this.currentLocation = Location.Outside;
			}
				
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			this.currentLocation = Location.ScienceModule;
		}
		else if (Input.GetKeyDown(KeyCode.E))
		{
			this.currentLocation = Location.BioLab;
		}
	}
	
	void LocationOutside()
	{
	
		string body = "You are now very cold.  There is not enough oxygen to support life.  It appears you found a module with a hole in it and have died as a result.";
				
		this.LocationText.text = "Frozen Solid";
		
		string choices = "\nS to start over";
		
		this.MainText.text = body + "\n" + choices;
		
		if( Input.GetKeyDown(KeyCode.S) )
		{
			this.Start();
		}
	}
	
}
