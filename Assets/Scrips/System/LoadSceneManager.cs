using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : BYSingletonMono<LoadSceneManager>
{
    // Start is called before the first frame update
    public GameObject ui_object;
    public Image image_progress;
    public Text progress_lb;
    //public float time_delay=0.1f;
    //public float width = 1980;

    // Update is called once per frame
    /// <summary>
    /// Load new scene by name
    /// </summary>
    /// <param name="scene_name"></param>
    /// <param name="callback"></param>
    public void LoadSceneByName(string scene_name, Action callback)
    {
        StartCoroutine(LoadSceneByNameProgress(scene_name, callback));
    }
    IEnumerator LoadSceneByNameProgress(string scene_name, Action callback)
    {
        ui_object.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Single);
        //WaitForSeconds wait_s = new WaitForSeconds(time_delay);
        WaitForSeconds wait_s = new WaitForSeconds(0.1f);
        int count = 0;
        while (count < 50)
        {
            yield return wait_s;
            count++;
            progress_lb.text = count.ToString() + "%";
            image_progress.fillAmount=(float)count/100f;
            //image_progress.rectTransform.sizeDelta = new Vector2(width * (float)count / 100f, 42);
        }
        while (!async.isDone)
        {
            yield return wait_s;
            progress_lb.text = ((int)(async.progress * 100)).ToString() + "%";
            image_progress.fillAmount = async.progress;
            //progress_lb.text = ((int)(async.progress * 100)).ToString() + "%";
            //image_progress.rectTransform.sizeDelta = new Vector2(width * async.progress, 42);
        }
        callback?.Invoke();
        ui_object.SetActive(false);
    }
    public void LoadSceneByIndex(int scene_index, Action callback)
    {
        StartCoroutine(LoadSceneByIndexProgress(scene_index, callback));
    }
    IEnumerator LoadSceneByIndexProgress(int scene_index, Action callback)
    {
        ui_object.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene_index, LoadSceneMode.Single);
        WaitForSeconds wait_s = new WaitForSeconds(0.1f);
        //WaitForSeconds wait_s = new WaitForSeconds(time_delay);
        int count = 0;
        while (count < 50)
        {
            yield return wait_s;
            count++;
            progress_lb.text = count.ToString() + "%";
            image_progress.fillAmount = (float)count / 100f;
            //image_progress.rectTransform.sizeDelta = new Vector2(width * (float)count / 100f, 42);
        }
        while (!async.isDone)
        {
            yield return wait_s;
            progress_lb.text = ((int)(async.progress * 100)).ToString() + "%";
            image_progress.fillAmount = async.progress;
           // image_progress.rectTransform.sizeDelta = new Vector2(width * async.progress, 42);
        }
        callback?.Invoke();
        ui_object.SetActive(false);
    }
    IEnumerator Wait()
    {
        //1
        yield return new WaitForSeconds(1);

        //2
        yield return new WaitForSeconds(1);
        //3
        yield return new WaitForSeconds(1);
        //
        yield return null;
        //
    }
}
