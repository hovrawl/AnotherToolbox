using AnotherToolBox.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AnotherToolBox.Views.Controls;

public partial class TeamBuilderView : UserControl
{
    public TeamBuilderView()
    {
        InitializeComponent();
    }

    protected async override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        if (DataContext is not TeamBuilderViewModel vm) return;
        if (vm.Characters.Count > 0) return;
        await vm.LoadCharacters();
    }
}