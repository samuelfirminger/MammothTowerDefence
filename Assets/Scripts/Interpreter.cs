using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script controlling the interpreter of the DragNDrop interface
public class Interpreter {
	private Draggable[] blocks = null;
	private Transform enemyPrefab = null;
	private int blockNum = 0;
	private bool conditionMet = false;
	private bool endReached = false;
	private bool isEnemy = false;

    //Interpret player statements
	public void interpret(Transform enemyPrefab, Draggable[] blocks){
		this.blocks = blocks;
		this.enemyPrefab = enemyPrefab;
		blockNum = 0;
		conditionMet = false;
		endReached = false;
		isEnemy = false;

		while (!endReached) {
			blockNum++;
			runInterpret ();
		}

		Enemy enemy = enemyPrefab.GetComponent (typeof(Enemy)) as Enemy;

		enemy.isEnemy = isEnemy;
	}

	private void runInterpret(){
		BlockType bt = blocks [blockNum].typeOfBlock;

		switch (bt) {
		case BlockType.SIZE:
			checkSize ();
			return;
		case BlockType.SPEED:
			checkSpeed ();
			return;
		case BlockType.SOURCE:
			checkSource();
			return;
		case BlockType.EXTENSION:
			checkExtension ();
			return;
		case BlockType.ENCRYPTION:
			checkEncryption ();
			return;
		case BlockType.OR:
			checkOr ();
			return;
		case BlockType.AND:
			checkAnd ();
			return;
		case BlockType.ELSEIF:
			return;
		case BlockType.ELSE:
			checkElse ();
			return;
		case BlockType.SHOOT:
		case BlockType.DONTSHOOT:
			checkShootStatement ();
			return;
		default:
			return;
		}
	}

    //Individual functions used to check each paramater of the user statement
	private void checkExtension(){
		blockNum += 2;

		CodeProperties cp = enemyPrefab.GetComponent (typeof(CodeProperties)) as CodeProperties;

		conditionMet = (blocks[blockNum].getCodeExtension() == cp.extension);
	}

	private void checkSource(){
		blockNum += 2;
		CodeProperties cp = enemyPrefab.GetComponent (typeof(CodeProperties)) as CodeProperties;

		conditionMet = (blocks [blockNum].getCodeSource() == cp.source);
	}

	private void checkEncryption(){
		blockNum += 2;
		BlockType bt = blocks [blockNum].typeOfBlock;
		CodeProperties cp = enemyPrefab.GetComponent (typeof(CodeProperties)) as CodeProperties;

		if (bt == BlockType.TRUE && cp.encryption == true) {
			conditionMet = true;
		} else if (bt == BlockType.FALSE && cp.encryption == false) {
			conditionMet = true;
		} else {
			conditionMet = false;
		}
	}

	private void checkElse(){
		setIsEnemy ();
	}

	private void checkShootStatement(){
		if (conditionMet) {
			setIsEnemy ();
			return;
		}
			
		if(blocks.Length == (blockNum + 1))
			endReached = true;
	}

	private void checkAnd(){
		if (conditionMet)
			return;

		while (blocks [blockNum].typeOfBlock != BlockType.SHOOT &&
			blocks [blockNum].typeOfBlock != BlockType.DONTSHOOT) {
			blockNum++;
		}
	}

	private void checkOr(){
		if (!conditionMet)
			return;

		setIsEnemy ();
	}

	private void setIsEnemy(){
		while (blocks [blockNum].typeOfBlock != BlockType.SHOOT &&
		      blocks [blockNum].typeOfBlock != BlockType.DONTSHOOT) {
			blockNum++;
		}

		if (blocks [blockNum].typeOfBlock == BlockType.SHOOT)
			isEnemy = true;

		endReached = true;
	}

	private void checkSize(){
		blockNum++;
		BlockType bt = blocks [blockNum].typeOfBlock;
		blockNum++;
		CodeProperties cp = enemyPrefab.GetComponent (typeof(CodeProperties)) as CodeProperties;

		switch (bt) {
		case BlockType.GREATER:
			conditionMet = (cp.size > blocks [blockNum].getIntValue());
			return;
		case BlockType.LESS:
			conditionMet = (cp.size < blocks [blockNum].getIntValue());
			return;
		case BlockType.EQUALS:
			conditionMet = (cp.size == blocks [blockNum].getIntValue());
			return;
		default:

			return;
		}
	}

	private void checkSpeed(){
		blockNum++;
		BlockType bt = blocks [blockNum].typeOfBlock;
		blockNum++;
		CodeProperties cp = enemyPrefab.GetComponent (typeof(CodeProperties)) as CodeProperties;

		switch (bt) {
		case BlockType.GREATER:
			conditionMet = (cp.speed > blocks [blockNum].getIntValue());
			return;
		case BlockType.LESS:
			conditionMet = (cp.speed < blocks [blockNum].getIntValue());
			return;
		case BlockType.EQUALS:
			conditionMet = (cp.speed == blocks [blockNum].getIntValue());
			return;
		default:

			return;
		}
	}
}
