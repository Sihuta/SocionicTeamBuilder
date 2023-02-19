using AutoMapper;
using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Models;

namespace SocionicTeamBuilder.BLL.Infrastructure
{
    public class Mapper
    {
        private IMapper mapper;
        public List<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> sourceCollection)
        {
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()).CreateMapper();
            return mapper.Map<IEnumerable<TSource>, List<TDestination>>(sourceCollection);
        }

        public TDestination Map<TSource, TDestination>(TSource sourceItem)
        {
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()).CreateMapper();
            return mapper.Map<TSource, TDestination>(sourceItem);
        }

        public List<CreatedGroupOfTeamsDTO> Map(IEnumerable<CreatedGroupOfTeams> sourceCollection)
        {
            var res = new List<CreatedGroupOfTeamsDTO>();
            foreach (var groupOfTeams in sourceCollection)
            {
                var groupOfTeamsDTO = new CreatedGroupOfTeamsDTO(groupOfTeams);
                res.Add(groupOfTeamsDTO);
            }

            return res;
        }
    }
}
