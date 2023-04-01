
using CelilCavus.Manager;
using CelilCavus.Models.Entites;
using CelilCavus.Validator;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CelilCavus.MvcCoreAPICrud_1_10.Controllers
{
    public class DersController : Controller
    {
        private readonly BaseClientManager<Dersler> _dersclient;

        public DersController(BaseClientManager<Dersler> dersclient)
        {
            _dersclient = dersclient;
            _dersclient.Url("http://localhost:5134/api/", "Ders");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var values = _dersclient.GetAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Dersler ders)
        {
            DersValidator validator = new DersValidator();
            ValidationResult result = validator.Validate(ders);
            if (result.IsValid)
            {
                await _dersclient.Add(ders);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

            }
            return View();

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var values = _dersclient.GetById(id);
                if (!string.IsNullOrEmpty(values.ToString()))
                {
                    return View(values);
                }
                else return NotFound();
            }
            else return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(Dersler ders)
        {
            DersValidator validator = new DersValidator();
            ValidationResult result = validator.Validate(ders);
            if (result.IsValid)
            {
                await _dersclient.UpdateAsync(ders);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                await _dersclient.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            else return NotFound();

        }
    }
}
