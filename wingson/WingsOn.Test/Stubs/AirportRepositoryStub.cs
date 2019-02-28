using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Test.Stubs
{
    public class AirportRepositoryStub : RepositoryBase<Airport>
    {
        public AirportRepositoryStub()
        {
            Repository.AddRange(new []
            {
                new Airport
                {
                    Id = 1,
                    Code = string.Empty,
                    City = string.Empty,
                    Country = string.Empty
                },
                new Airport
                {
                    Id = 2,
                    Code = string.Empty,
                    City = string.Empty,
                    Country = string.Empty
                },
                new Airport
                {
                    Id = 3,
                    Code = string.Empty,
                    City = string.Empty,
                    Country = string.Empty
                },
                new Airport
                {
                    Id = 4,
                    Code = string.Empty,
                    City = string.Empty,
                    Country = string.Empty
                }
            });
        }
    }
}
