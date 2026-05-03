using System.Windows.Input;
using AnotherToolBox.ViewModels.TeamBuilder;
using Avalonia.Controls;

namespace AnotherToolBox.Views.Controls.Characters;

public partial class CharacterStatusView : UserControl
{
    public CharacterStatusView()
    {
        InitializeComponent();
    }
    
    public CharacterStatusViewModel? ViewModel => DataContext as CharacterStatusViewModel;

    
    public ICommand? SelectEquipmentCommandProxy => ViewModel?.SelectEquipmentCommand;
    
    public ICommand? SelectSkillCommandProxy => ViewModel?.SelectSkillCommand;


}