/*
    *** API sample program ***
    Programmer: Keiji Mitsubuchi
    Date : 2014.10.26
*/

using UnityEngine;
using System.Collections;

public class APItest : MonoBehaviour {
	public string api_url="http://52.88.102.36/api/"; // <-自分のWEBページのIPアドレス
	public string api_put="put.php";
	public string api_get="get.php";
	public string api_id="G151TG2035";

	private int sw;
	private int sh;
	private string api_type="long";
	private GUIContent msg;
	private string ldata ="0";
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

	IEnumerator get_api(){
		string[] rets;

		WWW www = new WWW(api_getstr());
		yield return www;
		get_ret = www.text;
		rets = get_ret.Split (':');
		ret_com=rets[0];
		if (ret_com == "S") ret_data = rets [1];
		else ret_data = "";
	}
	
	IEnumerator put_api(long data){
		string[] rets;

		WWW www = new WWW(api_putstr(data));
		yield return www;
		put_ret = www.text;

		rets = put_ret.Split (':');
		ret_com=rets[0]; 
		ret_data = "";
	}
	
	// Use this for initialization
	void Start () 
	{
		sw = Screen.width;
		sh = Screen.height;		
		msg = new GUIContent();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){	
		int lx = 10,lw = 80;
		int tx = 100,tw = 250;
		int y0 = 10, yh=20,yd = 25;

		GUI.Box(new Rect(5,5,500,300),"Box");

		GUI.Label (new Rect(lx,y0,lw,yh),"API URL");
		GUI.TextField(new Rect(tx,y0,tw,yh),api_url);
		y0 += yd;
		GUI.Label (new Rect(lx,y0,lw,yh),"API PUT");
		GUI.TextField(new Rect(tx,y0,tw,yh),api_url+api_put);
		y0 += yd;
		GUI.Label (new Rect(lx,y0,lw,yh),"API GET");
		GUI.TextField(new Rect(tx,y0,tw,yh),api_url+api_get);
		y0 += yd;
		GUI.Label (new Rect(lx,y0,lw,yh),"STD ID");
		api_id = GUI.TextField (new Rect (tx, y0, 100, yh), api_id);
		y0 += yd;
		y0 += yd;

		if (GUI.Button(new Rect(lx,y0,80,20),"GET")) {
			StartCoroutine(get_api());
		}
		GUI.TextField (new Rect (tx, y0, tw, yh), get_ret);
		y0 += yd;

		ldata = GUI.TextField (new Rect (lx, y0, lw, yh), ldata);
		y0 += yd;

		if (GUI.Button(new Rect(lx,y0,80,20),"PUT")) {
			long d=0L;
			if(long.TryParse(ldata,out d)) StartCoroutine(put_api(d));
			else put_ret="No long data";
		}
		GUI.TextField (new Rect (tx, y0, tw, yh), put_ret);
		y0 += yd;
		GUI.Label (new Rect(lx,y0,lw,yh),"Return Comm");
		GUI.TextField(new Rect(tx,y0,lw,yh),ret_com);
		GUI.TextField(new Rect(tx+lw+10,y0,lw,yh),ret_data);
		y0 += yd;

	}

}
