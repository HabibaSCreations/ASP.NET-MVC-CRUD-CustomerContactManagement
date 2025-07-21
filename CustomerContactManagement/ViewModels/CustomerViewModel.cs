using CustomerContactManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace CustomerContactManagement.ViewModels
{
	public class CustomerViewModel
	{
		public int CustomerId { get; set; }

		[Display(Name = "Customer Name")]
		[Required(ErrorMessage = "নাম অবশ্যক")]
		[StringLength(25, ErrorMessage = "নাম সর্বোচ্চ ২৫ অক্ষরের হতে পারে")]
		public string CustomerName { get; set; }

		[CustomeDateTimeValidation("1900-01-01", "2025-12-31", ErrorMessage = "জন্মতারিখ অবশ্যই ১৯০০ থেকে ২০২৫ এর মধ্যে হতে হবে")]
		[Required(ErrorMessage = "জন্মতারিখ অবশ্যক")]
		[Display(Name = "Date Of Birth")]
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime DateOfBirth { get; set; }

		[Display(Name = "Mobile No")]
		[Remote("IsMobileNoAvailable", "Customers", ErrorMessage = "এই মোবাইল নাম্বার আগে থেকেই আছে")]
		[Required(ErrorMessage = "মোবাইল নম্বর অবশ্যক")]
		[StringLength(11, ErrorMessage = "মোবাইল নম্বর ১১ সংখ্যার মধ্যে হতে হবে")]
		public string MobileNo { get; set; }

		[Display(Name = "Active?")]
		public bool IsActive { get; set; }

		public string ImageUrl { get; set; }

		[Display(Name = "Deposit Amount")]
		[Required(ErrorMessage = "ডিপোজিট এমাউন্ট অবশ্যক")]
		[Range(1, 100000, ErrorMessage = "পরিমাণ ১ থেকে ৫০০০ এর মধ্যে হতে হবে")]
		public decimal DepositeAmount { get; set; }

		[Display(Name = "Email")]
		[RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w{2,4}$", ErrorMessage = "সঠিক ইমেইল অ্যাড্রেস দিন")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Customer Type নির্বাচন করুন")]
		public int CustomerTypeId { get; set; }

		[Display(Name = "Upload Picture")]
		public HttpPostedFileBase ProfileFile { get; set; }

		// Address List
		public virtual IList<Address> Addresses { get; set; } = new List<Address>();

		// Dropdown data
		public virtual IList<CustomerType> CustomerTypes { get; set; } = new List<CustomerType>();
	}
}
