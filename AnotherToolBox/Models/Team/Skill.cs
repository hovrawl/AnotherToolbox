using System.ComponentModel.DataAnnotations.Schema;
using WikiClientLibrary.Cargo.Schema;

namespace AnotherToolBox.Models.Team;

[Table("HeroSkills")]
public class CargoSkill
{
    [Column(CargoSpecialColumnNames.PageName)]
    public string PageName { get; set; }
    [Column(CargoSpecialColumnNames.Id)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string Element { get; set; }
    public string SkillType { get; set; }
    public string ActionType { get; set; }
    public string SkillGroup { get; set; }
    public int? MPCost  { get; set; }
    public string UnlockCondition  { get; set; }
    public bool? Manifestation  { get; set; }
    public string Multiplier  { get; set; }
    public string CriticalMultiplier  { get; set; }
    public int? Style  { get; set; }
    public bool? Unreleased  { get; set; }
}