using AnotherToolBox.ViewModels;
using Avalonia;
using Avalonia.Controls;

namespace AnotherToolBox.Views.Controls.Characters;

public partial class CharacterList : UserControl
{
    private CharacterListViewModel _vm;
    
    public CharacterList()
    {
        InitializeComponent();
    }
    
    protected async override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        if (DataContext is not CharacterListViewModel vm) return;
        if (vm.Characters.Count > 0) return;
        await vm.LoadCharacters();
    }

    private void ItemsControl_OnPreparingContainer(object? sender, ContainerPreparedEventArgs e)
    {
        if (_vm is null)
        {
            if (DataContext is not CharacterListViewModel vm) return;
            _vm = vm;
        }
        if (_vm.Characters.Count < e.Index+1) return;
        
        var character = _vm.Characters[e.Index];
        if (character.Thumbnail != null) return;
        
        // Load image
        _vm.LoadCharacterImage(character);
    }
}