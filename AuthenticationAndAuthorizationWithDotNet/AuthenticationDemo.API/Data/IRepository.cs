using System.Collections.Generic;
using System.Linq;

namespace AuthenticationDemo.API.Data
{
    public interface IRepository
    {
        List<User> GetUsers();
    }

    public class AppRepository : IRepository
    {
        private UserDbContext _dbContext ;

        public AppRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }
    }
}
