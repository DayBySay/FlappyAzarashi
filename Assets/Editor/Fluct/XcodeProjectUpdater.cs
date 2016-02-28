using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using UnityEditor.iOS.Xcode;
using System.IO;

public class XcodeProjectUpdater : Editor {

	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget buildTarget, string path) {
		if (buildTarget == BuildTarget.iOS) {
			OnPostProcessBuildForiOS (buildTarget, path);
		}
	}

	public static void OnPostProcessBuildForiOS(BuildTarget buildTarget, string path) {
		string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
		PBXProject proj = new PBXProject();
		proj.ReadFromString(File.ReadAllText(projPath));
		string target = proj.TargetGuidByName("Unity-iPhone");

		proj.AddFrameworkToProject (target, "AdSupport.framework", false);
		proj.AddFrameworkToProject (target, "SystemConfiguration.framework", false);
		proj.AddFrameworkToProject (target, "AVFoundation.framework", false);
		proj.AddFrameworkToProject (target, "MediaPlayer.framework", false);
		proj.AddFrameworkToProject (target, "CFNetwork.framework", false);
		proj.AddFrameworkToProject (target, "CoreMedia.framework", false);
		proj.AddFrameworkToProject (target, "CoreTelephony.framework", false);
		proj.AddFrameworkToProject (target, "Social.framework", false);
		proj.AddFrameworkToProject (target, "StoreKit.framework", false);
		proj.AddFileToBuild(target, proj.AddFile("usr/lib/libxml2.dylib", "Frameworks/libxml2.dylib", PBXSourceTree.Sdk));

		File.WriteAllText(projPath, proj.WriteToString());
	}
}