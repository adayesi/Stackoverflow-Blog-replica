using AutoMapper;
using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using DecaBlog.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecaBlog.Services.Implementations
{
    public class SquadService : ISquadService
    {
        private readonly ISquadRepository _squadRepository;
        private readonly IMapper _mapper;
        public SquadService(ISquadRepository squadRepository, IMapper mapper)
        {
            _mapper = mapper;
            _squadRepository = squadRepository;
        }

        public async Task<SquadMinInfoToReturnDto> AddSquad(SquadToAddDto model)
        {
            var squad = _mapper.Map<Squad>(model);
            var response = await _squadRepository.AddSquad(squad);
            if (!response)
                return null;
            var data = _mapper.Map<SquadMinInfoToReturnDto>(squad);
            return data;
        }

        public async Task<List<Squad>> GetAllSquads()
        {
            List<Squad> squads = await _squadRepository.GetAllSquads();
            return squads ?? null;
        }
        public async Task<Squad> GetSquad(string Id)
        {
            return await _squadRepository.GetSquad(Id);
        }

        public async Task<(bool, string)> UpdateSquad(string id, SquadToUpdateDto model)
        {
            var squad = await _squadRepository.GetSquad(id);
            if (squad == null)
                return (false, "Squad does not exist");

            _mapper.Map(model, squad);
            var updateRes = await _squadRepository.UpdateSquad(squad);

            if (!updateRes)
                return (false, "Unable to update squad");

            return (true, "Squad has been updated");
        }
    }
}