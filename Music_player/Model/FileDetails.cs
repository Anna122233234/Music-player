using System.Collections.Generic;

namespace Music_player.Models
{
    public class FileDetails
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Duration { get; set; } = "";
    }

}