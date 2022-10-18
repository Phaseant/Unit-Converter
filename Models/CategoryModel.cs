using System;
using System.Collections.Generic;

namespace UnitConverter.Models
{
	public class Category
	{
		public int CategoryID {get; set;}
		public string CategoryName {get; set;}

		public List<Unit> Units;
	}
}