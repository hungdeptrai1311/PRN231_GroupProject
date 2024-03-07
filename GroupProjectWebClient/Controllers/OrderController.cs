using BusinessObject.Models;
using GroupProjectWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
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
            int userID = utill.GetAccountID(HttpContext);
            if (userID > 0)
            {
                //HttpResponseMessage responseUser =
                //   await client.GetAsync(ApiUrl + "Order/GetOrderByUserID=" + userID);
                //string strDataUser =
                //    await responseUser.Content.ReadAsStringAsync();

                //Order order = JsonConvert.DeserializeObject<Order>(strDataUser);

                // Tam thoi
                GroupProjectContext context = new GroupProjectContext();
                Order order = context.Orders.First();
                //End

                return View(order);
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }
    }
}
