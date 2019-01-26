using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Movies.Api.Data.Repositories.Logic
{
    public class RawDataRepository : IRawDataRepository
    {
        private DbContext _dbContext;

        private readonly DbSet<RawData> _rawDatas;

        public RawDataRepository(DbContext dbContext)
        {
            _rawDatas = dbContext.Set<RawData>();

            _dbContext = dbContext;
        }

        public async Task Add(RawData rawData)
        {
            _rawDatas.Add(rawData);
        }

        public async Task Update(RawData rawData)
        {
            _dbContext.Entry(rawData).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RawData> Get(int groupId, int postId)
        {
            try
            {
                return await _rawDatas.FirstOrDefaultAsync(t => t.PostId == postId && t.GroupId == groupId);
            }
            catch(Exception e)
            {
                var s = e;
            }

            return null;
        }
    }
}