using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScr : MonoBehaviour {

	void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
