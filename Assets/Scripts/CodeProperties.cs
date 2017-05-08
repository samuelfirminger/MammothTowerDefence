using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ways of identifying "good" and "bad" code
public struct CodeProperties {

	//the extension of the file
	//eg .zip, .exe etc.
	string extension;

	int speed;
	int size;

	bool encryption;

	//the author of the file
	CodeSource source;

}
