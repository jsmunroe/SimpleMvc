# SimpleMVC
Simple standalone platform-agnostic MVC engine with optional WPF support.

##Configuration & Bootstrap

    var mvc = new MvcEngine(_container)
       .RegisterControllerCatalog("Controllers", "Controller")
       .RegisterViewCatalog("Views", "", typeof (Page))
       .RegisterViewTarget(new FrameViewTarget("MainFrame"))
       .RegisterModelBinder(new DataContextBinder());

    var mainWindow = new MainWindow();

    mvc.Navigator.Navigate<UserItemsController>(c => c.Index());

    mainWindow.ShowDialog();
    
## XAML Integration

    <Window x:Class="CutReady.Esi.UserItems.MainWindow"
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
            xmlns:mvc="clr-namespace:SimpleMvc.Wpf;assembly=SimpleMvc.Wpf"
            mc:Ignorable="d"
            Title="Main Window" Height="600" Width="900">
        <Grid>
            <Frame mvc:ControlViewTarget.RegisterAs="MainFrame"></Frame>
        </Grid>
    </Window>
