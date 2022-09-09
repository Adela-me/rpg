namespace rpg.Dtos.Weapon
{
    public class CreateWeaponDto
    {
        public string Name { get; set; } = string.Empty;
        public int Demage { get; set; }
        public int CharacterId { get; set; }
    }
}
