using Beau.Data;
using Microsoft.EntityFrameworkCore;
using Beau.Models;
using Google;
using System.Collections.Generic;
using System.Linq;


namespace Beau.Repository
{
    public class UserRepository
    {
        private readonly DataBContext dbcon;
        public UserRepository(DataBContext db)
        {
            dbcon = db;
        }
        public String GetUserNameByID(Guid id)
        {
            var user = dbcon.Users
                .Include(inc => inc.UserCredentials)
                .Include(p => p.Posts)
                .FirstOrDefault(u => u.UserId == id);
            if(user == null) { 
                return null;
            }
            return user.UserCredentials.UserName;
        }
    }
}
