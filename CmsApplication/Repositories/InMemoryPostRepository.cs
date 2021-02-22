using CmsApplication.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CmsApplication.Repositories
{
    public class InMemoryPostRepository : IPostRepository
    {
        List<Post> posts = new List<Post>
        {

        };

        public async Task<List<Post>> SelectAll()
        {
            return posts;
        }

        public Task Update(Post entity)
        {
            var repoEntity = posts.FirstOrDefault(x => x.Id == entity.Id);

            repoEntity.IsPublicVisible = entity.IsPublicVisible;
            repoEntity.PublishTime = entity.PublishTime;
            repoEntity.Category = entity.Category;
            repoEntity.Title = entity.Title;

            return Task.CompletedTask;
        }
    }
}
