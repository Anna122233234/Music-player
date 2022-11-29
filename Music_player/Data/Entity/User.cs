namespace Music_player.Data.Entity
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Password { get; set; }
        public string Filename { get; set; }
        public string Login { get;  set; }
    }
}
