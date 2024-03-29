﻿using BusinessObject.Models;
using GroupProjectWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GroupProjectWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;
        private string ApiUrl = "";
        private Utill utill;

        public HomeController() //hàm khởi tạo
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7086/api/";
            utill = new Utill();

        }

        public async Task<IActionResult> Index(int sort = 0, string name = "", int categoryID = 0, int brandID = 0, int addCart = 0)
        {
            // List product
            HttpResponseMessage responseProducts = await client.GetAsync(ApiUrl + "Products/GetProducts");
            string strDataProducts = await responseProducts.Content.ReadAsStringAsync();

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(strDataProducts);

            //List Brand
            HttpResponseMessage responseBrands = await client.GetAsync(ApiUrl + "Brands/GetBrands");
            string strDataBrands = await responseBrands.Content.ReadAsStringAsync();

            List<Brand> brands = JsonConvert.DeserializeObject<List<Brand>>(strDataBrands);

            //List Category
            HttpResponseMessage responseCategories = await client.GetAsync(ApiUrl + "Categories/GetCategories");
            string strDataCategories = await responseCategories.Content.ReadAsStringAsync();

            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(strDataCategories);

            ViewBag.Brands = brands;
            ViewBag.Categorys = categories;

            (int userID, int roleId) = utill.GetAccountID(HttpContext);

            if (roleId == 2)
            {
                if (HttpContext.Session.GetString("roleAdmin") == null)
                {
                    HttpContext.Session.SetString("roleAdmin", "true");
                }
            }
            List<Cart> carts = new List<Cart>();

            if (userID > 0)
            {
                // List Cart 
                HttpResponseMessage responseCart = await client.GetAsync(ApiUrl + "Carts/GetCartsByUserId?userId=" + userID);
                string strDataCart = await responseCart.Content.ReadAsStringAsync();

                carts = JsonConvert.DeserializeObject<List<Cart>>(strDataCart);

                if (addCart != 0)
                {
                    Cart cart = carts.FirstOrDefault(x => x.ProductId == addCart);
                    if (cart != null)
                    {
                        cart.Quantity++;

                        Cart cartUpdate = cart;

                        cartUpdate.Product = null;
                        cartUpdate.User = null;
                        string strData = JsonConvert.SerializeObject(cartUpdate);
                        HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response =
                            await client.PutAsync(ApiUrl + "Carts/UpdateCart", content);
                    }
                    else
                    {
                        cart = new Cart();
                        cart.Quantity = 1;
                        cart.UserId = 1;
                        cart.ProductId = addCart;

                        string strData = JsonConvert.SerializeObject(cart);
                        HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response =
                            await client.PostAsync(ApiUrl + "Carts/AddCart", content);
                    }
                }

                responseCart = await client.GetAsync(ApiUrl + "Carts/GetCartsByUserId?userId=" + userID);

                strDataCart = await responseCart.Content.ReadAsStringAsync();

                carts = JsonConvert.DeserializeObject<List<Cart>>(strDataCart);

                int totalPrice = 0;

                foreach (Cart item in carts)
                {
                    totalPrice += item.Product.Price * item.Quantity ?? 0;
                }

                ViewBag.Carts = carts;
                ViewBag.TotalPrice = totalPrice;
            }

            if (!String.IsNullOrEmpty(name))
            {
                products = products.Where(x => x.ProductName.StartsWith(name.Trim())).ToList();
            }

            if (brandID != 0)
            {
                products = products.Where(x => x.BrandId == brandID).ToList();
            }

            if (categoryID != 0)
            {
                products = products.Where(x => x.CategoryId == categoryID).ToList();
            }

            if (sort != 0)
            {
                switch (sort)
                {
                    case 1:
                        products = products.OrderBy(x => x.ProductName).ToList();
                        break;
                    case 2:
                        products = products.OrderByDescending(x => x.ProductName).ToList();
                        break;
                    case 3:
                        products = products.OrderBy(x => x.Price).ToList();
                        break;
                    case 4:
                        products = products.OrderByDescending(x => x.Price).ToList();
                        break;
                }
            }

            ViewBag.Name = name ?? "";
            ViewBag.Sort = sort;
            ViewBag.BrandID = brandID;
            ViewBag.CategoryID = categoryID;

            return View(products);
        }

        public async Task<IActionResult> ViewCart()
        {
            (int userID, int roleId) = utill.GetAccountID(HttpContext);

            List<Cart> carts = new List<Cart>();

            if (userID > 0)
            {
                //List Brand
                HttpResponseMessage responseBrands = await client.GetAsync(ApiUrl + "Brands/GetBrands");
                string strDataBrands = await responseBrands.Content.ReadAsStringAsync();

                List<Brand> brands = JsonConvert.DeserializeObject<List<Brand>>(strDataBrands);

                //List Category
                HttpResponseMessage responseCategories = await client.GetAsync(ApiUrl + "Categories/GetCategories");
                string strDataCategories = await responseCategories.Content.ReadAsStringAsync();

                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(strDataCategories);

                ViewBag.Brands = brands;
                ViewBag.Categorys = categories;

                HttpResponseMessage responseCart = await client.GetAsync(ApiUrl + "Carts/GetCartsByUserId?userId=" + userID);
                string strDataCart = await responseCart.Content.ReadAsStringAsync();

                carts = JsonConvert.DeserializeObject<List<Cart>>(strDataCart);

                int totalPrice = 0;

                foreach (Cart item in carts)
                {
                    totalPrice += item.Product.Price * item.Quantity ?? 0;
                }

                ViewBag.Carts = carts;
                ViewBag.TotalPrice = totalPrice;

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Payment()
        {
            (int userID, int roleId) = utill.GetAccountID(HttpContext);

            List<Cart> carts = new List<Cart>();

            if (userID > 0)
            {
                HttpResponseMessage responseCart = await client.GetAsync(ApiUrl + "Carts/GetCartsByUserId?userId=" + userID);
                string strDataCart = await responseCart.Content.ReadAsStringAsync();

                carts = JsonConvert.DeserializeObject<List<Cart>>(strDataCart);

                int totalPrice = 0;

                Order order = new Order();
                order.UserId = userID;
                order.OrderDetails = new List<OrderDetail>();
                order.Date = DateTime.Now;
                order.Status = "Pending Approval";


                //List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (Cart item in carts)
                {
                    totalPrice += item.Product.Price * item.Quantity ?? 0;

                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.ProductId = item.ProductId;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.OrderId = order.OrderId;

                    order.OrderDetails.Add(orderDetail);
                }

                string strDataOrder = JsonConvert.SerializeObject(order);
                HttpContent contentOrder = new StringContent(strDataOrder, Encoding.UTF8, "application/json");
                HttpResponseMessage responseOrder =
                    await client.PostAsync(ApiUrl + "Orders/AddOrder", contentOrder);

                HttpResponseMessage responseDeleteCart = await client.GetAsync(ApiUrl + "Carts/DeleteCartByUser?userID=" + userID);

                return View("PaymentDone");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> UpdateCart(int cartID = 0, int changValue = 0, int quantity = -1)
        {

            (int userID, int roleId) = utill.GetAccountID(HttpContext);

            if (quantity < 0 && changValue == 0)
            {
                return RedirectToAction("ViewCart");
            }
            if (userID > 0)
            {
                HttpResponseMessage responseCart = await client
                    .GetAsync(ApiUrl + "Carts/GetCartById?id=" + cartID);
                string strDataCart = await responseCart.Content.ReadAsStringAsync();


                Cart cart = JsonConvert.DeserializeObject<Cart>(strDataCart);

                if (quantity >= 0)
                {
                    cart.Quantity = quantity;
                    changValue = 2;
                }
                else
                {
                    switch (changValue)
                    {
                        case 1:
                            // Tang so luong
                            cart.Quantity++;
                            break;
                        case 2:
                            // Giam so luong
                            cart.Quantity--;
                            break;
                    }
                }

                // So luong == 0 thi xoa khoi gio hang
                if (cart.Quantity == 0)
                {
                    changValue = 3;
                }

                if (changValue > 0)
                {
                    if (changValue == 3)
                    {
                        HttpResponseMessage responseCartDelete = await client
                            .DeleteAsync(ApiUrl + "Carts/DeleteCart?id=" + cartID);
                    }
                    else
                    {
                        cart.Product = null;
                        string strData = JsonConvert.SerializeObject(cart);
                        HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response =
                            await client.PutAsync(ApiUrl + "Carts/UpdateCart", content);
                    }
                }

                return RedirectToAction("ViewCart");

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Login()
        {
            (int userID, int roleId) = utill.GetAccountID(HttpContext);

            if (userID > 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Signup()
        {
            (int userID, int roleId) = utill.GetAccountID(HttpContext);

            if (userID > 0)
            {
                return RedirectToAction("Index");
            }

            return View("Signup");
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LoginCheck(string email, string pass)
        {
            User user = new User();
            user.Email = email;
            user.Password = pass;

            string strData = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
            HttpResponseMessage response =
                await client.PostAsync(ApiUrl + "Carts/UpdateCart", content);

            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("userID", response.Content.ToString());

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = true;
                return View("Login");
            }

            // Setup tam thoi
            //GroupProjectContext context = new GroupProjectContext();

            //User u = context.Users.FirstOrDefault(x => x.Email == email && x.Password == pass);


            //if (u != null)
            //{
            //    u.Role = null; ;
            //    u.Order = null;
            //    u.Carts = null;
            //    string strData = JsonConvert.SerializeObject(u);

            //    HttpContext.Session.SetString("userID", strData);
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    ViewBag.Error = true;
            //    return View("Login");
            //}
            // End
        }

        [HttpPost]
        public async Task<IActionResult> SignupCheck(User user, string rePass)
        {
            if (user == null)
            {
                return RedirectToAction("Signup");
            }
            else
            {
                if (user.Password == rePass)
                {
                    //HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response =
                        await client.GetAsync(ApiUrl + "User/GetEmail?emai=" + user.Email);
                    if (response.IsSuccessStatusCode)
                    {
                        user.RoleId = 2;
                        string strData = JsonConvert.SerializeObject(user);
                        HttpContent contentUser = new StringContent(strData, Encoding.UTF8, "application/json");
                        HttpResponseMessage responseUser =
                            await client.PostAsync(ApiUrl + "User/AddUser", contentUser);
                        if (responseUser.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            ViewBag.ErrorSyS = true;
                            return View("Signup");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorInfomatio = true;
                        return View("Signup");
                    }
                }
                else
                {
                    ViewBag.ErrorConfirmPass = true;
                    return View("Signup");
                }
            }
        }

    }
}
