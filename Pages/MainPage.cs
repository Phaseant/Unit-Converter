using UnitConverter.Models;
using Microsoft.Maui.Controls.Shapes;
namespace UnitConverter;

public partial class MainPage : ContentPage
{
	static List<Category> Categories = new List<Category>(){new Category("Время"), new Category("Длина"), new Category("Масса"), new Category("Скорость"), new Category("Объем"), new Category("Площадь")};

	Picker pickerCategories = new Picker{Title = "Категория", FontSize = 30, HeightRequest = 60, FontAutoScalingEnabled = true, HorizontalTextAlignment = TextAlignment.Center}; //pickers
	Picker pickerLeft = new Picker{Title = "Из чего", FontSize = 24, HorizontalTextAlignment = TextAlignment.Center, HeightRequest = 60};
	Picker pickerRight = new Picker{Title = "Во что", FontSize = 24, HorizontalTextAlignment = TextAlignment.Center, HeightRequest = 60};

	Entry entryLeft = new  Entry{Placeholder = "", FontSize = 26, MaxLength = 18, Keyboard = Keyboard.Numeric, HeightRequest = 100};
	Entry entryRight = new  Entry{Placeholder = "", FontSize = 26, MaxLength = 18, IsReadOnly = true, HeightRequest = 100};

	Button ButtonCalc = new Button{FontSize = 26, Text = "Вычислить"};

	

	public MainPage()
	{
		pickerCategories.ItemsSource = Categories;
		pickerCategories.ItemDisplayBinding = new Binding("CategoryName");

		pickerCategories.SelectedIndexChanged += pickerCategoriesSelectedIndexChanged;
		pickerLeft.SelectedIndexChanged += pickerLeftSelectedIndexChanged;
		pickerRight.SelectedIndexChanged += pickerRightSelectedIndexChanged;
		ButtonCalc.Clicked += ButtonCalcClicked;


		StackLayout stacklayoutLeft = new StackLayout(){Children = {new Border{Content = entryLeft, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,0,0)}}, new Border{Content = pickerLeft, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(0,0,8,8)}}}, WidthRequest = 300};
		StackLayout stacklayoutRight = new StackLayout(){Children = {new Border{Content = entryRight, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,0,0)}}, new Border{Content = pickerRight, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(0,0,8,8)}}}, WidthRequest = 300};

		StackLayout stacklayout2stacks = new StackLayout(){Orientation = StackOrientation.Horizontal, Children = {stacklayoutLeft, stacklayoutRight}, Spacing = 30};
		

		Content = new StackLayout{Children = {new Border{Content = pickerCategories, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,8,8)}}, stacklayout2stacks, ButtonCalc}, Padding = 20, Spacing = 30};
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Shell.SetNavBarIsVisible(this, false);
	}
	
	private void ButtonCalcClicked(object sender, EventArgs e)//changes entries
	{
		double number = GetNumberFromEntry(entryLeft);
		Category chosenCategory = (Category)pickerCategories.SelectedItem;
		ConvertUnits(chosenCategory, number, pickerLeft, (string)pickerRight.SelectedItem, entryRight);
	}


	private void pickerCategoriesSelectedIndexChanged(object sender, EventArgs e)
	{
		Category selectedCategory = (Category)pickerCategories.SelectedItem;
		pickerLeft.ItemsSource = selectedCategory.UnitNames;
		pickerRight.ItemsSource = selectedCategory.UnitNames;
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

	private double GetNumberFromEntry(Entry entry)
	{
		double number;
		string value = entryLeft.Text;
		bool success = double.TryParse(value,out number);
		if(number > 0)
			return number;
		else return 0;
	}

	private void pickerLeftSelectedIndexChanged(object sender, EventArgs e)
	{
		entryRight.Text = "";
	}

	private void pickerRightSelectedIndexChanged(object sender, EventArgs e)
	{
		entryRight.Text = "";
	}

}