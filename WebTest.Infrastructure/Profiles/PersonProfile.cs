using AutoMapper;
using WebTest.Domain;
using WebTest.Infrastructure.Requests;

namespace WebTest.Infrastructure.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonRequest>().ReverseMap();
        }
    }
}
