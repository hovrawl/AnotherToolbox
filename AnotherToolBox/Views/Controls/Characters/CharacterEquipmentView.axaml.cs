using System.Windows.Input;
using AnotherToolBox.ViewModels.TeamBuilder;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AnotherToolBox.Views.Controls.Characters;

public partial class CharacterEquipmentView : UserControl
{
    public CharacterEquipmentView()
    {
        InitializeComponent();
    }
    
    public CharacterStatusViewModel? ViewModel => DataContext as CharacterStatusViewModel;

    
    public ICommand? SelectEquipmentCommandProxy => ViewModel?.SelectEquipmentCommand;
    
    public ICommand? SelectSkillCommandProxy => ViewModel?.SelectSkillCommand;
}