using UnitConverter.Models;
using Microsoft.Maui.Controls.Shapes;

namespace UnitConverter;

public partial class SecondPage : ContentPage
{
	static List<Category> Categories = new List<Category>(){new Category("Время"), new Category("Длина"), new Category("Масса"), new Category("Скорость"), new Category("Объем"), new Category("Площадь")};

	Picker pickerCategories = new Picker{Title = "Категория", FontSize = 30, HeightRequest = 60, FontAutoScalingEnabled = true, HorizontalTextAlignment = TextAlignment.Center}; //pickers
	Picker pickerLeft = new Picker{Title = "Из чего", FontSize = 24, HorizontalTextAlignment = TextAlignment.Center, HeightRequest = 60};

	Entry entryLeft = new  Entry{Placeholder = "", FontSize = 26, MaxLength = 18, Keyboard = Keyboard.Numeric, HeightRequest = 100};

	Button ButtonCalc = new Button{FontSize = 26, Text = "Вычислить"};

	List<string> ListUnitNames = new List<string>();

	Entry NameEntry1 = new Entry{Placeholder = "", IsReadOnly = true};
	Entry NameEntry2 = new Entry{Placeholder = "", IsReadOnly = true};
	Entry NameEntry3 = new Entry{Placeholder = "", IsReadOnly = true};
	Entry NameEntry4 = new Entry{Placeholder = "", IsReadOnly = true};

	Entry ValEntry1 = new Entry{Placeholder = "", IsReadOnly = true};
	Entry ValEntry2 = new Entry{Placeholder = "", IsReadOnly = true};
	Entry ValEntry3 = new Entry{Placeholder = "", IsReadOnly = true};
	Entry ValEntry4 = new Entry{Placeholder = "", IsReadOnly = true};


	public SecondPage()
	{
		pickerCategories.ItemsSource = Categories;
		pickerCategories.ItemDisplayBinding = new Binding("CategoryName");

		pickerCategories.SelectedIndexChanged += pickerCategoriesSelectedIndexChanged;
		ButtonCalc.Clicked += ButtonCalcClicked;

		pickerLeft.SelectedIndexChanged += pickerLeftIndexChanged;

		Content = new StackLayout{Children = {pickerCategories,NameEntry1, NameEntry2, NameEntry3, NameEntry4, ValEntry1, ValEntry2, ValEntry3, ValEntry4, pickerLeft, entryLeft, ButtonCalc}, Padding = 20, Spacing = 30};
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Shell.SetNavBarIsVisible(this, false);
	}

	private void pickerCategoriesSelectedIndexChanged(object sender, EventArgs e)
	{
		Category selectedCategory = (Category)pickerCategories.SelectedItem;
		pickerLeft.ItemsSource = selectedCategory.UnitNames;
	}

	private void ButtonCalcClicked(object sender, EventArgs e)
	{
		double number = GetNumberFromEntry(entryLeft);
		Category chosenCategory = (Category)pickerCategories.SelectedItem;
		ConvertUnits(chosenCategory, number, pickerLeft, ListUnitNames[0], ValEntry1);
		ConvertUnits(chosenCategory, number, pickerLeft, ListUnitNames[1], ValEntry2);
		ConvertUnits(chosenCategory, number, pickerLeft, ListUnitNames[2], ValEntry3);
		ConvertUnits(chosenCategory, number, pickerLeft, ListUnitNames[3], ValEntry4);
	}

	private void ConvertUnits(Category chosenCategory, double number, Picker pickerLeft, string unitName, Entry valueEntry)
	{
		int unit_index = chosenCategory.UnitNames.IndexOf(unitName);
		switch(chosenCategory.CategoryName)
		{
			case "Время":
			case "Длина":
			case "Масса":
				if(pickerLeft.SelectedIndex > unit_index)
					valueEntry.Text = Convert.ToString(number*chosenCategory.UnitVals[unit_index, pickerLeft.SelectedIndex]);
				else
					valueEntry.Text = Convert.ToString(number/chosenCategory.UnitVals[unit_index, pickerLeft.SelectedIndex]);
				break;
			case "Скорость":
			case "Объем":
			case "Площадь":
				if(pickerLeft.SelectedIndex > unit_index)
					valueEntry.Text = Convert.ToString(number/chosenCategory.UnitVals[unit_index, pickerLeft.SelectedIndex]);
				else
					valueEntry.Text = Convert.ToString(number*chosenCategory.UnitVals[unit_index, pickerLeft.SelectedIndex]);
				break;
		}
	}	

	private void pickerLeftIndexChanged(object sender, EventArgs e) //если меняем левый пикер то ставим названия всех остальных юнитов
	{
		Category selectedCategory = (Category)pickerCategories.SelectedItem;
		ListUnitNames = selectedCategory.UnitNames.GetRange(0,selectedCategory.UnitNames.Count);
		ListUnitNames.Remove((string)pickerLeft.SelectedItem);

		NameEntry1.Text = ListUnitNames[0];
		NameEntry2.Text = ListUnitNames[1];
		NameEntry3.Text = ListUnitNames[2];
		NameEntry4.Text = ListUnitNames[3];
		
	}

	private double GetNumberFromEntry(Entry entry) //безопасный ввод из энтри
	{
		double number;
		string value = entryLeft.Text;
		bool success = double.TryParse(value,out number);
		return number;
	}
}


//не работает апдейт от левого пикера почини бля братан прошу хз что здесь не так
