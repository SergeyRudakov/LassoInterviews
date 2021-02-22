using CmsApplication.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CmsApplication.Repositories
{
    public class InMemoryAuthorRepository : IAuthorRepository
    {
        List<Author> authors = new List<Author>
        {

        };

        public async Task<List<Author>> GetAll()
        {
            return authors;
        }

        public Task Update(Author entity)
        {
            var repoEntity = authors.FirstOrDefault(x => x.id == entity.id);

            repoEntity.isEmailVerified = entity.isEmailVerified;

            return Task.CompletedTask;
        }
    }
}
