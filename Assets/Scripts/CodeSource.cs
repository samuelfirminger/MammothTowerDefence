using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CodeSource {
	// Must update parser checkSource() method if this is changed
	Trusted,
	Known,
	Unknown,
	Suspicious
};
