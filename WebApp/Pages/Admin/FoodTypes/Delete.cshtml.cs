
using DataAccess.Data;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public FoodType FoodType { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db=db;
        }
        public void OnGet(int id)
        {
            FoodType = _db.FoodType.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {

            var foodTypeFromDb = _db.FoodType.Find(FoodType.Id);
            if(foodTypeFromDb != null)
            {
                _db.FoodType.Remove(foodTypeFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "FoodType Deleted Successfully";

                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
