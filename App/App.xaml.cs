#if WINDOWS
	using Microsoft.UI;
	using Microsoft.UI.Windowing;
	using Windows.Graphics;
#endif

namespace UnitConverter;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		SetMainWindowStartSize(670, 415);
		MainPage = new MainPage();
	}
	private void SetMainWindowStartSize(int width, int height)
	{
		Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
		{
			#if MACCATALYST
				var size = new CoreGraphics.CGSize(width, height);
				handler.PlatformView.WindowScene.SizeRestrictions.MinimumSize = size;
				handler.PlatformView.WindowScene.SizeRestrictions.MaximumSize = size;
				Task.Run(() =>
				{
					Thread.Sleep(1000);
					MainThread.BeginInvokeOnMainThread(() =>
					{
						handler.PlatformView.WindowScene.SizeRestrictions.MinimumSize = new CoreGraphics.CGSize(670, 415);
						handler.PlatformView.WindowScene.SizeRestrictions.MaximumSize = new CoreGraphics.CGSize(670, 415);
					});
				});
			#endif

			#if WINDOWS
				var mauiWindow = handler.VirtualView;
				var nativeWindow = handler.PlatformView;
				nativeWindow.Activate();
				IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
				WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
				AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
				appWindow.Resize(new SizeInt32(width, height));
				var presenter = appWindow.Presenter as OverlappedPresenter;
				presenter.IsResizable = false;
				presenter.IsMaximizable = false;
			#endif
		});
	}
}
