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

    <Frame mvc:ControlViewTarget.RegisterAs="MainFrame"></Frame>


