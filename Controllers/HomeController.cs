using CelilCavus.Manager;
using CelilCavus.Models.Entites;
using CelilCavus.Validator;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PagedList;


namespace CelilCavus.Controllers
{
    public class HomeController : Controller
    {


        private readonly BaseClientManager<Ogrenci> _manager;

        public HomeController(BaseClientManager<Ogrenci> manager)
        {
            _manager = manager;
            _manager.Url("http://localhost:5134/api/", "Ogrenci");
        }

        [HttpGet]
        public IActionResult Index()
        {

            var values = _manager.GetAll();
            return View(values);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Ogrenci ogrenci)
        {
            OgrenciValidator ogrenciValid = new OgrenciValidator();
            ValidationResult result = ogrenciValid.Validate(ogrenci);
            if (result.IsValid)
            {
                for (int i = 0; i < 500; i++)
                {
                    string[] bolumler = { "Bilisim", "Muhasebe", "Buro", "Moda", "Saglik", "Ekonomi", "Edebiyat", "Elektrik", "Kimya" };

                    Random rand = new Random();
                    string cins = rand.Next(0, 2) == 0 ? "Kadin" : "Erkek";

                    await _manager.Add(new()
                    {
                        ad = FakeData.NameData.GetFirstName(),
                        soyad = FakeData.NameData.GetSurname(),
                        cinsiyet = cins,
                        bolum = bolumler[rand.Next(0, bolumler.Length)].ToString(),
                        sinif = rand.Next(9, 12)
                    });
                    return RedirectToAction("Index");
                }
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }



        [HttpGet]
        public IActionResult Update(int id)
        {
            var NullOrEmpty = id > 0 ? true : false;
            if (NullOrEmpty == true)
            {
                var returnValue = _manager.GetById(id);
                return View(returnValue);
            }
            else return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> Update(Ogrenci ogrenci)
        {
            OgrenciValidator ogrenciValid = new OgrenciValidator();
            ValidationResult result = ogrenciValid.Validate(ogrenci);
            if (result.IsValid)
            {
                await _manager.UpdateAsync(ogrenci);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool NullOrEmptyId = id > 0 ? true : false;
            if (NullOrEmptyId == true)
            {
                await _manager.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            else return NotFound();
        }
    }
}