//Defines block types for the DragNDrop Interface

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType {
	// Conditions
	IF, 
	ELSE, 
	ELSEIF, 
	OR,
	AND,

	// Operators
	GREATER,
	LESS,
	EQUALS,

	// Shoot instructions
	SHOOT, 
	DONTSHOOT, 

	// Code Properties
	EXTENSION,
	SPEED,
	SIZE,
	ENCRYPTION,
	SOURCE,

	// Size/Speed property values
	INTEGER,

	//Encryption property values
	TRUE,
	FALSE,

	// Extension property values
	EXTENSION_PROP,

	// Source property values
	SOURCE_PROP
}
