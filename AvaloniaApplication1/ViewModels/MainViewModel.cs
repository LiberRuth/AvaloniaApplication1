using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AvaloniaApplication1.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    //public string Greeting => "Welcome to Avalonia!";

    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty]
    private ViewModelBase? _currentPage = new HomePageViewModel();

    [ObservableProperty]
    private ListItemTemplate? _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value) 
    {
        if (value is  null) return;
        var instance = Activator.CreateInstance(value.ModelType!);
        Debug.WriteLine($"Value >>>>> {value.ModelType!}");
        if (instance is null) return;
        CurrentPage = (ViewModelBase?)instance;
        //CurrentPage = new ButtonPageViewModel();
    }

    public ObservableCollection<ListItemTemplate> Items { get; } = new ()
    {
        new ListItemTemplate(typeof(HomePageViewModel), "home_regular"),
        new ListItemTemplate(typeof(ButtonPageViewModel), "cursor_hover_regular"),
        new ListItemTemplate(typeof(TextPageViewModel), "text_number_format_regular"),
        new ListItemTemplate(typeof(ValueSelectionPageViewModel), "calendar_checkmark_regular"),
    };

    [RelayCommand]
    private void TriggerPane() 
    {
        IsPaneOpen = !IsPaneOpen;
    }
}

public class ListItemTemplate 
{
    public ListItemTemplate(Type type, string iconKey) 
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");

        Application.Current!.TryFindResource(iconKey, out var res);
        ListItemIcon = (StreamGeometry)res!;
    }

    public string? Label { get; }
    public Type? ModelType { get; }
    public StreamGeometry? ListItemIcon { get; }

}