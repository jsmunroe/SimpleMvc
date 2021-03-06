# SimpleMVC
Simple MVC engine with optional WPF support.

## MVC Engine
Configure `MvcEngine` with a fluent style interface. Use namespace folders to organize your controllers and views. 

```C#
    var mvc = new MvcEngine(_container)
       .RegisterControllerCatalog("Controllers", "Controller")
       .RegisterViewCatalog("Views", "", typeof(Page))
       .RegisterViewTarget(new FrameViewTarget("MainFrame"))
       .RegisterModelBinder(new DataContextBinder());

    var mainWindow = new MainWindow();
    mainWindow.ShowDialog();
    
    mvc.Navigator.Navigate<UserItemsController>(c => c.Index());
```

## XAML Integration
Make a `Frame` or any `ContentControl` into a view target.

```XAML
    <Frame mvc:ControlViewTarget.RegisterAs="MainFrame"></Frame>
```

