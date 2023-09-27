namespace Crm3D.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Naslov { get; set; } = string.Empty;
        public string Mesto { get; set; } = string.Empty;
        public uint Posta { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        //public KategorijaEnum Kategorija { get; set; }
        public bool PodpisGDPR { get; set; } = false;
    }
}
