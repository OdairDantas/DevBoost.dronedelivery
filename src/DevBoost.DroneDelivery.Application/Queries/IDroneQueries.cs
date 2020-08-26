using DevBoost.DroneDelivery.Application.Queries.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public interface IDroneQueries
    {
        Task<DroneViewModel> ObterDroneDisponiveil();
        Task<DroneViewModel> ObterDrone(Guid id);
        Task<IEnumerable<DroneViewModel>> ObterDrones();
        Task<SituacaoViewModel> ObterSituacao(Guid id);
    }
}
