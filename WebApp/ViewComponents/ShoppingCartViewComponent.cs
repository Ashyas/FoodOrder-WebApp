using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utility;

namespace WebApp.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;
            if (claim != null)
            {
                //user is logged in
                if (HttpContext.Session.GetInt32(StaticDetail.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(StaticDetail.SessionCart));
                }
                else
                {
                    count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                    HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);
                    return View(count);
                }
            }
            else
            {
                //user has not logged in
                HttpContext.Session.Clear();
                return View(count);
            }
        }
    }
}
