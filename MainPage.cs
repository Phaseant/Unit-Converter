using UnitConverter.Models;
using Microsoft.Maui.Controls.Shapes;
namespace UnitConverter;

class MainPage : ContentPage
{
	static List<Category> Categories = new List<Category>(){new Category("Время"), new Category("Длина"), new Category("Масса"), new Category("Скорость"), new Category("Объем"), new Category("Площадь")};

	Picker pickerCategories = new Picker{Title = "Категория", FontSize = 30, HeightRequest = 60, FontAutoScalingEnabled = true, HorizontalTextAlignment = TextAlignment.Center}; //pickers
	Picker pickerLeft = new Picker{Title = "Из чего", FontSize = 24, HorizontalTextAlignment = TextAlignment.Center, HeightRequest = 60};
	Picker pickerRight = new Picker{Title = "Во что", FontSize = 24, HorizontalTextAlignment = TextAlignment.Center, HeightRequest = 60};

	Entry entryLeft = new  Entry{Placeholder = "", FontSize = 26, MaxLength = 18, Keyboard = Keyboard.Numeric, HeightRequest = 100};
	Entry entryRight = new  Entry{Placeholder = "", FontSize = 26, MaxLength = 18, IsReadOnly = true, HeightRequest = 100};

	Button ButtonCalc = new Button{FontSize = 26, Text = "Вычислить"};

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

	double[,] MassVals = 
	{
		{1, 28.35, 453.6, 1000, 1000000},
		{28.35, 1, 16, 35.274, 35270},
		{453.6, 16, 1, 2.205, 2205},
		{1000, 35.274, 2.205, 1, 1000},
		{1000000, 35270, 2205, 1000, 1},
	};
	double[,] SpeedVals = 
	{
		{1, 1.944, 2.237, 3.28084, 3.6},
		{1.944, 1, 1.151, 1.688, 1.852},
		{2.237, 1.151, 1, 1.467, 1.609},
		{3.28084, 1.688, 1.467, 1, 1.097},
		{3.6, 1.852, 1.609, 1.097, 1},
	};

	double[,] VolumeVals = 
	{
		{1, 35.315, 264.2, 1000, 1000000},
		{35.315, 1, 7.481, 28.317, 28320},
		{264.2, 7.481, 1, 3.785, 3785},
		{1000, 28.317, 3.785, 1, 1000},
		{1000000, 28320, 3785, 1000, 1},
	};

	double[,] AreaVals = 
	{
		{1, 2.59, 259, 640, 2590000},
		{2.59, 1, 100, 247.1, 1000000},
		{ 259, 100, 1, 2.471, 10000},
		{640, 247.1, 2.471, 1, 4047},
		{2590000, 1000000, 10000, 4047, 1},
	};

	public MainPage()
	{
		pickerCategories.ItemsSource = Categories;
		pickerCategories.ItemDisplayBinding = new Binding("CategoryName");

		pickerCategories.SelectedIndexChanged += pickerCategoriesSelectedIndexChanged;
		ButtonCalc.Clicked += ButtonCalcClicked;

		StackLayout stacklayoutLeft = new StackLayout(){Children = {new Border{Content = entryLeft, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,0,0)}}, new Border{Content = pickerLeft, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(0,0,8,8)}}}, WidthRequest = 300};
		StackLayout stacklayoutRight = new StackLayout(){Children = {new Border{Content = entryRight, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,0,0)}}, new Border{Content = pickerRight, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(0,0,8,8)}}}, WidthRequest = 300};

		StackLayout stacklayout2stacks = new StackLayout(){Orientation = StackOrientation.Horizontal, Children = {stacklayoutLeft, stacklayoutRight}, Spacing = 30};
		

		Content = new StackLayout{Children = {new Border{Content = pickerCategories, StrokeShape = new RoundRectangle{CornerRadius = new CornerRadius(8,8,8,8)}}, stacklayout2stacks, ButtonCalc}, Padding = 20, Spacing = 30};
	}
	
	private void ButtonCalcClicked(object sender, EventArgs e)//changes entries
	{
		double number = GetNumberFromEntry(entryLeft);
		switch(pickerCategories.SelectedIndex)
		{
			case 0:
				ConvertUnits(TimeVals, number, pickerRight, pickerLeft, false); //passed
				break;
			case 1:
				ConvertUnits(LengthVals, number, pickerRight, pickerLeft, false); //not passed
				break;
			case 2:
				ConvertUnits(MassVals, number, pickerRight, pickerLeft, false); //passed
				break;
			case 3:
				ConvertUnits(SpeedVals, number, pickerRight, pickerLeft, true); //not passed
				break;
			case 4:
				ConvertUnits(VolumeVals, number, pickerRight, pickerLeft, true); //not passed
				break;
			case 5:
				ConvertUnits(AreaVals, number, pickerRight, pickerLeft, true); //not passed
				break;
		}
	}


	private void pickerCategoriesSelectedIndexChanged(object sender, EventArgs e)
	{
		Category selectedCategory = (Category)pickerCategories.SelectedItem;
		pickerLeft.ItemsSource = selectedCategory.UnitNames;
		pickerRight.ItemsSource = selectedCategory.UnitNames;
	}

	private void ConvertUnits(double[,] Table, double number, Picker pickerRight, Picker pickerLeft, bool minus)
	{
		if(minus)
		{
			if(pickerLeft.SelectedIndex > pickerRight.SelectedIndex)
			{
				entryRight.Text = Convert.ToString(number/Table[pickerRight.SelectedIndex, pickerLeft.SelectedIndex]);
			}
			else
			{
				entryRight.Text = Convert.ToString(number*Table[pickerRight.SelectedIndex, pickerLeft.SelectedIndex]);
			}
		}
		else
		{
			if(pickerLeft.SelectedIndex > pickerRight.SelectedIndex)
			{
				entryRight.Text = Convert.ToString(number*Table[pickerRight.SelectedIndex, pickerLeft.SelectedIndex]);
			}
			else
			{
				entryRight.Text = Convert.ToString(number/Table[pickerRight.SelectedIndex, pickerLeft.SelectedIndex]);
			}
		}
	}

	private double GetNumberFromEntry(Entry entry)
	{
		double number;
		string value = entryLeft.Text;
		bool success = double.TryParse(value,out number);
		return number;
	}

}