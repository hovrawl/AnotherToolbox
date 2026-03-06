using AnotherToolBox.Models.Team;

namespace AnotherToolBox.ViewModels.TeamBuilder;

public partial class EquipmentItemViewModel : ViewModelBase
{
    public string Id { get; set; }
    public string Name { get; set; }
    public EquipmentType Type { get; set; }
}