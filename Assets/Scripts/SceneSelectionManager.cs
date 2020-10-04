using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneSelectionManager : MonoBehaviour
{
    public TMP_Dropdown dropDown;

    public void Awake()
    {
        List<TMP_Dropdown.OptionData> dropDownOptions = new List<TMP_Dropdown.OptionData >();
        // add all scenes to dropdown
        Debug.Log("number of scenes: "+SceneManager.sceneCountInBuildSettings);
        for(int i = 1; i<SceneManager.sceneCountInBuildSettings; i++)
        {
            //var scene = SceneManager.GetSceneByBuildIndex(i);
            var option = new TMP_Dropdown.OptionData(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
            Debug.Log(option);
            dropDownOptions.Add(option);
        }
        dropDown.AddOptions(dropDownOptions);
    }

    public void LoadScene(TMP_Dropdown sceneDropDown)
    {
        SceneManager.LoadScene(sceneDropDown.value+1);
    }
}
