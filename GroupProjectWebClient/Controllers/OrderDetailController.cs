using BusinessObject.Models;
using GroupProjectWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GroupProjectWebClient.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly HttpClient client = null;
        private string ApiUrl = "";
        private Utill utill;

        public OrderDetailController() //hàm khởi tạo
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7086/api/";
            utill = new Utill();
        }

        public async Task<IActionResult> Index(int OrderId)
        {
            (int userID, int roleId) = utill.GetAccountID(HttpContext);
            if (userID > 0)
            {
                HttpResponseMessage responseUser =
                   await client.GetAsync(ApiUrl + "OrderDetails/GetByOrderID?OrderID=" + OrderId);
                string strDataUser =
                    await responseUser.Content.ReadAsStringAsync();

                List<OrderDetail> orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(strDataUser);

                // Tam thoi
                //GroupProjectContext context = new GroupProjectContext();
                //List<OrderDetail> orderDetails = context.OrderDetails.Include(x=> x.Product)
                //    .Where(x => x.OrderId == OrderId).ToList();
                //End

                return View(orderDetails ?? new List<OrderDetail>());
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }
    }
}
