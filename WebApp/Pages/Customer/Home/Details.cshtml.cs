using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Utility;

namespace WebApp.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCart = new()
            {
                ApplicationUserId = claim.Value,
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(x => x.Id == id, includeProperties: "Category,FoodType"),
                MenuItemId = id
            };
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid) 
            {
                ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                    filter: x => x.ApplicationUserId == ShoppingCart.ApplicationUserId &&
                    x.MenuItemId == ShoppingCart.MenuItemId);

                if (shoppingCartFromDb == null)
                {
                    _unitOfWork.ShoppingCart.Add(ShoppingCart);
                    _unitOfWork.Save();
                    var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == ShoppingCart.ApplicationUserId).ToList().Count;
                    HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);


                }
                else
                {
                    _unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDb, ShoppingCart.Count);
                }           
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}

