using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;

public class FilePicker : MonoBehaviour
{
    
    public void TranslationFile(TranslationUI translationUI, bool load){
        FileBrowser.SetFilters(true, new FileBrowser.Filter("TextFile", ".json"));
        FileBrowser.SetDefaultFilter(".json");
        FileBrowser.AddQuickLink( "Users", "C:\\Users", null );
        if(load){
            StartCoroutine(ShowLoadLanguagueDialogCoroutine(translationUI));
        }
        else{
            StartCoroutine(ShowSaveLanguagueDialogCoroutine());
        }
    }
    public void ImageFile(ImageUI imageUI, bool load){
        string[] filters = new string[2];
        filters[0] = ".jpg";
        filters[1] = ".png";
        filters[1] = ".jpeg";
        FileBrowser.SetFilters(true, filters);
        FileBrowser.SetDefaultFilter(".png");
        FileBrowser.AddQuickLink( "Users", "C:\\Users", null );
        if(load){
            StartCoroutine(ShowLoadImageDialogCoroutine(imageUI));
        }
        else{
            StartCoroutine(ShowSaveImageDialogCoroutine());
        }
    }
    IEnumerator ShowLoadLanguagueDialogCoroutine(TranslationUI translationUI)
	{
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load json File", "Load" );
		Debug.Log( FileBrowser.Success );
		if( FileBrowser.Success )
		{
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile( FileBrowser.Result[0] );
            if(bytes.Length != 0){
                translationUI.ImportedLanguage = JsonUtility.FromJson<Language>(System.Text.Encoding.UTF8.GetString(bytes));
                translationUI.DisplayTranslation(false);
            }
		}
	}
    IEnumerator ShowSaveLanguagueDialogCoroutine()
	{
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, false, "C:\\", TranslationStorage.CurrentLanguageName +".json", "Save As", "Save" );
		//yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load json File", "Load" );
		Debug.Log( FileBrowser.Success );
		if( FileBrowser.Success )
		{
            //string destinationPath = Path.Combine( Application.persistentDataPath, FileBrowserHelpers.GetFilename( FileBrowser.Result[0] ) );
            var languageData = JsonUtility.ToJson(TranslationStorage.AllLanguages.Find(x => x.LanguageName == TranslationStorage.CurrentLanguageName), true);
            if (languageData != null)
                File.WriteAllBytes(FileBrowser.Result[0], System.Text.Encoding.UTF8.GetBytes(languageData));
        }
	}
    IEnumerator ShowLoadImageDialogCoroutine(ImageUI imageUI)
	{
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load Image", "Load" );
		Debug.Log( FileBrowser.Success );
		if( FileBrowser.Success )
		{
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile( FileBrowser.Result[0] );
            if(bytes.Length != 0){
                imageUI.setImage(bytes, FileBrowserHelpers.GetFilename( FileBrowser.Result[0]));
                //translationUI.DisplayTranslation(false);
            }
		}
	}
    IEnumerator ShowSaveImageDialogCoroutine()
	{
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, false, "C:\\", TranslationStorage.CurrentLanguageName +".json", "Save As", "Save" );
		//yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load json File", "Load" );
		Debug.Log( FileBrowser.Success );
		if( FileBrowser.Success )
		{
            //string destinationPath = Path.Combine( Application.persistentDataPath, FileBrowserHelpers.GetFilename( FileBrowser.Result[0] ) );
            var languageData = JsonUtility.ToJson(TranslationStorage.AllLanguages.Find(x => x.LanguageName == TranslationStorage.CurrentLanguageName), true);
            Debug.Log(languageData);
            if (languageData != null)
                File.WriteAllBytes(FileBrowser.Result[0], System.Text.Encoding.UTF8.GetBytes(languageData));
        }
	}
}
