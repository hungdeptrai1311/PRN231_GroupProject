using BusinessObject.Models;
using GroupProjectWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GroupProjectWebClient.Controllers
{
    public class UsersManagerController : Controller
    {
        private readonly HttpClient client = null;
        private string ApiUrl = "";
        private Utill utill;

        public UsersManagerController() //hàm khởi tạo
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7086/api/";
            utill = new Utill();
        }

        private async Task<List<User>> GetUsers()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "User/GetUsers");
            string strData = await responseMessage.Content.ReadAsStringAsync();

            List<User> users = JsonConvert.DeserializeObject<List<User>>(strData);

            return users;
        }

        private async Task<List<Role>> GetRoles()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Roles/GetRoles");
            string strData = await responseMessage.Content.ReadAsStringAsync();

            List<Role> data = JsonConvert.DeserializeObject<List<Role>>(strData);

            return data;
        }

        private async Task<User> GetUserById(int? id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "User/GetUser?userID=" + id);
            string strData = await responseMessage.Content.ReadAsStringAsync();

            User user = JsonConvert.DeserializeObject<User>(strData);

            return user;
        }


        public async Task<IActionResult> Index()
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            return View(GetUsers().Result);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var user = GetUserById(id).Result;
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            ViewData["RoleId"] = new SelectList(GetRoles().Result, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Email,Password,Name,Address,Phone,RoleId")] User user)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            if (ModelState.IsValid)
            {
                string strData = JsonConvert.SerializeObject(user);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage =
                    await client.PostAsync(ApiUrl + "User/AddUser", content);

                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(GetRoles().Result, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: UsersManager/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var user = GetUserById(id).Result;
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(GetRoles().Result, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Email,Password,Name,Address,Phone,RoleId")] User user)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string strData = JsonConvert.SerializeObject(user);
                    HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage =
                        await client.PutAsync(ApiUrl + "User/UpdateUser", content);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(GetRoles().Result, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: UsersManager/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var user = GetUserById(id).Result;
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UsersManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            var user = GetUserById(id).Result;
            if (user != null)
            {
                HttpResponseMessage responseMessage =
                        await client.DeleteAsync(ApiUrl + "User/Delete?id=" + id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return GetUserById(id).Result != null;
        }
    }
}
