using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get(string? staus=null)
        {
            var orderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            if (staus == "cancelled")
            {
                orderList = orderList.Where(u => u.Status == StaticDetail.StatusCancelled || u.Status == StaticDetail.StatusRejected);
            }
            else if(staus == "completed")
            {
                orderList = orderList.Where(u => u.Status == StaticDetail.StatusCompleted);
            }
            else if (staus == "ready")
            {
                orderList = orderList.Where(u => u.Status == StaticDetail.StatusReady);
            }
            else
            {
                orderList = orderList.Where(u => u.Status == StaticDetail.StatusInProccess || u.Status == StaticDetail.StatusSubmtted);
            }
            return Json(new { data = orderList });
        }

    }
}
