using System.ComponentModel;
using AnotherToolBox.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AnotherToolBox.Views.Controls.Characters;

public partial class CharacterFrame : UserControl
{
    public CharacterFrame()
    {
        InitializeComponent();
    }

    private void PopupFlyoutBase_OnClosing(object? sender, CancelEventArgs e)
    {
        if (DataContext is CharacterFrameViewModel vm)
        {
            vm.SelectorOpen = false;
        }
    }
    
    private void StatsFlyout_OnClosing(object? sender, CancelEventArgs e)
    {
        if (DataContext is CharacterFrameViewModel vm)
        {
            vm.StatsOpen = false;
        }
    }
}