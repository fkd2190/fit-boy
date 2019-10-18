using UnityEngine;
using System.Collections;
using SimpleJSON;

public class WeatherAPI : MonoBehaviour
{
    public string url = "http://api.openweathermap.org/data/2.5/weather?lat=35&lon=139&APPID=f345b05b38a618ce356cc179bafec0e0";
    public string city;
    public string weatherDescription;
    public float temp;
    public float temp_min;
    public float temp_max;
    public float rain;
    public float wind;
    public float clouds;
    
    public void Start()
    {
        StartCoroutine(Starts());
    }
    // Use this for initialization
    public IEnumerator Starts()
    {
        using (WWW request = new WWW(url))
        {
            yield return request;
            if (request.error == null || request.error == "")
            {
                setWeatherAttributes(request.text);
            }
            else
            {
                Debug.Log("Error: " + request.error);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    void setWeatherAttributes(string jsonString)
    {
        var weatherJson = JSON.Parse(jsonString);
        city = weatherJson["name"].Value;
        weatherDescription = weatherJson["weather"][0]["description"].Value;
        temp = weatherJson["main"]["temp"].AsFloat;
        temp_min = weatherJson["main"]["temp_min"].AsFloat;
        temp_max = weatherJson["main"]["temp_max"].AsFloat;
        rain = weatherJson["rain"]["3h"].AsFloat;
        clouds = weatherJson["clouds"]["all"].AsInt;
        wind = weatherJson["wind"]["speed"].AsFloat;
    }
}