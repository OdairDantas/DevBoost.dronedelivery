using DevBoost.DroneDelivery.Domain.Menssages;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Handles
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<bool> EnviarComando<T>(T comando) where T : Command;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    }
}
