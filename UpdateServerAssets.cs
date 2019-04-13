/* 
 * define model sprite tex 
 * _schleichmodel
 * _schleichtex
 * _schleichsprite
 * 
 * _mod
 * _mat
 * _tex
 * _spr
 * 
 * create materials
 * hard replace
 * else update if file is older
 * 
 */

using UnityEngine;
using UnityEditor;
using System.IO;


public class UpdateServerAssets : ScriptableWizard
{
    static string extPath = null;

    public static bool hardReplace = true;

    public static bool updMod = true;
    public static bool updTex = true;
    public static bool updSpr = true;

    public static string extMod;
    public static string extTex;
    public static string extSpr;

    public static string intMod;
    public static string intTex;
    public static string intSpr;

    public string setextMod = "_mod";
    public string setextTex = "_tex";
    public string setextSpr = "_spr";
				  
    public string setintMod = "_mod";
    public string setintTex = "_tex";
    public string setintSpr = "_spr";

    public bool setupdMod = true;
    public bool setupdTex = true;
    public bool setupdSpr = true;
    public bool setHardReplace = true;
    public string setPath = "";


    [MenuItem("My Tools/Update With Server Assets...")]
    static void CreateWizard()
    {
        // Creates the wizard for display
        ScriptableWizard.DisplayWizard("Title", typeof(UpdateServerAssets), "Execute", "Folder");
    }

    void OnWizardUpdate()
    {
        extPath = setPath;
        hardReplace = setHardReplace;

        updMod = setupdMod;
        extMod = setextMod;
        intMod = setintMod;

        updTex = setupdTex;
        extTex = setextTex;
        intTex = setintTex;

        updSpr = setupdSpr;
        extSpr = setextSpr;
        intSpr = setintSpr;
    }

    void OnWizardCreate()
    {

        if (hardReplace) {

            if (updMod) {

                replaceElements(extMod, intMod);
            }
            if (updTex)
            {

                replaceElements(extTex, intTex);
            }
            if (updSpr)
            {

                replaceElements(extSpr, intSpr);
            }
        }
    }

    void replaceElements(string exFolder, string inFolder) {
     
        string[] files = Directory.GetFiles(extPath +"/"+ exFolder);

        foreach (string file in files)
        {
          
            if (hardReplace) {

                File.Copy(file, Application.dataPath + "/" + inFolder + "/" + Path.GetFileName(file), true);
            } else {

                if (File.GetLastWriteTime(file)> File.GetLastWriteTime(Application.dataPath + "/" + inFolder + "/" + Path.GetFileName(file))) {

                    File.Copy(file, Application.dataPath + "/" + inFolder + "/" + Path.GetFileName(file), true);
                }
            }
        }
    }


    void OnWizardOtherButton()
    {
        setPath = EditorUtility.OpenFolderPanel("Select Project Folder on Server", "", "");
    }

}