using AutoMapper;

namespace RhythmBack.Model.Util
{
    public class MappingProfile<TDTO, T> : Profile
    where TDTO : class
    where T : class
    {
        public MappingProfile()
        {
            CreateMap<TDTO, T>();
        }
    }
        
}
