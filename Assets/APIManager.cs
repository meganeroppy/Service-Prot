using UnityEngine;
using System.Collections;

public class APIManager : MonoBehaviour {
	public string api_url="http://52.88.102.36/api/"; // <-自分のWEBページのIPアドレス
	public string api_put="put.php";
	public string api_get="get.php";
	public string api_id="more_cat";

	private string api_type="long";
	private string get_ret="";
	private string put_ret="";

	private string ret_com="";
	private string ret_data="";

	string api_putstr(long data){
		string api;
		api = api_url + api_put + "?id=" + api_id + "&type=" + api_type + "&value="+data.ToString();
		return api;
	}

	string api_getstr(){
		string api;
		api = api_url + api_get + "?id=" + api_id + "&type=" + api_type;
		return api;
	}

	public IEnumerator get_api(){
		string[] rets;

		WWW www = new WWW(api_getstr());
		yield return www;
		get_ret = www.text;

		// print(get_ret);

		rets = get_ret.Split (':');
		ret_com=rets[0];
		if (ret_com == "S"){ 
			ret_data = rets [1];
		//	Debug.Log( "get 成功! 値 = " + ret_data );

		}else{
			Debug.LogError( "get 失敗" );
			ret_data = "";
		}
	}

	public IEnumerator put_api(long data){
		string[] rets;

		WWW www = new WWW(api_putstr(data));
		yield return www;
		put_ret = www.text;

		// print(put_ret);

		rets = put_ret.Split (':');
		ret_com=rets[0]; 
		if (ret_com.Contains("S")){ 
		//	Debug.Log( "put 成功!");
		}else{
			Debug.LogError( "put 失敗" );
		}
	}

	public string GetValue(){
		return ret_data;
	}


}
