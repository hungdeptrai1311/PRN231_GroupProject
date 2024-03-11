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
    public class ProductsManagerController : Controller
    {
        private readonly HttpClient client = null;
        private string ApiUrl = "";
        private Utill utill;

        public ProductsManagerController() //hàm khởi tạo
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7086/api/";
            utill = new Utill();
        }

        private async Task<Product> GetProduct(int? id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Products/GetProductById?id=" + id);
            string strData = await responseMessage.Content.ReadAsStringAsync();

            Product product = JsonConvert.DeserializeObject<Product>(strData);

            return product;
        }

        private async Task<List<Brand>> GetBrand()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Brands/GetBrands");
            string strData = await responseMessage.Content.ReadAsStringAsync();

            List<Brand> data = JsonConvert.DeserializeObject<List<Brand>>(strData);

            return data;
        }

        private async Task<List<Category>> GetCategories()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Categories/GetCategories");
            string strData = await responseMessage.Content.ReadAsStringAsync();

            List<Category> data = JsonConvert.DeserializeObject<List<Category>>(strData);

            return data;
        }

        public async Task<IActionResult> Index()
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl + "Products/GetProducts");
            string strData = await responseMessage.Content.ReadAsStringAsync();

            List<Product> product = JsonConvert.DeserializeObject<List<Product>>(strData);

            return View(product);
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

            var product = GetProduct(id).Result;
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            ViewData["BrandId"] = new SelectList(GetBrand().Result, "BrandId", "BrandName");
            ViewData["CategoryId"] = new SelectList(GetCategories().Result, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,Price,Quantity,ProductImage,BrandId,CategoryId")] Product product)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            if (ModelState.IsValid)
            {
                string strData = JsonConvert.SerializeObject(product);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage =
                    await client.PostAsync(ApiUrl + "Products/AddProduct", content);

                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(GetBrand().Result, "BrandId", "BrandName", product.BrandId);
            ViewData["CategoryId"] = new SelectList(GetCategories().Result, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
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

            var product = GetProduct(id).Result;
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(GetBrand().Result, "BrandId", "BrandName", product.BrandId);
            ViewData["CategoryId"] = new SelectList(GetCategories().Result, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,Price,Quantity,ProductImage,BrandId,CategoryId")] Product product)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string strData = JsonConvert.SerializeObject(product);
                    HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage =
                        await client.PutAsync(ApiUrl + "Products/UpdateProduct", content);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["BrandId"] = new SelectList(GetBrand().Result, "BrandId", "BrandName", product.BrandId);
            ViewData["CategoryId"] = new SelectList(GetCategories().Result, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
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

            var product = GetProduct(id).Result;
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (utill.checkRole(HttpContext))
            {
                return Redirect("/Home/Index");
            }
            var product = GetProduct(id).Result;
            if (product != null)
            {
                HttpResponseMessage responseMessage =
                                        await client.DeleteAsync(ApiUrl + "Products/DeleteProduct?id=" + id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return GetProduct(id).Result != null;
        }
    }
}
