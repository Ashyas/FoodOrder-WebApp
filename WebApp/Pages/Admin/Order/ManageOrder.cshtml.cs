using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.ViewModel;
using Utility;

namespace WebApp.Pages.Admin.Order
{
    [Authorize(Roles = $"{StaticDetail.ManagerRole},{StaticDetail.KitchenRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<OrderDetail_VM> orderDetailVM { get; set; }
        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            orderDetailVM = new();
            List<OrderHeader> orderHeader = _unitOfWork.OrderHeader.GetAll(u => u.Status == StaticDetail.StatusSubmtted ||
            u.Status == StaticDetail.StatusInProccess).ToList();

            foreach (OrderHeader item in orderHeader)
            {
                OrderDetail_VM individual = new OrderDetail_VM()
                {
                    orderHeader = item,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == item.Id).ToList()
                };
                orderDetailVM.Add(individual);
            }
        }
        public IActionResult OnPostOrderInProccess(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetail.StatusInProccess);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderReady(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetail.StatusReady);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetail.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
    }
}
