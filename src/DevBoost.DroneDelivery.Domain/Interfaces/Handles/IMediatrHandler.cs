using DevBoost.DroneDelivery.Domain.Menssages;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Handles
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
