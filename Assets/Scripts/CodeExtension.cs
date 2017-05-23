using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that encodes the possible code extensions for enemies shown in briefings
public enum CodeExtension {
	// Must update parser checkExtension() method if this is changed
	BAT,
	EXE,
	SYS,
	XLS
}
