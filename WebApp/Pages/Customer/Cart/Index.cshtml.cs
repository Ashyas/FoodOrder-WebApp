using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Security.Claims;
using Utility;

namespace WebApp.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public double cartTotal;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            cartTotal = 0;
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: a=> a.ApplicationUserId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
            }
            foreach (var cartItem in ShoppingCartList)
            {
                cartTotal+=(cartItem.MenuItem.Price*cartItem.Count);
            }
         
        }
        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c=>c.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            return RedirectToPage("/Customer/Cart/Index");
        }
        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
            var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;

            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);
            return RedirectToPage("/Customer/Cart/Index");
        }
        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
            if (cart.Count > 1)
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }
            else
            {
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;

                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);

            }
            return RedirectToPage("/Customer/Cart/Index");
        }
    }
}
