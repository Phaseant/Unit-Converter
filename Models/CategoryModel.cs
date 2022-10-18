using System;
using System.Collections.Generic;

namespace UnitConverter.Models
{
	public class Category
	{
		public string CategoryName {get; set;}
		public List<string> UnitNames;
		public Category(string Name)
		{
			CategoryName = Name;
			switch(Name)
			{
				case "Время":
					UnitNames = new List<String>(){"Секунда", "Минута", "Час", "Месяц", "Год"};
					break;
				case "Длина":
					UnitNames = new List<string>(){"Сантиметр","Ярд","Метр","Километр","Миля"};
					break;
				case "Масса":
					UnitNames = new List<string>(){"Грамм","Унция","Фунт","Килограмм","Тонна"};
					break;
				case "Скорость":
					UnitNames = new List<string>(){"Метр в секунду","Узел","Миля в час","Фут в секунду","Километр в час"};
					break;
				case "Объем":
					UnitNames = new List<string>(){"Кубический метр","Кубический фут","Американский галлон","Литр","Милилитр"};
					break;
				case "Площадь":
					UnitNames = new List<string>(){"Квадратная миля","Квадратный км","Гектар","Акр","Квадратный метр"};
					break;
			}
		}
	}
}