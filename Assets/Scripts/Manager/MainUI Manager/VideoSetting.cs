using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VideoSetting : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolution;
    public TMPro.TMP_Dropdown displayMode;

    public Toggle screentoggle;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private int currentResolutionIndex;
    private FullScreenMode currentFullscreenMode;

    private void Start()
    {
        // InitialResolutions();
        // InitializeDisplayModes();

        screentoggle.isOn = !Screen.fullScreen;
    }


    void InitialResolutions()//拉取分辨率列表
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();


        var SeenResolutions = new HashSet<string>();
        for(int i = resolutions.Length - 1; i >= 0; i--)
        {
            string resolutionKey = $"{resolutions[i].width}×{resolutions[i].height}";
            if (!SeenResolutions.Contains(resolutionKey))
            {
                SeenResolutions.Add(resolutionKey);
                filteredResolutions.Add(resolutions[i]);
            }
        }

        resolution.ClearOptions();
        List<string> options = new List<string>();

        for(int i = 0; i < filteredResolutions.Count; i++)
        {
            string option = $"{filteredResolutions[i].width}×{filteredResolutions[i].height}";
            options.Add(option);

            if(filteredResolutions[i].width==Screen.width&&
                filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;//现在的分辨率
            }
        }

        resolution.AddOptions(options);
        resolution.value = currentResolutionIndex;
        resolution.RefreshShownValue();
    }

    void InitializeDisplayModes()
    {
        displayMode.ClearOptions();
        List<string> modes = new List<string>()
        {
            "全屏","窗口","无边框窗口"  
        };

        displayMode.AddOptions(modes);
        displayMode.value = (int)Screen.fullScreenMode;
        displayMode.RefreshShownValue();

    }

    //public void SetResolution(int resolution)
    //{
    //    currentResolutionIndex = resolution;
    //}

    //public void SetDisplayMode(int modeindex)
    //{
    //    currentFullscreenMode = (FullScreenMode)modeindex;
    //}

    //public void ApplySettings()
    //{
    //   // Resolution selected = filteredResolutions[currentResolutionIndex];
    //    //Screen.SetResolution(1920, 1080, currentFullscreenMode, selected.refreshRateRatio);
    //}

    public void ScreenModeToggle()
    {
        Screen.fullScreen = screentoggle.isOn;
    }
}
