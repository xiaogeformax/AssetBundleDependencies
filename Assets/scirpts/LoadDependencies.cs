using UnityEngine;
using System.Collections;

public class LoadDependencies : MonoBehaviour
{

    //总的minifest名称
    public string manifestName;
    //要加载AssetBundle名称
    public string assetBundleName;

    IEnumerator Start()
    {
        //assetBundle的路径
        string assetBundlePath = "file://" + Application.dataPath + "/AssetBundles/";
        //manifest 文件路径
        string manifestPath = assetBundlePath + manifestName;

        //先加载manifest文件
        WWW wwwManifest = new WWW(manifestPath);
            //WWW.LoadFromCacheOrDownload(manifestPath, 0);
        yield return wwwManifest;

        if (wwwManifest.error == null)
        {
            //解压
            AssetBundle manifestBundle = wwwManifest.assetBundle;
            AssetBundleManifest manifest = (AssetBundleManifest) manifestBundle.LoadAsset("AssetBundleManifest");
            manifestBundle.Unload(false);

            //获取依赖文件列表,只是列表,不是文件
            string[] dependentAssetBundles = manifest.GetAllDependencies(assetBundleName);

            AssetBundle[] abs = new AssetBundle[dependentAssetBundles.Length];

            for (int i = 0; i < dependentAssetBundles.Length; i++)
            {
                //加载所有依赖文件 
                WWW www = new WWW(assetBundlePath + dependentAssetBundles[i]);
                    //WWW.LoadFromCacheOrDownload(assetBundlePath + dependentAssetBundles[i], 0);
                yield return www;
                abs[i] = www.assetBundle;

            }
            //加载需要的文件
            WWW wwwSphere = new WWW(assetBundlePath + assetBundleName);
                //WWW.LoadFromCacheOrDownload(assetBundlePath + assetBundleName, 0);
            yield return wwwSphere;
            AssetBundle assetBundle = wwwSphere.assetBundle;


            WWW wwwCube =new WWW(assetBundlePath + "Cube");
                //WWW.LoadFromCacheOrDownload(assetBundlePath + "Cube", 0);
            yield return wwwCube;
            AssetBundle assetBundle1 = wwwCube.assetBundle;

            //加载资源
            GameObject gSphere = assetBundle.LoadAsset("Sphere") as GameObject;

            GameObject gCube = assetBundle1.LoadAsset("Cube") as GameObject;

            if (gSphere != null && gCube != null)
            {
                //实例化
                Instantiate(gSphere);
                Instantiate(gCube);
                assetBundle.Unload(false);
                assetBundle1.Unload(false);
            }
        }
        else
        {
            Debug.Log(wwwManifest.error);
        }
    }

}
