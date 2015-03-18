using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainTextScript : MonoBehaviour 
{

	public Text MainText;
	public Text LocationText;
	public Text CommandText;
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
		
		this.CommandText.text = "Please Wait";
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
		string body = "You are in a science module, there appears to be a very small air leak.  You might want to leave this area and seal it off before you run out of oxygen!";
	
		string choices = "S to " + ((this.ScienceModuleHatch == HatchState.Open) ? "close" : "open")  + " the science module hatch";
		
		if( this.ScienceModuleHatch == HatchState.Open )
			choices += "\nE to move east into main module";
	
		this.MainText.text = body + "\n" + GetHatchDescription(this.ScienceModuleHatch, "science module");
		
		this.CommandText.text = choices;
		
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
		string body = "You are in a bio lab that is heavily damaged.  There are broken components all around the module.  There is nothing here you can see to salvage.  Maybe the escape pod is still working?";
		
		this.LocationText.text = "Bio Lab";
	
		string choices = "B to " + ((this.BiolabHatch == HatchState.Open) ? "close" : "open")  + " the biolab hatch.";
		
		if( this.BiolabHatch == HatchState.Open )
			choices += "\nW to move west (main module).";
		
		this.MainText.text = body + "\n" + GetHatchDescription(this.BiolabHatch, "bio lab");
		
		this.CommandText.text = choices;
		
		this.LocationText.text = "Bio Lab";
		
		if( Input.GetKeyDown(KeyCode.B) )
		{
			if( this.BiolabHatch == HatchState.Open ) 
				this.BiolabHatch = HatchState.Closed;
			else if( this.BiolabHatch == HatchState.Closed ) 
				this.BiolabHatch = HatchState.Open;			
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			this.currentLocation = Location.MainHabitat;
		}
		
	}
	
	void LocationMainHabitat()
	{
		string body = 	"The main module is a mess.  There has clearly been a fire here recently. " + 
						"There is a broken datapad floating nearby.";
		
		string choices = "S to " + ((this.ScienceModuleHatch == HatchState.Open) ? "close" : "open")  + " the science module hatch. " +
			"B to " + ((this.BiolabHatch == HatchState.Open) ? "close" : "open")  + " the biolab hatch.\n";
		
		if( this.ScienceModuleHatch == HatchState.Open )
			choices += "W to move west (science module). ";

		if( this.BiolabHatch == HatchState.Open )
			choices += "E to move east (biolab module).";
		
		this.MainText.text = body + "\n" + GetHatchDescription(this.ScienceModuleHatch, "science module") +
			GetHatchDescription(this.BiolabHatch, "biolab module");
		
		this.LocationText.text = "Main Module";
		this.CommandText.text = choices;
		
		
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
				
		this.CommandText.text = "S to start over";
		
		this.MainText.text = body;
		
		if( Input.GetKeyDown(KeyCode.S) )
		{
			this.Start();
		}
	}
	
}
