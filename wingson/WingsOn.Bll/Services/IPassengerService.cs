using System.Collections.Generic;
using WingsOn.Bll.SearchCriteria;
using WingsOn.Domain;

namespace WingsOn.Bll.Services
{
    public interface IPassengerService
    {
        IEnumerable<Person> GetPassengers(PersonSearchCriterion personSearch);

        Person GetPassenger(int id);

        Person CreatePassenger(Person passenger);

        void UpdatePassenger(int id, Person passenger);
    }
}