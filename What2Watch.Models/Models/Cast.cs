
namespace What2Watch.Models.Models
{
    public class Base
    {
        public string Id { get; set; }
        public Image Image { get; set; }
        public int RunningTimeInMinutes { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
        public int Year { get; set; }
    }
    public class Cast
    {
        public string Id { get; set; }
        public ImageDetails Image { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<string> Characters { get; set; }
    }
    public class Crew
    {
        public List<Director> Director { get; set; }
    }

    public class Director
    {
        public List<string> Akas { get; set; }
        public string Disambiguation { get; set; }
        public string Id { get; set; }
        public ImageDetails Image { get; set; }
        public string LegacyNameText { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
    public class Role
    {
        public string Character { get; set; }
    }
    public class Credits
    {
        public string Id { get; set; }
        public Base Base { get; set; }
        public List<Cast> Cast { get; set; }
        public Crew Crew { get; set; }
    }
}
