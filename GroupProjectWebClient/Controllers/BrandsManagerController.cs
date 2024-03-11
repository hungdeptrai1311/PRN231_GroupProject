using BusinessObject.Models;
using GroupProjectWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GroupProjectWebClient.Controllers
{
    public class BrandsManagerController : Controller
    {
        private readonly HttpClient client = null;
        private string ApiUrl = "";
        private Utill utill;

        public BrandsManagerController() //hàm khởi tạo
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
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Brands/GetBrands");
            string strData = await responseMessage.Content.ReadAsStringAsync();

            List<Brand> brands = JsonConvert.DeserializeObject<List<Brand>>(strData);

            return View(brands);
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

            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Brands/GetBrandById?id=" + id);
            string strData = await responseMessage.Content.ReadAsStringAsync();

            Brand brand = JsonConvert.DeserializeObject<Brand>(strData);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: BrandsManager/Create
        public IActionResult Create()
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,BrandName,BrandImage,Description")] Brand brand)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            if (ModelState.IsValid)
            {
                string strData = JsonConvert.SerializeObject(brand);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage =
                    await client.PostAsync(ApiUrl + "Brands/AddBrand", content);

                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

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

            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Brands/GetBrandById?id=" + id);
            string strData = await responseMessage.Content.ReadAsStringAsync();

            Brand brand = JsonConvert.DeserializeObject<Brand>(strData);

            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,BrandName,BrandImage,Description")] Brand brand)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            if (id != brand.BrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string strData = JsonConvert.SerializeObject(brand);
                    HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage =
                        await client.PutAsync(ApiUrl + "Brands/UpdateBrand", content);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.BrandId).Result)
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
            return View(brand);
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

            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Brands/GetBrandById?id=" + id);
            string strData = await responseMessage.Content.ReadAsStringAsync();

            Brand brand = JsonConvert.DeserializeObject<Brand>(strData);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            HttpResponseMessage responseMessage =
                        await client.DeleteAsync(ApiUrl + "Brands/DeleteBrand?id=" + id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BrandExists(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Brands/GetBrandById?id=" + id);
            string strData = await responseMessage.Content.ReadAsStringAsync();

            Brand brand = JsonConvert.DeserializeObject<Brand>(strData);

            return brand != null;
        }
    }
}
