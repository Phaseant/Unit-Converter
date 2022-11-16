using System;
using System.Collections.Generic;

namespace UnitConverter.Models
{
	public class Category
	{
		public string CategoryName {get; set;}
		public double [,] UnitVals = new double[5,5];
		public List<string> UnitNames;

		public Category(string Name)
		{
			CategoryName = Name;
			switch(Name)
			{
				case "Время":
					UnitNames = new List<String>(){"Секунда", "Минута", "Час", "Месяц", "Год"};
					UnitVals = Time;
					break;
				case "Длина":
					UnitNames = new List<string>(){"Сантиметр","Ярд","Метр","Километр","Брит. Миля"};
					UnitVals = Length;
					break;
				case "Масса":
					UnitNames = new List<string>(){"Грамм","Унция","Фунт","Килограмм","Тонна"};
					UnitVals = Mass;
					break;
				case "Скорость":
					UnitNames = new List<string>(){"Метр/сек","Узел","Миля/ч","Фут/сек","Километр/ч"};
					UnitVals = Speed;
					break;
				case "Объем":
					UnitNames = new List<string>(){"Куб. Метр","Куб. фут","Амер. галлон","Литр","Милилитр"};
					UnitVals = Volume;
					break;
				case "Площадь":
					UnitNames = new List<string>(){"Кв. миля","Кв. км","Гектар","Акр","Кв. метр"};
					UnitVals = Area;
					break;
			}
		}

		private double[,] Time =
		{
			{1, 60, 3600, 86400, 31536000},
			{60, 1, 60, 1440, 525000},
			{3600, 60, 1, 24, 8760},
			{86400, 1440, 24, 1, 365},
			{31536000, 525000, 8760, 365, 1}
		};

		private double[,] Length = 
		{
			{1, 91.44, 100, 100000, 160900},
			{91.44, 1, 1.094, 1094, 1760},
			{100, 1.094, 1, 1000, 1609.34},
			{100000, 1094, 1000, 1, 1.609},
			{160900, 1760, 1609.34, 1.609, 1}
		};

		private double[,] Mass = 
		{
			{1, 28.35, 453.6, 1000, 1000000},
			{28.35, 1, 16, 35.274, 35270},
			{453.6, 16, 1, 2.205, 2205},
			{1000, 35.274, 2.205, 1, 1000},
			{1000000, 35270, 2205, 1000, 1}
		};
		private double[,] Speed = 
		{
			{1, 1.944, 2.237, 3.28084, 3.6},
			{1.944, 1, 1.151, 1.688, 1.852},
			{2.237, 1.151, 1, 1.467, 1.609},
			{3.28084, 1.688, 1.467, 1, 1.097},
			{3.6, 1.852, 1.609, 1.097, 1}
		};

		private double[,] Volume = 
		{
			{1, 35.315, 264.2, 1000, 1000000},
			{35.315, 1, 7.481, 28.317, 28320},
			{264.2, 7.481, 1, 3.785, 3785},
			{1000, 28.317, 3.785, 1, 1000},
			{1000000, 28320, 3785, 1000, 1}
		};

		private double[,] Area = 
		{
			{1, 2.59, 259, 640, 2590000},
			{2.59, 1, 100, 247.1, 1000000},
			{ 259, 100, 1, 2.471, 10000},
			{640, 247.1, 2.471, 1, 4047},
			{2590000, 1000000, 10000, 4047, 1}
		};
	}
}