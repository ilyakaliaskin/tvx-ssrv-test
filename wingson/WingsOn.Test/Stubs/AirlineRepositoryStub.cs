using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Test.Stubs
{
    public class AirlineRepositoryStub : RepositoryBase<Airline>
    {
        public AirlineRepositoryStub() 
        {
            Repository.AddRange(new []
            {
                new Airline
                {
                    Id = 1,
                    Code = string.Empty,
                    Address = string.Empty,
                    Name = string.Empty
                },
                new Airline
                {
                    Id = 2,
                    Code = string.Empty,
                    Address = string.Empty,
                    Name = string.Empty
                }
            });
        }
    }
}
