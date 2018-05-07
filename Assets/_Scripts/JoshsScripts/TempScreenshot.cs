using UnityEngine;
using System.Collections;
using System;

public class TempScreenshot : MonoBehaviour
{
    public RenderTexture overviewTexture;
    GameObject OVcamera;
    public string path = "";

    private bool doThing = false;

    void Start()
    {
        OVcamera = GameObject.FindGameObjectWithTag("OverviewCamera");
    }

    void LateUpdate()
    {

        if (doThing == true)
        {
            StartCoroutine(TakeScreenShot());
        }

    }

    // return file name
    string FileName(int width, int height)
    {
        return string.Format("screen_{0}x{1}_{2}.png",
                              width, height,
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void MakeThingGo() {
        doThing = true;
    }

    public IEnumerator TakeScreenShot()
    {

        OVcamera.gameObject.SetActive(true);

        yield return new WaitForEndOfFrame();

        Camera camOV = OVcamera.GetComponent<Camera>();

        RenderTexture currentRT = RenderTexture.active;

        RenderTexture.active = camOV.targetTexture;
        camOV.Render();
        Texture2D imageOverview = new Texture2D(camOV.targetTexture.width, camOV.targetTexture.height, TextureFormat.RGB24, false);
        imageOverview.ReadPixels(new Rect(0, 0, camOV.targetTexture.width, camOV.targetTexture.height), 0, 0);
        imageOverview.Apply();
        RenderTexture.active = currentRT;


        // Encode texture into PNG
        byte[] bytes = imageOverview.EncodeToPNG();

        // save in memory
        string filename = FileName(Convert.ToInt32(imageOverview.width), Convert.ToInt32(imageOverview.height));
        path = Application.persistentDataPath + "/Snapshots/" + filename;

        System.IO.File.WriteAllBytes(path, bytes);

        OVcamera.gameObject.SetActive(false);
    }
}