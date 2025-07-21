using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerContactManagement.Models;
using System.Data.Entity;
using CustomerContactManagement.ViewModels;
using System.IO;

namespace CustomerContactManagement.Controllers
{
	public class CustomersController : Controller
	{
		con db = new con();

		[HttpGet]
		public ActionResult Index()
		{
			IEnumerable<Customer> customers = db.Customers
				.Include(c => c.CustomerType)
				.Include(ct => ct.Addresses)
				.ToList();
			return View(customers);
		}

		public JsonResult IsMobileNoAvailable(string MobileNo)
		{
			return Json(db.Customers.Any(s => s.MobileNo == MobileNo), JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			Customer customer = db.Customers.Find(id);
			if (customer != null)
			{
				var addresses = db.Addresses.Where(c => c.CustomerId == id).ToList();
				db.Addresses.RemoveRange(addresses);
				db.Entry(customer).State = EntityState.Deleted;
				db.SaveChanges();
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Create()
		{
			CustomerViewModel customer = new CustomerViewModel();
			customer.CustomerTypes = db.CustomerTypes.ToList();
			customer.Addresses.Add(new Address() { AddressId = 1 });
			return View(customer);
		}

		[HttpPost]
		public ActionResult Create(CustomerViewModel vObj)
		{
			if (!ModelState.IsValid)
			{
				vObj.CustomerTypes = db.CustomerTypes.ToList();
				return Json(new
				{
					success = false,
					errors = ModelState.Values.SelectMany(v => v.Errors)
											  .Select(e => e.ErrorMessage)
				});
			}

			Customer obj = new Customer();
			if (vObj.ProfileFile != null)
			{
				HttpPostedFileBase file = vObj.ProfileFile;
				string fileName = GetFileName(file);
				obj.ImageUrl = fileName;
			}
			else
			{
				obj.ImageUrl = "noimage.png";
			}

			obj.CustomerName = vObj.CustomerName;
			obj.DateOfBirth = vObj.DateOfBirth;
			obj.MobileNo = vObj.MobileNo;
			obj.IsActive = vObj.IsActive;
			obj.CustomerTypeId = vObj.CustomerTypeId;
			obj.Addresses = vObj.Addresses;
			obj.Email = vObj.Email;
			obj.DepositeAmount = vObj.DepositeAmount;

			db.Customers.Add(obj);
			try
			{
				db.SaveChanges();
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Error Occured while saving Customer");
				vObj.CustomerTypes = db.CustomerTypes.ToList();
				return Json(new
				{
					success = false,
					errors = new[] { "Error occured while saving Customer: " + ex.Message }
				});
			}
		}

		private string GetFileName(HttpPostedFileBase file)
		{
			string fileName = "";
			if (file != null)
			{
				fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
				string path = Path.Combine(Server.MapPath("~/images/"), fileName);
				file.SaveAs(path);
			}
			return fileName;
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var customer = db.Customers
				.Include(c => c.CustomerType)
				.Include(c => c.Addresses)
				.FirstOrDefault(s => s.CustomerId == id);

			if (customer == null)
				return HttpNotFound("Customer Not found");

			var vObj = new CustomerViewModel
			{
				CustomerName = customer.CustomerName,
				DepositeAmount = customer.DepositeAmount,
				CustomerId = customer.CustomerId,
				DateOfBirth = customer.DateOfBirth,
				MobileNo = customer.MobileNo,
				CustomerTypeId = customer.CustomerTypeId,
				IsActive = customer.IsActive,
				ImageUrl = customer.ImageUrl,
				Addresses = customer.Addresses.ToList(),
				CustomerTypes = db.CustomerTypes.ToList()
			};
			return View(vObj);
		}

		[HttpPost]
		public ActionResult Edit(CustomerViewModel vobj, string OldImageUrl)
		{
			// Validation Check
			if (!ModelState.IsValid)
			{
				vobj.CustomerTypes = db.CustomerTypes.ToList();
				return Json(new
				{
					success = false,
					errors = ModelState.Values.SelectMany(v => v.Errors)
											  .Select(e => e.ErrorMessage)
				});
			}

			Customer obj = db.Customers
				.Include(a => a.Addresses)
				.FirstOrDefault(x => x.CustomerId == vobj.CustomerId);

			if (obj == null)
				return Json(new { success = false, errors = new[] { "Customer not found." } });

			// Basic Info Update
			obj.CustomerName = vobj.CustomerName;
			obj.CustomerTypeId = vobj.CustomerTypeId;
			obj.MobileNo = vobj.MobileNo;
			obj.IsActive = vobj.IsActive;
			obj.DateOfBirth = vobj.DateOfBirth;
			obj.DepositeAmount = vobj.DepositeAmount;

			// Profile Picture Update
			if (vobj.ProfileFile != null)
			{
				string uniqueFileName = GetFileName(vobj.ProfileFile);
				obj.ImageUrl = uniqueFileName;
			}
			else
			{
				obj.ImageUrl = OldImageUrl;
			}

			// Remove old addresses that are not in the updated list
			var addressesToRemove = obj.Addresses
				.Where(existingAddress => !vobj.Addresses.Any(updated =>
					updated.AddressType == existingAddress.AddressType &&
					updated.City == existingAddress.City &&
					updated.State == existingAddress.State))
				.ToList();

			foreach (var address in addressesToRemove)
			{
				db.Addresses.Remove(address);
			}

			// Add new addresses
			foreach (var item in vobj.Addresses)
			{
				if (!obj.Addresses.Any(existing =>
						existing.AddressType == item.AddressType &&
						existing.City == item.City &&
						existing.State == item.State))
				{
					var newAddress = new Address()
					{
						CustomerId = obj.CustomerId,
						AddressType = item.AddressType,
						City = item.City,
						State = item.State
					};
					obj.Addresses.Add(newAddress);
				}
			}

			try
			{
				db.SaveChanges();
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Error Occured while editing Customer");
				vobj.CustomerTypes = db.CustomerTypes.ToList();
				return Json(new
				{
					success = false,
					errors = new[] { "Error occured while saving customer: " + ex.Message }
				});
			}
		}
	}
}
