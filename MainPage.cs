using UnitConverter.Models;

namespace UnitConverter;

class MainPage : ContentPage
{
	// static List<string> CategoriesList = new List<string>(){"Время", "Длина", "Масса","Скорость","Объем","Площадь"}; //lists for pickers
	static List<Unit> Time = new List<Unit>(){new Unit(){UnitID = 0, UnitName = "Секунда"},new Unit(){UnitID = 0, UnitName = "Минута"},new Unit(){UnitID = 0, UnitName = "Час"},new Unit(){UnitID = 0, UnitName = "День"},new Unit(){UnitID = 0, UnitName = "Год"}};
	static List<string> LengthList = new List<string>(){"Сантиметр","Ярд","Метр","Километр","Миля"};
	static List<string> MassList = new List<string>(){"Килограмм","Грамм","Тонна","Фунт","Унция"};
	static List<string> SpeedList = new List<string>(){"Метр в секунду","Километр в час","Миля в час","Фут в секунду","Узел"};
	static List<string> VolumeList = new List<string>(){"Литр","Милилитр","Американский галлон","Кубический метр","Кубический фут"};
	static List<string> AreaList = new List<string>(){"Квадратный метр","Квадратный сантиметр","Квадратная миля","Гектар","Акр"};

	Picker pickerCategories = new Picker{Title = "Категория", FontSize = 24}; //pickers
	Picker pickerLeft = new Picker{Title = "Из чего", FontSize = 24, TextColor = Color.FromArgb("ffd5d9e0")};
	// Picker pickerRight = new Picker{Title = "Во что", FontSize = 24, TextColor = Color.FromArgb("ffd5d9e0")};

	// Entry entryLeft = new  Entry{Placeholder = "", FontSize = 24, MaxLength = 18, Keyboard = Keyboard.Numeric, Text = "0"};
	// Entry entryRight = new  Entry{Placeholder = "", FontSize = 24, MaxLength = 18, IsReadOnly = true};

	double[,] TimeVals =
	{
		{1, 60, 3600, 86400, 31536000},
		{60, 1, 60, 1440, 525000},
		{3600, 60, 1, 24, 8760},
		{86400, 1440, 24, 1, 365},
		{31536000, 525000, 8760, 365, 1}
	};

	double[,] LengthVals = 
	{
		{1, 91.44, 100, 100000, 160900},
		{91.44, 1, 1.094, 1094, 1760},
		{100, 1.094, 1, 1000, 1609.34},
		{100000, 1094, 1000, 1, 1.609},
		{160900, 1760, 1609.34, 1.609, 1}
	};
	//написать матрицы для длины, массы, скорости, объема, площади


	public MainPage()
	{
		BackgroundColor = Color.FromArgb("ff010c1e");
		List<Category> Categories = new List<Category>();
		Categories.Add(new Category(){CategoryID = 0, CategoryName = "Время", Units = Time});
		Categories.Add(new Category(){CategoryID = 1, CategoryName = "Длина"});
		Categories.Add(new Category(){CategoryID = 2, CategoryName = "Масса"});
		Categories.Add(new Category(){CategoryID = 3, CategoryName = "Скорость"});
		Categories.Add(new Category(){CategoryID = 4, CategoryName = "Объем"});
		Categories.Add(new Category(){CategoryID = 5, CategoryName = "Площадь"});

		// pickerCategories.ItemsSource = Categories;
		pickerCategories.ItemDisplayBinding = new Binding("CategoryName");
		pickerCategories.SetBinding(Picker.ItemsSourceProperty, "Categories");
		pickerCategories.SetBinding(Picker.SelectedItemProperty, "SelectedCategory");

		pickerLeft.SetBinding(Picker.ItemsSourceProperty, "SelectedCategory.Units");
		// Content = new StackLayout{Children = {pickerCategories, pickerLeft, pickerRight, entryLeft, entryRight}, Padding = 12};
		Content = new StackLayout{Children = {pickerCategories, pickerLeft}};
	}
	
	// private void entryLeft_TextChanged(object sender, EventArgs e)//changes entries
	// {
	// 	double number;
	// 	string value = entryLeft.Text;
	// 	bool success = double.TryParse(value,out number);
	// 	switch(pickerCategories.SelectedIndex)
	// 	{
	// 		case 0:
	// 			if(pickerLeft.SelectedIndex > pickerRight.SelectedIndex)
	// 			{
	// 				entryRight.Text = Convert.ToString(number*TimeVals[pickerRight.SelectedIndex, pickerLeft.SelectedIndex]);
	// 			}
	// 			else
	// 			{
	// 				entryRight.Text = Convert.ToString(number/TimeVals[pickerRight.SelectedIndex, pickerLeft.SelectedIndex]);
	// 			}
	// 			break;
	// 		case 1:
	// 			if(pickerLeft.SelectedIndex > pickerRight.SelectedIndex)
	// 			{
	// 				entryRight.Text = Convert.ToString(number*LengthVals[pickerRight.SelectedIndex, pickerLeft.SelectedIndex]);
	// 			}
	// 			else
	// 			{
	// 				entryRight.Text = Convert.ToString(number/LengthVals[pickerRight.SelectedIndex, pickerLeft.SelectedIndex]);
	// 			}
	// 			break;
	// 	}
		
		
	// }

}




