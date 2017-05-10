using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Parser : MonoBehaviour {
	private Draggable[] blocks = null;
	private int minNumBlocks = 5;
	private int blockNum = 0;
	private bool validInstruction = true;
	private bool endReached = false;

	public void Update(){
		if (parse ()) {
			Debug.Log ("Valid Tower Instruction");
			// program tower
		} 
		else {
			Debug.Log ("Error: Invalid Instruction Set");
			// Display error message to player
		}
	}

	private bool parse() {
		blockNum = 0;
		validInstruction = true;
		endReached = false;
		blocks = this.GetComponentsInChildren<Draggable> ();

		checkFirstBlock ();
		if (!validInstruction)
			return false;
		
		nextBlock ();

		while (!endReached) {
			checkBlock ();
			nextBlock ();
			if (!validInstruction)
				return false;
		}

		if (!validInstruction)
			return false;
		return true;
	}

	private void checkFirstBlock() {
		// If no instructions
		if (blocks.Length == 0) {
			validInstruction = false;
			return;
		}

		// Check instruction is at least min length
		if (blocks.Length < minNumBlocks) {
			validInstruction = false;
			return;
		}
		// First instruction is an IF?
		if (blocks [blockNum].typeOfBlock != BlockType.IF)
			validInstruction = false;
	}

	private void nextBlock() {
		// End of instruction already reached
		if (endReached == true)
			return;
		
		blockNum++;

		// Check if end of instruction has been reached prematurely
		if (blocks.Length < (blockNum + 1)) {
			validInstruction = false;
			endReached = true;
			blockNum--;
		}
	}

	private void checkBlock() {
		switch (blocks [blockNum - 1].typeOfBlock) {
		// Conditions
		case BlockType.IF:
		case BlockType.ELSEIF:
		case BlockType.AND:
		case BlockType.OR:
			isCodeProperty ();
			return;
		case BlockType.ELSE:
			checkElseStatement ();
			return;
		// Code properties
		case BlockType.ENCRYPTION:
			checkEncryption ();
			return;
		case BlockType.EXTENSION:
			checkExtension ();
			return;
		case BlockType.SPEED:
		case BlockType.SIZE:
			checkInteger ();
			return;
		case BlockType.SOURCE:
			checkSource ();
			return;
		// Code property values
		case BlockType.TRUE:
		case BlockType.FALSE:
		case BlockType.INTEGER:
		case BlockType.SOURCE_PROP:
		case BlockType.EXTENSION_PROP:
			checkPostValue ();
			return;
		// Shoot/Don't Shoot
		case BlockType.SHOOT:
		case BlockType.DONTSHOOT:
			checkPostShoot ();
			return;
		// Default
		default:
			validInstruction = false;
			return;	
		}
	}

	// check current blocktype is a code property
	private void isCodeProperty(){
		BlockType bt = blocks [blockNum].typeOfBlock;
		switch (bt) {
		case BlockType.ENCRYPTION:
		case BlockType.EXTENSION:
		case BlockType.SPEED:
		case BlockType.SIZE:
		case BlockType.SOURCE:
			return;
		default: 
			validInstruction = false;
			return;
		}
	}

	// check instruction ends with valid else statement
	private void checkElseStatement(){
		BlockType bt = blocks [blockNum].typeOfBlock;

		// Last block must be either shoot or dont shoot
		if (bt != BlockType.SHOOT && bt != BlockType.DONTSHOOT) {
			validInstruction = false;
			return;
		}

		// current block should be the last
		if (blocks.Length == (blockNum + 1)) {
			endReached = true;
		} else {
			validInstruction = false;
		}	
	}

	// check current block is boolean
	private void checkEncryption(){
		BlockType bt = blocks [blockNum].typeOfBlock;
		if (bt != BlockType.EQUALS) {
			validInstruction = false;
			return;
		}

		nextBlock ();

		bt = blocks [blockNum].typeOfBlock;
		if (bt != BlockType.TRUE && bt != BlockType.FALSE)
			validInstruction = false;
	}

	private void checkExtension(){
		BlockType bt = blocks [blockNum].typeOfBlock;
		if (bt != BlockType.EQUALS) {
			validInstruction = false;
			return;
		}

		nextBlock ();

		bt = blocks [blockNum].typeOfBlock;
		if (bt != BlockType.EXTENSION_PROP)
			validInstruction = false;
	}

	// Check current block is an integer
	private void checkInteger(){
		BlockType bt = blocks [blockNum].typeOfBlock;
		if (bt != BlockType.EQUALS && bt != BlockType.LESS && bt != BlockType.GREATER) {
			validInstruction = false;
			return;
		}

		nextBlock ();

		bt = blocks [blockNum].typeOfBlock;
		if (bt != BlockType.INTEGER)
			validInstruction = false;
	}

	// Check current block is a source property e.g. trusted, known
	private void checkSource(){
		BlockType bt = blocks [blockNum].typeOfBlock;
		if (bt != BlockType.EQUALS) {
			validInstruction = false;
			return;
		}

		nextBlock ();

		bt = blocks [blockNum].typeOfBlock;
		if (bt != BlockType.SOURCE_PROP)
			validInstruction = false;
	}

	private void checkPostValue(){
		BlockType bt = blocks [blockNum].typeOfBlock;

		switch (bt) {
		case BlockType.SHOOT:
		case BlockType.DONTSHOOT:
			if(blocks.Length == (blockNum + 1))
				endReached = true;
			return;
		case BlockType.AND:
		case BlockType.OR:
			return;
		default:
			validInstruction = false;
			return;
		}
	}

	private void checkPostShoot(){
		BlockType bt = blocks [blockNum].typeOfBlock;

		switch(bt){
		case BlockType.ELSE:
		case BlockType.ELSEIF:
			return;
		default:
			validInstruction = false;
			return;
		}
	}

	/*
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
		if (blocks [instructNum].typeOfBlock == BlockType.IF) {
			if (!nextInstruct ())
				return false;
			enemy ();
			if (!nextInstruct ())
				return false;
			instruction ();
		} 
		// First instruction is SHOOT or DON'T SHOOT
		else if (blocks [instructNum].typeOfBlock == BlockType.SHOOT ||
		         blocks [instructNum].typeOfBlock == BlockType.DONTSHOOT) {
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
		if (blocks [instructNum].typeOfBlock != BlockType.ENEMY)
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
		if (blocks [instructNum].typeOfBlock == BlockType.OR)
			return true;
		return false;
	}

	// Check if instruction is SHOOT/DON'T SHOOT
	private bool isShootInstruct(){
		if (blocks [instructNum].typeOfBlock == BlockType.SHOOT)
			return true;
		else if (blocks [instructNum].typeOfBlock == BlockType.DONTSHOOT)
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
		if (blocks [instructNum].typeOfBlock == BlockType.ELSE) {
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
		} else if (blocks [instructNum].typeOfBlock == BlockType.ELSEIF) {
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
	*/
}
