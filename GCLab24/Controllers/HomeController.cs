using GCLab24.Models;
using System.Linq;
using System.Web.Mvc;

namespace GCLab24.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            gccoffeeshopEntities ORM = new gccoffeeshopEntities();
            ViewBag.Items = ORM.items.ToList();
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult NewItem()
        {
            return View();
        }
        public ActionResult SaveNewItem(item newItem)
        {
            gccoffeeshopEntities ORM = new gccoffeeshopEntities(); //need this is every operation that takes in information

            //ToDo : validation

            ORM.items.Add(newItem);
            ORM.SaveChanges(); //sync with the database

            return RedirectToAction("Index");
        }
        public ActionResult DeleteItem(int itemid)
        {
            gccoffeeshopEntities ORM = new gccoffeeshopEntities(); //need this is every operation that takes in information
            //for loop to find the id
            //find is a method that is used to find objects by using the primary key
            item ItemToDelete = ORM.items.Find(itemid);
            //remove
            ORM.items.Remove(ItemToDelete);
            ORM.SaveChanges(); // table needs to have a primary key (use try and catch)
            return RedirectToAction("Index");
        }
        public ActionResult ItemDetails(int itemid)
        {
            gccoffeeshopEntities ORM = new gccoffeeshopEntities(); //need this is every operation that takes in information
            item ItemtoEdit = ORM.items.Find(itemid);
            if(ItemtoEdit == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ItemToEdit = ItemtoEdit;
            return View();
        }
        public ActionResult SaveChanges(item UpdatedItem)
        {
            gccoffeeshopEntities ORM = new gccoffeeshopEntities(); //need this is every operation that takes in information
            //find the old record
            item OldRecord = ORM.items.Find(UpdatedItem.itemid);
            //ToDo check for null
            OldRecord.name = UpdatedItem.name;
            OldRecord.description = UpdatedItem.description;
            OldRecord.quantity = UpdatedItem.quantity;
            OldRecord.price = UpdatedItem.price;
            ORM.Entry(OldRecord).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult SearchItemByName (string name)
        {
            gccoffeeshopEntities ORM = new gccoffeeshopEntities(); //need this is every operation that takes in information
            ViewBag.Items = ORM.items.Where(x => x.name.ToLower().Contains(name.ToLower())).ToList();

            return View("Index");
        }
    }
}