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

	Entry NameEntry1 = new Entry{Placeholder = "Название", IsReadOnly = true, WidthRequest = 120};
	Entry NameEntry2 = new Entry{Placeholder = "Название", IsReadOnly = true, WidthRequest = 120};
	Entry NameEntry3 = new Entry{Placeholder = "Название", IsReadOnly = true, WidthRequest = 120};
	Entry NameEntry4 = new Entry{Placeholder = "Название", IsReadOnly = true, WidthRequest = 120};

	Entry ValEntry1 = new Entry{Placeholder = "", IsReadOnly = true, WidthRequest = 270, HorizontalTextAlignment = TextAlignment.Start};
	Entry ValEntry2 = new Entry{Placeholder = "", IsReadOnly = true, WidthRequest = 270, HorizontalTextAlignment = TextAlignment.Start};
	Entry ValEntry3 = new Entry{Placeholder = "", IsReadOnly = true, WidthRequest = 270, HorizontalTextAlignment = TextAlignment.Start};
	Entry ValEntry4 = new Entry{Placeholder = "", IsReadOnly = true, WidthRequest = 270, HorizontalTextAlignment = TextAlignment.Start};

//new Border{Content = entryLeft, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,0,0)}}

	public SecondPage()
	{
		NameEntry1.SetAppThemeColor(Entry.BackgroundColorProperty, Color.FromArgb("ff84817a"), Color.FromArgb("ff001e38"));
		NameEntry2.SetAppThemeColor(Entry.BackgroundColorProperty, Color.FromArgb("ff84817a"), Color.FromArgb("ff001e38"));
		NameEntry3.SetAppThemeColor(Entry.BackgroundColorProperty, Color.FromArgb("ff84817a"), Color.FromArgb("ff001e38"));
		NameEntry4.SetAppThemeColor(Entry.BackgroundColorProperty, Color.FromArgb("ff84817a"), Color.FromArgb("ff001e38"));

		pickerCategories.ItemsSource = Categories;
		pickerCategories.ItemDisplayBinding = new Binding("CategoryName");

		pickerCategories.SelectedIndexChanged += pickerCategoriesSelectedIndexChanged;
		ButtonCalc.Clicked += ButtonCalcClicked;

		pickerLeft.SelectedIndexChanged += pickerLeftSelectedIndexChanged;

		Border Unit1 = new Border{Content = new StackLayout{Orientation = StackOrientation.Horizontal, Children = {NameEntry1, ValEntry1}}, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,8,8)}};
		Border Unit2 = new Border{Content = new StackLayout{Orientation = StackOrientation.Horizontal, Children = {NameEntry2, ValEntry2}}, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,8,8)}};
		Border Unit3 = new Border{Content = new StackLayout{Orientation = StackOrientation.Horizontal, Children = {NameEntry3, ValEntry3}}, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,8,8)}};
		Border Unit4 = new Border{Content = new StackLayout{Orientation = StackOrientation.Horizontal, Children = {NameEntry4, ValEntry4}}, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,8,8)}};
		
		StackLayout stacklayoutLeft = new StackLayout(){Children = {new Border{Content = entryLeft, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,0,0)}}, new Border{Content = pickerLeft, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(0,0,8,8)}}}, WidthRequest = 200};
		StackLayout stacklayoutRight = new StackLayout(){Children = {Unit1, Unit2, Unit3, Unit4}, WidthRequest = 400, Spacing = 8};

		StackLayout stacklayout2stacks = new StackLayout(){Orientation = StackOrientation.Horizontal, Children = {stacklayoutLeft, stacklayoutRight}, Spacing = 30};

		Content = new StackLayout{Children = {new Border{Content = pickerCategories, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,8,8)}}, stacklayout2stacks, ButtonCalc}, Padding = 20, Spacing = 30};
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

	private void pickerLeftSelectedIndexChanged(object sender, EventArgs e) //если меняем левый пикер то ставим названия всех остальных юнитов
	{
		Category selectedCategory = (Category)pickerCategories.SelectedItem;
		ListUnitNames = selectedCategory.UnitNames.GetRange(0,selectedCategory.UnitNames.Count);
		ListUnitNames.Remove((string)pickerLeft.SelectedItem);

		NameEntry1.Text = ListUnitNames[0];
		NameEntry2.Text = ListUnitNames[1];
		NameEntry3.Text = ListUnitNames[2];
		NameEntry4.Text = ListUnitNames[3];

		ValEntry1.Text = "";
		ValEntry2.Text = "";
		ValEntry3.Text = "";
		ValEntry4.Text = "";
		
	}

	private double GetNumberFromEntry(Entry entry) //безопасный ввод из энтри
	{
		double number;
		string value = entryLeft.Text;
		bool success = double.TryParse(value,out number);
		if(number > 0)
			return number;
		else return 0;
	}
}
