using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTellDataManager : MonoBehaviour
{
	private static StoryTellDataManager _instance;

	public static StoryTellDataManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new StoryTellDataManager();
			}
			return _instance;
		}
	}


	public static int count = 0;
    public static GameObject[] go;
}
