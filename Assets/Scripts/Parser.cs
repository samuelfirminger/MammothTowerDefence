using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Parser : MonoBehaviour {
	private Draggable[] blocks = null;
	private int minNumBlocks = 5;
	private int blockNum = 0;
	private bool validInstruction = true;
	private bool endReached = false;
    public GameObject errorPanel;
    
	public void nextScene(){
		if (parse ()) {
			BetweenScenes.parsedInstructionSet = blocks;
			SceneManager.LoadScene(BetweenScenes.CurrentLevel);
		} else {
			errorPanel.SetActive(true);
		}
	}

	private bool parse() {
		blockNum = 0;
		validInstruction = true;
		endReached = false;
		//blocks = this.GetComponentsInChildren<Draggable> ();

		GameObject g = GameObject.Find ("ProgramTower");
		blocks = g.GetComponentsInChildren<Draggable> ();

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
		if (bt != BlockType.EXTENSION_PROP) {
			validInstruction = false;
			return;
		}

		//Dropdown dropDown = blocks [blockNum].GetComponent (typeof(Dropdown)) as Dropdown;
		Dropdown dropDown = blocks[blockNum].GetComponentInChildren(typeof(Dropdown)) as Dropdown;

		switch (dropDown.value) {
		case 0:
			blocks [blockNum].setCodeExtension (CodeExtension.BAT);
			return;
		case 1:
			blocks [blockNum].setCodeExtension (CodeExtension.EXE);
			return;
		case 2:
			blocks [blockNum].setCodeExtension (CodeExtension.SYS);
			return;
		case 3:
			blocks [blockNum].setCodeExtension (CodeExtension.XLS);
			return;
		default:
			return;
		}
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
		else {
			InputField inputField = blocks [blockNum].GetComponentInChildren (typeof(InputField)) as InputField;
			string text = inputField.text;

			// check user has made an entry
			if (text == null || text.Equals ("")) {
				validInstruction = false;
				return;
			} 

			int num = int.Parse (inputField.text);
			// check user entry is >0
			if (num < 0) {
				validInstruction = false;
				return;
			}
				
			blocks [blockNum].setIntValue (num);
		}
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
		if (bt != BlockType.SOURCE_PROP) {
			validInstruction = false;
			return;
		}

		Dropdown dropDown = blocks [blockNum].GetComponentInChildren (typeof(Dropdown)) as Dropdown;

		switch (dropDown.value) {
		case 0:
			blocks [blockNum].setCodeSource(CodeSource.Trusted);
			return;
		case 1:
			blocks [blockNum].setCodeSource(CodeSource.Known);
			return;
		case 2:
			blocks [blockNum].setCodeSource(CodeSource.Unknown);
			return;
		case 3:
			blocks [blockNum].setCodeSource(CodeSource.Suspicious);
			return;
		default:
			return;
		}
		
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


}
