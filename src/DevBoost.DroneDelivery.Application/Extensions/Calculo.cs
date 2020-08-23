using System.Device.Location;
using DevBoost.DroneDelivery.Domain.ValueObjects;

namespace DevBoost.DroneDelivery.Application.Extensions
{
    public static class Calculo
    {

        public static double CalcularDistanciaEmKilometros(this Localizacao localizacao)
        {
            var origemCoord = new GeoCoordinate(-23.5880684, -46.6564195);
            var destinoCoord = new GeoCoordinate(localizacao.Latitude, localizacao.Longitude);

            var distance = origemCoord.GetDistanceTo(destinoCoord);
            
            if (distance > 0)
                distance /= 1000;

            return distance;
        }
    }


}
