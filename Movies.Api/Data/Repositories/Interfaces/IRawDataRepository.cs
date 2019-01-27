using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Api.Data.Repositories.Interfaces
{
    public interface IRawDataRepository
    {
        Task Add(RawData rawData);

        Task Update(RawData rawData);

        Task<RawData> Get(int groupId, int postId);

        Task<RawData> Get(Guid id);

        Task<RawData> GetFirstNotPublished();

        Task Save();
    }
}
