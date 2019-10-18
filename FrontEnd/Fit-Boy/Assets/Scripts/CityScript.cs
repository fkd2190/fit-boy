using UnityEngine;
using UnityEngine.UI;

public class CityScript : MonoBehaviour
{
    public bool knowsWeather;
    public WeatherAPI weather;
    public Button button;
    public Sprite clouds;
    public Sprite rain;
    public Sprite clear;
    Text cityText;
    Text wind;
    Text city;
    Text desc;


    void Start()
    {
        knowsWeather = false;
        cityText = GameObject.Find("City").GetComponent<Text>();
        wind = GameObject.Find("Wind").GetComponent<Text>();
        city = GameObject.Find("Location").GetComponent<Text>();
        desc = GameObject.Find("Desc").GetComponent<Text>();
        weather = gameObject.AddComponent<WeatherAPI>();
        weather.url = "http://api.openweathermap.org/data/2.5/weather?q=Auckland&APPID=f345b05b38a618ce356cc179bafec0e0";
    }
    // Update is called once per frame
    void Update()
    {
        if (!knowsWeather && weather.temp != 0)
        {
            cityText.text = (int)(weather.temp - 273.15) + " °C";
            wind.text = "Wind speed: "+weather.wind;
            city.text = "City: "+weather.city;
            desc.text = weather.weatherDescription;
            knowsWeather = true;
            if(weather.weatherDescription.Contains("cloud"))
            {
                button.GetComponent<Image>().sprite = clouds;
            }
            else if(weather.weatherDescription.Contains("clear"))
            {
                button.GetComponent<Image>().sprite = clear;
            }
            else if (weather.weatherDescription.Contains("rain"))
            {
                button.GetComponent<Image>().sprite = rain;
            }
        }
    }

    void UpdateInfoPanel()
    {

    }
}