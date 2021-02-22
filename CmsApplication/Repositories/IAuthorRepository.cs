using CmsApplication.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CmsApplication.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAll();
        Task Update(Author entity);
    }
}
