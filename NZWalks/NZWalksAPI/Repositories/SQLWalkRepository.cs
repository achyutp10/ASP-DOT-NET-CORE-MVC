using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext dBContext;

        public SQLWalkRepository(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dBContext.Walks.AddAsync(walk);
            await dBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dBContext.Walks.ToListAsync();
        }
    }
}
