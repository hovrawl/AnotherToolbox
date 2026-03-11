namespace AnotherToolBox.ViewModels.TeamBuilder;

public partial class CharacterSkillViewModel : ViewModelBase
{
    public string Id { get; set; }
    public string Name { get; set; }
    
    public string Image { get; set; }
    public string Description { get; set; }
    public string Element { get; set; }
    public string SkillType { get; set; }
    public string ActionType { get; set; }
    public string SkillGroup { get; set; }
    public int? MPCost  { get; set; }
    public string Multiplier  { get; set; }
    public string CriticalMultiplier  { get; set; }
}