using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script encodes the source properties of enemies shown in the briefings
public enum CodeSource {
	// Must update parser checkSource() method if this is changed
	Trusted,
	Known,
	Unknown,
	Suspicious
};
