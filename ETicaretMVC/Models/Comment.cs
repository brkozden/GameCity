namespace ETicaretMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserComment { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int StarCount { get; set; }
        public DateTime Created { get; set; }
    }
}
