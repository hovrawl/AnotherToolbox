using System.Windows.Input;
using AnotherToolBox.ViewModels.TeamBuilder;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AnotherToolBox.Views.Controls.Team;

public partial class CharacterStatusView : UserControl
{
    public CharacterStatusView()
    {
        InitializeComponent();
    }
    
    public CharacterStatusViewModel? ViewModel => DataContext as CharacterStatusViewModel;

    
    public ICommand? SelectEquipmentCommandProxy => ViewModel?.SelectEquipmentCommand;

}