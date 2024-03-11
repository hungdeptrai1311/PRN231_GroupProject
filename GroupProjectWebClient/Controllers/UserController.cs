using BusinessObject.Models;
using GroupProjectWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GroupProjectWebClient.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient client = null;
        private string ApiUrl = "";
        private Utill utill;

        public UserController() //hàm khởi tạo
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7086/api/";
            utill = new Utill();
        }

        public async Task<IActionResult> Profile()
        {
            (int userID, int roleId) = utill.GetAccountID(HttpContext);

            if (userID > 0)
            {
                HttpResponseMessage responseUser =
                    await client.GetAsync(ApiUrl + "User/GetUser?userID=" + userID);
                string strDataUser =
                    await responseUser.Content.ReadAsStringAsync();

                User user = JsonConvert.DeserializeObject<User>(strDataUser);

                //Start Setup tạm để test
                //GroupProjectContext groupProjectContext = new GroupProjectContext();

                //User user = groupProjectContext.Users.First();
                // End set tạm

                return View(user);
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        public async Task<IActionResult> Edit()
        {

            (int userID, int roleId) = utill.GetAccountID(HttpContext);

            if (userID > 0)
            {
                HttpResponseMessage responseUser =
                    await client.GetAsync(ApiUrl + "User/GetUser?userID=" + userID);
                string strDataUser =
                    await responseUser.Content.ReadAsStringAsync();

                User user = JsonConvert.DeserializeObject<User>(strDataUser);

                //Start Setup tạm để test
                //GroupProjectContext groupProjectContext = new GroupProjectContext();

                //User user = groupProjectContext.Users.First();
                // End set tạm

                return View("UpdateUser", user);
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            (int userID, int roleId) = utill.GetAccountID(HttpContext);

            if (userID > 0)
            {
                HttpResponseMessage responseUser =
                    await client.GetAsync(ApiUrl + "User/GetUser?userID=" + userID);
                string strDataUser =
                    await responseUser.Content.ReadAsStringAsync();

                User userUpdate = JsonConvert.DeserializeObject<User>(strDataUser);

                //Start Setup tạm để test
                //GroupProjectContext groupProjectContext = new GroupProjectContext();

                //User userUpdate = groupProjectContext.Users.First();
                // End set tạm

                userUpdate.Address = user.Address;
                userUpdate.Email = user.Email;
                userUpdate.Name = user.Name;
                userUpdate.Phone = user.Phone;

                userUpdate.Role = null;
                userUpdate.Carts = null;

                string strData = JsonConvert.SerializeObject(userUpdate);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");

                HttpResponseMessage responseUserUpdate =
                    await client.PostAsync(ApiUrl + "User/UpdateUser", content);

                return RedirectToAction("Profile");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }
    }
}
