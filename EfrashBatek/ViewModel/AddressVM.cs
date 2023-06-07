using EfrashBatek.Models;
using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.ViewModel
{
	public class AddressVM
	{

		public int ID { get; set; }
	
		public string FirstName { get; set; }
	
		public string LastName { get; set; }
		
		public string phone { get; set; }
		
		public int PostalCode { get; set; }
	
		public Zone Zone { get; set; }	


		public string FullAddress { get; set; }

	}
}
