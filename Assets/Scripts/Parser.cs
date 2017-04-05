using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Parser : MonoBehaviour {
	private Draggable[] blocks = null;
	private int instructNum = 0;
	private bool correctCode = true;

	public void Update(){
		instructNum = 0;
		correctCode = true;
		if (parse ()) {
			Debug.Log ("Valid Tower Instruction");
			// program tower
		} 
		else {
			Debug.Log ("Error: Invalid Instruction Set");
			// Display error message to player
		}
	}

	// Returns true if sequence of instructions is valid
	private bool parse(){
		blocks = this.GetComponentsInChildren<Draggable> ();

		// If no instructions
		if (blocks.Length == 0)
			return false;

		// First instruction is IF?
		if (blocks [instructNum].typeOfBlock == Draggable.blockType.IF) {
			if (!nextInstruct ())
				return false;
			enemy ();
			if (!nextInstruct ())
				return false;
			instruction ();
		} 
		// First instruction is SHOOT or DON'T SHOOT
		else if (blocks [instructNum].typeOfBlock == Draggable.blockType.SHOOT ||
		         blocks [instructNum].typeOfBlock == Draggable.blockType.DONTSHOOT) {
			if (!nextInstruct ())
				return false;
			enemy ();
			if (nextInstruct ()) {
				correctCode = false;
			}
		}
		else {
			correctCode = false;
		}

		return correctCode;
	}

	// Increment instructNum and return false if end of instruction set has been reached
	private bool nextInstruct(){
		instructNum++;
		if (blocks.Length < (instructNum + 1)) {
			return false;
		}
		return true;
	}

	// Check instruction is of type ENEMY
	private void enemy(){
		if (blocks [instructNum].typeOfBlock != Draggable.blockType.ENEMY)
			correctCode = false;
	}

	// Check instruction is either an OR or SHOOT/DON'T SHOOT
	private void instruction(){
		if (orStatement ()) {
			if (!nextInstruct ()) {
				correctCode = false;
				return;
			}
			enemy ();
			if (!nextInstruct ()) {
				correctCode = false;
				return;
			}
			instruction ();
		} else if (isShootInstruct ()) {
			connectingStatement ();
		} else
			correctCode = false;
	}

	// Return true if instruction is OR
	private bool orStatement(){
		if (blocks [instructNum].typeOfBlock == Draggable.blockType.OR)
			return true;
		return false;
	}

	// Check if instruction is SHOOT/DON'T SHOOT
	private bool isShootInstruct(){
		if (blocks [instructNum].typeOfBlock == Draggable.blockType.SHOOT)
			return true;
		else if (blocks [instructNum].typeOfBlock == Draggable.blockType.DONTSHOOT)
			return true;
		return false;
	}

	// Check for ELSE/ELSE IF statement or end of instruction sett
	private void connectingStatement(){
		// Reached end of valid instruction set?
		if (!nextInstruct ()) {
			return;
		}

		// ELSE statement?
		if (blocks [instructNum].typeOfBlock == Draggable.blockType.ELSE) {
			if (!nextInstruct ()) {
				correctCode = false;
				return;
			}
			if (!isShootInstruct ()) {
				correctCode = false;
				return;
			}
			// This should be the end of the program
			if (nextInstruct ()) {
				correctCode = false;
			}
			return;
		// ELSE IF statement?
		} else if (blocks [instructNum].typeOfBlock == Draggable.blockType.ELSEIF) {
			if (!nextInstruct ()) {
				correctCode = false;
				return;
			}
			enemy ();
			if (!nextInstruct ()) {
				correctCode = false;
				return;
			}
			instruction ();
		} 
		// Incorrect instruction set if none of the above
		else {
			correctCode = false;
		}
	}
}
