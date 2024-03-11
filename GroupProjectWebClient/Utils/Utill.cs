using BusinessObject.Models;
using Newtonsoft.Json;

namespace GroupProjectWebClient.Utils
{
    public class Utill
    {
        public (int, int) GetAccountID(HttpContext context)
        {
            if (context.Session.GetString("userID") == null)
            {
                return (0, 0);
            }

            string strData = context.Session.GetString("userID");

            User user = JsonConvert.DeserializeObject<User>(strData) ?? new User();

            return (user.UserId, user.RoleId);
        }

        public bool checkRole(HttpContext context)
        {
            (int userID, int roleId) = GetAccountID(context);
            if (roleId == 1)
            {
                return false;
            }

            return true;
        }
    }
}
