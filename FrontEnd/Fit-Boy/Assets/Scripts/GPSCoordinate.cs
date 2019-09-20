public class GPSCoordinate
{
    
    public double Lat;
    
    public double Lon;
    
    string Name = null;
    
    public GPSCoordinate(double lat, double lon, string name)
{
        this.Lat = lat;
        this.Lon = lon;
        this.Name = name;
    }
}
