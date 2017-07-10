using UnityEngine;
using System.Collections.Generic;
using Twitter;
using MiniJSON;

public class TwitterTest : MonoBehaviour {

    Stream stream;

    void Start() {
        // jsonの読み込み
        var textAsset = Resources.Load ("Config") as TextAsset;
        var jsonText = textAsset.text;
        var TwitterConfig = Json.Deserialize (jsonText) as Dictionary<string, object>;

        // AOuthの設定
        Twitter.Oauth.consumerKey = (string)TwitterConfig["consumerKey"];
        Twitter.Oauth.consumerSecret = (string)TwitterConfig["consumerSecret"];
        Twitter.Oauth.accessToken = (string)TwitterConfig["accessToken"];
        Twitter.Oauth.accessTokenSecret = (string)TwitterConfig["accessTokenSecret"];

        // ストリームの設定
        stream = new Stream(Type.User);
        Dictionary<string, string> streamParameters = new Dictionary<string, string>();
        streamParameters.Add("with", "followings");
        StartCoroutine(stream.On(streamParameters, this.OnStream));
    }

    void OnStream(string response) {
        try {
            Tweet Response = JsonUtility.FromJson<Tweet>(response);
            string tweet = Response.text;
            //ツイートの出力
            Debug.Log(tweet);
            // 自分にリプライが飛んできたとき(改善の余地あり)
            if (tweet.Split(' ')[0].Equals("@emusute1212")) {
                Debug.Log("get reply");
            }
        } catch (System.Exception e) {
            Debug.Log("Invalid Response");
        }
    }
}
