using CmsApplication.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CmsApplication.Repositories
{
    public interface IPostRepository
    {
        Task<List<Post>> SelectAll();
        Task Update(Post entity);
    }
}
