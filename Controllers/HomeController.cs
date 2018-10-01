using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shaker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace shaker.Controllers
{
    public class HomeController : Controller
    {
        private ModelsContext _context;
        public HomeController(ModelsContext context) {
            _context = context;
        }

        public IActionResult index()
        {
            return View("index");
        }
        
        //Admin page
        [HttpGet("admin")]
        public IActionResult admin() {
            ViewBag.AdminLoginError = TempData["AdminLoginError"];
            return View("adminLogin");
        }
        //Admin login request
        [HttpPost("admin/login")]
        public IActionResult adminLogin(string username, string password) {
            //Temporary admin login
            if(username == "admin" && password == "admin") {
                return RedirectToAction("adminDashboard");
            }
            TempData["AdminLoginError"] = "Incorrect username or password";
            return RedirectToAction("admin");
        }
        //Admin Dashboard
        [HttpGet("admin/dashboard")]
        public IActionResult adminDashboard() {
            return View("adminDashboard");
        }


        //Admin category routes
        [HttpGet("admin/categories")]
        public IActionResult adminCategories() {
            ViewBag.categories = _context.categories;
            return View("adminCategories");
        }
        //Create new category
        [HttpPost("admin/category")]
        public IActionResult createCategory(Category cat) {
            _context.categories.Add(cat);
            _context.SaveChanges();
            return RedirectToAction("adminCategories");
        }
        //update category with id
        [HttpPost("admin/category/{cat_id}")]
        public IActionResult updateCategory(int cat_id) {
            Category cat = _context.categories.SingleOrDefault(c => c.id == cat_id);
            //Update category details here

            _context.SaveChanges();
            return RedirectToAction("category/{cat_id}");
        }
        //Remove category with id
        [HttpPost("admin/category/remove/{cat_id}")]
        public IActionResult removeCategory(int cat_id) {
            return RedirectToAction("adminCategories");
        }

        //Admin Ingredients routes

        //All ingredients
        [HttpGet("admin/ingredients")]
        public IActionResult adminIngredients() {
            ViewBag.ingredients = _context.ingredients.Include(i => i.category);
            ViewBag.categories = _context.categories;
            return View("adminIngredients");
        }
        [HttpPost("admin/ingredient")]
        public IActionResult createIngredient(Ingredient ing) {
            _context.ingredients.Add(ing);
            _context.SaveChanges();
            return RedirectToAction("adminIngredients");
        }
        //update ingredient with id
        [HttpPost("admin/ingredient/{ing_id}")]
        public IActionResult updateIngredient(int ing_id) {
            Ingredient ing = _context.ingredients.SingleOrDefault(i => i.id == ing_id);
            //Update ingredient details here

            _context.SaveChanges();
            return RedirectToAction("ingredient/{ing_id}");
        }
        //Remove ingredient with id
        [HttpPost("admin/ingredient/remove/{ing_id}")]
        public IActionResult removeIngredient(int ing_id) {
            return RedirectToAction("adminIngredients");
        }

        //Admin recipe routes
        [HttpGet("admin/recipes")]
        public IActionResult adminRecipes() {
            ViewBag.recipes = _context.recipes;
            ViewBag.ingredients = _context.ingredients;
            return View("adminRecipes");
        }
        [HttpPost("admin/recipe")]
        public IActionResult createRecipe(string name, string description, string directions, int[] ings, float[] ingMeasurements, string[] ingUnits) {
            Recipe rec = new Recipe();
            rec.name = name;
            rec.description = description;
            rec.directions = directions;
            _context.recipes.Add(rec);
            _context.SaveChanges();

            //Loop through arrays to add recipe ingredient relationship
            for(int i=0;i<ings.Length;i++) {
                RecipeIngredient recIngs = new RecipeIngredient();
                recIngs.recipe_id = rec.id;
                recIngs.ingredient_id = ings[i];
                recIngs.measurement = ingMeasurements[i];
                recIngs.units = ingUnits[i];
                _context.recipeingredients.Add(recIngs);
                _context.SaveChanges();
            }

            return RedirectToAction("adminRecipes");
        }

        //Classic cocktails routes
        [HttpGet("classics")]
        public IActionResult classics() {
            // ViewBag.recipes = _context.recipes.Include(r => r.ingredients).ThenInclude(i => i.ingredient);
            ViewBag.spirits = _context.categories.Include(c => c.ingredients).Where(c => c.category == "Spirit");
            ViewBag.liqueurs = _context.categories.Include(c => c.ingredients).Where(c => c.category == "Liqueur");
            ViewBag.mixers = _context.categories.Include(c => c.ingredients).Where(c => c.category == "Mixer");
            ViewBag.produce = _context.categories.Include(c => c.ingredients).Where(c => c.category == "Produce");
            
            return View("classics");
        }


        //classic recipe search requests
        [HttpPost("classics/search")]
        public IActionResult classicsSearchByName(string search) {
            TempData["results"] = search;
            return RedirectToAction("classicResultsByName");
        }
        [HttpPost("classics/searchIngredients")]
        public IActionResult searchIngredients(int[] ings) {
            // var allIngs = _context.recipeingredients.Include(i => i.recipe).Where(ri => ings.Contains(ri.ingredient_id)).ToList();
            var allIngs = _context.recipeingredients.Include(i => i.recipe);
                for(int ing=0;ing < ings.Length;ing++) {
                    var newIngs = allIngs.Where(ri => ri.ingredient_id == ings[ing]);
                    if(ing+1 == ings.Length){
                        ViewBag.results = newIngs.ToList();
                        return View("classicResults");
                    }
                }
            // return RedirectToAction("classicsSearchByIngredients");
            // var recs = _context.recipes.Include(r => r.ingredients);
            // foreach(var ing in ings) {
            //     recs = recs.Where(r => r.ingredients.ingredient_id.Contains(ing));
            // }
            // foreach(var ing in ings) {
            //     int idCopy = ing;
            //     allIngs = allIngs.Where(i => idCopy.Contains(i.ingredient_id));
            // }
            return RedirectToAction("classicsSearchResults");
        }
        [HttpGet("classics/resultsByIngredients")]
        public IActionResult classicsSearchByIngredients() {
            var results = TempData["fuck"];
            return View("classicResults");
        }

        [HttpGet("classics/results")]
        public IActionResult classicResultsByName() {
            var results = _context.recipes.Where(r => r.name.Contains((string)TempData["results"])).ToList();
            ViewBag.results = results;
            return View("classicResultsByName");
        }

        //Classic search ajax routes
        [HttpGet("categories/spirits")]
        public JsonResult getSpirits() {
            var spirits = _context.categories.Include(c => c.ingredients).SingleOrDefault(c => c.category == "Spirit");

            List<Ingredient> ings = new List<Ingredient>();
            List<string[]> data = new List<string[]>();
            foreach(Ingredient ing in spirits.ingredients) {
                string[] sa = { ing.id.ToString(), ing.name};
                data.Add(sa);

            }
            var toJson = Json(data);
            
            return toJson;
        }
        [HttpGet("categories/liqueurs")]
        public JsonResult getLiqueurs() {
            var spirits = _context.categories.Include(c => c.ingredients).SingleOrDefault(c => c.category == "Liqueur");

            List<Ingredient> ings = new List<Ingredient>();
            List<string[]> data = new List<string[]>();
            foreach(Ingredient ing in spirits.ingredients) {
                string[] sa = { ing.id.ToString(), ing.name};
                data.Add(sa);

            }
            var toJson = Json(data);
            
            return toJson;
        }
        [HttpGet("categories/mixers")]
        public JsonResult getMixers() {
            var spirits = _context.categories.Include(c => c.ingredients).SingleOrDefault(c => c.category == "Mixer");

            List<Ingredient> ings = new List<Ingredient>();
            List<string[]> data = new List<string[]>();
            foreach(Ingredient ing in spirits.ingredients) {
                string[] sa = { ing.id.ToString(), ing.name};
                data.Add(sa);

            }
            var toJson = Json(data);
            
            return toJson;
        }
        [HttpGet("categories/produce")]
        public JsonResult getProduce() {
            var spirits = _context.categories.Include(c => c.ingredients).SingleOrDefault(c => c.category == "Produce");

            List<Ingredient> ings = new List<Ingredient>();
            List<string[]> data = new List<string[]>();
            foreach(Ingredient ing in spirits.ingredients) {
                string[] sa = { ing.id.ToString(), ing.name};
                data.Add(sa);

            }
            var toJson = Json(data);
            
            return toJson;
        }

        //Recipe page
        [HttpGet("/recipe/{rec_id}")]
        public IActionResult recipe(int rec_id) {
            var query = _context.recipes.Include(r => r.ingredients).ThenInclude(i => i.ingredient).SingleOrDefault(r => r.id == rec_id);
            ViewBag.recipe = query;
            return View("recipe");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
