using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        /* Sync Index. Must leave this action commented because, as this is a Controller, the async action must also be
         * called Index, and not IndexAsync.
         */
        /*
       public IActionResult Index()
       {
           List<Seller> sellers = _sellerService.FindAll();
           return View(sellers);
       }
       */

        // Async Index
        public async Task<IActionResult> Index()
        {
            List<Seller> sellers = await _sellerService.FindAllAsync(); // db access. This is the FindAllAsync I created in SellerService!
            return View(sellers);
        }

        // This takes us to the Create view, so it is HttpGet, which is default
        // Sync Create (GET). Commented because of the same reason as Sync Index
        /*
        public IActionResult Create()
        {
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        */

        // Async Create (GET)
        public async Task<IActionResult> Create()
        {
            List<Department> departments = await _departmentService.FindAllAsync(); // db access. This is the FindAllAsync I created in DepartmentService!
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // This really creates a seller entry in the DB, so it is HttpPost, which must be stated in annotation like below
        // Sync Create (POST). Commented because of the same reason as Sync Index
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = _departmentService.FindAll();
                SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
        */

        // Async Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = await _departmentService.FindAllAsync(); // db access. This is the FindAllAsync I created in DepartmentService!
                SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller); // db access. This is the InsertAsync I created in SellerService!
            return RedirectToAction(nameof(Index));
        }

        // This takes us to the Delete view, so it is HttpGet, which is default
        // Sync Delete (GET). Commented because of the same reason as Sync Index
        /*
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            // As this id below is optional (nullable), id.Value must be used instead of just id
            Seller seller = _sellerService.FindById(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(seller);
        }
        */

        // Async Delete (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            // As this id below is optional (nullable), id.Value must be used instead of just id
            Seller seller = await _sellerService.FindByIdAsync(id.Value); // db access. This is the FindByIdAsync I created in SellerService!

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(seller);
        }

        // This really deletes a seller entry in the DB, so it is HttpPost, which must be stated in annotation like below
        // Sync Delete (POST). Commented because of the same reason as Sync Index
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _sellerService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        */

        // Async Delete (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id); // db access. This is the RemoveAsync I created in SellerService!
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // This takes us to the Details view, so it is HttpGet, which is default
        // Sync Details (GET). Commented because of the same reason as Sync Index
        /*
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            // As this id below is optional (nullable), id.Value must be used instead of just id
            Seller seller = _sellerService.FindById(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(seller);
        }
        */

        // Async Details (GET)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            // As this id below is optional (nullable), id.Value must be used instead of just id
            Seller seller = await _sellerService.FindByIdAsync(id.Value); // db access. This is the FindByIdAsync I created in SellerService!

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(seller);
        }

        // This takes us to the Edit view, so it is HttpGet, which is default
        // Sync Edit (GET). Commented because of the same reason as Sync Index
        /*
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            // As this id below is optional (nullable), id.Value must be used instead of just id
            Seller seller = _sellerService.FindById(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }
        */

        // Async Edit (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            // As this id below is optional (nullable), id.Value must be used instead of just id
            Seller seller = await _sellerService.FindByIdAsync(id.Value); // db access. This is the FindByIdAsync I created in SellerService!

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync(); // db access. This is the FindAllAsync I created in DepartmentService!
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        // This really edits a seller entry in the DB, so it is HttpPost, which must be stated in annotation like below
        // Sync Edit (POST). Commented because of the same reason as Sync Index
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Seller seller, int id)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = _departmentService.FindAll();
                SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        */

        // Async Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Seller seller, int id)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = await _departmentService.FindAllAsync(); // db access. This is the FindAllAsync I created in DepartmentService!
                SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller); // db access. This is the UpdateAsync I created in SellerService!
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        // ERROR
        // Doesn't need to be async!
        public IActionResult Error(string message)
        {
            ErrorViewModel viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // Just accept this
            };
            return View(viewModel);
        }
    }
}