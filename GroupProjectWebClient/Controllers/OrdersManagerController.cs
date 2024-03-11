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
    public class OrdersManagerController : Controller
    {
        private readonly HttpClient client = null;
        private string ApiUrl = "";
        private Utill utill;

        public OrdersManagerController() //hàm khởi tạo
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7086/api/";
            utill = new Utill();
        }

        public async Task<IActionResult> Index()
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Order/GetOrders");
            string strData = await responseMessage.Content.ReadAsStringAsync();

            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(strData);

            return View(orders);
        }

        private async Task<Order> GetOrder(int? id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Order/GetOrdersById?id=" + id);
            string strData = await responseMessage.Content.ReadAsStringAsync();

            Order order = JsonConvert.DeserializeObject<Order>(strData);

            return order;
        }

        private async Task<List<User>> GetUsers()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "User/GetUsers");
            string strData = await responseMessage.Content.ReadAsStringAsync();

            List<User> users = JsonConvert.DeserializeObject<List<User>>(strData);

            return users;
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

            Order order = GetOrder(id).Result;

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }



        // GET: OrdersManager/Edit/5
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

            var order = GetOrder(id).Result;
            if (order == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(GetUsers().Result, "UserId", "Name", order.OrderId);
            return View(order);
        }

        // POST: OrdersManager/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,UserId,Date,Status")] Order order)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string strData = JsonConvert.SerializeObject(order);
                    HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage =
                        await client.PutAsync(ApiUrl + "Order/UpdateOrder", content);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["OrderId"] = new SelectList(GetUsers().Result, "UserId", "Name", order.OrderId);
            return View(order);
        }

        public async Task<IActionResult> UpdateStatus(int id, string stauts)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            Order order = GetOrder(id).Result;

            if (order != null)
            {
                try
                {
                    order.Status = stauts;

                    string strData = JsonConvert.SerializeObject(order);
                    HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage =
                        await client.PutAsync(ApiUrl + "Order/UpdateOrder", content);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");
        }

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

            var order = GetOrder(id).Result;
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            var order = GetOrder(id).Result;
            if (order != null)
            {
                HttpResponseMessage responseMessage =
                       await client.DeleteAsync(ApiUrl + "Orders/DeleteOrder?id=" + id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return GetOrder(id).Result == null;
        }
    }
}
