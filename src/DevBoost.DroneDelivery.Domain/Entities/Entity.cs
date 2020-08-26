using DevBoost.DroneDelivery.Domain.Menssages;
using System;
using System.Collections.Generic;

namespace DevBoost.DroneDelivery.Domain.Entities
{
    public abstract class Entity
    {

        private List<Event> _notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }
        public void AdicionarEvento(Event evento)
        {
            _notificacoes = _notificacoes ?? new List<Event>();
            _notificacoes.Add(evento);
        }

        public void RemoverEvento(Event eventItem)
        {
            _notificacoes?.Remove(eventItem);
        }

        public void LimparEventos()
        {
            _notificacoes?.Clear();
        }
        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
