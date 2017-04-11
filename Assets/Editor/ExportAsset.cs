using UnityEngine;
using System.Collections;
using UnityEditor;
public class ExportAsset : MonoBehaviour {

    [MenuItem("Export Editor/Build AssetBundles")]
    static void CreateAssetBundlesMain()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles");

    }
	
}
