using SanProject.Data.Repositories.Interfaces;
using SanProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanProject.Data.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(SanProjectDBContext dBContext) : base(dBContext)
        {

        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public User FindUser(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }
        public void ActivateUser(User user)
        {

            _context.Update(user);
        }
        public void SoftDeleteUser(User user)
        {
            _context.Update(user);
        }
        public void EditUser(User user)
        {
            _context.Update(user);
        }
    }
}
