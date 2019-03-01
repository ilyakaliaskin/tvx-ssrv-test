using System.Collections.Generic;
using System.Linq;
using WingsOn.Bll.SearchCriteria;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Exceptions;

namespace WingsOn.Bll.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IRepository<Person> _personRepository;

        public PassengerService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public IEnumerable<Person> GetPassengers(PersonSearchCriterion personSearch)
        {
            return _personRepository.GetAll().Where(person => personSearch.Gender == null || person.Gender == personSearch.Gender);
        }

        public Person GetPassenger(int id)
        {
            return _personRepository.Get(id)

                ?? throw new ResourceNotFoundException($"Passenger with the Id specified does not exist: {id}.");
        }

        public Person CreatePassenger(Person passenger)
        {
            passenger.Id = GetNewPersonId();

            _personRepository.Save(passenger);

            return _personRepository.Get(passenger.Id);
        }

        public void UpdatePassenger(int id, Person passenger)
        {
            passenger.Id = id;

            _personRepository.Save(passenger);
        }

        private int GetNewPersonId()
        {
            return _personRepository.GetAll().Max(person => person.Id) + 1;
        }
    }
}
