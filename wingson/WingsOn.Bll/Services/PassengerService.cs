﻿using System.Collections.Generic;
using System.Linq;
using WingsOn.Bll.SearchCriteria;
using WingsOn.Dal;
using WingsOn.Domain;

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
            return _personRepository.Get(id);
        }

        public Person CreatePassenger(Person passenger)
        {
            passenger.Id = GetNewPersonId();

            _personRepository.Save(passenger);

            return _personRepository.Get(passenger.Id);
        }

        private int GetNewPersonId()
        {
            return _personRepository.GetAll().Max(person => person.Id) + 1;
        }
    }
}
