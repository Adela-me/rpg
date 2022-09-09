namespace rpg.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Demage { get; set; }
        public List<Character> Characters { get; set; }
    }
}
