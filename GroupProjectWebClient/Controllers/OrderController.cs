using BusinessObject.Models;
using GroupProjectWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GroupProjectWebClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient client = null;
        private string ApiUrl = "";
        private Utill utill;

        public OrderController() //hàm khởi tạo
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7086/api/";
            utill = new Utill();
        }

        public async Task<IActionResult> Index()
        {
            (int userID, int roleId) = utill.GetAccountID(HttpContext);
            if (userID > 0)
            {
                HttpResponseMessage responseUser =
                   await client.GetAsync(ApiUrl + "Order/GetOrdersByUserID?UserID=" + userID);
                string strDataUser =
                    await responseUser.Content.ReadAsStringAsync();

                List<Order> order = JsonConvert.DeserializeObject<List<Order>>(strDataUser);

                //// Tam thoi
                //GroupProjectContext context = new GroupProjectContext();
                //List<Order> order = context.Orders.Where(x=> x.UserId == userID).ToList();
                ////End

                return View(order ?? new List<Order>());
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }


    }
}
