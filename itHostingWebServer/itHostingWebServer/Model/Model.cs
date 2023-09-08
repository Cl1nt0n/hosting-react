using itHostingWebServer.Entities;
using System.Net;

namespace itHostingWebServer.Model
{
    public class Model
    {
        public User FindUser(User user)
        {
            using (ItHostingContext db = new ItHostingContext())
            {
                user = db.Users.Where(x => x.Login == user.Login && x.Password == user.Password).FirstOrDefault();

                if (user != null)
                    return user;

                return null;
            }
        }
    }
}
