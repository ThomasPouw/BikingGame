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
            StartCoroutine(ShowLoadDialogCoroutine(translationUI));
        }
        else{
            StartCoroutine(ShowSaveDialogCoroutine());
        }
    }
    IEnumerator ShowLoadDialogCoroutine(TranslationUI translationUI)
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
    IEnumerator ShowSaveDialogCoroutine()
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
