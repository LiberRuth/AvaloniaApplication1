using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels;

public partial class ValueSelectionPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _count;

    [RelayCommand]
    private void Increment()
    {
        Count++;
    }
}