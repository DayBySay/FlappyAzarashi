using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FluctBanner))]
public class BannerEditor : Editor {

	public override void OnInspectorGUI()
	{
		var FluctBanner = target as FluctBanner;

		FluctBanner.Banner =
			EditorGUILayout.RectField ("Banner", FluctBanner.Banner);
	}
}
