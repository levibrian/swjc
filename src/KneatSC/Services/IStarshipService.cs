using KneatSC.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KneatSC.Services
{
    public interface IStarshipService
    {
        Task<IEnumerable<StarshipDTO>> Get(int page, long distance);
        Task<IEnumerable<StarshipDTO>> GetAll(long distance);
        Task<IEnumerable<StarshipDTO>> GetWithCalculatedJumps(long distance);
    }
}
